'use client';
import React, { useState, use } from 'react';
import { useRouter } from 'next/navigation';
import { ArrowLeft, Smile, Send, Heart } from 'lucide-react';
import PostCard from '@/components/Posts/PostCard';
import { defaultProfilePhotoPath } from '@/utils/fileHandling';
import PhotoDisplay from '@/components/PhotoDisplay';

export default function PostDetails({ params }) {
    const router = useRouter();
    const { id } = use(params); 
 
    const [post, setPost] = useState({
        id: Number(id),
        author: id === '2' ? 'Rola' : 'Ahmad',
        title: id === '2' ? 'The Marvel of Divine Engineering' : 'Why C++ is Still the King?',
        content: id === '2' 
            ? 'Anatomy is far more than just memorizing the names of bones and muscles...'
            : 'Ever feel like you’re staring at your books for hours but nothing is sticking? You’re not alone!',
        likes: 4567,
        isLiked: false,
        commentsCount: 2,
        shares: 35,
        userImage: ''
    });

    const [comments, setComments] = useState([
        {
            id: 1,
            author: 'Sara',
            content: 'Great summary! The Pomodoro Technique is an absolute game-changer for my daily workflow. Thanks for sharing these valuable insights!',
            likes: 1000,
            isLiked: false, 
            userImage: ''
        },
        {
            id: 2,
            author: 'Ali',
            content: 'Spot on! Studying smarter is the key to continuous learning in any career. High-quality post!',
            likes: 500,
            isLiked: false,
            userImage: ''
        }
    ]);

    const handleLike = () => {
        setPost(prev => ({
            ...prev,
            isLiked: !prev.isLiked,
            likes: prev.isLiked ? prev.likes - 1 : prev.likes + 1
        }));
    };

    const handleCommentLike = (commentId) => {
        setComments(prevComments =>
            prevComments.map(comment => {
                if (comment.id === commentId) {
                    return {
                        ...comment,
                        isLiked: !comment.isLiked,
                        likes: comment.isLiked ? comment.likes - 1 : comment.likes + 1
                    };
                }
                return comment;
            })
        );
    };

    return (
        <div className="w-full min-h-screen bg-[#f4f6fa] p-6 pb-24 relative">
            <div className="max-w-4xl mx-auto">
        
                <div className="flex justify-end mb-2">
                    <button 
                        onClick={() => router.back()}
                        className="p-1 rounded-full border border-gray-300 bg-white hover:bg-gray-50 text-gray-700 transition-colors"
                    >
                        <ArrowLeft className="w-7 h-7 rotate-180" />
                    </button>
                </div>

                <PostCard post={post} isDetailView={true} onLikeClick={handleLike} />

                <div className="mt-6">
                    <h3 className="text-xl font-bold text-gray-900 mb-4">
                        Comments
                    </h3>
                    
                    <div className="space-y-3">
                        {comments.map((comment) => {
                            const isCommentLiked = comment.isLiked;
                            return (
                                <div key={comment.id}
                                    className="bg-white rounded-2xl p-5 border border-gray-100 shadow-sm flex justify-between gap-4"
                                >

                                    <div className="flex gap-4">
                                        <PhotoDisplay
                                            photo={comment.userImage || defaultProfilePhotoPath}
                                            sizeClass="w-14 h-14 shrink-0"
                                            alt={comment.author}
                                        />
                    
                                        <div className='flex flex-col gap-1'>
                                            <h5 className="font-bold text-gray-900 text-md inline-block mr-2">
                                                {comment.author}
                                            </h5>

                                            <p className="text-gray-700 text-sm leading-relaxed">
                                                {comment.content}
                                            </p>
                                        </div>
                                    </div>

                                    <button 
                                        onClick={() => handleCommentLike(comment.id)}
                                        className={`flex gap-1.5 items-center text-xs self-center whitespace-nowrap transition-colors ${
                                            isCommentLiked ? 'text-red-500 font-bold' : 'text-gray-400 hover:text-red-500'
                                        }`}
                                    >
                                        <Heart className={`w-4 h-4 ${isCommentLiked ? 'fill-red-500 text-red-500' : 'text-red-300'}`} />
                                        <span>{comment.likes}</span>
                                    </button>
                                </div>
                            );
                        })}
                    </div>
                </div>
            </div>

            <div className="fixed bottom-0 left-0 right-0 md:left-60 p-4 bg-[#f4f6fa]/90">
                <div className="max-w-4xl mx-auto relative flex items-center">
                    <input
                        type="text"
                        placeholder="Type your comment"
                        className="w-full bg-secondary placeholder-gray-100 text-black pl-5 pr-24 py-3.5 rounded-xl focus:outline-none text-md font-medium shadow-sm"
                    />     

                    <div className="absolute right-4 flex items-center gap-4 text-black">
                        <button>
                            <Smile className="w-5 h-5" />
                        </button>

                        <button className=" active:scale-95">
                            <Send className="w-5 h-5" />
                        </button>
                    </div>
                </div>  
            </div>
        </div>  
    );
}