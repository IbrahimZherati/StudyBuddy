import React, { useMemo } from 'react'
import ClickableCard from './ClickableCard'
import PhotoDisplay from './PhotoDisplay'
import { defaultProfilePhotoPath, fileFromBase64 } from '@/utils/fileHandling';

export default function RecommendedBuddyCard({user}) {

    const userPhoto = useMemo(() => {
        return fileFromBase64(user.photo, defaultProfilePhotoPath);
    }, [user.photo]);

    return (
        <ClickableCard href={`profile/${user.id}`} additionalStyles="flex flex-col gap-2 rounded-3xl lg:w-[90%]">
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

                    <p className='text-[1.1rem] leading-tight'>
                        {user.university}
                    </p>

                    <p className='text-gray-700 flex flex-wrap gap-1 leading-tight'>
                        <span> Available Days: </span> 
                        {user.availableDays?.map((day, index) => (
                            <span key={index} className='text-sm text-gray-500'>
                                {day}  
                                {index < user.availableDays.length - 1 ? ',' : ''}
                            </span>
                        ))}
                        {!user.availableDays?.length &&
                            <span className='text-sm text-gray-500'>
                                <i>none</i>
                            </span>
                        }
                    </p>
                </div>
            </div>
            
            <p className={`bg-tertiary text-lg px-4 py-1 rounded-2xl border-b truncate 
                            ${!user.bio? "text-gray-500": ""}`}>
                {user.bio || "No bio set"}
            </p>

            <div className='flex gap-3 flex-wrap'>
                {user.studyInterests?.map((interest , index) => (
                    <span key={index} className='text-black bg-secondary px-4 py-1 rounded-xl shadow-sm'>
                        {interest}
                    </span>
                ))}
            </div>

            <button className='btn select-none transition-all duration-150 active:scale-95 active:brightness-90' 
                    
            >
                Connect
            </button>

        </ClickableCard>
    )
}
