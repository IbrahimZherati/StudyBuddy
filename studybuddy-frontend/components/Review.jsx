import React from 'react'
import { StarIcon } from 'lucide-react';

export default function Review() {
    let starIconCounter = 0;
    return (
        <section className="mx-12 my-6">
            <h3 className="text-3xl font-bold mb-5">
                Reviews
            </h3>
            
            <p className="text-yellow-500 text-2xl flex">
                {[1,2,3,4,5].map(()=>(
                    <StarIcon key={++starIconCounter}/>
                ))}
            </p>
            
            <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
                {[1,2,3].map((i) => (
					<div key={i} className="p-4 my-4 rounded-lg shadow-lg bg-[#F5F6FF]">
						<h4 className='font-bold text-xl flex gap-2'>
							Yara
							<span className="text-yellow-500 text-2xl flex">
								{[1,2,3].map(()=>(
								<StarIcon key={++starIconCounter}/>
								))}
							</span>
						</h4>

						<p className="mt-2 text-sm text-gray-500">
							Dentist major
						</p>
						
						<p className="mt-3 text-gray-700">
							I found a study partner within 10 minutes. It&#39;s great.
						</p>
					</div>
                ))}
            </div>
        </section>
    );
}

