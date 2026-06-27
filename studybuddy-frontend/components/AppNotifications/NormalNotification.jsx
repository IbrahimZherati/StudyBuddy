import Link from 'next/link';
import React, { useEffect, useState } from 'react'

export default function NormalNotification({title, userName, message, visible, href="#"}) {
    const [entered, setEntered] = useState(false);

    useEffect(() => {
        const id = requestAnimationFrame(() => {
            setEntered(true);
        });

        return () => cancelAnimationFrame(id);
    }, []);

    const show = visible && entered;

    return (
        <Link
            href={href}
            className={`
                bg-secondary shadow-lg rounded-xl px-4 py-3 border min-w-40 max-w-90
                hover:bg-[#bbbbef]
                active:scale-[95%]
                transition-all duration-300
                ${show ? "opacity-100 translate-y-0 scale-100" : "opacity-0 -translate-y-4 scale-95"}
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
