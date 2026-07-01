import React from 'react'

export default function HtmlRenderer({ html }) {
    return (
        <section dangerouslySetInnerHTML={{ __html: html }} />
    );
}
