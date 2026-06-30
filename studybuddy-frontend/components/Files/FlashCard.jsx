import React, { useState } from 'react';

export default function FlashCard({ card, gradient }) {
    const [isFlipped, setIsFlipped] = useState(false);

    return (
        <div 
            onClick={() => setIsFlipped(!isFlipped)}
            className="w-full h-48 cursor-pointer perspective-[1000px]"
        >
            <div 
                className={`relative w-full h-full duration-700 transform-3d transition-all ${
                    isFlipped ? 'transform-[rotateY(180deg)]' : ''
                }`}
            >
        
                <div 
                    style={{ backgroundImage: `linear-gradient(135deg, ${gradient.from}, ${gradient.to})` }}
                    className="absolute inset-0 backface-hidden rounded-2xl p-6 flex flex-col justify-between text-black shadow-md group"
                >
                    <span className="text-sm font-bold uppercase tracking-wider opacity-75">
                        Question #{card.id}
                    </span>

                    <div 
                        onClick={(e) => e.stopPropagation()} 
                        className="text-base font-semibold text-center my-auto w-full pr-1 max-h-22.5 overflow-y-auto overflow-x-hidden select-text scrollbar-card-light"
                    >
                        {card.question}
                    </div>

                    <span className="text-[12px] font-medium text-center text-gray-700 opacity-0 group-hover:opacity-100 transition-opacity duration-300 select-none pointer-events-none">
                        Click to see answer
                    </span>
                </div>

                <div style={{ backgroundImage: 'linear-gradient(135deg, #F5F6FF 0%, #D9DFFF 100%)' }}
                    className="absolute inset-0 backface-hidden transform-[rotateY(180deg)]  text-black rounded-2xl p-6 flex flex-col justify-between shadow-md"
                >
                    <span className="text-sm font-bold uppercase tracking-wider">
                        Answer
                    </span>

                    <div 
                        onClick={(e) => e.stopPropagation()} 
                        className="text-sm font-medium text-center my-auto w-full pr-1 max-h-22.5 overflow-y-auto overflow-x-hidden select-text scrollbar-card-dark"
                    >
                        {card.answer}
                    </div>

                    <span className="text-[12px] text-center text-gray-900 opacity-55">
                        Click again to see question
                    </span>
                </div>
            </div>
        </div>
    );
}