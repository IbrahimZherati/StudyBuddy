import React from 'react'
import ClickableCard from './ClickableCard';
import PhotoDisplay from './PhotoDisplay';

export default function UserCard({image, userName, bio, major, id}) {
    return (
        <ClickableCard href={`../profile/${id}`}>
            <PhotoDisplay
                photo={image || "/images/avatar-default.svg"}
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
