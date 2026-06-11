import React from 'react'
import ClickableCard from './ClickableCard'
import PhotoDisplay from './PhotoDisplay'
import { useRouter } from 'next/navigation';
import { defaultProfilePhotoPath } from '@/utils/fileHandling';

export default function RecommendedBuddyCard({image , name , major , university , availableDays=[] , bio , studyInterests=[] , href , connectHref}) {
    const router = useRouter();

    const handleConnectClick = (e) => {
        e.preventDefault();
      
        if (connectHref) {
            router.push(connectHref); 
        }
    };
    
    return (
        <ClickableCard href={href} additionalStyles="flex flex-col gap-2 rounded-3xl lg:w-[90%]">
            <div className='flex gap-6'>
                <PhotoDisplay
                    photo={image || defaultProfilePhotoPath}
                    sizeClass="w-12 h-12"
                    alt={name}
                />
                

                <div className='flex flex-col'>
                    <p className='font-bold text-[1.5rem]'>
                        {name}
                    </p>

                    <p className='text-[1.1rem] leading-tight'>
                        {major}
                    </p>

                    <p className='text-[1.1rem] leading-tight'>
                        {university}
                    </p>

                    <p className='text-gray-700 flex flex-wrap gap-1 leading-tight'>
                        <span> Available Days : </span> 
                        {availableDays.map((day , index) => (
                            <span key={index} className='text-sm text-gray-500'>
                                {day}  
                                {index < availableDays.length - 1 ? ',' : '.'}
                            </span>
                        ))}
                    </p>
                </div>
            </div>
            
            <p className=' bg-tertiary text-lg px-4 py-1 rounded-2xl border-b truncate'>
                {bio}
            </p>

            <div className='flex gap-3 flex-wrap'>
                {studyInterests.map((interest , index) => (
                    <span key={index} className='text-black bg-secondary px-4 py-1 rounded-xl shadow-sm'>
                        {interest}
                    </span>
                ))}
            </div>

            <button className='btn' onClick={handleConnectClick}>
                Connect
            </button>

        </ClickableCard>
    )
}
