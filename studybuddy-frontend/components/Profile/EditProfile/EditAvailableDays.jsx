'use client';

import AvailableDays from "../AvailableDays";

export default function EditAvailableDays({ name, value, dayOptions, handleChange, handleFocus }) {
    const toggleDay = (dayId) => {
        handleFocus();
        
        const updated = value.includes(dayId)
            ? value.filter(d => d !== dayId)
            : [...value, dayId];

        handleChange({
            target: {
                name,
                value: updated
            }
        });
    };

    return (
        <AvailableDays 
            dayOptions={dayOptions}
            value={value}
            sizeClass="text-xl"
            toggleDay={toggleDay}
        />
    );
}