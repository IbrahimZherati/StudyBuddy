"use client";
import React, { useCallback } from 'react';
import { useState } from 'react';
import ChatCard from '@/components/ChatsPage/ChatCard';
import ChatCategories from '@/components/ChatsPage/ChatCategories';
import useLazyContainter from '@/app/hooks/useLazyContainer';
import { useNotificationHub } from '@/app/hooks/useNotificationHub';
import { processNotification } from "@/utils/processors"

export default function ChatDashboard() {

    const categories = ['All', 'Unread'];
    const [activeCategory, setActiveCategory] = useState('All');

    const url = activeCategory === 'All' ? "Chat/GetPrivateChats" : "Chat/GetUnReadPrivateChats";

    const loadFactor = 20;

    const [numberOfNewChats, setNumberOfNewChats] = useState(0);

    const [items, containerRef, handleScroll, , addNewItem] = 
        useLazyContainter(url, loadFactor, null, null, false, numberOfNewChats);

    const addNewChat = useCallback((notification) => {
        if (notification.type != "Message")
            return;
        const processedNotification = processNotification(notification);

        addNewItem(prevChats => {
            const findResult = prevChats.find(chat => chat.id == processedNotification.from);
            
            if(!findResult)
                setNumberOfNewChats(prev => prev + 1);

            const existingChat = findResult ?? {
                "id": processedNotification.from,
                "name": processedNotification.name,
                "photo": notification.photo,
                "unReadMessages": 0
            }

            const newChat = {
                ...existingChat,
                unReadMessages: existingChat.unReadMessages + 1,
                lastMessage: {
                    text: processedNotification.content,
                    createDate: processedNotification.time
                }
            }

            return [newChat, 
                ...prevChats.filter(chat => chat.id != processedNotification.from)
            ];
        })
    }, [addNewItem]);

    useNotificationHub("NotificationHub", addNewChat);

    return (
        <div className="flex flex-col h-full min-h-0 w-full bg-white p-6">
            <ChatCategories
                categories={categories}
                activeCategory={activeCategory}
                setActiveCategory={setActiveCategory}
            />

            <div
                className="flex flex-col w-full gap-2 overflow-y-auto no-scrollbar py-4"
                ref={containerRef}
                onScroll={handleScroll}
            >
                {items.map((chat) => (
                    <ChatCard key={chat.id} chat={chat} />
                ))}
            </div>

            {items.length === 0 && (
                <div className="text-center py-12 text-gray-500 text-xl">
                    You don&apos;t have any chats yet.
                </div>
            )}
        </div>
    );
}