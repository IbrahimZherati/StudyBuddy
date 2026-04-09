import React from 'react'

export default function AvailableDays() {
    const days = [
        { day: "Sun", active: false },
        { day: "Mon", active: true },
        { day: "Tue", active: false },
        { day: "Wed", active: false },
        { day: "Thu", active: false },
        { day: "Fri", active: false },
        { day: "Sat", active: true },
    ]

    return (
        <div className='flex flex-col gap-2'>
		    <h3 className="text-xl font-bold">
				Available Days
			</h3>

			<div className='flex flex-wrap gap-3'>
				{days.map((day, index) => (
					<span key={index}
						className={`px-4 py-1 rounded-full text-sm transition 
                        ${day.active
							? "bg-secondary"
							: "bg-tertiary "
						}`}
					>
						{day.day}
					</span>
				))}
			</div>

		</div>
    )
}
