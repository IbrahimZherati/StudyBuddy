'use client';
import React, { useState } from 'react';
import { useRouter } from 'next/navigation';
import { Plus } from 'lucide-react';
import PostCard from '@/components/Posts/PostCard';

export default function Posts() {
    const router = useRouter();
    const [posts, setPosts] = useState([
        {
            id: 1,
            author: 'Ahmad',
            title: 'Why C++ is Still the King?',
            content: 'Behind the sleek user interfaces, C++ is the heavy lifter...',
            likes: 3600,
            isLiked: false, 
            commentsCount: 145,
            shares: 189,
            userImage: ''
        },
        {
            id: 2,
            author: 'Rola',
            title: 'The Marvel of Divine Engineering',
            content: 'Anatomy is far more than just memorizing the names...',
            likes: 4567,
            isLiked: true, 
            commentsCount: 678,
            shares: 35,
            userImage: ''
        },
    ]);

    const handleLike = (postId) => {
        setPosts(prevPosts => 
            prevPosts.map(post => {
                if (post.id === postId) {
                    return {
                        ...post,
                        isLiked: !post.isLiked,
                        likes: post.isLiked ? post.likes - 1 : post.likes + 1 
                    };
                }
                return post;
            })
        );
    };

    const handlePostClick = (post) => {
        router.push(`/posts/${post.id}`);
    };

    return (
        <div className="w-full min-h-screen bg-[#f4f6fa] p-6 relative">
            <div className="max-w-4xl mx-auto space-y-4">
                {posts.map((post) => (
                    <PostCard 
                        key={post.id} 
                        post={post} 
                        isDetailView={false} 
                        onPostClick={handlePostClick} 
                        onLikeClick={handleLike}
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