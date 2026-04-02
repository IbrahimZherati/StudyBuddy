import React from 'react'
import Image from 'next/image'

export default function BestBuddies() {
    const buddies = [
        {
            name: "Sara",
            image: "",
            bio: "Computer Science",
            university: "Damascus University",
        },
        {
            name: "John",
            image: "",
            bio: "Software Engineering",
            university: "Aleppo University",
        },
        {
            name: "Lina",
            image: "",
            bio: "Data Science",
            university: "Homs University",
        },
        {
            name: "Omar",
            image: "",
            bio: "Artificial Intelligence",
            university: "Damascus University",
        },
    ];

    return (
        <div className='flex flex-col gap-2'>
            <h3 className="font-bold text-xl">
                Best Buddies
            </h3>

            <div className="grid sm:grid-cols-2 md:grid-cols-4 gap-4">
                {buddies.map((buddy, index) => (
                    <div key={index}
                        className="bg-[#F5F6FF] p-3 rounded-3xl shadow-lg flex items-center gap-1"
                    >
                        <Image src={buddy.image || "/images/avatar-default.svg"} alt={buddy.name}
                            width={48} height={48} className="rounded-full inline"
                        />

                        <div className="flex flex-col gap-0.5"> 
                            <h4 className="font-bold">
                                {buddy.name}
                            </h4>

                            <p className="text-sm text-gray-700">
                                {buddy.bio}
                            </p>

                            <p className="text-xs text-gray-700">
                                {buddy.university}
                            </p>
                        </div>

                    </div>
                ))}

            </div>
        </div>
    )
}
