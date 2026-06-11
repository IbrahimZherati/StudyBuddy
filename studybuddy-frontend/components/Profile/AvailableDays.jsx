import React from 'react'

export default function AvailableDays({ dayOptions, value, sizeClass, toggleDay}) {
    return (
        <div className="flex flex-col gap-2">
            <h3 className={`${sizeClass} font-bold`}>Available Days</h3>

            <div className="flex flex-wrap gap-3">
                {dayOptions.map(day => (
                    <span
                        key={day.id}
                        onClick={toggleDay? () => toggleDay(day.id): () => {}}
                        className={`px-4 py-1 rounded-full ${toggleDay? "cursor-pointer": ""}
                        ${value.includes(day.id)
                            ? "bg-secondary"
                            : "bg-tertiary"
                        }`}
                    >
                        {day.name}
                    </span>
                ))}
            </div>
        </div>
    )
}
