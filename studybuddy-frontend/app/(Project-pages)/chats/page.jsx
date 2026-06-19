"use client";
import React from 'react';
import { useState } from 'react';
import ChatCard from '@/components/ChatsPage/ChatCard';
import ChatCategories from '@/components/ChatsPage/ChatCategories';
import useLazyContainter from '@/app/hooks/useLazyContainer';

export default function ChatDashboard() {

    const categories = ['All', 'Unread'];
    const [activeCategory, setActiveCategory] = useState('All');

    const url = activeCategory === 'All'? "Chat/GetPrivateChats": "Chat/GetUnReadPrivateChats";

    const loadFactor = 20;
    const [items, containerRef, handleScroll] = useLazyContainter(url, loadFactor);

    console.log(items);

    return (
        <div className="w-full min-h-screen bg-white p-6">
            <ChatCategories 
                categories={categories}
                activeCategory={activeCategory}
                setActiveCategory={setActiveCategory}
            />
                
            <div 
                className="flex flex-col w-full gap-2"
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