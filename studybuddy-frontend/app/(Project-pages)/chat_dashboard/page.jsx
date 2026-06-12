"use client";
import React from 'react';
import ChatCard from '@/components/chatsDashboard/chatCard';
import ChatsDashboardCategories from '@/components/chatsDashboard/chatsDashboardCategories';

export default function ChatDashboard() {

    const DUMMY_CHATS = [
        {
            id: 1,
            title: 'Studing together',
            lastMessage: 'Hi there, how are you?',
            time: '12:00 PM',
            unreadCount: 1,
            href:"#",
            avatar: '',
        },
        {
            id: 2,
            title: 'Let\'s study',
            lastMessage: 'Hi there, how are you?',
            time: '11:00 AM',
            unreadCount: 2,
            href:"#",
            avatar: '',
        },
        {
            id: 3,
            title: 'Maths',
            lastMessage: 'Hi there, how are you?',
            time: '9:56 AM',
            unreadCount: 0,
            href:"#",
            avatar: '',
        },
        {
            id: 4,
            title: 'chemistry',
            lastMessage: 'Hi there, how are you?',
            time: 'Yes 3:54 PM',
            unreadCount: 0,
            href:"#",
            avatar: '',
        },
        {
            id: 5,
            title: 'Science',
            lastMessage: 'Hi there, how are you?',
            time: 'Yes 2:00 PM',
            unreadCount: 3,
            href:"#",
            avatar: '',
        },
    ];

    return (
        <div className="w-full min-h-screen bg-white p-6">
            <ChatsDashboardCategories />
                
            <div className="flex flex-col w-full gap-2">
                {DUMMY_CHATS.map((chat) => (
                    <ChatCard key={chat.id} chat={chat} />
                ))}
            </div>
        </div>
    );
}