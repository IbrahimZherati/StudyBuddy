'use client';
import React, { useState } from 'react';
import { useRouter } from 'next/navigation';
import { Plus, X } from 'lucide-react';

export default function CreatePost() {
    const router = useRouter();

    const [title, setTitle] = useState('');
    const [content, setContent] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault(); 

        console.log('New Post:', { title, content });

        router.push('/posts');
    };

    const handleCancel = () => {
        router.back(); 
    };

    return (
        <div className="w-full min-h-screen bg-[#f4f6fa] p-6 flex items-start justify-center">
      
            <div className="w-full max-w-2xl bg-white rounded-2xl p-8 border border-gray-100 shadow-sm">
        
                <h2 className="text-2xl font-bold text-gray-900 mb-6">
                    Create New Post
                </h2>

                <form onSubmit={handleSubmit} className="space-y-5">
          
                    <div className="flex flex-col gap-2">
                        <label className="text-md font-semibold text-black">
                            Title
                        </label>

                        <input
                            type="text"
                            value={title}
                            onChange={(e) => setTitle(e.target.value)}
                            placeholder="Give your post a title..."
                            required
                            className="w-full px-4 py-3 bg-gray-50 border border-gray-200 rounded-xl text-gray-900 text-sm focus:outline-none focus:border-[#9ab2ff] focus:bg-white transition-all"
                        />
                    </div>

                    <div className="flex flex-col gap-2">
                        <label className="text-md font-semibold text-black">
                            Content
                        </label>

                        <textarea
                            value={content}
                            onChange={(e) => setContent(e.target.value)}
                            placeholder="What is on your mind? Share your knowledge..."
                            required
                            rows={6}
                            className="w-full px-4 py-3 bg-gray-50 border border-gray-200 rounded-xl text-gray-900 text-sm focus:outline-none focus:border-[#9ab2ff] focus:bg-white transition-all resize-none leading-relaxed"
                        />
                    </div>

                    <div className="flex items-center justify-end gap-3 pt-4 border-t border-gray-50">
            
                        <button
                            type="button"
                            onClick={handleCancel}
                            className="flex items-center gap-1.5 px-5 py-2.5 rounded-xl border border-gray-200 text-gray-600 hover:bg-gray-50 text-sm font-medium transition-colors"
                        >
                            <X className="w-4 h-4" />
                                Cancel
                        </button>

                        <button
                            type="submit"
                            className="flex items-center gap-1.5 px-6 py-2.5 bg-[#b4c3ff] hover:bg-[#a1b2fa] text-[#1e293b] rounded-xl text-sm font-bold shadow-sm transition-colors active:scale-95"
                        >
                            <Plus className="w-4 h-4" />
                            Post
                        </button>

                    </div>
                </form>

            </div>
        </div>
    );
}
