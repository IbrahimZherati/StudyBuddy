import post from '@/utils/API/post';
import Link from 'next/link';
import React from 'react'

export default function FriendshipStatus({ user }) {
    return (
        <div className='flex gap-7'>
            {user.isFriend &&
                <span className="btn disabled opacity-100 text-[1rem]" disabled>
                    Buddies!
                </span>
            }
            {!user.isFriend && !user.isRequestSent && !user.isRequestReceived &&
                <button
                    className='btn text-[1rem]'
                    onClick={() => post({
                        requestClientUserId: user.id
                    },
                        "ClientUser/FriendRequest")}
                >
                    Add Buddy
                </button>
            }
            {!user.isFriend && user.isRequestReceived &&
                <button
                    className='btn text-[1rem]'
                    onClick={() => post({
                        fromClientId: user.id
                    },
                        "ClientUser/AcceptFriendRequestByClientId")}
                >
                    Accept Request
                </button>
            }
            {!user.isFriend && user.isRequestSent &&
                <span className='btn text-[1rem] disabled' disabled>
                    Request Pending
                </span>
            }

            <Link href={`${user.isFriend ? `/chat/${user.id}` : ""}`}>
                <button className={`btn ${!user.isFriend ? "disabled" : ""} text-[1rem]`}>
                    Message
                </button>
            </Link>
        </div>
    )
}
