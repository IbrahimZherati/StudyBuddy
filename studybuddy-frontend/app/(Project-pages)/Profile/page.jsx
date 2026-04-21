import React from 'react'
import HeaderProfile from '@/components/Profile/HeaderProfile'
import Bio from '@/components/Profile/Bio'
import AvailableDays from '@/components/Profile/AvailableDays'
import StudyInterests from '@/components/Profile/StudyInterests'
import FavoriteGroups from '@/components/Profile/FavoriteGroups'
import BestBuddies from '@/components/Profile/BestBuddies'
import RecommendedBuddies from '@/components/Profile/RecommendedBuddies'
import ActiveGroup from '@/components/Profile/ActiveGroup'
import ImportantReminders from '@/components/Profile/ImportantReminders'

export default function Profile() {
    return (
        <div className='flex flex-col gap-6 p-6'>
            <HeaderProfile isProfile={true} />

            {/* Bio */}
            <Bio />

            {/* Important Reminders */}
            <ImportantReminders />

            {/* Study Interests */}
            <StudyInterests />

            {/* Available Days */}
            <AvailableDays />

            {/* Favorite Groups */}
            <FavoriteGroups />

            {/* Best Buddies */}
            <BestBuddies />

            {/* Recommended Buddies */}
            <RecommendedBuddies />

            {/* Active Group */}
            <ActiveGroup />

        </div>
    )
}
