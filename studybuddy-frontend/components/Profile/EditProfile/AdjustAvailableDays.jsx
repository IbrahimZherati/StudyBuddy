'use client';

export default function AvailableDays({ value = [], onChange, dayOptions = [] }) {
    const toggleDay = (dayId) => {
        const updated = value.includes(dayId)
            ? value.filter(d => d !== dayId)
            : [...value, dayId];

        onChange(updated);
    };

    return (
        <div className="flex flex-col gap-2">
            <h3 className="text-xl font-bold">Available Days</h3>

            <div className="flex flex-wrap gap-3">
                {dayOptions.map(day => (
                    <span
                        key={day.id}
                        onClick={() => toggleDay(day.id)}
                        className={`px-4 py-1 rounded-full cursor-pointer
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
    );
}