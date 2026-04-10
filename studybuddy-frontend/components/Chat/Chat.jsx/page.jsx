'use client'

import { useChatConnection } from '@/app/hooks/useChatConnection'
import useGetId from '@/app/hooks/useGetId';
import { useEffect, useState } from 'react';
import MessageBubble from '../MessageBubble/page';
import useGetUserInfo from '@/app/hooks/useGetUserInfo';
import useGetUserId from '@/app/hooks/useGetUserId';

export default function Chat({hubUrlSuffix, to}) {
    const { messages, sendMessage, status } = useChatConnection(hubUrlSuffix);
    const [text, setText] = useState("");
    const handleChange = (e) => {
        setText(e.target.value);
    }

    const id = useGetId();
    const userId = useGetUserId();
    const myInfo = useGetUserInfo();
    console.log("myinfo:", myInfo);
    if(id && userId)
        console.log(id, userId);

    const handleSend = () => {
        sendMessage(Number(id), Number(to), text);
        setText("");
    };

    return (
        <div className='flex-col-center h-full gap-6'>
            <div className='flex flex-col gap-1 w-full h-full'>
                {messages.map((message, index) => 
                    <MessageBubble 
                        key={index}
                        {...message}
                        fromMe={myInfo?.userName == message.sender}
                    />
                )}
            </div>

            <div className='grid grid-cols-[1fr_80px] gap-6 w-full mt-auto'>
                <input className='border-2 block p-2 rounded-xl bg-tertiary' 
                    value={text}
                    placeholder='Message'
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
