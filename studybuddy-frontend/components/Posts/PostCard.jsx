import React from 'react';
import { Heart, MessageSquare, Share2 } from 'lucide-react';
import { defaultProfilePhotoPath } from '@/utils/fileHandling';
import PhotoDisplay from '../PhotoDisplay';

export default function PostCard({ post, isDetailView = false, onCommentClick }) {
    return (
        <div className="bg-white rounded-2xl p-6 shadow-sm border border-gray-100 mb-4 relative">

            <div className="flex items-center gap-3 mb-4">
                <PhotoDisplay
                    photo={post.userImage || defaultProfilePhotoPath}
                    sizeClass="w-12 h-12"
                    alt={post.author}
                />

                <div>
                    <h4 className="font-bold text-gray-900 text-md">
                        {post.author}
                    </h4>

                    {post.role && 
                        <p className="text-sm text-gray-500">
                            {post.role}
                        </p>
                    }
                </div>
            </div>

            <div className="mb-6">
                <h3 className="font-bold text-gray-900 text-base mb-2 flex items-center gap-1">
                    {post.title}
                </h3>
        
                <p className="text-gray-700 text-sm leading-relaxed">
                    {post.content}
                </p>
        
                {!isDetailView && 
                    <span className="text-gray-400 text-xs block mt-1">...</span>
                }
            </div>

            <div className="flex items-center gap-6 text-gray-500 text-xs">
                <button className="flex items-center gap-2 hover:text-red-500 transition-colors">
                    <Heart className="w-4 h-4 text-red-400" />
                    <span>{post.likes}</span>
                </button>
        
                <button 
                    className="flex items-center gap-2 hover:text-blue-500 transition-colors"
                    onClick={(e) => {
                        if (!isDetailView && onCommentClick) {
                            e.stopPropagation(); // يمنع تداخل الضغطات في المتصفح
                            onCommentClick(post);
                        }
                    }}
                >
                    <MessageSquare className="w-4 h-4" />
                    <span>{post.commentsCount}</span>
                </button>
        
                <button className="flex items-center gap-2 hover:text-green-500 transition-colors">
                    <Share2 className="w-4 h-4" />
                    <span>{post.shares}</span>
                </button>
            </div>
        </div>
    );
}