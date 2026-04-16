'use client'

import { useChatConnection } from '@/app/hooks/useChatConnection'
import useGetId from '@/app/hooks/useGetId';
import { useEffect, useState, useRef } from 'react';
import MessageBubble from '../MessageBubble/page';
import Loading from '@/components/Loading/page';

export default function Chat({ hubUrlSuffix, to }) {
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
        console.log("Scroll happened");

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
        if(lastMessage?.senderId == id) {
            requestAnimationFrame(() => {
                const el = containerRef.current;
                if (el) {
                    el.scrollTop = el.scrollHeight;
                }
            });
        }
    }, [lastMessage, id]);

    if (!id)
        return <Loading />;

    return (
        <div className='flex flex-col h-full min-h-0 gap-6'>
            <div 
                className='flex flex-col gap-1 w-full flex-1 min-h-0 overflow-y-auto no-scrollbar'
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

            <div className='grid grid-cols-[1fr_80px] gap-6 w-full shrink-0'>
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
