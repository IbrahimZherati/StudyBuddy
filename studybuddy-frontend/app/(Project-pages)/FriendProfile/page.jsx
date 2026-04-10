import HeaderFriendProfile from '@/components/Profile/HeaderFriendProfile'
import Bio from '@/components/Profile/Bio'
import AvailableDays from '@/components/Profile/AvailableDays'
import StudyInterests from '@/components/Profile/StudyInterests'
import FavoriteGroups from '@/components/Profile/FavoriteGroups'
import BestBuddies from '@/components/Profile/BestBuddies'
import React from 'react'

export default function page() {
	return (
		<div className='flex flex-col gap-6'>
			<HeaderFriendProfile />

			{/* Bio */}
			<Bio />

			{/* Study Interests */}
			<StudyInterests />

			{/* Available Days */}
			<AvailableDays />

			{/* Favorite Groups */}
			<FavoriteGroups />

			{/* Best Buddies */}
			<BestBuddies />

		</div>
	)
}
