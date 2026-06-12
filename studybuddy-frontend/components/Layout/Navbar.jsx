"use client"

import React from 'react'
import { Home, User, Bell, Folder, MessageCircle, Users, Globe2 } from "lucide-react";
import Link from 'next/link';
import useGetUserInfo from '@/app/hooks/useGetUserInfo';
import PhotoDisplay from '../PhotoDisplay';
import { usePathname } from 'next/navigation';
import { defaultProfilePhotoPath, fileFromBase64 } from '@/utils/fileHandling';

export default function Navbar() {
    const [userData] = useGetUserInfo(true);
    const userName = userData?.userName;
    const userPhoto = fileFromBase64(userData?.photo, defaultProfilePhotoPath);

    const pathname = usePathname();

    const getIconClass = (path) => {
        return pathname === path 
            ? 'icon-navbar text-primary font-bold' 
            : 'icon-navbar text-gray-700';
    };

    return (
        <nav className='fixed top-0 left-16 right-0 md:left-56 h-16 z-40
            flex-row-center justify-start p-2 bg-tertiary 
            border-b whitespace-nowrap
            overflow-x-auto overflow-y-hidden'
        >

            <div className='gap-8 px-2 md:px-6 flex-row-center md:gap-16 min-w-max'>
                <Link href="/posts">
                    <Home className={getIconClass('/posts')}/>
                </Link>

                <Link href="/Profile">
                    <User className={getIconClass('/Profile')}/>
                </Link>

                <Link href="/notification">
                    <Bell className={getIconClass('/notification')}/>
                </Link>

                <Link href="/chat_dashboard">
                    <MessageCircle className={getIconClass('/chat_dashboard')}/>
                </Link>

                <Link href="/friends_list">
                    <Users className={getIconClass('/friends_list')}/>
                </Link>
                
                <Link href="">
                    <Folder className='icon-navbar'/>
                </Link>

                <Link href="">
                    <Globe2 className='icon-navbar'/>
                </Link>

            </div>

            <div className='flex items-center gap-2.5 ml-auto pl-4'>
                <p className='text-2xl font-bold'>
                    {userName}
                </p>

                <PhotoDisplay
                    photo={userPhoto}
                    sizeClass="h-14 w-14"
                    alt={userName || "user"}
                />
            </div>
        </nav>
    )
}
