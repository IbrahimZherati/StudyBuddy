import post from '@/utils/API/post';
import Link from 'next/link';
import React, { useState } from 'react'
import FriendShipStatusButton from './FriendShipStatusButton';

export default function FriendshipStatus({ user, noMessageButton = false }) {

    const [isWaiting, setIsWaiting] = useState(false);

    const postRequest = async (url, param, onSuccess) => {
        setIsWaiting(true);
        try {
            const data = await post(null, url, param);
            if(data?.isSuccess && onSuccess)
                onSuccess();
        }
        catch(error) {
            console.log("Error with request: ", error?.data?.response);
            window.location.reload();
        }
        finally {
            setIsWaiting(false);
        }
    }

    const [isFriend, setIsFriend] = useState(Boolean(user.isFriend));
    const [isRequestSent, setIsRequestSent] = useState(Boolean(user.isRequestSent));
    const [isRequestReceived, setIsRequestReceived] = useState(Boolean(user.isRequestReceived));

    return (
        <div className='flex-row-center gap-7'>
            {isFriend &&
                <span
                    className={`btn text-[1rem] disabled opacity-100
                                ${noMessageButton ? "w-full" : ""}`}
                    disabled
                >
                    Buddies!
                </span>
            }
            {!isFriend && !isRequestSent && !isRequestReceived &&
                <FriendShipStatusButton 
                    isWaiting={isWaiting}
                    styles={`btn text-[1rem] 
                                ${noMessageButton ? "w-full" : ""}`}
                    onClick={() => {
                        postRequest("ClientUser/FriendRequest", {
                            key: "requestClientUserId",
                            value: user.id
                        }, () => setIsRequestSent(true));
                    }}
                    text="Add Buddy"
                />
            }
            {!isFriend && isRequestReceived &&
                <div className="flex gap-2 w-full">
                    <FriendShipStatusButton 
                        isWaiting={isWaiting}
                        styles={`btn text-[1rem] whitespace-nowrap
                                ${noMessageButton ? "w-full" : ""}`}
                        onClick={() => {
                            postRequest("ClientUser/AcceptFriendRequestByClientId", {
                                key: "fromClientId",
                                value: user.id
                            }, () => setIsFriend(true));
                        }}
                        text="Accept Request"
                    />

                    <FriendShipStatusButton 
                        isWaiting={isWaiting}
                        styles={`btn text-[1rem] whitespace-nowrap
                                ${noMessageButton ? "w-full" : ""}`}
                        onClick={() => {
                            postRequest("ClientUser/RejectFriendRequestByClientId", {
                                key: "fromClientId",
                                value: user.id
                            }, () => setIsRequestReceived(false));
                        }}
                        text="Reject Request"
                    />
                </div>
            }
            {!isFriend && isRequestSent &&
                <FriendShipStatusButton 
                    isWaiting={isWaiting}
                    styles={`btn bg-[#7979e6] text-[1rem] 
                                ${noMessageButton ? "w-full" : ""}`}
                    onClick={() => {
                        postRequest("ClientUser/CancelFriendRequestByClientId", {
                            key: "toClientId",
                            value: user.id
                        }, () => setIsRequestSent(false))
                    }}
                    text="Cancel Request"
                />
            }

            {!noMessageButton &&
                <Link href={`${isFriend ? `/chat/${user.id}` : ""}`}>
                    <button disabled={isWaiting || !isFriend} className={`btn ${!isFriend ? "disabled" : ""} text-[1rem]`}>
                        Message
                    </button>
                </Link>
            }

        </div>
    )
}
