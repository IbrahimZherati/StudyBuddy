import Link from 'next/link';
import React from 'react'

export default function NormalNotification({title, userName, message, visible, href="#"}) {
    return (
        <Link
            href={href}
            className={`
                bg-secondary shadow-lg rounded-xl px-4 py-3 border min-w-40 max-w-90
                hover:bg-[#bbbbef]
                active:scale-[95%]
                transition-all duration-300
                ${visible ? "opacity-100 translate-y-0" : "opacity-0 -translate-y-4"}
            `}
        >
            {title && <p className="font-semibold">{`${title}:`}</p>}
            <div className="text-gray-600 text-sm flex flex-row gap-1">
                {userName && <p className="font-bold italic">{`${userName}:`}</p>}
                {message && <p className="truncate">{message}</p>}
            </div>
        </Link>
    )
}
