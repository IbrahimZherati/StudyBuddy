import React from 'react'
import ClickableCard from './ClickableCard'
import Image from 'next/image'

export default function RecommendedBuddyCard({image , name , major , university , availableDays=[] , bio , studyInterests=[] , href}) {
    return (
        <ClickableCard href={href} additionalStyles="flex flex-col gap-2 rounded-3xl">
            <div className='flex gap-6'>
                <Image src={image || "/images/avatar-default.svg"} alt={name}
                    width={40} height={40} className="rounded-full inline w-12 h-12 my-2"
                />

                <div className='flex flex-col gap-0.5'>
                    <p className='font-bold text-[1.5rem]'>
                        {name}
                    </p>

                    <p className='font-semibold text-lg'>
                        {major}
                    </p>

                    <p className='font-semibold text-lg'>
                        {university}
                    </p>

                    <p className='text-gray-700 flex flex-wrap gap-1'>
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
            
            <p className=' bg-tertiary text-lg px-4 py-1 rounded-2xl border-b truncate w-90'>
                {bio}
            </p>

            <div className='flex gap-3 flex-wrap'>
                {studyInterests.map((interest , index) => (
                    <span key={index} className='text-black bg-secondary px-4 py-1 rounded-xl shadow-sm'>
                        {interest}
                    </span>
                ))}
            </div>

            <button className='btn'>
                Connect
            </button>

        </ClickableCard>
    )
}
