'use Client'

import Chat from '@/components/Chat/ChatPage/page'
import { use } from 'react'

export default function PrivateChat({params}) {
    const { id } = use(params); 
    return (
        <Chat hubUrlSuffix="PrivateChatHub" to={id}/>
    )
}
