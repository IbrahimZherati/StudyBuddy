import React from 'react'

export default function StudyInterests({ interests }) {
    return (
        <div className='flex flex-col gap-2'>
		    <h3 className="text-2xl font-bold">
				Study Interests
			</h3>

			<div className='flex flex-wrap gap-3'>
				{interests.map((interest, index) => (
					<span key={index} className='px-3 py-1 text-md rounded-full bg-secondary'>
						{interest}
					</span>
				))}
			</div>

		</div>
    )
}
