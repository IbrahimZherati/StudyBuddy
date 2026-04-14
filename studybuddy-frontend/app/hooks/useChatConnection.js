"use client";

import { useEffect, useRef, useState, useCallback } from "react";
import * as signalR from "@microsoft/signalr";
import getMessages from '@/utils/API/getMessages';

export function useChatConnection(hubUrlSuffix) {
    const connectionRef = useRef(null);
    const [messages, setMessages] = useState([]);
    const [status, setStatus] = useState("connecting");

    const processMessage = (message) => {
        return {
            id:message.id,  
            senderId: message.fromClientUserId,
            senderName: message.userName,
            text: message.text,
            createTime: message.createDate
        }
    };

    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl(`http://localhost:5203/hubs/${hubUrlSuffix}`)
            .withAutomaticReconnect()
            .build();

        connectionRef.current = connection;

        connection.on("ReceiveMessage", (message) => {
            console.log("Received Message: ", message);
            const processedMessage = processMessage(message);
            setMessages((prev) => [
                ...prev, 
                processedMessage
            ]); 
        });

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
        const newMessages = await getMessages(to, skip, take);
        console.log(newMessages);
        setMessages(messages => [
            ...newMessages.map(processMessage),
            ...messages
        ]);
    }, []);

    return { messages, sendMessage, status, loadMessages };
}