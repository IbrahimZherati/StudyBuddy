import React from 'react'
import { Home, User, Bell, Folder, MessageCircle, Users, Globe2 } from "lucide-react";
import Link from 'next/link';
import Image from 'next/image';

export default function Navbar() {
    const userData = {
        name: "Alexa Max",
        image: ""
    };

    return (
        <nav className='fixed top-0 left-0 md:left-56 right-0 h-16 z-40
            flex-row-center justify-between gap-4 py-2 px-4 bg-[#F5F6FF] 
            border-b pl-18 md:pl-0 whitespace-nowrap
            overflow-x-auto /* scroll */'
        >

            <div className='flex-row-center gap-8 md:gap-16 px-6 min-w-max '>
                <Link href="">
                    <Home className='icon-navbar'/>
                </Link>

                <Link href="">
                    <User className='icon-navbar'/>
                </Link>

                <Link href="">
                    <Bell className='icon-navbar'/>
                </Link>

                <Link href="">
                    <Folder className='icon-navbar'/>
                </Link>

                <Link href="">
                    <MessageCircle className='icon-navbar'/>
                </Link>

                <Link href="">
                    <Users className='icon-navbar'/>
                </Link>

                <Link href="">
                    <Globe2 className='icon-navbar'/>
                </Link>

            </div>

            <div className='flex items-center gap-2.5'>
                <p className='text-lg font-bold'>
                    {userData.name}
                </p>

                <Image src={userData.image || "/images/avatar-default.svg"} alt={userData.name}
                    width={32} height={32} className="rounded-full inline object-cover"
                />
            </div>
        </nav>
    )
}
