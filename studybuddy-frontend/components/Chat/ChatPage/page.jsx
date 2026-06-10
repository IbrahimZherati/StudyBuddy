'use client'

import { useChatConnection } from '@/app/hooks/useChatConnection'
import useGetId from '@/app/hooks/useGetId';
import { useEffect, useState, useRef } from 'react';
import MessageBubble from '../MessageBubble/page';
import Loading from '@/components/Loading';
import { fileFromBase64 } from '@/utils/fileHandling';
import PhotoDisplay from '@/components/PhotoDisplay';

export default function Chat({hubUrlSuffix, to, chatTitle, chatPhoto, defaultChatPhoto}) {
    const { messages, sendMessage, status, loadMessages } = useChatConnection(hubUrlSuffix);
    const [text, setText] = useState("");

    const canSend = status === "connected" && text.trim() !== "";

    const skipRef = useRef(0);

    const id = useGetId();

    const loadFactor = 15;

    useEffect(() => {
        if (!id || !to) return;

        const init = async () => {
            await loadMessages(to, 0, loadFactor);
            skipRef.current = loadFactor;

            requestAnimationFrame(() => {
                const el = containerRef.current;
                if (el) {
                    el.scrollTop = el.scrollHeight;
                }
            });
        };
        init();

    }, [id, to, loadMessages]);

    const containerRef = useRef(null);
    const loadingMoreRef = useRef(false);

    // To be used later to display a spinning icon while loading messages
    const [loadingMore, setLoadingMore] = useState(false);

    const handleLoadMore = async () => {
        if (loadingMoreRef.current) return;

        setLoadingMore(true);
        loadingMoreRef.current = true;

        await loadMessages(to, skipRef.current, loadFactor);
        skipRef.current += loadFactor;

        setLoadingMore(false);
        loadingMoreRef.current = false;
    };

    const handleScroll = () => {
        const el = containerRef.current;
        if (!el) return;

        if (el.scrollTop < 100) {
            handleLoadMore();
        }
    };

    const handleChange = (e) => {
        setText(e.target.value);
    }

    const handleSend = () => {
        if(!canSend)
            return;

        sendMessage(Number(to), text);
        setText("");
    };

    const lastMessage = messages.length > 0? messages[messages.length - 1]: null;

    useEffect(() => {
        const el = containerRef.current;
        if (!el) return;

        if(lastMessage?.senderId == id || el.scrollTop > el.scrollHeight - 800) {
            requestAnimationFrame(() => {
                const el = containerRef.current;
                if (el) {
                    el.scrollTop = el.scrollHeight;
                }
            });
        }
    }, [lastMessage, id]);

    const photo = fileFromBase64(chatPhoto, defaultChatPhoto);

    if (!id || !chatTitle)
        return <Loading />;

    return (
        <div className='flex flex-col h-full min-h-0'>
            <div className='flex items-center gap-2 h-16 px-8 border-b border-b-gray-200 bg-gray-50'>
                <PhotoDisplay
                    photo={photo}
                    size={12}
                    alt={chatTitle}
                />

                <span className='text-[1.6rem] font-bold'>{chatTitle}</span>
            </div>

            <div 
                className='flex flex-col gap-1 w-full flex-1 min-h-0 p-6 overflow-y-auto no-scrollbar'
                ref={containerRef}
                onScroll={handleScroll}
            >
                {messages.map((message) =>
                    <MessageBubble
                        key={message.id}
                        {...message}
                        fromMe={id == message.senderId}
                    />
                )}
            </div>

            <div className='grid grid-cols-[1fr_80px] gap-6 p-6 w-full shrink-0 border-t
                             border-t-gray-200 bg-gray-50'>
                <input className='border-2 block p-2 rounded-xl bg-tertiary'
                    value={text}
                    placeholder='Message'
                    onChange={handleChange}
                    onKeyDown={(e) => {
                        if (e.key === 'Enter') {
                            handleSend();
                        }
                    }}
                />

                <button id="send"
                    onClick={handleSend}
                    className={`btn place-self-center w-20 ${!canSend? "disabled": ""}`}>
                    Send
                </button>
            </div>
        </div>
    )
}
