import React from 'react'

export default function DateAndTime({ dateAndTime }) {
    const displayDate = dateAndTime?.substring(0, 10);
    const displayTime = dateAndTime?.substring(11, 16);

    return (
        <div className="flex-col-center">
            <span className="text-md text-gray-600 font-medium whitespace-nowrap w-20 text-center">
                {displayTime}
            </span>

            <span className="text-xs text-gray-600 font-medium whitespace-nowrap w-20 text-center">
                {displayDate}
            </span>

        </div>
    )
}
