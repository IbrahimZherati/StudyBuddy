import React from 'react'
import CardContainer from '../CardContainer';
import FeatureCard from '../FeatureCard';
import { BookOpen , Clock , Calendar , Calculator } from 'lucide-react';

export default function GuestFeatures() {
	const features = [
		{ 
			icon: <BookOpen />,
			title: "Article",
			desc: "Read articles about anything you want, how to study, how to manage your time and any thing about your major.",
			href: "#" 
		},
		{ 
			icon: <Clock />,
			title: "Pomodoro",
			desc: "Use our pomodoro timer to manage your study time and get excited!",
			href: "#" 
		},
		{ 
			icon: <Calendar />,
			title: "Schedule",
			desc: "Make your own study schedule, enjoy and achieve success.",
			href: "#" 
		},
		{ 
			icon: <Calculator />,
			title: "Calculator",
			desc: "Calculate your school year rate.",
			href: "#" 
		},
	];

	return (
		<section id='guestFeatures' className="mx-4 md:mx-10 my-6 scroll-mt-24">
			<div className="flex-row-center flex-wrap justify-start gap-1 md:text-3xl font-bold mb-5">
				<h3 className='title m-0 shrink-0'>Guest Features</h3>
				<span className='sub-title font-normal md:ml-1 text-[1.1rem] md:text-[1.3rem] 
								whitespace-nowrap'>(Without signing up)</span>
			</div>

			<CardContainer additionalStyles="grid-cols-1 sm:grid-cols-2 xl:grid-cols-4">
				{features.map((feature, i) => (
					<FeatureCard key={i}
						{...feature}
					/> 
				))}
			</CardContainer>
		</section>
	);
}

