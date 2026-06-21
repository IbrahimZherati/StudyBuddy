"use client";

import { useCallback, useEffect, useRef, useState} from "react";
import * as signalR from "@microsoft/signalr";
import useLazyContainer from "./useLazyContainer";

export function useNotificationHub(hubUrlSuffix, addNewItem) {
    const connectionRef = useRef(null);
    const [status, setStatus] = useState("connecting");
    
    useEffect(() => {

        const connection = new signalR.HubConnectionBuilder()
        .withUrl(`http://localhost:5203/hubs/${hubUrlSuffix}`)
        .withAutomaticReconnect()
        .build();
        
        connectionRef.current = connection;
        
        const handleReceive = (not) => {
            console.log("Received Notification: ", not);
            addNewItem(not);
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
    }, [hubUrlSuffix, addNewItem]);
}