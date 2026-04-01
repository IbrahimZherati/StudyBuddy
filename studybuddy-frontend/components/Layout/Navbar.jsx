import React from 'react'
import { Home, User, Bell, Folder, MessageCircle, Users, Globe2} from "lucide-react";
import Link from 'next/link';

export default function Navbar() {
  const userName = { 
    name: "Alexa Max" ,
    img : "https://i.pravatar.cc/40"
};
  return (
    <nav className='fixed top-0 left-0 md:left-56 right-0 h-16 z-40
        flex-row-center justify-between gap-4 py-2 px-4 bg-[#F5F6FF] 
        border-b pl-18 md:pl-0 whitespace-nowrap
        overflow-x-auto /* scroll */'
    >
        
        <div className='flex-row-center gap-7 md:gap-16  min-w-max '>
            <Link href="">
              <Home className='icon-navbar '/>
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

        <div className='flex gap-2.5'>
            <p className='text-lg font-bold'>
                {userName.name}
            </p>

            <img src={userName.img} alt="Profile" 
                 className='rounded-full w-8 h-8 object-cover'
            />
        </div>
    </nav>
  )
}
