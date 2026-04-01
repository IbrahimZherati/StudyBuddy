'use client'
import React from 'react'
import { FileText, Timer, Book, Calculator, Calendar , GraduationCap , Menu , X} from "lucide-react";
import Link from 'next/link';
import { useState } from 'react';

export default function Sidebar() {
    const [isOpen, setIsOpen] = useState(false);

    const items = [
        {icon : <FileText/> , label : "Your Notes" , href : "#"},
        {icon : <Timer/> , label : "Pomodoro Timer" , href : "#"},
        {icon : <Book/> , label : "Learning Articles" , href : "#"},
        {icon : <Calculator/> , label : "Rate Calculator" , href : "#"},
        {icon : <Calendar/> , label : "Schedule Calendar" , href : "#"}
    ]
  return (
    <>
        {/* mobile button */}
        <button
          onClick={() => setIsOpen(true)}
          className="md:hidden fixed top-4 left-4 z-50 bg-white p-2 rounded shadow"
        >
            <Menu />
        </button>

        {/* Overlay */}
        {isOpen && (
            <div
              onClick={() => setIsOpen(false)}
              className="fixed inset-0 bg-black/40 z-40 md:hidden"
            />
        )}
      
        {/* Sidebar */}
        <aside className={`
            fixed  top-0 left-0 h-full border-r w-64 md:w-56 bg-[#F5F6FF] p-2 z-50
            transform transition-transform duration-300
            ${isOpen ? "translate-x-0" : "-translate-x-full"}
            md:translate-x-0 md:block flex flex-col gap-2
            `}
        >
        {/* className='flex-col-center gap-4     hidden md:block' */}
            
            <div className='absolute top-2.5 right-0 p-1'>
                <button onClick={() => setIsOpen(false)}
                    className="md:hidden cursor-pointer"
                >
                    <X />
                </button>
            </div>

            <div className='flex gap-2'>
                <GraduationCap className='text-[#002CFF] w-9 h-9'/>

                <h1 className='font-bold text-2xl'>
                    Study Buddy
                </h1>

            </div>

            <ul>
                {items.map((item, index) => (
                    <li key={index} >
                        <Link href={item.href}
                            className='flex items-center gap-2 p-2 hover:bg-[#E0E4FF] cursor-pointer rounded-md'
                        >
                            {item.icon}
                            <span>{item.label}</span>
                        </Link>

                    </li>
                ))}
            </ul>
        </aside>
    </>
  )
}
