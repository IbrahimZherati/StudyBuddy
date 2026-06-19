import Link from 'next/link';
import React from 'react'

export default function FriendshipStatus({ user }) {
    return (
        <div className='flex gap-7'>
            {user.isFriend &&
                <span className="btn disabled opacity-100 text-[1rem]">
                    Buddies!
                </span>
            }
            {!user.isFriend && !user.isRequestSent && !user.isRequestReceived &&
                <Link href="ClientUser/FriendRequest">
                    <button className='btn text-[1rem]'>
                        Add Buddy
                    </button>
                </Link>
            }
            {!user.isFriend && user.isRequestReceived &&
                <Link href="ClientUser/FriendRequest">
                    <button className='btn text-[1rem]'>
                        Accept Request
                    </button>
                </Link>
            }
            {!user.isFriend && user.isRequestSent &&
                <button className='btn text-[1rem] disabled' disabled>
                    Request Pending
                </button>
            }

            <Link href={`${user.isFriend? `/chat/${user.id}`: ""}`}>
                <button className={`btn ${!user.isFriend ? "disabled" : ""} text-[1rem]`}>
                    Message
                </button>
            </Link>
        </div>
    )
}
