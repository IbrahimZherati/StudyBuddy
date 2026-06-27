"use client";

import { useEffect, useRef, useState} from "react";
import * as signalR from "@microsoft/signalr";

export function useNotificationHub(hubUrlSuffix, addNewItem) {
    const connectionRef = useRef(null);
    const [status, setStatus] = useState("connecting");

    const addNewItemRef = useRef(addNewItem);

    useEffect(() => {
        addNewItemRef.current = addNewItem;
    }, [addNewItem]);
    
    useEffect(() => {

        const connection = new signalR.HubConnectionBuilder()
        .withUrl(`http://localhost:5203/hubs/${hubUrlSuffix}`)
        .withAutomaticReconnect()
        .build();
        
        connectionRef.current = connection;
        
        const handleReceive = (not) => {
            if(addNewItemRef.current)
                addNewItemRef.current(not);
        }

        connection.on("ReceiveNotification", handleReceive);

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
            connection.off("ReceiveNotification", handleReceive);
            connection.stop();
        };
    }, [hubUrlSuffix]);
}