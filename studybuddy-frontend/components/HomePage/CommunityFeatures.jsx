import React from 'react'
import Link from 'next/link';
import CardContainer from '../CardContainer';
import FeatureCard from '../LandingPage/FeatureCard';
import { MessageCircle, Users, Globe, Sparkles } from 'lucide-react';

export default function CommunityFeatures() {
	const features = [
		{ 
			icon: <MessageCircle /> ,
			title: "Chat",
			desc: "Direct messaging for quick help.",
			href: "/chats" 
		},
		{ 
			icon: <Users /> ,
			title: "Buddies",
			desc: "Meet students with similar interests from all over the world!",
			href: "/search_buddy" 
		},
		{ 
			icon: <Globe /> ,
			title: "Community",
			desc: "Ask everyone or share knowledge by creating posts.",
			href: "posts/new" 
		},
		{
			icon: <Sparkles />,
			title: "AI Analysis",
			desc: "Upload lectures and file for AI summary and flash cards!",
			href: "/files"
		}
	];

  	return (
		<section id='communityFeatures' className="mx-4 md:mx-10 my-6 scroll-mt-24">
			<h3 className="title font-bold mb-5">
				Features
			</h3>

			<div className="flex-col-center">
				<CardContainer additionalStyles="grid-cols-1 sm:grid-cols-2 xl:grid-cols-4">
					{features.map((feature, i) => (
						<FeatureCard key={i}
							{...feature}
						/> 
					))}
				</CardContainer>

				<div className="text-center mt-8">
					<Link href="/register">
						<button className="btn">
							Unlock everything (Register)
						</button>
					</Link>
				</div>
			</div>
		</section>
  	);
}
