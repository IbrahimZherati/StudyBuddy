'use client'

import useGetUserInfo from '@/app/hooks/useGetUserInfo';
import Chat from '@/components/Chat/ChatPage/page'
import { use } from 'react'

export default function PrivateChat({params}) {
    const { id } = use(params); 
    const userInfo = useGetUserInfo();
    
    return (
        <Chat hubUrlSuffix="PrivateChatHub" to={id} chatTitle={userInfo?.userName}/>
    )
}
