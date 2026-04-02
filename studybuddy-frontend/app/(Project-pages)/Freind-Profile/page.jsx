import HeaderFriendProfile from '@/components/HeaderFriendProfile'
import FavoriteGroups from '@/components/FavoriteGroups'
import BestBuddies from '@/components/BestBuddies'
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
				<h3 className="font-bold text-xl">
					Bio
				</h3>

				<p className="bg-[#F5F6FF] p-3 rounded-xl">
					{profile.bio}
				</p>
			</div>

			{/* Study Interests */}
			<div className='flex flex-col gap-2'>
				<h3 className="font-bold text-xl">
					Study Interests
				</h3>

				<div className='flex gap-3 flex-wrap'>
					{profile.interests.map((interest, index) => (
						<span key={index} className='bg-[#a0aaef] px-3 py-1 rounded-full text-sm'>
							{interest}
						</span>
					))}
				</div>
			</div>

			{/* Available Days */}
			<div className='flex flex-col gap-2'>
				<h3 className="font-bold text-xl">
					Available Days
				</h3>

				<div className='flex gap-3 flex-wrap'>
					{profile.days.map((day, index) => (
						<span key={index}
							className={`px-4 py-1 rounded-full text-sm transition 
                    ${day.active
									? "bg-[#a0aaef]"
									: "bg-[#F5F6FF] "
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
