import React, { useMemo } from 'react'
import Card from './Card'
import PhotoDisplay from './PhotoDisplay'
import { defaultProfilePhotoPath, fileFromBase64 } from '@/utils/fileHandling';
import FriendshipStatus from './FriendshipStatus';
import Link from 'next/link';

export default function RecommendedBuddyCard({ user }) {

    const userPhoto = useMemo(() => {
        return fileFromBase64(user.photo, defaultProfilePhotoPath);
    }, [user.photo]);

    return (
        <Card additionalStyles="flex flex-col rounded-3xl lg:w-[90%]"> 
            <Link 
                href={`/profile/${user.id}`}
                className="flex flex-col gap-6 rounded-3xl w-full"
            >
                <div className='flex gap-6'>
                    <PhotoDisplay
                        photo={userPhoto}
                        sizeClass="w-12 h-12"
                        alt={user.userName}
                    />

                    <div className='flex flex-col'>
                        <p className='font-bold text-[1.5rem]'>
                            {user.userName}
                        </p>

                        <p className='text-[1.1rem] leading-tight'>
                            {user.major}
                        </p>

                        <p className={`text-[1.1rem] leading-tight
                                        ${!user.university ? "text-gray-500" : ""}
                        `}>
                            {user.university || "No Universtiy Set"}
                        </p>

                        <p className='text-gray-700 flex flex-wrap gap-1 leading-tight'>
                            <span> Available Days: </span>
                            {user.availableDaysList?.map((day, index) => (
                                <span key={index} className='text-sm text-gray-500'>
                                    {day.substring(0, 3)}
                                    {index < user.availableDaysList.length - 1 ? ',' : ''}
                                </span>
                            ))}
                            {!user.availableDaysList?.length &&
                                <span className='text-sm text-gray-500'>
                                    <i>none</i>
                                </span>
                            }
                        </p>
                    </div>
                </div>

                <p className={`bg-tertiary text-lg px-4 py-1 rounded-2xl border-b truncate 
                            ${!user.bio ? "text-gray-500" : ""}`}>
                    {user.bio || "No bio set"}
                </p>

                <div className='flex gap-3 flex-wrap'>
                    {user.studyInterestsList?.map((interest, index) => (
                        <span key={index} className='text-black bg-secondary px-4 py-1 rounded-xl shadow-sm'>
                            {interest}
                        </span>
                    ))}
                </div>
            </Link>

            <div className="mt-auto">
                <FriendshipStatus
                    user={user}
                    noMessageButton={true}
                />
            </div>
        </Card>
    )
}
