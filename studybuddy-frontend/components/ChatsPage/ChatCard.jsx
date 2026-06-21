import React, { useMemo } from 'react'
import PhotoDisplay from '@/components/PhotoDisplay';
import { defaultProfilePhotoPath, fileFromBase64 } from '@/utils/fileHandling';
import ClickableCard from '../ClickableCard';

export default function ChatCard({ chat }) {

    const profilePhoto = useMemo(() => {
        return fileFromBase64(chat.photo, defaultProfilePhotoPath);
    }, [chat.photo]);

    const chatLastMessageDate = chat?.lastMessage?.createDate;
    const displayDate = chatLastMessageDate?.substring(0, 10);
    const displayTime = chatLastMessageDate?.substring(11, 16);

    return (
        <ClickableCard
            key={chat.id} href={`/chat/${chat.id}`}
            additionalStyles="flex items-center justify-between py-3 px-2 border-none cursor-pointer transition-colors"
        >
            <div className="flex items-center gap-4">
                
                <PhotoDisplay
                    photo={profilePhoto}
                    sizeClass="w-12 h-12 shrink-0"
                    alt={chat.name}
                />
                    
                <div>
                    <h3 className="font-bold text-md text-gray-900 mb-0.5 capitalize">
                        {chat.name}
                    </h3>

                    <p className="text-sm text-gray-500 font-medium">
                        {chat?.lastMessage?.text}
                    </p>
                </div>
            </div>

            <div className="flex items-center gap-6 min-w-30 justify-end mr-6">
                {chat.unReadMessages > 0 ? (
                    <span className="w-7 h-7 flex items-center justify-center bg-primary text-white text-md font-bold rounded-full">
                        {chat.unReadMessages}
                    </span>
                ) : (
                        <div className="w-5" />
                )}

                <div className="flex-col-center">
                    <span className="text-md text-gray-600 font-medium whitespace-nowrap w-20 text-center">
                        {displayTime}
                    </span>
                    
                    <span className="text-xs text-gray-600 font-medium whitespace-nowrap w-20 text-center">
                        {displayDate}
                    </span>

                </div>
            </div>
        </ClickableCard>       
    )
}
