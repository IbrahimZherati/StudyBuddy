"use client";

import { useEffect, useRef, useState } from "react";
import * as signalR from "@microsoft/signalr";

export function useChatConnection(hubUrlSuffix) {
    const connectionRef = useRef(null);
    const [messages, setMessages] = useState([]);
    const [status, setStatus] = useState("connecting");

    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl(`http://localhost:5203/hubs/${hubUrlSuffix}`)
            .withAutomaticReconnect()
            .build();

        connectionRef.current = connection;

        connection.on("ReceiveMessage", (message) => {
            console.log("Received Message: ", message);
            setMessages((prev) => [...prev, {
                sender: message.UserName,
                text: message.Text
            }]);
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

    const sendMessage = async (sender, receiver, text) => {
        if (!connectionRef.current) return;
        await connectionRef.current.invoke("SendMessage", {
            text,
            toClientUserId:receiver,
            fromClientUserId:sender
        });
        console.log("Message Sent");
    };

    return { messages, sendMessage, status };
}