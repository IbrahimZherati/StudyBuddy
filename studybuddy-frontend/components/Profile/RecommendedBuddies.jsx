import React from 'react'
import CardContainer from '../CardContainer';
import RecommendedBuddyCard from '../RecommendedBuddyCard';
import useLazyContainter from '@/app/hooks/useLazyContainer';

export default function RecommendedBuddies() {

    const numberOfSuggestionsToShow = 4;
    const [recommendedBuddies] = useLazyContainter("Search/SuggestedClients", numberOfSuggestionsToShow); 

    return (
        <div className="flex flex-col gap-3">
            <h3 className="text-2xl font-bold">
                Recommended Buddies
            </h3>
            
            <CardContainer additionalStyles="grid grid-cols-1 md:grid-cols-2 gap-10">
                {recommendedBuddies?.map((user, index) => (
                    <RecommendedBuddyCard key={index}
                        user={user}
                    />
                ))}
            </CardContainer>
        </div>
    )
}
