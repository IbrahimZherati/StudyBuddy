import React from 'react'

export default function NormalNotification({title, message, visible}) {
    return (
        <div
            className={`
                bg-white shadow-lg rounded-xl px-4 py-3 border
                transition-all duration-300
                ${visible ? "opacity-100 translate-y-0" : "opacity-0 -translate-y-4"}
            `}
        >
            {title && <p className="font-semibold">{title}</p>}
            {message && <p className="text-sm text-gray-600">{message}</p>}
        </div>
    )
}
