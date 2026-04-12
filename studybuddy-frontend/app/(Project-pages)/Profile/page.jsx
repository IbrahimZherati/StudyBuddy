import React from 'react'
import Link from 'next/link'

export default function page() {
    return (
        <div>
            <Link href="/Profile/EditProfile">
                <button className='btn'>
                    Edit Profile
                </button>
            </Link>
        </div>
    )
}
