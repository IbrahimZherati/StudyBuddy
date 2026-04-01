import { Users } from 'lucide-react';
import React from 'react'

export default function FavoriteGroups() {
    const groups = [
        { 
            name: "Math Learning",
            members: 120 ,
            bio : "Computer Science",
            image : "https://i.pravatar.cc/40"
        },
        { 
            name: "Algorithm Masters",
            members: 85 ,
            bio : "Algorithm Design",
            image : "https://i.pravatar.cc/40"
        },
        { 
            name: "Data Structures",
            members: 150 ,
            bio : "Data Organization",
            image : "https://i.pravatar.cc/40"
        },
        { 
            name: "Java Enthusiasts",
            members: 200 ,
            bio : "Java Programming",
            image : "https://i.pravatar.cc/40"
        },
    ];
  return (
    <div className='flex flex-col gap-2'>
        <h3 className="font-bold text-xl">
          Favorite Groups
        </h3>

        <div className="grid sm:grid-cols-2 md:grid-cols-4 gap-4">
            {groups.map((group, index) => (
                <div key={index} 
                 className="bg-[#F5F6FF] p-4 flex gap-2 rounded-3xl shadow-lg"
                >
                    <img src={group.image} alt={group.name}
                     className="w-10 h-10 rounded-full mb-2" 
                    />

                    <div className='flex flex-col gap-1'>
                        <h4 className="font-bold text-md">
                            {group.name}
                        </h4>

                        <p className="text-sm text-gray-600">
                            {group.bio}
                        </p>

                        <div className='flex gap-1'>
                            <Users className='text-blue-500 w-4 h-4 mr-1' />
                            <p className="text-xs text-gray-500">
                                {group.members} members
                            </p>
                        </div>
                    
                    </div>
                </div>
            ))}

        </div>
    </div>
  )
}
