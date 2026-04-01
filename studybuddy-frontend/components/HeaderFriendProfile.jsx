import { Users } from 'lucide-react'
import Image from "next/image"
import React from 'react'

export default function HeaderFriendProfile() {
	const user = {
		name: "Sara",
		major: "Computer Science, Damascus University",
		year: "Third Year",
		profilePicture: "",
		FriendsNumber: 3042,
	}

	return (
		<div className='flex items-center gap-7 flex-wrap'>
			
			<Image src={user.profilePicture || "/images/avatar-default.svg"} alt={user.name}
				width={56} height={56} className="rounded-full inline"
			/>

			<div className='flex flex-col gap-0.5'>
				<h2 className='text-xl font-semibold'>
					{user.name}
				</h2>

				<p className='text-sm text-gray-600'>
					{user.major}
				</p>

				<p className='text-sm text-gray-600'>
					{user.year}
				</p>
			</div>

			<div className='flex gap-2'>
				<Users className='text-blue-600' />
				<span>
					{user.FriendsNumber}
				</span>
			</div>

			<div className='flex gap-7'>
				<button className='btn-sign text-[1rem]'>
					Add Friend
				</button>

				<button className='btn-sign text-[1rem]'>
					Message
				</button>
			</div>

		</div>
	)
}
