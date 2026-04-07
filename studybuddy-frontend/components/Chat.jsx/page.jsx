'use client'

import { useChatConnection } from '@/app/hooks/useChatConnection'
import React, { useEffect, useState } from 'react'

export default function Chat({hubUrlSuffix}) {
    const { messages, sendMessage, status } = useChatConnection(hubUrlSuffix);
    const [userId, setUserId] = useState(null);

    useEffect(() => {
        const storedId = localStorage.getItem('id');
        // eslint-disable-next-line react-hooks/set-state-in-effect
        setUserId(storedId);
    }, []);

    console.log(userId);

    return (
        <div>
            <div className='flex flex-col'>
                {messages.map((message, index) => 
                    <div key={index}> 
                        <span>{`${message.sender}:`}</span>
                        <span>{message.text}</span>
                    </div>
                )}
            </div>
            
            <button onClick={() => sendMessage(Number(userId), Number(userId), "Hi")}
                    className='btn'>
                Send
            </button>
        </div>
    )
}
