"use client";

import { useEffect, useRef, useState, useCallback } from "react";
import * as signalR from "@microsoft/signalr";
import getMessages from '@/utils/Chat/PrivateChat/getMessages';

export function useChatConnection(hubUrlSuffix, myId, otherUserId) {
    const connectionRef = useRef(null);
    const [messages, setMessages] = useState([]);
    const [status, setStatus] = useState("connecting");
    
    const processMessage = (msg) => {
        return {
            id: msg.id,  
            senderId: msg.fromClientUserId,
            recevieId:msg.toClientUserId,
            senderName: msg.userName,
            text: msg.text,
            createTime: msg.createDate
        }
    };
    
    useEffect(() => {
        if(myId == null || otherUserId == null)
            return;
        const connection = new signalR.HubConnectionBuilder()
        .withUrl(`http://localhost:5203/hubs/${hubUrlSuffix}`)
        .withAutomaticReconnect()
        .build();
        
        connectionRef.current = connection;
        
        const handleReceive = async (msg) => {
           
            msg = processMessage(msg);
            console.log(msg)
           
            if((msg.senderId == myId && msg.recevieId == otherUserId) || msg.senderId == otherUserId) {
                try {
                    await connectionRef.current.invoke("ReadMessage",
                        msg.id
                    );
                }
                catch(error) {
                    console.log(error);
                }

                setMessages((messages) => {
                    if(messages.some(m => m.id === msg.id))
                        return messages;
                    return [...messages, msg];
                })
            }
        }

        connection.on("ReceiveMessage", handleReceive);

        connection.onreconnecting(() => setStatus("reconnecting"));
        connection.onreconnected(() => setStatus("connected"));
        connection.onclose(() => setStatus("disconnected"));

        connection
            .start()
            .then(() => setStatus("connected"))
            .catch((err) => {
                console.error(err);
                setStatus("error");
            });

        return () => {
            connection.off("ReceiveMessage", handleReceive);
            connection.stop();
        };
    }, [hubUrlSuffix, myId, otherUserId]);

    const sendMessage = async (receiver, text) => {
        if (!connectionRef.current) return;

        const isGroupChat = hubUrlSuffix.includes("GroupChat");
        const toId = isGroupChat? "groupChatId": "toClientUserId";

        await connectionRef.current.invoke("SendMessage", {
            text,
            [toId]: receiver
        });
    };

    const loadMessages = useCallback(async (to, skip, take) => {
        let olderMessages = await getMessages(to, skip, take);
        olderMessages = olderMessages.map(processMessage);

        setMessages(messages => {
            if(skip === 0) {
                return olderMessages;
            }

            const merged = [
                ...olderMessages,
                ...messages
            ]
                
            const seen = new Set();
            return merged.filter(msg => {
                    if(seen.has(msg.id)) 
                        return false;
                    seen.add(msg.id);
                    return true;
                }
            )
        });

    }, []);

    return { messages, sendMessage, status, loadMessages };
}