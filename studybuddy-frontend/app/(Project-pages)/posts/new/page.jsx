'use client';
import React from 'react';
import { useRouter } from 'next/navigation';
import { Plus } from 'lucide-react';
import handleFormSubmit from '@/utils/forms/handleSubmit';
import PostEdit from '@/components/Posts/PostEdit';

export default function CreatePost() {
    const router = useRouter();

    const initialState = {
        title: "",
        text: ""
    };

    const handleCreateSubmit = async (e, canSubmit, setTriedToSubmit, form, setForm) => {
        try {
            const data = await handleFormSubmit(e, canSubmit, setTriedToSubmit, form, setForm, "Post", "post", initialState);
    
            if (data?.isSuccess)
                router.push('/posts/mine');
        } catch (error) {
            console.log("An Error Occurred with POST request:", error?.response?.data);
            throw error; 
        }
    };

    return (
        <PostEdit 
            formTitle="Create New Post"
            submitButtonText="Post"
            SubmitIcon={Plus}
            onSubmitAction={handleCreateSubmit}
            initialData={initialState}
        />
    );
}