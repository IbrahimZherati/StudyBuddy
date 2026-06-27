"use client"

import { useNotificationHub } from '@/app/hooks/useNotificationHub';
import { notify } from '@/utils/notify';
import { processNotification } from '@/utils/processors';
import { useCallback } from 'react'

export default function NotificationListener() {

    const handleNotification = useCallback((notification) => {
        const processedNotification = processNotification(notification);

        let notificationObj = {
            userName: processedNotification.name
        }

        switch(processedNotification.type) {
            case "FriendRequest":
                notificationObj = {
                    ...notificationObj,
                    title: "Buddy Request",
                    message: "has sent you a buddy request!",
                    href: `/profile/${processedNotification.from}`
                }
                break;

            case "RequestAccepted":
                notificationObj = {
                    ...notificationObj,
                    title: "Request Accepted",
                    message: "has accepted your buddy request!",
                    href: `/profile/${processedNotification.from}`
                }
                break;

            case "Message":
                notificationObj = {
                    ...notificationObj,
                    title: "Message",
                    message: processedNotification.content,
                    href: `/chat/${processedNotification.from}`
                }
                break;
        }

        notify(notificationObj);
    }, []);

    useNotificationHub("NotificationHub", handleNotification);

    return null;
}
