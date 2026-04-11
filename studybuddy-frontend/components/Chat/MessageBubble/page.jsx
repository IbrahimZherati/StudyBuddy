import React from 'react'

export default function MessageBubble({sender, text, time, fromMe}) {
    return (
        <div className={`flex flex-col text-tertiary
                py-1 px-3 rounded-2xl w-fit max-w-[70%]
                ${fromMe? 'bg-primary self-end': 'bg-secondary self-start'}`}>
            <span className='font-bold'>{sender}</span>
            <span>{text}</span>
            <span>{time}</span>
        </div>
    )
}
