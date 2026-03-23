import React from 'react'

export default function HowItWorks() {
	const steps = [
		{
			title: "Create Profile",
			desc: "Tell us your major, courses and study goals."
		},
		{ 
			title: "Get Matched",
			desc: "Our algorithm finds students with similar interests."
		},
		{ 
			title: "Start Studying",
			desc: "Chat, create posts and join groups instantly." 
		},
	];

	return (
		<section id='howItWorks' className="mx-4 md:mx-10 my-6 scroll-mt-24">
			<h3 className="title font-bold mb-5">
				How it Works
			</h3>

			<div className="flex gap-10"> 
				{/* LEFT: timeline */} 
				<div className="relative flex flex-col items-center">
					{/* vertical line */} 
					<div className="absolute top-6 bottom-6 w-px bg-black"></div> 
					
					<div className='flex flex-col justify-between'>
						{steps.map((_, i) => (
							<div key={i} className="relative z-10 mb-13">
								<div className={`w-12 h-12 flex-row-center font-bold rounded-full text-xl
									${i === 0 ? "border-4 border-blue-600 bg-white" : "bg-gray-100 text-black"}`}> 
										{i + 1} 
								</div> 
							</div> 
						))} 
					</div>
				</div> 

				{/* RIGHT: text */} 
				<div className="flex flex-col justify-between"> 
					{steps.map((step, i) => (
						<div key={i} className="mb-10"> 
							<h3 className="font-bold text-[1.2rem] md:text-[1.3rem] mb-2">
								{step.title}
							</h3>

							<p className="sub-title text-[1rem] md:text-[1.1rem]"> 
								{step.desc} 
							</p> 
						</div> 
					))} 
				</div> 
			</div>	
		</section>
	);
}

