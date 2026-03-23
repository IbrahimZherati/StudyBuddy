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
			<h3 className="text-3xl font-bold mb-5">
				How it Works
			</h3>

			<div className="relative"> 
				{/* timeline */} 
				<div className="absolute left-6 top-0 bottom-0 w-px bg-black"></div>

        		{steps.map((step, i) => (
          				<div key={i} className="grid grid-cols-[50px_1fr] gap-6 mb-10">

            				{/* circle  */}
            				<div className="flex justify-center relative z-10">

            				  <div className={`w-12 h-12 flex items-center justify-center font-bold rounded-full text-xl
            				      ${i === 0
            				        ? "border-4 border-blue-600 bg-white"
            				        : "bg-gray-100 text-black"}`}>

            				  		{i + 1}
            				  </div>

            				</div>

							{/* RIGHT: text */} 
							<div>
              					<h3 className="title text-[1.3rem] mb-2">
              					  {step.title}
              					</h3>

              					<p className="sub-title text-[1.1rem]">
              					  {step.desc}
              					</p>
            				</div>

                        </div>
                ))}
			</div>	
		</section>
	);
}

