"use client";
import React, { useMemo } from 'react';
import { useState } from 'react';
import SearchBar from "@/components/searchBar";
import CardContainer from '@/components/CardContainer';
import RecommendedBuddyCard from '@/components/RecommendedBuddyCard';
import useLazyContainter from '@/app/hooks/useLazyContainer';

export default function SearchBuddy() {
  
    const filters = ["All", "University", "Interest", "Major"];
    const [activeFilter, setActiveFilter] = useState("All"); 

    const [searchQuery, setSearchQuery] = useState("");
    
    const url = searchQuery || activeFilter != "All"? "Search/Buddy": "Search/SuggestedClients";

    const params = useMemo(() => {
        const p = {};
        if(searchQuery)
            p["filter"] = searchQuery;
        if(activeFilter != "All")
            p[`Same${activeFilter}`] = true;
        return p;
    }, [searchQuery, activeFilter]);

    const loadFactor = 20;
    const [items, containerRef, handleScroll] = useLazyContainter(url, loadFactor, params);

    return (
        <div className="flex flex-col h-full min-h-0 flex-1 p-6 bg-white">
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

            <CardContainer 
                ref={containerRef}
                onScroll={handleScroll}
                additionalStyles="grid grid-cols-1 md:grid-cols-2 gap-6 overflow-y-auto no-scrollbar flex-1 min-h-0 auto-rows-max"
            >
                {items.map((user) => (
                    <RecommendedBuddyCard 
                        key={user.id}
                        user={user}
                    />
                ))}
            </CardContainer>

            {items.length === 0 && (
                <div className="text-center text-gray-500 mt-12 py-8 border border-dashed border-gray-200 rounded-2xl">
                    No users match your search.
                </div>
            )}

        </div>
    );
}
