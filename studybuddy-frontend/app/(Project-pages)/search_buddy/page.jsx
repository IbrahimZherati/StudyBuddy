"use client";
import React from 'react';
import { useState } from 'react';
import { Search } from 'lucide-react';
import CardContainer from '@/components/CardContainer';
import RecommendedBuddyCard from '@/components/RecommendedBuddyCard';

export default function SearchBuddy() {
  
    const buddies = [
        {
            name: "Milla",
            major: "Computer Science",
            university: "Homs university",
            availableDays: ["Mon", "Wed"],
            bio: "profissional with java, need to learn c++",
            studyInterests: ["Java", "C++"],
            href: "#",
            image: ""
        },
        {
            name: "Ro'a",
            major: "Computer Science",
            university: "Damas university",
            availableDays: ["Sun", "Mon", "Thurs"],
            bio: "Love Programming",
            studyInterests: ["Java", "Algorithm", "C++"],
            href: "#",
            image: ""
        },
        {
            name: "Sozy",
            major: "Computer Science",
            university: "Damas university",
            availableDays: ["Sun", "Mon"],
            bio: "need to learn python",
            studyInterests: ["Python", "Algorithm"],
            href: "#",
            image: ""
        },
        {
            name: "Lolo",
            major: "Computer Science",
            university: "Damas university",
            availableDays: ["Sun", "Mon", "Thurs"],
            bio: "I love teaching others what I knew",
            studyInterests: ["Java", "JS", "Algorithm"],
            href: "#",
            image: ""
        }
    ];


    const filters = ["All", "Days", "University", "Interest", "Major"];
    const [activeFilter, setActiveFilter] = useState("Major"); 

    return (
        <div className="flex-1 p-6 bg-white">
      

            <div className="flex justify-end mb-6">
                <div className="relative w-full max-w-md">
                    <span className="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
                        <Search className="h-5 w-5 text-indigo-600" />
                    </span>

                    <input
                        type="text"
                        placeholder="Search Buddies..."
                        className="w-full pl-10 pr-4 py-2 bg-[#DCE4FF] text-black placeholder-gray-500 rounded-xl text-md focus:outline-none focus:ring-2 focus:ring-indigo-400"
                    />
                </div>
            </div>


            <div className="flex flex-wrap gap-3 mb-8 text-md font-medium">
                {filters.map((filter) => (
                    <button
                        key={filter}
                        onClick={() => setActiveFilter(filter)}
                        className={`px-5 py-1.5 rounded-full transition-colors ${
                            activeFilter === filter
                            ? 'bg-secondary text-black'
                            : 'bg-gray-100 text-gray-500 hover:bg-gray-200'
                        }`}
                    >
                        {filter}
                    </button>
                ))}
            </div>

      
            <CardContainer additionalStyles="grid grid-cols-1 md:grid-cols-2 gap-6">
                {buddies.map((buddy, index) => (
                    <RecommendedBuddyCard key={index}
                        {...buddy}
                    />
                ))}
            </CardContainer>

        </div>
    );
}
