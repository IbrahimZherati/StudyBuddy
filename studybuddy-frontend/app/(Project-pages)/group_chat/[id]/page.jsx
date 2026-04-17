'use client'

import useGetGroupInfo from '@/app/hooks/useGetGroupInfo';
import Chat from '@/components/Chat/ChatPage/page'
import { use } from 'react'

export default function GroupChat({params}) {
    const { id } = use(params); 
    const groupInfo = useGetGroupInfo();
    
    return (
        <Chat hubUrlSuffix="GroupChatHub" to={id} chatTitle={groupInfo?.name}/>
    )
}
