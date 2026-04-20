import React from 'react'
import CardContainer from '../CardContainer';
import ClickableCard from '../ClickableCard';

export default function ImportantReminders({ reminders = [] }) {
    const defaultReminders = [
        { title: "Math Lesson", time: "22-oct-2025" },
        { title: "Math Lesson", time: "22-oct-2025" },
        { title: "Math Lesson", time: "22-oct-2025" },
    ];

    const remindersToRender = reminders.length ? reminders : defaultReminders;

    return (
        <div className="flex flex-col gap-2">
            <h3 className="text-xl font-bold">
                Important Reminders
            </h3>

            <CardContainer>
                {remindersToRender.map((reminder, index) => (
                    <ClickableCard key={`${reminder.title}-${reminder.time}-${index}`} href="#">
                        <div className="flex flex-col gap-4">
                            <h4 className="font-bold">
                                {reminder.title}
                            </h4>

                            <p className="text-sm">
                                {reminder.time}
                            </p>
                        </div>
                    </ClickableCard>
                ))}
            </CardContainer>

        </div>
    )
}

// bg-[#B2C0FF]