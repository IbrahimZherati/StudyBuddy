"use client";
import React from "react";
import { useRouter } from "next/navigation";
import useLazyContainter from "@/app/hooks/useLazyContainer";
import FriendItem from "@/components/FriendItem";

export default function FriendsList() {

    const router = useRouter();

    const loadFactor = 15;
    const [items, containerRef, handleScroll] = useLazyContainter("ClientUser/GetFriends", loadFactor);

    console.log(items);

    const handleMessageClick = (e, id) => {
        e.preventDefault();  
        router.push(`/chat/${id}`);
    };

    return (
        <div className='flex flex-col h-full min-h-0'>
            <div 
                className='flex flex-col gap-1 w-full flex-1 min-h-0 p-6 overflow-y-auto no-scrollbar space-y-4'
                ref={containerRef}
                onScroll={handleScroll}
            >
                {items.map((item) =>
                    <FriendItem
                        key={item.id}
                        friend={item}
                        handleMessageClick={handleMessageClick}
                    />
                )}

                {items.length === 0 && (
                    <p className="text-center text-gray-500 mt-6 py-4 bg-white rounded-xl shadow-sm border border-gray-100">
                        No results found.
                    </p>
                )}
            </div>
        </div>
    )
}
