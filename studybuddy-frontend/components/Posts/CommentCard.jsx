import React, { useState } from 'react'
import PhotoDisplay from '../PhotoDisplay';
import { defaultProfilePhotoPath, fileFromBase64 } from '@/utils/fileHandling';
import { Heart } from 'lucide-react';
import put from '@/utils/API/put';
import Link from 'next/link';

export default function CommentCard({comment}) {
    const [isCommentLiked, setIsCommentLiked] = useState(comment.isLiked);

    const putRequest = async (url, param) => {
        await put(null, url, param);
    }

    const onLikeClick = async () => {
        if(isCommentLiked) {
            setIsCommentLiked(false);
            putRequest("PostReply/Unlike", {
                key: "Id",
                value: comment.id
            });
        }
        else {
            setIsCommentLiked(true);
            putRequest("PostReply/Like", {
                key: "Id",
                value: comment.id
            });
        }
    }

    return (
        <div key={comment.id}
            className="bg-white rounded-2xl p-5 border border-gray-100 shadow-sm flex justify-between gap-4"
        >

            <div className="flex gap-4">
                <Link
                    href={`/profile/${comment.clientUserId}`}
                >
                    <PhotoDisplay
                        photo={fileFromBase64(comment.clientPhoto, defaultProfilePhotoPath)}
                        sizeClass="w-14 h-14 shrink-0"
                        alt={comment.clientUserName}
                    />
                </Link>

                <div className='flex flex-col gap-1'>
                    <Link
                        href={`/profile/${comment.clientUserId}`}
                    >
                        <h5 className="font-bold text-gray-900 text-md inline-block mr-2">
                            {comment.clientUserName}
                        </h5>
                    </Link>

                    <p className="text-gray-700 text-sm leading-relaxed">
                        {comment.text}
                    </p>
                </div>
            </div>

            <button
                onClick={onLikeClick}
                className={`flex gap-1.5 items-center text-xs self-center whitespace-nowrap transition-colors 
                            ${isCommentLiked ? 'text-red-500 font-bold' : 'text-gray-400 hover:text-red-500'
                }`}
            >
                <Heart className={`w-4 h-4 ${isCommentLiked ? 'fill-red-500 text-red-500' : 'text-red-300'}`} />
                <span>{comment.likes}</span>
            </button>
        </div>
    );
}
