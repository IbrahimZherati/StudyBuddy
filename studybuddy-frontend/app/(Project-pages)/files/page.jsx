'use client';

import React, { useState, useRef, useMemo } from 'react';
import { Upload } from 'lucide-react';
import FileRow from '@/components/Files/FileRow';
import post from '@/utils/API/post';
import { fileToBase64 } from '@/utils/fileHandling';
import useLazyContainter from '@/app/hooks/useLazyContainer';
import useGetId from '@/app/hooks/useGetId';
import { notify } from '@/utils/notify';

export default function FileManager() {

    const clientId = useGetId();

    const params = useMemo(() => {
        return {
            clientId
        }
    }, [clientId]);

    const [isUploading, setIsUploading] = useState(false);

    const uploadFile = async (file) => {
        const base64 = await fileToBase64(file);
        const body = {
            title: file.name,
            bin: base64
        }

        setIsUploading(true);
        try {
            await post(body, "ClientFile");
            window.location.reload();
        }
        catch(error) {
            const errorReason = error?.response?.data?.error;
            console.log("Error uploading file", errorReason);

            if(errorReason) {
                notify({
                    title: "Error",
                    message: errorReason,
                    sound: false,
                    error: true
                })
            }
        }
        finally {
            fileInputRef.current.value = "";
            setIsUploading(false);
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

    const loadFactor = 20;
    const [files, containerRef, onScroll] = useLazyContainter("ClientFile", loadFactor, params);

    return (
        <div className="flex flex-col h-full min-h-0 p-6">
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
                    className={`btn flex flex-row gap-2 items-center ${isUploading? "disabled": ""}`}
                    disabled={isUploading}
                >
                    <Upload size={18} />
                    <span>{isUploading? "Uploading...": "Upload File"}</span>
                </button>
            </div>

            <div 
                className="space-y-4"
                ref={containerRef}
                onScroll={onScroll}
            >
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