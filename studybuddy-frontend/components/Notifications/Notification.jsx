import React from 'react'
import { FriendAcceptedNotification, FriendRejectedNotification, FriendRequestNotification, MessageNotification } from './NotificationTypes';

export default function Notification({ notification }) {
    switch (notification.type) {
        case "FriendRequest":
            return (
                <FriendRequestNotification
                    photo={notification.photo}
                    id={notification.from}
                    name={notification.name}
                    time={notification.time}
                />
            );

        case "RequestAccepted":
            return (
                <FriendAcceptedNotification
                    photo={notification.photo}
                    id={notification.from}
                    name={notification.name}
                    time={notification.time}
                />
            );

        case "RequestRejected":
            return (
                <FriendRejectedNotification
                    photo={notification.photo}
                    id={notification.from}
                    name={notification.name}
                    time={notification.time}
                />
            );

        case "Message":
            return (
                <MessageNotification
                    photo={notification.photo}
                    id={notification.from}
                    name={notification.name}
                    time={notification.time}
                    message={notification.content}
                />
            );
        default:
            return null;
    }
}
