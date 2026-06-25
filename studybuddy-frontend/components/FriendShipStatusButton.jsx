import { LoaderCircle } from 'lucide-react';
import React from 'react'

export default function FriendShipStatusButton({ isWaiting, onClick, styles, text }) {
    return (
        <button
            disabled={isWaiting}
            className={`${styles} flex-row flex-row-center gap-1.5`}
            onClick={onClick}
        >
            <span>{text}</span>
            {isWaiting &&
                <LoaderCircle
                    className="h-4 w-4 animate-spin
                                     text-white drop-shadow-[0_0_6px_rgba(255,255,255,0.8)]"
                    strokeWidth={3}
                />
            }
        </button>
    )
}
