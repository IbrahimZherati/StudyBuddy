import { Users } from 'lucide-react';
import Image from "next/image"
import React from 'react'

export default function FavoriteGroups() {
    const groups = [
        {
            name: "Math Learning",
            members: 120,
            bio: "Computer Science",
            image: ""
        },
        {
            name: "Algorithm Masters",
            members: 85,
            bio: "Algorithm Design",
            image: ""
        },
        {
            name: "Data Structures",
            members: 150,
            bio: "Data Organization",
            image: ""
        },
        {
            name: "Java Enthusiasts",
            members: 200,
            bio: "Java Programming",
            image: ""
        },
    ];

    return (
        <div className='flex flex-col gap-2'>
            <h3 className="font-bold text-xl">
                Favorite Groups
            </h3>

            <div className="grid sm:grid-cols-2 md:grid-cols-4 gap-4">
                {groups.map((group, index) => (
                    <div key={index}
                        className="bg-[#F5F6FF] p-4 flex gap-2 rounded-3xl shadow-lg"
                    >
                        <Image src={group.image || "/images/group-default.svg"} alt={group.name}
                            width={40} height={40} className="rounded-full inline"
                        />

                        <div className='flex flex-col gap-1'>
                            <h4 className="font-bold text-md">
                                {group.name}
                            </h4>

                            <p className="text-sm text-gray-600">
                                {group.bio}
                            </p>

                            <div className='flex gap-1'>
                                <Users className='text-blue-500 w-4 h-4 mr-1' />
                                <p className="text-xs text-gray-500">
                                    {group.members} members
                                </p>
                            </div>

                        </div>
                    </div>
                ))}

            </div>
        </div>
    )
}
