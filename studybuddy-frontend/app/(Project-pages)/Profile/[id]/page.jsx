"use client"

import React from 'react'
import { use } from 'react'
import useGetOtherUserInfo from '@/app/hooks/useGetOtherUserInfo';
import Profile from '@/components/Profile/Profile';
import useGetId from '@/app/hooks/useGetId';
import Loading from '@/components/Loading';

export default function OtherProfile({ params }) {
    const { id } = use(params); 
    const user = useGetOtherUserInfo(id, false);
    const myId = useGetId();

    if(!myId)
        return <Loading />

    return <Profile 
        user={user}
        isMyProfile={id == myId}
    />
}
