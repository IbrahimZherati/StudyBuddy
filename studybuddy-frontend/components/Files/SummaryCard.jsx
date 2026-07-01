import React from 'react';
import HtmlRenderer from '../HtmlRenderer';

export default function SummaryCard({ summaryText }) {
    return (
        <div className="bg-tertiary shadow-sm rounded-2xl p-6 flex flex-col h-64">
            <h2 className="text-2xl font-bold text-black mb-3 shrink-0">
                Summary
            </h2>

            <div className="overflow-y-auto pr-2 text-gray-800 leading-relaxed text-lg flex-1 custom-scrollbar">
                {summaryText && 
                    <HtmlRenderer 
                        html={summaryText}
                    />
                }
                
                {!summaryText && "Generating Summary..."}
            </div>
        </div>
    );
}