import React from 'react'
import CardContainer from '../CardContainer';
import ClickableCard from '../ClickableCard';

export default function Reminders({ reminders = [] }) {
    const defaultReminders = [
        { title: "Math Lesson", time: "22-oct-2025" },
        { title: "Math Lesson", time: "22-oct-2025" },
        { title: "Math Lesson", time: "22-oct-2025" },
    ];

    const remindersToRender = reminders.length ? reminders : defaultReminders;

    return (
        <div className="flex flex-col gap-3">
            <h3 className="text-2xl font-bold">
                Reminders
            </h3>

            < div className="flex flex-wrap gap-3">
                {remindersToRender.map((reminder, index) => (
                    <ClickableCard key={`${reminder.title}-${reminder.time}-${index}`} href="#" additionalStyles="bg-secondary hover:bg-secondary/80">
                        <div className="flex flex-col gap-4 mx-auto">
                            <h4 className="font-bold text-lg">
                                {reminder.title}
                            </h4>

                            <p className="text-sm mx-auto">
                                {reminder.time}
                            </p>
                        </div>
                    </ClickableCard>
                ))}
            </div>

        </div>
    )
}

// bg-[#B2C0FF]