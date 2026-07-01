'use client'
import React from 'react'
import { GraduationCap, Menu, X, Search, Newspaper, FilePlusCorner } from "lucide-react";
import Link from 'next/link';
import { useState } from 'react';
import { usePathname } from 'next/navigation';

export default function Sidebar() {
    const [isOpen, setIsOpen] = useState(false);
    const pathname = usePathname();

    const items = [
        { icon: <Search />, label: "Search Buddy", href: "/search_buddy" },
        { icon: <Newspaper />, label: "My Posts", href: "/posts/mine"},
        { icon: <FilePlusCorner />, label: "My Files", href: "/files" }
    ]

    return (
        <>
            <button
                onClick={() => setIsOpen(true)}
                className="fixed z-50 p-5 md:hidden bg-[#E0E4FF] shadow cursor-pointer"
            >
                <Menu />
            </button>

            {isOpen && (
                <div
                    onClick={() => setIsOpen(false)}
                    className="fixed inset-0 z-40 bg-black/40 md:hidden"
                />
            )}

            <aside className={`
                    fixed top-0 left-0 h-full border-r w-64 md:w-56 bg-tertiary p-2 pt-4 z-50
                    transform transition-transform duration-300
                    ${isOpen ? "translate-x-0" : "-translate-x-full"}
                    md:translate-x-0 md:block flex flex-col gap-4 md:gap-8
                `}
            >

                <div className='absolute top-0 right-0 p-1'>
                    <button onClick={() => setIsOpen(false)}
                        className="cursor-pointer md:hidden"
                    >
                        <X size={30}/>
                    </button>
                </div>

                <Link href="/" className='flex gap-2'>
                    <GraduationCap className='text-primary w-9 h-9' />

                    <h1 className='text-2xl font-bold'>
                        Study Buddy
                    </h1>

                </Link>

                <ul className='md:mt-4'>
                    {items.map((item, index) => {
                        const isActive = pathname === item.href;
                        return (
                            <li key={index} >
                                <Link href={item.href}
                                    className={`flex items-center gap-2 p-4 cursor-pointer rounded-md ${isActive ? 'bg-[#E0E4FF]' : 'hover:bg-[#E0E4FF]'}`}
                                >
                                    {item.icon}
                                    <span className='text-[1.2rem] md:text-[1.05rem]'>{item.label}</span>
                                </Link>
                            </li>
                        );
                    })}
                </ul>
            </aside>
        </>
    )
}
