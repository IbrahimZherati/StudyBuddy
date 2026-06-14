import React from 'react'
import ClickableCard from './ClickableCard';
import PhotoDisplay from './PhotoDisplay';
import { defaultProfilePhotoPath, fileFromBase64 } from '@/utils/fileHandling';

export default function UserCard({photo, userName, bio, major, id}) {
    const profilePhoto = fileFromBase64(photo, defaultProfilePhotoPath);

    return (
        <ClickableCard href={`../profile/${id}`}>
            <PhotoDisplay
                photo={profilePhoto}
                sizeClass="h-14 w-14"
                alt={userName}
            />

            <div className="flex flex-col gap-0.5">
                <h4 className="text-[1.3rem] font-bold">
                    {userName}
                </h4>

                <p className="text-gray-700">
                    {bio || "No bio set"}
                </p>

                <p className="text-xs text-gray-700">
                    {major || "No major set"}
                </p>
            </div>
        </ClickableCard>
    )
}
