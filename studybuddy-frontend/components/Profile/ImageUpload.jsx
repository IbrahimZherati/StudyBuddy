import { CameraIcon } from 'lucide-react';
import Image from 'next/image';
import React from 'react'
import { useState } from 'react';

export default function ImageUpload({ onChange, initialPreview }) {
    const [selectedPreview, setSelectedPreview] = useState("");
    const preview = selectedPreview || initialPreview || "/images/avatar-default-2.png";

    const handleFile = (e) => {
        const file = e.target.files[0];
        if (file) {
            setSelectedPreview(URL.createObjectURL(file));
            onChange(file);
        }
    };
    
    return (
        <div className="relative flex flex-col left-18">
            <div className="overflow-hidden rounded-full h-44 w-44">
                {preview && (
                    <Image width={48} height={48} 
                        src={preview} alt="Profile image preview" 
                        className="object-cover" 
                    />
                )}
            </div>

<<<<<<< HEAD
            <label className="absolute mt-3 cursor-pointer text-secondary top-26 left-30">
                <CameraIcon className='w-14 h-14'/>
=======
            <label className="absolute mt-3 cursor-pointer text-gray-900 top-26 left-30">
                <CameraIcon className=' w-14 h-14'/>
>>>>>>> 9684887d11ca0be7a4a0779916bb79a4b88ae92e
                <input type="file" accept="image/*" className="hidden" onChange={handleFile} />
            </label>
        </div>
    )
}
