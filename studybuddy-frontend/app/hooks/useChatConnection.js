"use client";

import { useEffect, useRef, useState, useCallback } from "react";
import * as signalR from "@microsoft/signalr";
import getMessages from '@/utils/API/getMessages';

export function useChatConnection(hubUrlSuffix) {
    const connectionRef = useRef(null);
    const [messages, setMessages] = useState([]);
    const [status, setStatus] = useState("connecting");

    const processMessage = (msg) => {
        return {
            id: msg.id,  
            senderId: msg.fromClientUserId,
            senderName: msg.userName,
            text: msg.text,
            createTime: msg.createDate
        }
    };

    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl(`http://localhost:5203/hubs/${hubUrlSuffix}`)
            .withAutomaticReconnect()
            .build();

        connectionRef.current = connection;

        const handleReceive = (msg) => {
            msg = processMessage(msg);
            console.log("Received Message: ", msg);
            setMessages((messages) => {
                if(messages.some(m => m.id === msg.id))
                    return messages;
                return [...messages, msg];
            })
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
    }, [hubUrlSuffix]);

    const sendMessage = async (receiver, text) => {
        if (!connectionRef.current) return;
        console.log("Message to be sent", text);

        await connectionRef.current.invoke("SendMessage", {
            text,
            toClientUserId: receiver
        });

        console.log("Message sent to:", receiver);
    };

    const loadMessages = useCallback(async (to, skip, take) => {
        console.log("load");
        let olderMessages = await getMessages(to, skip, take);
        olderMessages = olderMessages.map(processMessage);
        console.log(olderMessages);

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