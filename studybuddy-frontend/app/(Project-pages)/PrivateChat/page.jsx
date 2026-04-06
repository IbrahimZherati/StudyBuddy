'use Client'

import Chat from '@/components/Chat.jsx/page'
import React from 'react'

export default function PrivateChat() {
    return (
        <Chat hubUrlSuffix="PrivateChatHub"/>
    )
}
