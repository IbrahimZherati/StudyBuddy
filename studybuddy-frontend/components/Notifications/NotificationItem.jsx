import React from 'react';
import PhotoDisplay from '../PhotoDisplay';
import Link from 'next/link';
import DateAndTime from '../DateAndTime';

export default function NotificationItem({ photo, id, name, description, time, children }) {

    const maxMessageLength = 40;

    const message = description.length <= maxMessageLength? description:
        `${description.substring(0, maxMessageLength)}...`

    return (
        <div className="flex flex-col sm:flex-row sm:items-center justify-between p-4 mb-2 bg-[#dbeafe] rounded-xl transition-all duration-200">
            
            <Link 
                className="flex items-center gap-3 min-w-0 flex-1"
                href={`/profile/${id}`}
            >
                <PhotoDisplay
                    photo={photo}
                    alt={name}
                    sizeClass="w-12 h-12"
                />

                <div className="min-w-0">
                    <h4 className="text-md font-bold text-gray-900 truncate">
                        {name}
                    </h4>

                    <p className="text-sm text-gray-800 line-clamp-2 mt-0.5 truncate">
                        {message}
                    </p>
                </div>
            </Link>

            <div className="flex items-center justify-between sm:justify-end gap-4 mt-3 sm:mt-0 shrink-0">
                <DateAndTime
                    dateAndTime={time}
                />

                <div className="flex items-center gap-2">
                    {children}
                </div>
            </div>
        </div>
    );
}