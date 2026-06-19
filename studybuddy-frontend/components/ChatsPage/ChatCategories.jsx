import React from 'react'

export default function ChatCategories({categories, activeCategory, setActiveCategory}) {
    return (
        <div className="flex items-center gap-3 mb-6 flex-wrap">
            {categories.map((category) => {
                const isActive = activeCategory === category;
                return (
                    <button
                        key={category}
                        onClick={() => setActiveCategory(category)}
                        className={`px-5 py-2 text-sm rounded-full border transition-all duration-200 font-bold flex items-center gap-2
                            ${isActive
                            ? 'bg-primary text-white border-primary ' 
                            : 'bg-white text-gray-800 border-gray-600 hover:bg-gray-100'
                        }`}
                    >
                        <span>{category}</span>
                    </button>
                );   
            })}        
        </div>
    )
}
