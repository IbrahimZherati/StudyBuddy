import React from 'react'
import UserCard from '../UserCard';
import CardContainer from '../CardContainer';

export default function BestBuddies({ buddies }) {
    return (
        <div className='flex flex-col gap-2'>
            <h3 className="font-bold text-2xl">
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
