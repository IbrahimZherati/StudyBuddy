'use client';
import React, { useState } from 'react';
import { useRouter } from 'next/navigation';
import { LoaderCircle, Plus, X } from 'lucide-react';
import handleFormSubmit from '@/utils/forms/handleSubmit';
import handleFormChange from '@/utils/forms/handleChange';
import InputField from '@/components/Profile/EditProfile/InputField';


export default function CreatePost() {
    const router = useRouter();

    const initialState = {
        title: "",
        text: ""
    }

    const [form, setForm] = useState(initialState);

    const [triedToSubmit, setTriedToSubmit] = useState(false);

    const [isLoading, setIsLoading] = useState(false);

    const canSubmit = form.title && form.text;

    const handleFocus = () => {
        setTriedToSubmit(false);
    }

    const handleChange = (e) => {
        const {name, value} = e.target;
        handleFormChange(setForm, name, value);
    }

    const handleSubmit = async (e) => {
        setIsLoading(true);
        try {
            const data = await handleFormSubmit(e, canSubmit, setTriedToSubmit, form, setForm, "Post", "post", initialState);
    
            if(data?.isSuccess)
                router.push('/posts/mine');
        }
        catch (error) {
            console.log("An Error Occured with POST request:", error?.response?.data);
        }
        finally {
            setIsLoading(false);
        }
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

                <form noValidate onSubmit={handleSubmit} className="space-y-5">
          
                    <div className="flex flex-col gap-2">
                        <label className="text-md font-semibold text-black">
                            Title
                        </label>

                        <InputField 
                            name="title"
                            type="text"
                            value={form.title}
                            handleChange={handleChange}
                            handleFocus={handleFocus}
                            placeholder="Give your post a title..."
                            hasError={!form.title}
                            triedToSubmit={triedToSubmit}
                            errorMessage={
                                (triedToSubmit && !form.title)
                                    ? "Title field is required": ""
                            }
                            additionalStyles="w-full px-4 py-3 bg-gray-50 rounded-xl 
                                            text-gray-900 text-sm border-2 border-gray-200
                                            focus:bg-white transition-all"
                        />
                    </div>

                    <div className="flex flex-col gap-2">
                        <label className="text-md font-semibold text-black">
                            Content
                        </label>

                        <InputField 
                            name="text"
                            type="text"
                            value={form.text}
                            handleChange={handleChange}
                            handleFocus={handleFocus}
                            placeholder="What is on your mind? Share your knowledge..."
                            hasError={!form.text}
                            triedToSubmit={triedToSubmit}
                            errorMessage={
                                (triedToSubmit && !form.text)
                                    ? "text field is required": ""
                            }
                            additionalStyles="w-full px-4 py-3 bg-gray-50 rounded-xl text-gray-900 
                                            text-sm focus:bg-white transition-all border-2 border-gray-200
                                            resize-none leading-relaxed"
                        />

                        {/* <textarea
                            value={form.text}
                            onChange={(e) => setform.text(e.target.value)}
                            placeholder="What is on your mind? Share your knowledge..."
                            required
                            rows={6}
                            className="w-full px-4 py-3 bg-gray-50 rounded-xl text-gray-900 text-sm focus:border-[#9ab2ff] focus:bg-white transition-all resize-none leading-relaxed"
                        /> */}
                    </div>

                    <div className="flex items-center justify-end gap-3 pt-4 border-t border-gray-50">
            
                        <button
                            type="button"
                            onClick={handleCancel}
                            className="flex items-center gap-1.5 px-5 py-2.5 rounded-xl text-gray-600 hover:bg-gray-50 text-sm font-medium transition-colors"
                        >
                            <X className="w-4 h-4" />
                                Cancel
                        </button>

                        <button
                            type="submit"
                            className="flex items-center gap-1.5 px-6 py-2.5 bg-[#b4c3ff] hover:bg-[#a1b2fa] text-[#1e293b] rounded-xl text-sm font-bold shadow-sm transition-colors active:scale-95"
                        >
                            <Plus className="w-4 h-4" />
                            <span>Post</span>
                            {isLoading &&
                                <LoaderCircle 
                                    className="h-4 w-4 animate-spin
                                            text-white drop-shadow-[0_0_6px_rgba(255,255,255,0.8)]" 
                                    strokeWidth={3}
                                />
                            }
                        </button>

                    </div>
                </form>

            </div>
        </div>
    );
}
