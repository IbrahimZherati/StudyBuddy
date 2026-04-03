import React from 'react'
import Image from 'next/image'
import ClickableCard from './ClickableCard';

export default function UserCard({image, name, bio, university, href}) {
    return (
        <ClickableCard href={href}>
            <Image src={image || "/images/avatar-default.svg"} alt={name}
                width={40} height={40} className="rounded-full inline w-10 h-10"
            />

            <div className="flex flex-col gap-0.5">
                <h4 className="font-bold">
                    {name}
                </h4>

                <p className="text-sm text-gray-700">
                    {bio}
                </p>

                <p className="text-xs text-gray-700">
                    {university}
                </p>
            </div>
        </ClickableCard>
    )
}
