import HeaderFreindProfile from '@/components/HeaderFreindProfile'
import React from 'react'

export default function page() {
    const profile = {
        bio : "What is more, the structure of the treatment would facilitate the development of the Limitation of competitive Manner ",
        interests : ["Java" , "Algorithms" , "Data Structures"],
        days: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
    }
  return (
    <div className='flex flex-col gap-6'>
        <HeaderFreindProfile />

        <div className='flex flex-col gap-2'>
            <h3 className="font-bold text-xl">Bio</h3>
            <p className="bg-[#F5F6FF] p-3 rounded-xl">
              {profile.bio}
            </p>
        </div>

        <div className='flex flex-col gap-2'>
            <h3 className="font-bold text-xl">Study Interests</h3>
            <div className='flex gap-3 flex-wrap'>
              {profile.interests.map((interest, index) => (
                <span key={index} className='bg-[#a0aaef] px-3 py-1 rounded-full text-sm'>
                  {interest}
                </span>
              ))}
            </div>
        </div>

        <div className='flex flex-col gap-2'>
            <h3 className="font-bold text-xl">Available Days</h3>
            <div className='flex gap-3 flex-wrap'>
              {profile.days.map((day, index) => (
                <span key={index} className='bg-[#F5F6FF] px-3 py-1 rounded-full text-sm'>
                  {day}
                </span>
              ))}
            </div>
        </div>


    </div>
  )
}
