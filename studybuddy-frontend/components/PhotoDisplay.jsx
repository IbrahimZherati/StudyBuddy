import Image from 'next/image';
import React from 'react'

export default function PhotoDisplay({ photo, sizeClass, alt }) {
    return (
        <div className={`relative overflow-hidden rounded-full bg-blue-50 ${sizeClass}`}>
            {photo && (
                <Image
                    src={photo}
                    alt={alt}
                    fill
                    className="object-cover"
                    priority
                />
            )}
        </div>
    )
}
