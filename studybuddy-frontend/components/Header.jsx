'use client'
import React, { useState } from 'react';
import Link from 'next/link'
import { GraduationCap, Menu , X} from 'lucide-react'

export default function Header() {

	const [open , setOpen] = useState(false);

    return (
		<header className='border-0 shadow-lg bg-[#F5F6FF] fixed w-full top-0 z-10000'>
			<div className='max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 flex-row-center justify-evenly py-4'>
				
				<div className='flex gap-2'>
					<GraduationCap className='text-[#002CFF] w-9 h-9'/>
				
					<h1 className='font-bold text-2xl'>
						Study Buddy
					</h1>
				</div>
                 
				{/* Desktop Menu */}
                <div className='hidden md:flex-row-center gap-11'>
					<Link href="#freeFeatures"><span className='nav-link'>Free Tools</span></Link>

					<Link href="#communityFeatures"><span className='nav-link'>Community Features</span></Link>

					<Link href="#howItWorks"><span className='nav-link'>How it Works</span></Link>

					<Link href="#reviews"><span className='nav-link'>Reviews</span></Link>

					<Link href="/register">
						<button className='btn-sign'>
							Register
						</button>
					</Link>
				</div>

				{/* Mobile Button */}
				<button className='md:hidden' onClick={() => setOpen(!open)}>
					{open ? <X /> : <Menu />}
				</button>

			</div>

			{/* Mobile Menu */}
			{open && (
				<div className='md:hidden bg-[#F5F6FF] px-6 pb-4 flex-col-center gap-3 shadow-lg '>
					<Link href="#freeFeatures"><span className='nav-link'>Free Tools</span></Link>

					<Link href="#communityFeatures"><span className='nav-link'>Community Features</span></Link>

					<Link href="#howItWorks"><span className='nav-link'>How it Works</span></Link>

					<Link href="#reviews"><span className='nav-link'>Reviews</span></Link>

					<Link href="/register">
						<button className='btn-sign'>
							Register
						</button>
					</Link>
				</div>
			)}

		</header>
    )
}
