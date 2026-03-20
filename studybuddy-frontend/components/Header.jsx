import Link from 'next/link'
import React from 'react'
import { GraduationCap } from 'lucide-react'

export default function Header() {
  return (
    <header className='flex justify-evenly items-center  py-4 border-0 shadow-lg bg-[#F5F6FF]'>
        <div className='flex gap-2 '>
          <GraduationCap className='text-[#002CFF] w-9 h-9'/>
          <h1 className='font-bold text-2xl'>
            Study Buddy
          </h1>
        </div>

        <div className='flex gap-11 items-center'>
            <Link href="">Free Tools</Link>
            <Link href="">Community Features</Link>
            <Link href="">How it Works</Link>
            <Link href="">Reviews</Link>
            <Link href="/register">
               <button className='rounded-2xl bg-[#002CFF] font-bold w-25 cursor-pointer text-white p-1.5 text-center mx-auto '>
                    Sign Up
                </button>
            </Link>
        </div>

    </header>
  )
}
