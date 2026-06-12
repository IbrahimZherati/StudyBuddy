import React from 'react';
import NotificationItem from './NotificationItem';

export function FriendRequestNotification({ avatar, name, description, time, onAccept, onReject }) {
    return (
        <NotificationItem avatar={avatar} name={name} description={description} time={time}>
            <button 
                onClick={onAccept}
                className="w-28 h-10 px-5 py-2 text-sm font-semibold text-white bg-primary hover:bg-blue-700 rounded-full transition-colors whitespace-nowrap active:scale-90"
            >
                Accept
            </button>

            <button 
                onClick={onReject}
                className="w-28 h-10 px-5 py-2 text-sm font-semibold text-gray-700 bg-white border border-gray-300 hover:bg-gray-50 rounded-full transition-colors whitespace-nowrap active:scale-90"
            >
                Reject
            </button>
        </NotificationItem>
    );
}

export function FriendAcceptedNotification({ avatar, name, description, time, onViewProfile, onMessage }) {
    return (
        <NotificationItem avatar={avatar} name={name} description={description} time={time}>
            <button 
                onClick={onMessage}
                className="w-28 h-10 px-5 py-2 text-sm font-semibold text-white bg-primary hover:bg-blue-700 rounded-full transition-colors whitespace-nowrap active:scale-90"
            >
                Message
            </button>
            
            <button 
                onClick={onViewProfile}
                className="w-28 h-10 px-5 py-2 text-sm font-semibold text-gray-700 bg-white border border-gray-300 hover:bg-gray-50 rounded-full transition-colors whitespace-nowrap active:scale-90"
            >
                View Profile
            </button>
        </NotificationItem>
    );
}

export function MessageNotification({ avatar, name, description, time, onReply, onIgnore }) {
    return (
        <NotificationItem avatar={avatar} name={name} description={description} time={time}>
            <button 
                onClick={onReply}
                className="w-28 h-10 px-5 py-2 text-sm font-semibold text-white bg-primary hover:bg-blue-700 rounded-full transition-colors whitespace-nowrap active:scale-90"
            >
                Reply
            </button>

            <button 
                onClick={onIgnore}
                className="w-28 h-10 px-5 py-2 text-sm font-semibold text-gray-700 bg-white border border-gray-300 hover:bg-gray-50 rounded-full transition-colors whitespace-nowrap active:scale-90"
            >
                I got it
            </button>
        </NotificationItem>
    );
}