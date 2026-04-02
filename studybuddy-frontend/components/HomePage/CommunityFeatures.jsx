import React from 'react'
import Link from 'next/link';
import CardContainer from '../CardContainer';
import FeatureCard from '../LandingPage/FeatureCard';
import { MessageCircle , Users , FileUp , Globe } from 'lucide-react';

export default function CommunityFeatures() {
	const features = [
		{ 
			icon: <MessageCircle /> ,
			title: "Chat",
			desc: "Direct messaging for quick help.",
			href: "../login" 
		},
		{ 
			icon: <Users /> ,
			title: "Groups",
			desc: "Join study groups for your class.",
			href: "../login" 
		},
		{ 
			icon: <FileUp /> ,
			title: "Share Files",
			desc: "Upload notes and resources easily.",
			href: "../login" 
		},
		{ 
			icon: <Globe /> ,
			title: "Community",
			desc: "Ask everyone by creating a general question or share knowledge by creating posts.",
			href: "../login" 
		},
	];

  	return (
		<section id='communityFeatures' className="mx-4 md:mx-10 my-6 scroll-mt-24">
			<h3 className="title font-bold mb-5">
				Community Features
			</h3>

			<CardContainer additionalStyles="grid-cols-1 sm:grid-cols-2 xl:grid-cols-4">
				{features.map((feature, i) => (
					<FeatureCard key={i}
						{...feature}
					/> 
				))}
			</CardContainer>

			<div className="text-center mt-8">
				<Link href="/register">
					<button className="btn-sign">
						Unlock everything (Register)
					</button>
				</Link>
			</div>
		</section>
  	);
}
