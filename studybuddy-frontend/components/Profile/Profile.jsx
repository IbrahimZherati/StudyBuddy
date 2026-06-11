"use client"

import React from 'react'
import HeaderProfile from '@/components/Profile/HeaderProfile'
import Bio from '@/components/Profile/Bio'
import AvailableDays from '@/components/Profile/AvailableDays'
import StudyInterests from '@/components/Profile/StudyInterests'
import BestBuddies from '@/components/Profile/BestBuddies'
import RecommendedBuddies from '@/components/Profile/RecommendedBuddies'
import Loading from '@/components/Loading';
import useGetDataList from '@/app/hooks/useGetDataList';
import { getDayIdsFromProfile } from '@/utils/DataLists/dataListsUtils';

export default function Profile({ user, isMyProfile }) {
    const dayOptions = useGetDataList("Day");
    const days = getDayIdsFromProfile(user?.availableDays, dayOptions);

    if(!user)
		return <Loading />

    return (
        <div className='flex flex-col gap-6 p-6'>
            <HeaderProfile 
                user={user} 
                isMyProfile={isMyProfile} 
            />

            <Bio 
                bio={user.bio}
            />

            <StudyInterests 
                interests={user.studyInterests}
            />

            {days && dayOptions &&
                <AvailableDays 
                    dayOptions={dayOptions}
                    value={days}
                    sizeClass="text-2xl"
                />
            }

            <BestBuddies 
                buddies={user.bestBuddies}
            />

            {isMyProfile && 
                <RecommendedBuddies />
            }

        </div>
    )
}
