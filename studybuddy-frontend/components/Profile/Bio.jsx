import React from 'react'

export default function Bio({ bio }) {
    return (
        <div className='flex flex-col gap-2'>
            <h3 className="text-2xl font-bold">
                Bio
            </h3>
        
            <p className="p-3 bg-tertiary rounded-xl">
                {bio}
            </p>
        </div>
    )
}
