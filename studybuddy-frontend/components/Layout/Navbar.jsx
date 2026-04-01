import React from 'react'
import { Home, User, Bell, Folder, MessageCircle, Users, Globe2} from "lucide-react";

export default function Navbar() {
  const userName = { 
    name: "Alexa Max" ,
    img : "https://i.pravatar.cc/40"
};
  return (
    <nav className='flex-row-center justify-between gap-4 py-2 px-4 bg-[#F5F6FF] border-b'>
        
        <div className='flex-row-center gap-16 '>
            <Home className='icon-navbar '/>
            <User className='icon-navbar'/>
            <Bell className='icon-navbar'/>
            <Folder className='icon-navbar'/>
            <MessageCircle className='icon-navbar'/>
            <Users className='icon-navbar'/>
            <Globe2 className='icon-navbar'/>
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
