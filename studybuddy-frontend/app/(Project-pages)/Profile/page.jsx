import React from 'react'
import Link from 'next/link'

export default function EditProfile() {
    return (
        <div>
            <Link href="../edit_profile">
                <button className='btn'>
                    Edit Profile
                </button>
            </Link>
        </div>
    )
}
