'use client'

import { useChatConnection } from '@/app/hooks/useChatConnection'
import useGetId from '@/app/hooks/useGetId';

export default function Chat({hubUrlSuffix, to}) {
    const { messages, sendMessage, status } = useChatConnection(hubUrlSuffix);
    const id = useGetId();
    console.log("User Id", id);

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
            
            <button onClick={() => sendMessage(Number(id), Number(to), "Hi")}
                    className='btn'>
                Send
            </button>
        </div>
    )
}
