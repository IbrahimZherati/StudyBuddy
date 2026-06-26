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

    const url = activeCategory === 'All'? "Chat/GetPrivateChats": "Chat/GetUnReadPrivateChats";

    const loadFactor = 20;
    const [items, containerRef, handleScroll, addNewItem] = useLazyContainter(url, loadFactor, null, null, true);

    const addNewChat = useCallback((notification) => {
        if(notification.type != "Message")
            return;
        const processedNotification = processNotification(notification);
        const filterResult = items.filter(item => item.id == processedNotification.from);
        
        const newChat = filterResult.length == 0? {
            "id": processedNotification.from,
            "name": processedNotification.name,
            "photo": notification.photo,
            "unReadMessages": 0
        } :
        filterResult[0];

        newChat.unReadMessages++;
        newChat.lastMessage = {
            text: processedNotification.content,
            createDate: processedNotification.time
        }

        addNewItem(newChat);
    }, [addNewItem, items]);

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
        </div>
    );
}