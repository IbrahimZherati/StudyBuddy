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
        <nav className='fixed top-0 left-16 right-0 md:left-56 h-16 z-40
            flex-row-center justify-start p-2 bg-tertiary 
            border-b whitespace-nowrap
            overflow-x-auto /* scroll */'
        >

            <div className='gap-8 px-6 flex-row-center md:gap-16 min-w-max'>
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

            <div className='flex items-center gap-2.5 ml-auto pl-4'>
                <p className='text-lg font-bold'>
                    {userData.name}
                </p>

                <Image src={userData.image || "/images/avatar-default.svg"} alt={userData.name}
                    width={32} height={32} className="inline object-cover rounded-full"
                />
            </div>
        </nav>
    )
}
