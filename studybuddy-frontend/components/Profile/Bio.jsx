import React from 'react'

export default function Bio() {
    const bio = "What is more, the structure of the treatment would facilitate the development of the Limitation of competitive Manner "
    return (
        <div className='flex flex-col gap-2'>
            <h3 className="text-xl font-bold">
                Bio
            </h3>
        
            <p className="p-3 bg-tertiary rounded-xl">
                {bio}
            </p>
        </div>
    )
}
