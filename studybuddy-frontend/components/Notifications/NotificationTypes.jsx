import React from 'react';
import NotificationItem from './NotificationItem';
import post from '@/utils/API/post';
import Link from 'next/link';

const postRequest = async (url, param) => {
    await post(null, url, param);
}

export function FriendRequestNotification({ photo, id, name, time }) {
    const description = `${name} has sent you a buddy request!`

    return (
        <NotificationItem photo={photo} name={name} description={description} time={time}>
            <button 
                onClick={() => postRequest("ClientUser/AcceptFriendRequestByClientId", {
                    key: "fromClientId",
                    value: id
                })}
                className="btn w-28 h-10 px-5 py-2 font-semibold text-white 
                        bg-primary hover:bg-blue-700 rounded-full transition-colors 
                        whitespace-nowrap active:scale-90"
            >
                Accept
            </button>

            <button 
                onClick={() => postRequest("ClientUser/RejectFriendRequestByClientId", {
                    key: "fromClientId",
                    value: id
                })}
                className="btn w-28 h-10 px-5 py-2 font-semibold
                         text-gray-700 bg-white border border-gray-300 hover:bg-gray-50 
                         rounded-full transition-colors whitespace-nowrap active:scale-90"
            >
                Reject
            </button>
        </NotificationItem>
    );
}

export function FriendAcceptedNotification({ photo, id, name, time }) {
    const description = `${name} has accepted your request. You are buddies now!`;

    return (
        <NotificationItem photo={photo} name={name} description={description} time={time}>
            <Link 
                href={`/chat/${id}`}
                className="btn w-28 h-10 px-5 py-2 font-semibold text-white 
                        bg-primary hover:bg-blue-700 rounded-full transition-colors 
                        whitespace-nowrap active:scale-90"
            >
                Message
            </Link>
            
            <Link 
                href={`/profile/${id}`}
                className="btn w-28 h-10 px-5 py-2 font-semibold text-gray-700
                        bg-white border border-gray-300 hover:bg-gray-50 rounded-full 
                            transition-colors whitespace-nowrap active:scale-90"
            >
                View Profile
            </Link>
        </NotificationItem>
    );
}

export function FriendRejectedNotification({ photo, id, name, time }) {
    const description = `${name} has rejected your request :(`;

    return (
        <NotificationItem 
            photo={photo} 
            name={name} 
            description={description} 
            time={time}
        >
        </NotificationItem>
    );
}

export function MessageNotification({ photo, id, name, time, message}) {
    const description = `${name} has sent you a message: ${message}`;

    return (
        <NotificationItem photo={photo} name={name} description={description} time={time}>
            <div className="flex-row-center w-56">
                <Link 
                    href={`/chat/${id}`}
                    className="btn w-full h-10 px-5 py-2 font-semibold text-white
                            bg-primary hover:bg-blue-700 rounded-full transition-colors 
                            whitespace-nowrap active:scale-90"
                >
                    Reply
                </Link>
            </div>
        </NotificationItem>
    );
}