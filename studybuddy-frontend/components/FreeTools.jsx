import React from 'react'
import { BookOpen , Clock , Calendar , Calculator } from 'lucide-react';

export default function FreeTools() {
	const tools = [
		{ 
			icon: <BookOpen />,
			title: "Article",
			desc: "Read article about anything you want,how to study, how to manage your time, and any thing about your major." 
		},
		{ 
			icon: <Clock/>,
			title: "Pomodoro",
			desc: "Use our pomodoro timer to manage your study time and get excited." 
		},
		{ 
			icon: <Calendar/>,
			title: "Schedule",
			desc: "Make your own study schedule, enjoy and success." 
		},
		{ 
			icon: <Calculator/>,
			title: "Calculator",
			desc: "Calculate your school year rate." 
		},
	];

	return (
		<section className="mx-12 my-6">
			<h3 className="text-3xl font-bold mb-5">
				Free Tools 
				<span className='text-lg font-normal'> (Without signing up)</span>
			</h3>

			<div className="grid grid-cols-1 md:grid-cols-4 gap-5">
				{tools.map((tool, i) => (
					<div key={i} className="p-4 w-fit flex gap-6 rounded-2xl shadow-lg bg-[#F5F6FF]"> 
						<div>
							{tool.icon}
						</div>

						<div>
							<h4 className="text-xl font-semibold mb-2">
								{tool.title}
							</h4>

							<p className="text-gray-600 mb-8 h-28">
								{tool.desc}
							</p>

							<a className="text-blue-600 font-semibold" href="#">
								Try it →
							</a>
						</div>
					</div>
				))}
			</div>
		</section>
	);
}

