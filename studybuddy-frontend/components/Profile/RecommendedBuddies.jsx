import React from 'react'
import CardContainer from '../CardContainer';
import RecommendedBuddyCard from '../RecommendedBuddyCard';

export default function RecommendedBuddies() {
    const recommendedBuddies = [
        {
            name: "Sara",
            image: "",
            major: "Computer Science",
            university: "Damascus University",
            availableDays: ["sun" , "mon"],
            bio: "Love Programming Love Programming Love Programming",
            studyInterests: ["java" , "python"],
            href:"#"
        },
        {
            name: "John",
            image: "",
            major: "Software Engineering",
            university: "Aleppo University",
            availableDays: ["sun" , "mon"],
            bio: "Love Programming",
            studyInterests: ["java" , "python"],
            href:"#"
        },
    ];

    return (
        <div className="flex flex-col gap-3">
            <h3 className="text-xl font-bold">
                Recommended Buddies
            </h3>
            
            <CardContainer>
                {recommendedBuddies.map((buddy, index) => (
                    <RecommendedBuddyCard key={index}
                        {...buddy}
                    />
                ))}
            </CardContainer>
        </div>
    )
}
