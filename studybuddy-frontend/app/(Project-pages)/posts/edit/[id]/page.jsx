'use client';
import React, { useState } from 'react';
import { Edit } from 'lucide-react';
import PostEdit from '@/components/Posts/PostEdit';

export default function EditPost() {
    const [currentPostData, setCurrentPostData] = useState({ title: "", text: "" });

    const handleEditSubmit = async () => {

    };

    return (
        <PostEdit 
            formTitle="Edit Post"
            submitButtonText="Edit"
            SubmitIcon={Edit}
            onSubmitAction={handleEditSubmit}
            initialData={currentPostData}
        />
    );
}