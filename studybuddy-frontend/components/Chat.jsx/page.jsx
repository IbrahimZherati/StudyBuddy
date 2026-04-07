'use client'

import { useChatConnection } from '@/app/hooks/useChatConnection'
import useGetId from '@/app/hooks/useGetId';

export default function Chat({hubUrlSuffix}) {
    const { messages, sendMessage, status } = useChatConnection(hubUrlSuffix);
    const userId = useGetId();
    console.log("User Id", userId);

    return (
        <div>
            <div className='flex flex-col'>
                {messages.map((message, index) => 
                    <div key={index}> 
                        <span>{`${message.sender}: `}</span>
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
