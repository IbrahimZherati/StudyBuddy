import React from 'react'
import { GraduationCap } from 'lucide-react'

export default function Footer() {
	return (
		<footer className="bg-[#020B2D] text-white rounded-t-[40px] mt-20">
			<div className='max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-10'>
				<div className='flex flex-col md:flex-row md:items-center gap-6 ml-8 md:ml-20'>
					<div>
						<GraduationCap className="text-3xl w-15 h-15"/>
					</div>

					<div>
						<h2 className="text-2xl md:text-3xl mb-3 font-bold">StudyBuddy</h2>
						<p className="text-gray-300 text-sm max-w-md">
							Making studying less lonely and more productive for students everywhere
						</p>
					</div>
				</div>

				<div className="grid grid-cols-1 sm:grid-cols-2 gap-6 ml-8 md:ml-20 mt-10">
					{/* PRODUCT */}
					<div className='flex flex-col md:flex-row gap-5'>
						<h3 className="font-semibold mb-4">PRODUCT</h3>
						<ul className="flex gap-5 text-gray-300 text-sm">
							<li className="hover:text-white cursor-pointer">Features</li>
							<li className="hover:text-white cursor-pointer">How it works</li>
						</ul>
					</div>

					{/* COMPANY */}
					<div className='flex flex-col md:flex-row gap-5'>
						<h3 className="font-semibold mb-4">COMPANY</h3>
						<ul className="flex gap-5 text-gray-300 text-sm">
							<li className="hover:text-white cursor-pointer">About us</li>
							<li className="hover:text-white cursor-pointer">Contact</li>
							<li className="hover:text-white cursor-pointer">Privacy Policy</li>
						</ul>
					</div>
				</div>
			</div>
			
		</footer>
	);
}


