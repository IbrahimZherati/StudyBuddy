import React from 'react'
import { Plus, Check, X } from 'lucide-react';
import { useState } from 'react';

export default function chatsDashboardCategories() {
    
    const [categories, setCategories] = useState(['All', 'Groups', 'Private', 'Favourites', 'Unread']);
    const [activeCategory, setActiveCategory] = useState('All');

    const [isAdding, setIsAdding] = useState(false); 
    const [newCategoryName, setNewCategoryName] = useState(''); 
    const [errorMessage, setErrorMessage] = useState(''); 

    const handleAddCategory = () => {
        const trimmedName = newCategoryName.trim();
        if (trimmedName === '') return;

        const isExisting = categories.some(
            (cat) => cat.toLowerCase() === trimmedName.toLowerCase()
        );

        if (isExisting) {
            setErrorMessage('This category already exists!'); 
            return; 
        }

        setCategories([...categories, trimmedName]);
        setActiveCategory(trimmedName);
    
        setNewCategoryName('');
        setErrorMessage('');
        setIsAdding(false);
    };

    const handleDeleteCategory = (categoryToDelete, e) => {
        e.stopPropagation();

        const updatedCategories = categories.filter(cat => cat !== categoryToDelete);
        setCategories(updatedCategories);

        if (activeCategory === categoryToDelete) {
            setActiveCategory('All');
        }
    };

    const handleKeyDown = (e) => {
        if (e.key === 'Enter') {
            handleAddCategory();
        }
    };

    const handleCloseInput = () => {
        setIsAdding(false);
        setNewCategoryName('');
        setErrorMessage('');
    };

    return (
        <div className="flex items-center gap-3 mb-6 flex-wrap">
            {categories.map((category) => {
                const isActive = activeCategory === category;
                return (
                    <button
                        key={category}
                        onClick={() => setActiveCategory(category)}
                        className={`px-5 py-2 text-sm rounded-full border transition-all duration-200 font-bold flex items-center gap-2
                            ${activeCategory === category 
                            ? 'bg-primary text-white border-primary ' 
                            : 'bg-white text-gray-800 border-gray-600 hover:bg-gray-100'
                        }`}
                    >
                        <span>{category}</span>
                            
                        {isActive && category !== 'All' && (
                            <span
                                onClick={(e) => handleDeleteCategory(category, e)}
                                className="p-0.5 rounded-full hover:bg-white/20 text-white/80 hover:text-white transition-colors"
                                title="Delete category"
                            >
                                <X size={14} />
                            </span>
                        )}
                    </button>
                );   
            })}        
        
            {isAdding ? (
                <div className="flex flex-col gap-1 min-w-45">
                    <div className="flex items-center gap-1 bg-white border border-gray-300 rounded-full px-3 py-1.5 animation-fade-in">
                        <input
                            type="text"
                            autoFocus
                            value={newCategoryName}
                            onChange={(e) =>{
                                setNewCategoryName(e.target.value);
                                if (errorMessage) setErrorMessage(''); 
                            }}
                            onKeyDown={handleKeyDown}
                            placeholder="New category..."
                            className="text-sm text-gray-800 outline-none w-28 placeholder-gray-400"
                        />
                           
                        <button onClick={handleAddCategory} className="text-green-600 hover:text-green-700 p-0.5">
                            <Check size={16} />
                        </button>
                        
                        <button onClick={handleCloseInput} className="text-red-500 hover:text-red-600 p-0.5">
                            <X size={16} />
                        </button>
                    </div>
                        
                    {errorMessage && (
                        <span className="text-xs text-red-500 font-medium px-2 animate-pulse">
                            {errorMessage}
                        </span>
                    )}
                </div>
            ) : (
                <button 
                    onClick={() => setIsAdding(true)}
                    className="p-2 text-gray-600 border border-gray-600 rounded-full hover:bg-gray-100 transition-colors ml-1"
                >
                    <Plus size={18} />
                </button>
            )}
        </div>
    )
}
