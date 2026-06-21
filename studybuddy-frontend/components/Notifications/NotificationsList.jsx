import React, { useCallback, useState } from "react";
import useLazyContainter from "@/app/hooks/useLazyContainer";
import Notification from "./Notification";
import { useNotificationHub } from "@/app/hooks/useNotificationHub";

export default function NotificationsList() {

    const filterOptions = ["All", "Requests", "Chats"];
    const [activeFilter, setActiveFilter] = useState("All");

    const processNotification = useCallback((not) => {
        return {
            id: not.requestId,  
            type: not.type,
            from: not.fromClientUserId,
            content: not.description
        }
    }, []);

    const url = `Notification/${activeFilter === "All" ? "" : activeFilter}`;

    const loadFactor = 20;
    const [items, containerRef, handleScroll, addNewItem] = useLazyContainter(url, loadFactor, null, processNotification);
    useNotificationHub("NotificationHub", addNewItem)

    return (
        <div className="w-full p-6 bg-white">
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
                className="space-y-1"
                ref={containerRef}
                onScroll={handleScroll}
            >
                {items.length > 0 && (
                    <div className="mb-6">
                        <div className="space-y-1">
                            {items.map(item =>
                                <Notification
                                    key={item.id}
                                    notification={item}
                                />
                            )}
                        </div>
                    </div>
                )}
            </div>

            {items.length === 0 && (
                <div className="text-center py-12 text-gray-500 text-xl">
                    No notifications found.
                </div>
            )}
        </div>
    );
}
