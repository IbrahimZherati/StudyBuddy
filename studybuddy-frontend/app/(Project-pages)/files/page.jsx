'use client';

import React, { useState, useRef } from 'react';
import { Upload } from 'lucide-react';
import FileRow from '@/components/Files/FileRow';
import post from '@/utils/API/post';
import { fileToBase64 } from '@/utils/fileHandling';

export default function FileManager() {

    const [files, setFiles] = useState([
        {
            id: '1',
            name: 'project-requirements.pdf',
            uploadDateTime: '2026-06-30T14:30'
        },
        {
            id: '2',
            name: 'dashboard-design.pdf',
            uploadDateTime: '2026-06-29T09:15'
        }
    ]);

    const [isUploading, setIsUploading] = useState(false);

    const uploadFile = async (file) => {
        const base64 = await fileToBase64(file);
        const body = {
            title: file.name,
            bin: base64
        }

        try {
            const response = await post(body, "ClientFile");
            return response.data;
        }
        catch(error) {
            console.log("Error uploading file", error?.response?.data);
        }
        finally {
            // window.location.reload();
        }
    };

    const handleFileChange = (e) => {
        const file = e.target.files[0];
        if(file)
            uploadFile(file);
    }

    const fileInputRef = useRef(null);

    const triggerFileInput = () => {
        fileInputRef.current?.click();
    };

    return (
        <div className="p-6">
            <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4 mb-4 pb-6">
                <div>
                    <h1 className="text-2xl font-bold text-gray-800">
                        File Management
                    </h1>

                    <p className="text-md text-gray-500 mt-1">
                        Upload, manage, and analyze your files using artificial intelligence.
                    </p>
                </div>

                <input
                    type="file"
                    ref={fileInputRef}
                    onChange={handleFileChange}
                    className="hidden"
                />

                <button
                    onClick={triggerFileInput}
                    className='btn flex flex-row gap-2 items-center'
                >
                    <Upload size={18} />
                    <span>Upload File</span>
                </button>
            </div>

            <div className="space-y-4">
                {files.length > 0 ? (
                    files.map((file) => (
                        <FileRow
                            key={file.id}
                            file={file}
                        />
                    ))
                ) : (
                    <div className="text-center py-12 border-2 border-dashed border-gray-200 
                                    rounded-xl bg-white text-gray-400">
                        No files uploaded yet. Click on Upload File to get started.
                    </div>
                )}
            </div>
        </div>
    );
}