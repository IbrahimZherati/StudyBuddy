import React from 'react';
import { Heart, MessageSquare, Share2 } from 'lucide-react';
import { defaultProfilePhotoPath } from '@/utils/fileHandling';
import PhotoDisplay from '../PhotoDisplay';

export default function PostCard({ post, isDetailView = false, onPostClick, onLikeClick, onShareClick }) {
    
    const liked = post.isLiked;
    
    return (
        <div className="bg-white rounded-2xl p-6 border border-gray-100 mb-4 shadow-sm overflow-hidden">
            <div 
                onClick={() => {
                    if (!isDetailView && onPostClick) onPostClick(post);
                }}
                className={`transition-all duration-150 ${
                    !isDetailView ? 'cursor-pointer active:scale-[0.99] hover:opacity-95' : ''
                }`}
            >
                <div className="flex items-center gap-3 mb-4">
                    <PhotoDisplay
                        photo={post.userImage || defaultProfilePhotoPath}
                        sizeClass="w-12 h-12"
                        alt={post.author}
                    />

                    <h4 className="font-bold text-gray-900 text-md">
                        {post.author}
                    </h4>
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
            </div>

            <div className="flex items-center gap-6 text-gray-500 text-xs">

                <button 
                    onClick={(e) => {
                        e.stopPropagation();
                        if (onLikeClick) onLikeClick(post.id);
                    }}
                    className={`flex items-center gap-2 transition-transform active:scale-90
                            ${liked ? 'text-red-500' : 'hover:text-red-500'}
                        `}
                >
                    <Heart className={`w-4 h-4 ${liked ? 'fill-red-500 text-red-500' : 'text-gray-400'}`} />
                    
                    <span className={liked ? 'font-bold text-red-500' : ''}>
                        {post.likes}
                    </span>
                </button>
        
                <button  
                    onClick={() => {
                        if (!isDetailView && onPostClick) onPostClick(post);
                    }} 
                    className="flex items-center gap-2 hover:text-blue-500 transition-transform active:scale-90"
                >
                    <MessageSquare className="w-4 h-4" />
                    <span>{post.commentsCount}</span>
                </button>
        
                <button 
                    onClick={(e) => {
                        e.stopPropagation();
                        if (onShareClick) onShareClick(post.id);
                    }}
                    className="flex items-center gap-2 hover:text-green-500 transition-transform active:scale-90"
                >
                    <Share2 className="w-4 h-4" />
                    <span>{post.shares}</span>
                </button>
            </div>
        </div>
    );
}