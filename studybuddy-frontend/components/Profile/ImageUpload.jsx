import { CameraIcon } from 'lucide-react';
import React from 'react'
import { useState } from 'react';

export default function ImageUpload({ onChange }) {
    
    const [preview, setPreview] = useState("/images/avatar-default.svg");

    const handleFile = (e) => {
        const file = e.target.files[0];
        if (file) {
            setPreview(URL.createObjectURL(file));
            onChange(file);
        }
    };
    
    return (
        <div className="relative flex flex-col left-18">
            <div className="overflow-hidden rounded-full bg-tertiary h-44 w-44">
                {preview && (
                    <img src={preview} className="object-cover w-48 h-48 " />
                )}
            </div>

            <label className="absolute mt-3 cursor-pointer text-secondary top-26 left-30">
                <CameraIcon className=' w-14 h-14'/>
                <input type="file" className="hidden" onChange={handleFile} />
            </label>
        </div>
    )
}
