import React from 'react'
import Link from 'next/link';
import { MessageCircle , Users , FileUp , Globe } from 'lucide-react';

export default function CommunityFeatures() {
	const features = [
		{ 
			icon: <MessageCircle/> ,
			title: "Chat",
			desc: "Direct messaging for quick help." 
		},
		{ 
			icon: <Users/> ,
			title: "Groups",
			desc: "Join study groups for your class." 
		},
		{ 
			icon: <FileUp/> ,
			title: "Share Files",
			desc: "Upload notes and resources easily." 
		},
		{ 
			icon: <Globe/> ,
			title: "Community",
			desc: "Ask everyone by creating a general question or share knowledge by creating posts." 
		},
	];

  	return (
		<section className="mx-12 my-6">
			<h3 className="text-3xl font-bold mb-5">
				Community Features
			</h3>

			<div className="grid grid-cols-1 md:grid-cols-4 gap-5 ">
				{features.map((feature, i) => (
					<div key={i} className="p-4 w-fit flex gap-6 rounded-2xl shadow-lg bg-[#F5F6FF]"> 
						<div>
							{feature.icon}
						</div>

						<div>
							<h4 className="text-xl font-semibold mb-2">
								{feature.title}
							</h4>

							<p className="text-gray-600 mb-7 h-28">
								{feature.desc}
							</p>
							
							<a className="text-blue-600 font-semibold" href="#">
								Try it →
							</a>
						</div>
					</div>
				))}
			</div>

			<div className="text-center mt-8">
				<Link href="/register">
					<button className="bg-[#002CFF] cursor-pointer font-bold text-white px-8 py-3 rounded-full">
						Unlock everything (Sign Up)
					</button>
				</Link>
			</div>
		</section>
  	);
}
