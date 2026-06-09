import Image from 'next/image';
import React from 'react'

export default function PhotoDisplay({ photo, size, alt}) {
    return (
        <div className={`relative overflow-hidden rounded-full h-${size} w-${size} bg-blue-50`}>
            {photo && (
                <Image
                    src={photo}
                    alt={alt}
                    fill
                    className="object-cover"
                    loading="eager"
                />
            )}
        </div>
    )
}
