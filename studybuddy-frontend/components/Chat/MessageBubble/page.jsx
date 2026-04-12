import React from 'react'

export default function MessageBubble({senderName, text, createTime, fromMe}) {
    return (
        <div className={`flex flex-col text-tertiary
                py-1 px-3 rounded-2xl w-fit max-w-[70%] min-w-20
                ${fromMe? 'bg-primary self-end': 'bg-secondary self-start'}`}>
            {!fromMe && <span className='font-bold -mb-1'>{senderName}</span>}
            <span>{text}</span>
            <span className='text-xs text-gray-200 self-end'>{createTime.substring(11, 16)}</span>
        </div>
    )
}
