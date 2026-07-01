'use client';

import React, { use, useState } from 'react';
import { ArrowLeft, Sparkles } from 'lucide-react';
import { useRouter } from 'next/navigation';
import SummaryCard from '@/components/Files/SummaryCard';
import FlashCard from '@/components/Files/FlashCard';
import useGetSummary from '@/app/hooks/useGetSummary';
import useGetFlashCards from '@/app/hooks/useGetFlashCards';
import useGetFileName from '@/app/hooks/useGetFileName';

export default function AIAnalysisView({ params }) {
    const { id } = use(params);

    const fileName = useGetFileName(id);
    const fileNameWithoutExtenstion = fileName?.substring(0, fileName.length - 4);

    const router = useRouter();

    const gradientPalette = [
        { from: '#fe3d6c', to: '#fc9995' },
        { from: '#3fff7c', to: '#3ffbe0' },
        { from: '#6b2cf5', to: '#d450e6' },
        { from: '#2f80ff', to: '#3ccbff' },
        { from: '#ff4ba7', to: '#ffda64' },
        { from: '#a1ffd9', to: '#f3ff77' },
    ];

    const [summary] = useState(
        "Next.js is an open-source full-stack web development framework created by the private company Vercel providing React-based web applications with server-side rendering and static rendering. React documentation mentions Next.js among Recommended Toolchains advising it to developers when building a server-rendered website with Node Where traditional React apps can only render their content in the client-side browser, Next.js extends this functionality to include applications rendered on the server-side. The copyright and trademarks for Next.js are owned by Vercel, which also maintains and leads its open-source development"
    );

    const [flashCards] = useState([
        { 
            id: 1, 
            question: "What is the primary advantage of using the Next.js App Router?", 
            answer: "It provides built-in support for Server Components, automatic performance optimization, and a simplified file-system based routing mechanism." 
        },
        { 
            id: 2, 
            question: "How do we create a reusable component in React?", 
            answer: "By isolating the UI logic into an independent file and making it accept dynamic data via Props instead of hardcoding values." 
        },
        { 
            id: 3, 
            question: "What is the purpose of the transform-style property in flip animations?", 
            answer: "It allows child elements to preserve their 3D positioning and depth when the parent container is rotated." 
        },
        { 
            id: 4, 
            question: "Why do we use color gradients in UI cards?", 
            answer: "They provide visual depth, making the user interface feel more lively, modern, and engaging compared to standard solid colors." 
        },
        { 
            id: 5, 
            question: "What is the primary use case for Client Components in Next.js?", 
            answer: "They are used when user interactivity is required, such as handling state hooks (useState, useEffect) or listening to UI events like onClick." 
        },
        { 
            id: 6, 
            question: "How does the Gemini tool assist in file analysis?", 
            answer: "It parses file content, comprehends the context, extracts key insights accurately, and dynamically generates interactive Q&As based on the data." 
        }
    ]);

    // const summary = useGetSummary(id);

    // const numberOfFlashCards = 6;
    // const [flashCards, refreshFlashCards] = useGetFlashCards(id, numberOfFlashCards);

    const gotFlashCards = Array.isArray(flashCards) && flashCards?.length > 0;

    const getNewFlashCards = () => {
        localStorage.removeItem(`file_flashCards_${id}`);
        // refreshFlashCards();
    }

    return (
        <div className="p-6">
            <div className="flex items-center justify-between mb-8 border-b border-gray-200 pb-4">
                <div className="flex items-center gap-3">
                    <div className="p-2 bg-purple-50 text-purple-600 rounded-xl">
                        <Sparkles size={30} />
                    </div>

                    <div>
                        <h1 className="text-2xl font-bold text-black">
                            Artificial intelligence analysis (Gemini AI)
                        </h1>

                        <p className="text-lg text-gray-500 mt-0.5">
                            Review of the file and interactive flashcards
                        </p>
                    </div>
                </div>

                <div className="flex justify-end mb-2">
                    <button
                        onClick={() => router.push("/files")}
                        className="p-1 rounded-full border border-gray-300 bg-white hover:bg-gray-50 
                                text-gray-700 transition-colors"
                    >
                        <ArrowLeft className="w-8 h-8 rotate-180" />
                    </button>
                </div>
            </div>

            <h1 className="flex gap-2 text-2xl font-bold text-gray-600 mb-8 ml-2">
                <span className="text-black">File:</span> 
                <span className="italic underline">{fileNameWithoutExtenstion}</span> 
            </h1>

            <div className="mb-10">
                <SummaryCard summaryText={summary} />
            </div>

            <div>
                <h2 className="text-2xl font-bold text-black mb-4">
                    Flash Cards
                </h2>

                <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
                    {gotFlashCards &&
                        flashCards.map((card, index) => (
                            <FlashCard
                                key={card.id}
                                card={card}
                                index={index + 1}
                                gradient={gradientPalette[card.id % gradientPalette.length]}
                            />
                        ))
                    }
                </div>

                <div className="text-gray-500 text-lg">
                    {!gotFlashCards && flashCards &&
                        <p>{flashCards}</p>
                    }

                    {!gotFlashCards && !flashCards &&
                        <p>Generating flash cards...</p>
                    }
                </div>
            </div>

            <div className='p-4 mt-6 flex justify-center'>
                <button 
                    className={`btn ${!gotFlashCards? "disabled": ""}`}
                    onClick={getNewFlashCards}
                    disabled={!gotFlashCards}
                >
                    New Flash Cards
                </button>
            </div>
        </div>
    );
}