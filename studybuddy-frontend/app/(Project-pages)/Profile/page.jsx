import React from 'react'
import Link from 'next/link'
import HeaderProfile from '@/components/Profile/HeaderProfile'

export default function Profile() {
    return (
        <div className='p-6'>
            <HeaderProfile isProfile={true} />
        </div>
    )
}
