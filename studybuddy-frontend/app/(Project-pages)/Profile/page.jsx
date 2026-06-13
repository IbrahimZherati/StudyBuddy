"use client"

import React from 'react'
import useGetUserInfo from '@/app/hooks/useGetUserInfo';
import Profile from '@/components/Profile/Profile';

export default function MyProfile() {
    const [user] = useGetUserInfo(false);

    return <Profile 
        user={user}
        isMyProfile={true}
    />
}
