import { Heart, Users } from 'lucide-react'
import React from 'react'

export default function HeaderFriendProfile() {
    const user = {
      name: "Sara",
      major : "Computer Science, Damascus University",
      year : "Third Year",
      profilePicture: "https://randomuser.me/api/portraits/women/44.jpg",
      FriendsNumber : 3042,
   }
  return (
    <div className='flex items-center gap-7 flex-wrap'>
        <img src={user.profilePicture} alt="profilePicture"
             className="w-15 h-15 rounded-full"
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
          <Users className='text-blue-600'/>
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
