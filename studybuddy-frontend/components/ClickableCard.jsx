import React from 'react'
import Link from 'next/link'
import Card from './Card'

export default function ClickableCard({href, children , additionalStyles}) {
    return (
        <Link href={href}>
            <Card additionalStyles={`active:bg-gray-200 active:translate-y-1 ${additionalStyles}`}>
                {children}
            </Card>
        </Link>
    )
}
