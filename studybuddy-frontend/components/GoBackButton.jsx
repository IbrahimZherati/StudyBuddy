'use client'
import React from 'react'
import {ArrowLeft} from "lucide-react"
import {useRouter} from "next/navigation"

export default function GoBackButton() {
    const x = useRouter()
  return (
    <div className='absolute top-8 left-[25%]'>
        <button onClick={()=>x.back()} className='border-2 backdrop-blur-md p-1 rounded-full text-white cursor-pointer'>
            <ArrowLeft size={20}/>
        </button>
    </div>
  )
}
