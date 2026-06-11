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
import useGetUserInfo from '@/app/hooks/useGetUserInfo';
import Loading from '@/components/Loading';
import useGetDataList from '@/app/hooks/useGetDataList';

export default function Profile() {
    const [user] = useGetUserInfo(false);
    const dayOptions = useGetDataList("Day");

    const findIdByName = (items, name) => {
        if (!name || !items) return null;

        const item = items.find(
            (i) => (i.name || "").toLowerCase() === String(name).toLowerCase()
        );

        return item ? item.id : null;
    };

    const getDayIdsFromProfile = (profileDays) => {
        if (!dayOptions || !profileDays)
            return [];

        return profileDays.map(
            (dayName) => findIdByName(dayOptions, dayName)
        );
    };

    const days = getDayIdsFromProfile(user?.availableDays);

    if(!user || !days)
		return <Loading />

    return (
        <div className='flex flex-col gap-6 p-6'>
            <HeaderProfile 
                user={user} 
                isMyProfile={true} 
            />

            <Bio 
                bio={user.bio}
            />

            <StudyInterests 
                interests={user.studyInterests}
            />

            <AvailableDays 
                dayOptions={dayOptions}
                value={days}
                sizeClass="text-2xl"
            />

            <BestBuddies 
                buddies={user.bestBuddies}
            />

            <RecommendedBuddies />

        </div>
    )
}
