import React from 'react'
import Link from 'next/link'
import Card from './Card'

export default function ClickableCard({href, children}) {
    const additionalStyles = "active:bg-gray-200 active:translate-y-1";
    return (
        <Link href={href}>
            <Card additionalStyles={additionalStyles}>
                {children}
            </Card>
        </Link>
    )
}
