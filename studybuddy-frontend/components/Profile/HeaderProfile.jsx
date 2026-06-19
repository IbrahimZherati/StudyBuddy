import { FileText, Users } from 'lucide-react'
import React, { useMemo } from 'react'
import Link from 'next/link'
import { defaultProfilePhotoPath, fileFromBase64 } from '@/utils/fileHandling';
import PhotoDisplay from '../PhotoDisplay';
import Logout from './Logout';
import FriendshipStatus from '../FriendshipStatus';

export default function HeaderProfile({ user, isMyProfile = true }) {

	const profilePhoto = useMemo(() => {
        return fileFromBase64(user.photo, defaultProfilePhotoPath);
    }, [user.photo]);

	return (
		<div className='flex items-center gap-7 flex-wrap'>

			<PhotoDisplay
				photo={profilePhoto}
				sizeClass="w-20 h-20"
				alt={user.userName}
			/>

			<div className='flex flex-col gap-0.5'>
				<h2 className='text-2xl font-bold'>
					{user.userName}
				</h2>

				<p className='text-sm text-gray-600'>
					{user.major}
				</p>

				<p className='text-sm text-gray-600'>
					{user.university}
				</p>
			</div>

			<div className='flex flex-col gap-1'>
				<div className='flex gap-2'>
					<Users className='text-blue-600' />
					<span className="font-semibold">
						{user.friendCount}
					</span>
				</div>

				<div className='flex gap-2'>
					<FileText className='text-blue-600' />
					<span className="font-semibold">
						{user.postCount}
					</span>
				</div>
			</div>

			{isMyProfile ? (
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
				<FriendshipStatus 
					user={user}
				/>
			)
			}

			{isMyProfile && 			
				<Logout />
			}

		</div>
	)
}
