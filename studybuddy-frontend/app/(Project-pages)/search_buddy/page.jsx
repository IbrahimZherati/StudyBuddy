"use client";
import React from 'react';
import { useState } from 'react';
import SearchBar from "@/components/searchBar";
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

    const [searchQuery, setSearchQuery] = useState("");  

    const filteredBuddies = buddies.filter((buddy) => {
        const query = searchQuery.toLowerCase();
        return (
            buddy.name.toLowerCase().includes(query) ||
            buddy.major.toLowerCase().includes(query) ||
            buddy.university.toLowerCase().includes(query) ||
            buddy.studyInterests.some(interest => interest.toLowerCase().includes(query)) ||
            buddy.availableDays.some(day => day.toLowerCase().includes(query))
        );
    });

    return (
        <div className="flex-1 p-6 bg-white">
      

            <SearchBar
                className="flex justify-end mb-6 mr-6"
                placeholder="Search for study buddies..."
                searchQuery={searchQuery}
                setSearchQuery={setSearchQuery}
            />

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
                {filteredBuddies.map((buddy, index) => (
                    <RecommendedBuddyCard key={index}
                        {...buddy}
                    />
                ))}
            </CardContainer>

            {filteredBuddies.length === 0 && (
                <div className="text-center text-gray-500 mt-12 py-8 border border-dashed border-gray-200 rounded-2xl">
                    No study buddies match your search.
                </div>
            )}

        </div>
    );
}
