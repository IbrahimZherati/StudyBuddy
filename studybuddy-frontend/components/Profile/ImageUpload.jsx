import { defaultProfilePhotoPath, fileToBase64 } from '@/utils/fileHandling';
import { CameraIcon, Trash2 } from 'lucide-react';
import Image from 'next/image';
import React from 'react'
import { useState } from 'react';

export default function ImageUpload({ name, handleChange, initialPreview }) {
    const [selectedPreview, setSelectedPreview] = useState("");
    const preview = selectedPreview || initialPreview || defaultProfilePhotoPath;

    const handleFile = async (e) => {
        const file = e.target.files[0];
        if (file) {
            if (file.size > 5 * 1024 * 1024) { 
                alert('Maximum allowed size of image is 5MB'); 
                return; 
            }

            setSelectedPreview(URL.createObjectURL(file));

            const file64 = await fileToBase64(file);

            handleChange({
                target: {
                    name,
                    value: file64
                }
            });
        }
    };

    const removeFile = () => {
        setSelectedPreview("");

        handleChange({
            target: {
                name,
                value: null
            }
        });
    }
    
    return (
        <div className="relative flex flex-col left-18">
            <div className="relative overflow-hidden rounded-full h-44 w-44 bg-blue-50">
                {preview && (
                    <Image
                        src={preview}
                        alt="Profile image preview"
                        fill
                        className="object-cover"
                        loading="eager"
                    />
                )}
            </div>

            <label className="absolute mt-3 cursor-pointer text-gray-900 top-26 left-30">
                <CameraIcon className='w-14 h-14'/>
                <input type="file" accept="image/*" className="hidden" onChange={handleFile} />
            </label>

            <label className="absolute mt-3 cursor-pointer text-gray-900 top-27 -left-2">
                <Trash2 className='w-14 h-12'/>
                <button className="hidden" onClick={removeFile} />
            </label>
        </div>
    )
}
