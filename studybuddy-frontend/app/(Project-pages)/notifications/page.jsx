"use client"

import React, { useCallback, useEffect, useState } from "react";
import useLazyContainter from "@/app/hooks/useLazyContainer";
import { useNotificationHub } from "@/app/hooks/useNotificationHub";
import Notification from "@/components/Notifications/Notification";
import { processNotification } from "@/utils/processors"
import { notify } from "@/utils/notify";
import Observer from "@/components/Observer";

export default function NotificationsList() {

    useEffect(() => {
        const stored = sessionStorage.getItem("pendingNotification");

        if (!stored) 
            return;

        const notification = JSON.parse(stored);
        sessionStorage.removeItem("pendingNotification");

        notify(notification);
    }, []);

    const filterOptions = ["All", "Requests", "Chats"];
    const [activeFilter, setActiveFilter] = useState("All");

    const processNotificationFunction = useCallback((not) => {
        return processNotification(not);
    }, []);

    const url = `Notification/${activeFilter === "All" ? "" : activeFilter}`;

    const loadFactor = 50;

    const [numberOfNewNotifications, setNumberOfNewNotifications] = useState(0);

    const [items, containerRef, handleScroll, addNewItem, , hasMoreToLoad] = 
        useLazyContainter(url, loadFactor, null, processNotificationFunction, false, numberOfNewNotifications);

    const onReceive = (notification) => {
        setNumberOfNewNotifications(prev => prev + 1);
        addNewItem(notification);
    }

    useNotificationHub("NotificationHub", onReceive);

    const seen = new Set();
    const filteredNotifications = items.filter(item => {
        const itemStr = `${item.from}_${item.type}`;
        if(seen.has(itemStr)) 
            return false;
        else {
            seen.add(itemStr);
            return true;
        }
    });

    return (
        <div className="flex flex-col h-full min-h-0 w-full p-6 bg-white">
            <div className="flex flex-wrap gap-2 mb-6">
                {filterOptions.map((filter) => {
                    const isActive = activeFilter === filter;
                    return (
                        <button
                            key={filter}
                            onClick={() => setActiveFilter(filter)}
                            className={`px-4 py-1.5 text-md font-bold rounded-full transition-all duration-200 border ${isActive
                                    ? "bg-primary text-white border-primary"
                                    : "bg-white text-gray-700 border-gray-600 hover:bg-gray-100"
                                }`}
                        >
                            {filter}
                        </button>
                    );
                })}
            </div>

            <div 
                className="space-y-1 overflow-y-auto no-scrollbar"
                ref={containerRef}
                onScroll={handleScroll}
            >
                {filteredNotifications.length > 0 && (
                    <div className="mb-6">
                        <div className="space-y-1">
                            {filteredNotifications.map(item =>
                                <Notification
                                    key={item.id}
                                    notification={item}
                                />
                            )}
                        </div>
                    </div>
                )}
            </div>

            <Observer 
                hasMoreToLoad={hasMoreToLoad}
                loadMore={handleScroll}
            />

            {filteredNotifications.length === 0 && (
                <div className="text-center py-12 text-gray-500 text-xl">
                    No notifications found.
                </div>
            )}
        </div>
    );
}
