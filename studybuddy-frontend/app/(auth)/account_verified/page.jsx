import Link from 'next/link';
import React from 'react'

export default function AccountVerified() {
    return (
        <div className="flex-col-center gap-8 text-amber-100 text-3xl">
            <div className="flex-col-center gap-5">
                <h1>Your email was verified!</h1>
            </div>

            <div className="flex-col-center gap-2 text-xl">
                <h2>You can login now</h2>
                <Link 
                    href="/login"
                >
                    <button className="btn w-24">
                        Login
                    </button>
                </Link>
            </div>
        </div>
    )
}
