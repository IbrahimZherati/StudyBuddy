import React from 'react'

export default function BestBuddies() {
    const buddies = [
        {
            name: "Sara",
            image: "https://randomuser.me/api/portraits/women/44.jpg",
            bio : "Computer Science",
            university: "Damascus University",
        },
        {
            name: "John",
            image: "https://randomuser.me/api/portraits/men/45.jpg",
            bio : "Software Engineering",
            university: "Aleppo University",
        },
        {
            name: "Lina",
            image: "https://randomuser.me/api/portraits/women/46.jpg",
            bio : "Data Science",
            university: "Tishreen University",
        },
        {
            name: "Omar",
            image: "https://randomuser.me/api/portraits/men/47.jpg",
            bio : "Artificial Intelligence",
            university: "Damascus University",
        },
    ];

  return (
    <div className='flex flex-col gap-2'>
        <h3 className="font-bold text-xl">
          Best Buddies
        </h3>

        <div className="grid sm:grid-cols-2 md:grid-cols-4 gap-4">
            {buddies.map((buddy, index) => (
                <div key={index} 
                  className="bg-[#F5F6FF] p-3 rounded-3xl shadow-lg flex-col-center gap-1"
                >
                    <img src={buddy.image} alt={buddy.name}
                         className="w-12 h-12 rounded-full" 
                    />

                    <h4 className="font-bold">
                        {buddy.name}
                    </h4>

                    <p className="text-sm text-gray-700">
                        {buddy.bio}
                    </p>

                    <p className="text-xs text-gray-700">
                        {buddy.university}
                    </p>
            
                </div>
            ))}

        </div>
    </div>
  )
}
