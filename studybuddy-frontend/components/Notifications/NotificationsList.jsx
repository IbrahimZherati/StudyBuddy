import React, { useState } from "react";
import { FriendRequestNotification, FriendAcceptedNotification, MessageNotification } from "./NotificationTypes";

export default function NotificationsList() {
    
    const [activeFilter, setActiveFilter] = useState("All");

    const allNotifications = [
        {
            id: 1,
            type: "request", 
            avatar: "",
            name: "Samer",
            description: "Samer sent you a friend request, they study in homs",
            time: "today",
        },
        {
            id: 2,
            type: "message", 
            avatar: "",
            name: "Kosae",
            description: "Would you study today?",
            time: "today",
        },
        {
            id: 3,
            type: "accepted", 
            avatar: "",
            name: "Samia",
            description: "accepted your friend request. You can now chat together.",
            time: "yesterday",
        },
        {
            id: 4,
            type: "group", 
            avatar: "",
            name: "Ahmad",
            description: "invited you to algorithms group",
            time: "today",
        },
    ];

    
    const filteredNotifications = allNotifications.filter((notif) => {
        if (activeFilter === "All") return true;
        if (activeFilter === "Requests")
            return notif.type === "request" || notif.type === "accepted";
        if (activeFilter === "Groups") return notif.type === "group";
        if (activeFilter === "Chats") return notif.type === "message";
        if (activeFilter === "Reminders") return notif.type === "reminder";
        return true;
    });

    const todayNotifications = filteredNotifications.filter(
        (n) => n.time === "today",
    );
    const yesterdayNotifications = filteredNotifications.filter(
        (n) => n.time === "yesterday",
    );

    const renderNotification = (notif) => {
        switch (notif.type) {
            case "request":
                return (
                    <FriendRequestNotification
                        key={notif.id}
                        avatar={notif.avatar}
                        name={notif.name}
                        description={notif.description}
                        time={notif.time}
                        onAccept={() => console.log("Accepted", notif.id)}
                        onReject={() => console.log("Rejected", notif.id)}
                    />
                );

            case "accepted":
                return (
                    <FriendAcceptedNotification
                        key={notif.id}
                        avatar={notif.avatar}
                        name={notif.name}
                        description={notif.description}
                        time={notif.time}
                        onViewProfile={() => console.log("Viewing profile", notif.id)}
                        onMessage={() => console.log("Messaging", notif.id)}
                    />
                );

            case "message":
            case "group": 
                return (
                    <MessageNotification
                        key={notif.id}
                        avatar={notif.avatar}
                        name={notif.name}
                        description={notif.description}
                        time={notif.time}
                        onReply={() => console.log("Action triggered for", notif.id)}
                        onIgnore={() => console.log("Ignored", notif.id)}
                    />
                );

            case "reminder":
                return (
                    <MessageNotification
                        key={notif.id}
                        avatar={notif.avatar}
                        name={notif.name}
                        description={notif.description}
                        time={notif.time}
                        onReply={() => console.log("Remind me later", notif.id)}
                        onIgnore={() => console.log("I got it", notif.id)}
                    />
                );
            default:
                return null;
        }
    };

    const filterOptions = ["All", "Requests", "Groups", "Chats", "Reminders"];

    return (
        <div className="w-full p-6 bg-white">
            
            <div className="flex flex-wrap gap-2 mb-6">
                {filterOptions.map((filter) => {
                    const isActive = activeFilter === filter;
                    return (
                        <button
                            key={filter}
                            onClick={() => setActiveFilter(filter)}
                            className={`px-4 py-1.5 text-md font-bold rounded-full transition-all duration-200 border ${
                                        isActive
                                        ? "bg-primary text-white border-primary" // ستايل الزر النشط
                                        : "bg-white text-gray-700 border-gray-600 hover:bg-gray-100" // ستايل الزر غير النشط
                            }`}
                        >
                            {filter}
                        </button>
                    );
                })}
            </div>

            {todayNotifications.length > 0 && (
                <div className="mb-6">
                    <h3 className="text-xl font-bold text-gray-900 mb-3">Today</h3>
                    <div className="space-y-1">
                        {todayNotifications.map(renderNotification)}
                    </div>
                </div>
            )}

            {yesterdayNotifications.length > 0 && (
                <div className="mb-6">
                    <h3 className="text-xl font-bold text-gray-900 mb-3">Yesterday</h3>
                    <div className="space-y-1">
                        {yesterdayNotifications.map(renderNotification)}
                    </div>
                </div>
            )}

            {filteredNotifications.length === 0 && (
                <div className="text-center py-12 text-gray-500 text-xl">
                    No notifications found in this category.
                </div>
            )}
        </div>
    );
}
