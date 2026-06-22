import post from '@/utils/API/post';
import Link from 'next/link';
import React from 'react'

export default function FriendshipStatus({ user, noMessageButton = false }) {

    const postRequest = async (url, param) => {
        await post(null, url, param);
    }

    return (
        <div className='flex-row-center gap-7'>
            {user.isFriend &&
                <span 
                    className={`btn text-[1rem] disabled opacity-100
                                ${noMessageButton? "w-full": ""}`}
                    disabled
                >
                    Buddies!
                </span>
            }
            {!user.isFriend && !user.isRequestSent && !user.isRequestReceived &&
                <button
                    className={`btn text-[1rem] 
                                ${noMessageButton? "w-full": ""}`}
                    onClick={() => postRequest("ClientUser/FriendRequest", {
                        key: "requestClientUserId",
                        value: user.id
                    })}
                >
                    Add Buddy
                </button>
            }
            {!user.isFriend && user.isRequestReceived &&
                <button
                    className={`btn text-[1rem] 
                                ${noMessageButton? "w-full": ""}`}
                    onClick={() => postRequest("ClientUser/AcceptFriendRequestByClientId", {
                        key: "fromClientId",
                        value: user.id
                    })}
                >
                    Accept Request
                </button>
            }
            {!user.isFriend && user.isRequestSent &&
                <span 
                    className={`btn text-[1rem] disabled 
                                ${noMessageButton? "w-full": ""}`}
                    disabled
                >
                    Request Pending
                </span>
            }

            {!noMessageButton &&
                <Link href={`${user.isFriend ? `/chat/${user.id}` : ""}`}>
                    <button className={`btn ${!user.isFriend ? "disabled" : ""} text-[1rem]`}>
                        Message
                    </button>
                </Link>
            }

        </div>
    )
}
