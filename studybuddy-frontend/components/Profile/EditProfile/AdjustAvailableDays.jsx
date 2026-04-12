'use client';

import { useState } from "react";

export default function AvailableDays({ value = [], onChange }) {
    const daysList = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
    const [selected, setSelected] = useState(value);

    const toggleDay = (day) => {
        let updated;

        if (selected.includes(day)) {
            updated = selected.filter(d => d !== day);
        } else {
            updated = [...selected, day];
        }

        setSelected(updated);
        onChange(updated);
    };

    return (
        <div className="flex flex-col gap-2">
            <h3 className="text-xl font-bold">Available Days</h3>

            <div className="flex flex-wrap gap-3">
                {daysList.map(day => (
                    <span
                        key={day}
                        onClick={() => toggleDay(day)}
                        className={`px-4 py-1 rounded-full cursor-pointer
                        ${selected.includes(day)
                            ? "bg-secondary"
                            : "bg-tertiary"
                        }`}
                    >
                        {day}
                    </span>
                ))}
            </div>
        </div>
    );
}