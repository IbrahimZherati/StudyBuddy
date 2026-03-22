import React from 'react'
import CardContainer from './CardContainer';
import FeatureCard from './FeatureCard';
import { BookOpen , Clock , Calendar , Calculator } from 'lucide-react';

export default function Freefeatures() {
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
		<section className="mx-12 my-6">
			<h3 className="flex-row-center justify-start gap-1 text-3xl font-bold mb-5">
				Free Features 
				<span className='sub-title font-normal ml-1 text-[1.3rem]'>(Without signing up)</span>
			</h3>

			<CardContainer>
				{features.map((feature, i) => (
					<FeatureCard key={i}
						{...feature}
					/> 
				))}
			</CardContainer>
		</section>
	);
}

