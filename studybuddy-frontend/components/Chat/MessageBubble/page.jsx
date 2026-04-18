import React from 'react'

export default function MessageBubble({senderName, text, createTime, fromMe}) {
    return (
        <div className={`flex flex-col text-tertiary
                py-1 px-3 rounded-2xl w-fit max-w-[40%] min-w-20
                ${fromMe? 'bg-[#2645D9] self-end': 'bg-[#000E4D] self-start'}`}
        >
            {!fromMe && <span className='md:text-[1.15rem] font-bold -mb-1'>{senderName}</span>}

            <span className='md:text-[1.1rem] wrap-anywhere'>{text}</span>

            <span className='text-xs text-[#cad2f1] self-end'>{createTime.substring(11, 16)}</span>
        </div>
    )
}
