import React from 'react'
import Link from 'next/link';
import CardContainer from './CardContainer';
import FeatureCard from './FeatureCard';
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
		<section id='communityFeatures' className="mx-12 my-6 scroll-mt-24">
			<h3 className="text-3xl font-bold mb-5">
				Community Features
			</h3>

			<CardContainer>
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
