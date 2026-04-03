import HeaderFriendProfile from '@/components/FriendProfilePage/HeaderFriendProfile'
import FavoriteGroups from '@/components/FriendProfilePage/FavoriteGroups'
import BestBuddies from '@/components/FriendProfilePage/BestBuddies'
import React from 'react'

export default function page() {
	const profile = {
		bio: "What is more, the structure of the treatment would facilitate the development of the Limitation of competitive Manner ",
		interests: ["Java", "Algorithms", "Data Structures"],
		days: [
			{ day: "Sun", active: false },
			{ day: "Mon", active: true },
			{ day: "Tue", active: false },
			{ day: "Wed", active: false },
			{ day: "Thu", active: false },
			{ day: "Fri", active: false },
			{ day: "Sat", active: true },
		],
	}
	return (
		<div className='flex flex-col gap-6'>
			<HeaderFriendProfile />

			{/* Bio */}
			<div className='flex flex-col gap-2'>
				<h3 className="text-xl font-bold">
					Bio
				</h3>

				<p className="p-3 bg-tertiary rounded-xl">
					{profile.bio}
				</p>
			</div>

			{/* Study Interests */}
			<div className='flex flex-col gap-2'>
				<h3 className="text-xl font-bold">
					Study Interests
				</h3>

				<div className='flex flex-wrap gap-3'>
					{profile.interests.map((interest, index) => (
						<span key={index} className='px-3 py-1 text-sm rounded-full bg-secondary'>
							{interest}
						</span>
					))}
				</div>
			</div>

			{/* Available Days */}
			<div className='flex flex-col gap-2'>
				<h3 className="text-xl font-bold">
					Available Days
				</h3>

				<div className='flex flex-wrap gap-3'>
					{profile.days.map((day, index) => (
						<span key={index}
							className={`px-4 py-1 rounded-full text-sm transition 
                    ${day.active
									? "bg-secondary"
									: "bg-tertiary "
								}`}
						>
							{day.day}
						</span>
					))}
				</div>
			</div>

			{/* Favorite Groups */}
			<FavoriteGroups />

			{/* Best Buddies */}
			<BestBuddies />

		</div>
	)
}
