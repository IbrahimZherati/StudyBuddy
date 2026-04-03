import { Users } from 'lucide-react';
import Image from "next/image"
import React from 'react'
import GroupCard from '../GroupCard';
import CardContainer from '../CardContainer';

export default function FavoriteGroups() {
    const groups = [
        {
            name: "Math Learning",
            members: 120,
            bio: "Computer Science",
            image: "",
            href:"#"
        },
        {
            name: "Algorithm Masters",
            members: 85,
            bio: "Algorithm Design",
            image: "",
            href:"#"
        },
        {
            name: "Data Structures",
            members: 150,
            bio: "Data Organization",
            image: "",
            href:"#"
        },
        {
            name: "Java Enthusiasts",
            members: 200,
            bio: "Java Programming",
            image: "",
            href:"#"
        },
    ];

    return (
        <div className='flex flex-col gap-2'>
            <h3 className="font-bold text-xl">
                Favorite Groups
            </h3>

            <CardContainer>
                {groups.map((group, index) => (
                    <GroupCard key={index}
                        {...group}
                    />
                ))}
            </CardContainer>
        </div>
    )
}
