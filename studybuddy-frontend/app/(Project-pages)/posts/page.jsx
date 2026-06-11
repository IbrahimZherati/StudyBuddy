'use client';
import React, { useState } from 'react';
import PostFeed from '@/components/Posts/PostFeed';
import PostDetails from '@/components/Posts/PostDetails';

export default function Posts() {
    const [selectedPost, setSelectedPost] = useState(null);
    const [view, setView] = useState('feed'); 

    const handlePostClick = (post) => {
        setSelectedPost(post);
        setView('details');
    };

    return (
        <div className="w-full">
            {view === 'feed' ? (
                <PostFeed onPostClick={handlePostClick} />
            ) : (
                <PostDetails post={selectedPost} onBack={() => setView('feed')} />
            )}
        </div>
    );
}