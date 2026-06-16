"use client";
import React from "react";
import { Search, Filter } from "lucide-react";

export default function SearchBar({placeholder, searchQuery, setSearchQuery, className}) {

    return (
        <div className={className}>
            <div className="relative flex items-center w-full max-w-md">
                <Search className="absolute left-3 w-5 h-5 text-indigo-600" />
                <input
                    type="text"
                    placeholder={placeholder}
                    value={searchQuery}
                    onChange={(e) => setSearchQuery(e.target.value)}
                    className="w-full pl-10 pr-10 py-2.5 bg-[#DCE4FF] text-black placeholder-gray-500 rounded-xl text-md focus:outline-none focus:ring-2 focus:ring-indigo-400"
                />
                <Filter className="absolute right-3 w-5 h-5 cursor-pointer text-gray-900" />
            </div>
        </div>
    )
}
