import React from 'react';
import { Plus } from 'lucide-react';
import PostCard from './PostCard';

export default function PostFeed({ onPostClick }) {

    const posts = [
        {
            id: 1,
            author: 'Ahmad',
            title: 'Why C++ is Still the King of Programming Languages?',
            content: 'If you think C++ is a relic of the past, think again! Behind the sleek user interfaces of the apps we use daily, C++ is the heavy lifter running the show behind the scenes.',
            likes: '3.6K',
            commentsCount: 145,
            shares: 189,
            userImage: ''
        },
        {
            id: 2,
            author: 'Rola',
            title: 'The Marvel of Divine Engineering Inside Your Body',
            content: 'Anatomy is far more than just memorizing the names of bones and muscles. It is a deep meditation on the incredibly precise human design that works flawlessly around the clock without a single pause.',
            likes: '4567',
            commentsCount: 678,
            shares: 35,
            userImage: ''
        },
        {
            id: 3,
            author: 'Roze',
            title: 'Study Smarter, Not Harder!',
            content: 'Do you spend endless hours staring at books only to feel like you’ve forgotten everything the next day? The issue isn’t your brain capacity; it’s your method.',
            likes: '567',
            commentsCount: 23,
            shares: 266,
            userImage: ''
        }
    ];

    return (
        <div className="w-full min-h-screen bg-[#f4f6fa] p-6 relative">
            <div className="max-w-4xl mx-auto space-y-4">
                {posts.map((post) => (
                    <div  key={post.id}>
                        <PostCard 
                            post={post} 
                            isDetailView={false}
                            onCommentClick={onPostClick}
                        />
                    </div>
                ))}
            </div>

            <button className="fixed bottom-10 right-10 w-14 h-14 bg-[#b4c3ff] text-[#1e293b] rounded-full flex items-center justify-center shadow-lg hover:bg-[#a1b2fa] transition-colors">
                <Plus className="w-6 h-6" />
            </button>
        </div>
    );
}