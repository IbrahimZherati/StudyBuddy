'use client'

import useGetOtherUserInfo from '@/app/hooks/useGetOtherUserInfo';
import Chat from '@/components/Chat/ChatPage/page'
import { use } from 'react'

export default function PrivateChat({params}) {
    const { id } = use(params); 
    const friendInfo = useGetOtherUserInfo(id);
    console.log(friendInfo);
    const friendName = friendInfo?.userName;
    
    return (
        <Chat hubUrlSuffix="PrivateChatHub" to={id} chatTitle={"Name"}/>
    )
}
