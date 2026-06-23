'use client';
import React from 'react';
import { useRouter } from 'next/navigation';
import { Plus } from 'lucide-react';
import PostCard from '@/components/Posts/PostCard';
import useLazyContainter from '@/app/hooks/useLazyContainer';

export default function Posts() {
    const router = useRouter();
    
    const loadFactor = 20;
    const [posts, containterRef, handleScroll] = useLazyContainter("Feed", loadFactor);

    return (
        <div className="flex flex-col w-full h-full min-h-0 bg-[#f4f6fa] p-6 relative">
            <div 
                className="max-w-4xl mx-auto space-y-4 overflow-y-auto no-scrollbar"
                ref={containterRef}
                onScroll={handleScroll}
            >
                {posts.map((post) => (
                    <PostCard 
                        key={post.id} 
                        post={post} 
                        isDetailView={false} 
                    />
                ))}
            </div>

            <button onClick={() => router.push('/posts/new')} 
                className="fixed bottom-10 right-10 w-14 h-14 bg-[#b4c3ff] rounded-full flex items-center justify-center shadow-lg">
                <Plus className="w-6 h-6" />
            </button>
        </div>
    );
}