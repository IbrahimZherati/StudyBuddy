'use client';
import React, { useState, useEffect } from 'react';
import { useRouter } from 'next/navigation';
import { LoaderCircle, X } from 'lucide-react';
import handleFormChange from '@/utils/forms/handleChange';
import InputField from '@/components/Profile/EditProfile/InputField';

export default function PostEdit({ 
    submitButtonText, 
    SubmitIcon: SubmitIcon, 
    onSubmitAction, 
    initialData = { title: "", text: "" },
    formTitle = "Create New Post"
}) {

    const router = useRouter();

    const [form, setForm] = useState(initialData);

    const [triedToSubmit, setTriedToSubmit] = useState(false);

    const [isLoading, setIsLoading] = useState(false);

    const canSubmit = form.title && form.text;

    useEffect(() => {
        if (initialData) {
            setForm(initialData);
        }
    }, [initialData]);

    const handleFocus = () => {
        setTriedToSubmit(false);
    }

    const handleChange = (e) => {
        const { name, value } = e.target;
        handleFormChange(setForm, name, value);
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!canSubmit) {
            setTriedToSubmit(true);
            return;
        }

        setIsLoading(true);
        try {
            await onSubmitAction(e, canSubmit, setTriedToSubmit, form, setForm);
        } catch (error) {
            console.error("An Error Occurred in PostForm Submit:", error);
        } finally {
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
                    {formTitle}
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
                                    ? "Title field is required" : ""
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
                            type="textarea"
                            rows={6}
                            value={form.text}
                            handleChange={handleChange}
                            handleFocus={handleFocus}
                            placeholder="What is on your mind? Share your knowledge..."
                            hasError={!form.text}
                            triedToSubmit={triedToSubmit}
                            errorMessage={
                                (triedToSubmit && !form.text)
                                    ? "Content field is required": ""
                            }
                            additionalStyles="w-full px-4 py-3 bg-gray-50 rounded-xl text-gray-900 
                                            text-sm focus:bg-white transition-all border-2 border-gray-200
                                            resize-none leading-relaxed"
                        />
                    </div>

                    <div className="flex items-center justify-end gap-3 pt-4 border-t border-gray-50">
                        
                        <button
                            type="button"
                            onClick={handleCancel}
                            className="flex items-center gap-1.5 px-5 py-2.5 rounded-xl text-gray-600 
                                        hover:bg-gray-50 text-sm font-medium transition-colors border border-gray-200"
                        >
                            <X className="w-4 h-4" />
                            Cancel
                        </button>

                        <button
                            type="submit"
                            className="flex items-center gap-1.5 px-6 py-2.5 bg-[#b4c3ff] hover:bg-[#a1b2fa] text-[#1e293b] 
                                        rounded-xl text-sm font-bold shadow-sm transition-colors active:scale-95"
                        >
                            {SubmitIcon && <SubmitIcon className="w-4 h-4" />}
                            <span>{submitButtonText}</span>
                            {isLoading && (
                                <LoaderCircle 
                                    className="h-4 w-4 animate-spin
                                            text-white drop-shadow-[0_0_6px_rgba(255,255,255,0.8)]" 
                                    strokeWidth={3}
                                />
                            )}
                        </button>
                        
                    </div>
                </form>
                
            </div>
        </div>
    );
}