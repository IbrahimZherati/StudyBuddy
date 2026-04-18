import { FileText, Users } from 'lucide-react'
import Image from "next/image"
import React from 'react'
import Link from 'next/link'

export default function HeaderFriendProfile(isProfile=false) {
	const user = {
		name: "Sara",
		major: "Computer Science, Damascus University",
		year: "Third Year",
		profilePicture: "",
		friendsNumber: 3042,
		postsNumber: 120,
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
            
			<div className='flex flex-col gap-1'>
				<div className='flex gap-2'>
					<Users className='text-blue-600' />
					<span>
						{user.friendsNumber}
					</span>
				</div>

				<div className='flex gap-2'>
					<FileText className='text-blue-600' />
					<span>
						{user.postsNumber}
					</span>
				</div>
			</div>
			
			{isProfile ? (
					<div className='flex gap-7'>
						<Link href="">
							<button className='btn text-[1rem]'>
								Search Buddy
							</button>
						</Link>

						<Link href="../edit_profile">
							<button className='btn text-[1rem]'>
								Edit Profile
							</button>
						</Link>
					</div>
				) : (
					<div className='flex gap-7'>
						<button className='btn text-[1rem]'>
							Add Friend
						</button>

						<button className='btn text-[1rem]'>
							Message
						</button>
					</div>
				) 
			}

		</div>
	)
}
