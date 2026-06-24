'use client';
import React from 'react';
import PostsList from '@/components/Posts/PostsList';

export default function Posts() {
    return (
        <PostsList 
            url="Post/GetMyPosts"
        />
    );
}