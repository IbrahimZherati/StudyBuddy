import React, { useMemo } from 'react'
import ClickableCard from './ClickableCard';
import PhotoDisplay from './PhotoDisplay';
import { defaultProfilePhotoPath, fileFromBase64 } from '@/utils/fileHandling';
import { MessageSquare } from 'lucide-react';

export default function FriendItem({ friend, handleMessageClick}) {

    const photo = friend.photo;

    const friendPhoto = useMemo(() => {
        return fileFromBase64(photo, defaultProfilePhotoPath);
    }, [photo]);

    return (
        <ClickableCard href={`/profile/${friend.id}`} key={friend.id}
            additionalStyles="flex items-center justify-between mb-4 p-4 rounded-xl shadow-sm border border-gray-100 hover:shadow-md"
        >
            <div className="flex items-center gap-4">
                <div className="relative">
                    <PhotoDisplay
                        photo={friendPhoto}
                        sizeClass="w-14 h-14"
                        alt={friend.userName}
                    />

                    {friend.isOnline && (
                        <span className="absolute bottom-0 right-0 w-3.5 h-3.5 bg-[#00FF66] border-2 border-white rounded-full"></span>
                    )}
                </div>

                <div className="flex flex-col text-left">
                    <h3 className="font-semibold text-gray-900 text-xl">
                        {friend.userName}
                    </h3>

                    <div className="text-sm text-gray-500 mt-0.5">
                        <p>
                            {friend.major}
                        </p>
                        <p className={`${!friend.university? "italic": ""}`}>
                            {friend.university || "No University set"}
                        </p>
                    </div>
                </div>
            </div>

            <button className="cursor-pointer select-none transition-all duration-150 active:scale-90"
                onClick={(e) => handleMessageClick(e, friend.id)}
            >
                <MessageSquare className="w-6 h-6 fill-current text-black active:text-secondary" />
            </button>
        </ClickableCard>
    )
}