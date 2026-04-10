'use client'

import { useChatConnection } from '@/app/hooks/useChatConnection'
import useGetId from '@/app/hooks/useGetId';
import { useState } from 'react';

export default function Chat({hubUrlSuffix, to}) {
    const { messages, sendMessage, status } = useChatConnection(hubUrlSuffix);
    const [text, setText] = useState("");
    const handleChange = (e) => {
        setText(e.target.value);
    }

    const id = useGetId();
    const handleSend = () => {
        sendMessage(Number(id), Number(to), text);
        setText("");
    };

    return (
        <div className='flex-col-center h-full gap-6'>
            <div className='flex flex-col w-full h-full'>
                <div className=''>
                    {messages.map((message, index) => 
                        <div key={index}> 
                            <span className='font-bold'>{`${message.sender}: `}</span>
                            <span>{message.text}</span>
                        </div>
                    )}
                </div>
            </div>

            <div className='grid grid-cols-[1fr_80px] gap-6 w-full mt-auto'>
                <input className='border-2 block p-2 rounded-xl bg-tertiary' 
                    value={text}
                    placeholder='Type your message here...'
                    onChange={handleChange}
                    onKeyDown={(e) => {
                        if(e.key === 'Enter') {
                            handleSend();
                        }
                    }}
                />
            
                <button id="send"
                        onClick={handleSend}
                        className='btn place-self-center w-20'>
                    Send
                </button>
            </div>
        </div>
    )
}
