import React from 'react';
import { ArrowLeft, Heart, Smile, Send } from 'lucide-react';
import PostCard from './PostCard';
import { defaultProfilePhotoPath } from '@/utils/fileHandling';
import PhotoDisplay from '../PhotoDisplay';

export default function PostDetails({ post, onBack }) {

    const comments = [
        {
            id: 1,
            author: 'Sara',
            role: 'dentist',
            content: 'Great summary! The Pomodoro Technique is an absolute game-changer for my daily workflow. I’ve been trying to implement Spaced Repetition lately too. Thanks for sharing these valuable insights!',
            likes: '1k',
            userImage: ''
        },
        {
            id: 2,
            author: 'Ali',
            role: 'Engineer',
            content: 'Spot on! Studying smarter is the key to continuous learning in any career. The Feynman Technique is especially powerful for truly mastering complex concepts. High-quality post!',
            likes: '1k',
            userImage: ''
        }
    ];

    const displayPost = post || {
        author: 'Ahmad',
        title: 'Master Your Mind: 5 Proven Study Techniques That Actually Work!',
        content: 'Ever feel like you’re staring at your books for hours but nothing is sticking? You’re not alone! Spending more time studying doesn’t always mean learning more. It’s all about studying smarter, not harder.\n\nHere are 5 scientifically proven study methods to boost your memory and ace your exams:...',
        likes: '1k',
        commentsCount: '2k',
        shares: '500',
        userImage: ''
    };

    return (
        <div className="w-full min-h-screen bg-[#f4f6fa] p-6 pb-24 relative">
            <div className="max-w-4xl mx-auto">
        
                <div className="flex justify-end mb-2">
                    <button 
                        onClick={onBack}
                        className="p-1 rounded-full border border-gray-300 bg-white hover:bg-gray-50 text-gray-700 transition-colors"
                    >
                        <ArrowLeft className="w-5 h-5 rotate-180" />
                    </button>
                </div>

                <PostCard post={displayPost} isDetailView={true} />

                <div className="mt-6">
                    <h3 className="text-xl font-bold text-gray-900 mb-4">
                        Comments
                    </h3>
          
                    <div className="space-y-3">
                        {comments.map((comment) => (
                            <div key={comment.id} className="bg-white rounded-2xl p-5 border border-gray-100 shadow-sm flex justify-between gap-4">
                                
                                <div className="flex gap-4">
                                    <PhotoDisplay
                                        photo={comment.userImage || defaultProfilePhotoPath}
                                        sizeClass="w-14 h-14 shrink-0"
                                        alt={comment.author}
                                    />
                                    
                                    <div>
                                        <div className="mb-1">
                                            <h5 className="font-bold text-gray-900 text-md inline-block mr-2">
                                                {comment.author}
                                            </h5>

                                            <p className="text-sm text-gray-400 capitalize inline-block">
                                                {comment.role}
                                            </p>
                                        </div>

                                        <p className="text-gray-700 text-sm leading-relaxed">
                                            {comment.content}
                                        </p>
                                    </div>
                                </div>

                                <button className="flex  gap-1.5 text-gray-400 hover:text-red-500 text-xs self-center whitespace-nowrap">
                                    <Heart className="w-4 h-4 text-red-300" />
                                    <span>{comment.likes}</span>
                                </button>
                            </div>
                        ))}
                    </div>
                </div>
            </div>

            <div className="fixed bottom-0 left-0 right-0 md:left-60 p-4 bg-[#f4f6fa]/90 ">
                <div className="max-w-4xl mx-auto relative flex items-center">
                    <input
                        type="text"
                        placeholder="Type your comment"
                        className="w-full bg-secondary placeholder-gray-100 text-black pl-5 pr-24 py-3.5 rounded-xl focus:outline-none text-md font-medium shadow-sm"
                    />
    
                    <div className="absolute right-4 flex items-center gap-4 text-black">
                       
                        <button >
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