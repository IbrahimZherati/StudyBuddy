'use client'
import React from 'react'
import { ArrowLeft } from "lucide-react"
import { useRouter } from "next/navigation"

export default function GoBackButton() {
    const router = useRouter();
    return (
        <div className='absolute -top-1 -left-15'>
            <button onClick={() => router.back()} 
                    className='border-4 backdrop-blur-md p-1 rounded-full text-white cursor-pointer
                                hover:scale-[110%] active:text-blue-300 transition duration-200'>
                <ArrowLeft size={22} strokeWidth={3}/>
            </button>
        </div>
    )
}
