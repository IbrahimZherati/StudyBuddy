import React from 'react'
import Image from 'next/image'
import ClickableCard from './ClickableCard';
import { Users } from 'lucide-react';

export default function GroupCard({image, name, bio, members, href}) {
    return (
        <ClickableCard href={href}>
            <Image src={image || "/images/group-default.svg"} alt={name}
                width={40} height={40} className="rounded-full inline w-10 h-10"
            />

            <div className='flex flex-col gap-1'>
                <h4 className="text-[1.1rem] font-bold text-md">
                    {name}
                </h4>

                <p className="text-gray-600">
                    {bio}
                </p>

                <div className='flex gap-1'>
                    <Users className='text-blue-500 w-4 h-4 mr-1' />
                    <p className="text-xs text-gray-500">
                        {members} members
                    </p>
                </div>
            </div>
        </ClickableCard>
    )
}
