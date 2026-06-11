import React from 'react'
import PhotoDisplay from '@/components/PhotoDisplay';
import { defaultProfilePhotoPath } from '@/utils/fileHandling';

export default function chatCard({ chat }) {
    return (
        <div
            key={chat.id}
            className="flex items-center justify-between py-3 px-2 border-b border-gray-200 hover:bg-gray-50 cursor-pointer transition-colors"
        >
            <div className="flex items-center gap-4">
                
                <PhotoDisplay
                    photo={chat.avatar || defaultProfilePhotoPath}
                    sizeClass="w-12 h-12 shrink-0"
                    alt={chat.title}
                />
                    
                <div>
                    <h3 className="font-bold text-md text-gray-900 mb-0.5 capitalize">
                        {chat.title}
                    </h3>

                    <p className="text-sm text-gray-500 font-medium">
                        {chat.lastMessage}
                    </p>
                </div>
            </div>

            <div className="flex items-center gap-6 min-w-30 justify-end">
                {chat.unreadCount > 0 ? (
                    <span className="w-5 h-5 flex items-center justify-center bg-primary text-white text-xs font-bold rounded-full">
                        {chat.unreadCount}
                    </span>
                ) : (
                        <div className="w-5" />
                )}

                <span className="text-xs text-gray-600 font-medium whitespace-nowrap w-20 text-right">
                    {chat.time}
                </span>
            </div>
        </div>       
    )
}
