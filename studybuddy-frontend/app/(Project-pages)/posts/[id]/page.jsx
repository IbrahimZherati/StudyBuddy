'use client';
import React, { useState, use, useEffect } from 'react';
import { useRouter } from 'next/navigation';
import { ArrowLeft, Send } from 'lucide-react';
import PostCard from '@/components/Posts/PostCard';
import CommentCard from '@/components/Posts/CommentCard';
import Loading from '@/components/Loading';
import get from '@/utils/API/get';
import useLazyContainter from '@/app/hooks/useLazyContainer';
import postReq from "@/utils/API/post"

export default function PostDetails({ params }) {
    const router = useRouter();
    const { id } = use(params); 

    const [post, setPost] = useState();

    useEffect(() => {
        if(!id)
            return;

        const fetchPost = async () => {
            const data = await get(null, `Post/${id}`);
            setPost(data.value);
        }
        fetchPost();

    }, [id]);

    const loadFactor = 20;
    const [comments, containerRef, onScroll] = useLazyContainter(`Post/${id}/Replies`, loadFactor);

    const [comment, setComment] = useState("");

    const onChange = (e) => {
        setComment(e.target.value);
    }

    const onSubmit = async () => {
        if(!comment)
            return;

        const result = await postReq({
            postId: id,
            text: comment
        }, "PostReply");

        console.log(result);
        if(result?.isSuccess)
            setComment("");
    }

    if(!post)
        return <Loading />

    return (
        <div className="w-full min-h-screen bg-[#f4f6fa] p-6 pb-24 relative">
            <div className="max-w-4xl mx-auto">
        
                <div className="flex justify-end mb-2">
                    <button 
                        onClick={() => router.push("/posts")}
                        className="p-1 rounded-full border border-gray-300 bg-white hover:bg-gray-50 text-gray-700 transition-colors"
                    >
                        <ArrowLeft className="w-7 h-7 rotate-180" />
                    </button>
                </div>

                <PostCard 
                    post={post} 
                    isDetailView={true} 
                />

                <div className="mt-6">
                    <h3 className="text-xl font-bold text-gray-900 mb-4">
                        Comments
                    </h3>
                    
                    <div 
                        className="space-y-3"
                        ref={containerRef}
                        onScroll={onScroll}
                    >
                        {comments.map((comment) => 
                            <CommentCard 
                                key={comment.id}
                                comment={comment}
                            />
                        )}
                    </div>
                </div>
            </div>

            <form 
                onSubmit={onSubmit}
                className="fixed bottom-0 left-0 right-0 md:left-58 p-4 bg-[#f4f6fa]/90"
            >
                <div className="max-w-4xl mx-auto relative flex items-center">
                    <input
                        type="text"
                        value={comment}
                        onChange={onChange}
                        placeholder="Type your comment"
                        className="w-full bg-secondary placeholder-gray-100 text-black 
                                    pl-5 pr-24 py-3.5 rounded-xl focus:outline-none text-md 
                                    font-medium shadow-sm"
                    />     

                    <div className="absolute right-4 flex items-center gap-4 text-black">
                        <button 
                            type="submit"
                            className="active:scale-95"
                        >
                            <Send className="w-5 h-5" />
                        </button>
                    </div>
                </div>  
            </form>
        </div>  
    );
}