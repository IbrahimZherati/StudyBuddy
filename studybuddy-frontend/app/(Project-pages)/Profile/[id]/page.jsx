"use client"

import React from 'react'
import HeaderProfile from '@/components/Profile/HeaderProfile'
import Bio from '@/components/Profile/Bio'
import AvailableDays from '@/components/Profile/AvailableDays'
import StudyInterests from '@/components/Profile/StudyInterests'
import FavoriteGroups from '@/components/Profile/FavoriteGroups'
import BestBuddies from '@/components/Profile/BestBuddies'
import RecommendedBuddies from '@/components/Profile/RecommendedBuddies'
import { use } from 'react'
import useGetOtherUserInfo from '@/app/hooks/useGetOtherUserInfo';

export default function OtherProfile({ params }) {
    const { id } = use(params); 

    const user = useGetOtherUserInfo(id, false);

    return (
        <div className='flex flex-col gap-6 p-6'>
            <HeaderProfile isMyProfile={true} />

            <Bio />

            <StudyInterests />

            <AvailableDays />

            <FavoriteGroups />

            <BestBuddies />

            <RecommendedBuddies />

        </div>
    )
}
