import React from 'react'
import UserCard from '../UserCard';
import CardContainer from '../CardContainer';

export default function BestBuddies() {
    const buddies = [
        {
            name: "Sara",
            image: "",
            bio: "Computer Science",
            university: "Damascus University",
            href:"#"
        },
        {
            name: "John",
            image: "",
            bio: "Software Engineering",
            university: "Aleppo University",
            href:"#"
        },
        {
            name: "Lina",
            image: "",
            bio: "Data Science",
            university: "Homs University",
            href:"#"
        },
        {
            name: "Omar",
            image: "",
            bio: "Artificial Intelligence",
            university: "Damascus University",
            href:"#"
        },
    ];

    return (
        <div className='flex flex-col gap-2'>
            <h3 className="font-bold text-xl">
                Best Buddies
            </h3>

            <CardContainer>
                {buddies.map((buddy, index) => (
                    <UserCard key={index}
                        {...buddy}
                    />
                ))}
            </CardContainer>
        </div>
    )
}
