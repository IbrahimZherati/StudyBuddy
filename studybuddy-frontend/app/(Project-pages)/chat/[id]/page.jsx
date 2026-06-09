'use client'

import useGetOtherUserInfo from '@/app/hooks/useGetOtherUserInfo';
import Chat from '@/components/Chat/ChatPage/page'
import { defaultProfilePhotoPath } from '@/utils/fileHandling';
import { use } from 'react'

export default function PrivateChat({params}) {
    const { id } = use(params); 
    const friendInfo = useGetOtherUserInfo(id);
    const friendName = friendInfo?.userName;
    const friendProfilePhoto = friendInfo?.photo;
    
    return (
        <Chat hubUrlSuffix="PrivateChatHub" to={id} chatTitle={friendName} 
            chatPhoto={friendProfilePhoto} defaultChatPhoto={defaultProfilePhotoPath}/>
    )
}
