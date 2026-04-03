'use client'
import React, { useState } from 'react';
import Link from 'next/link'
import { GraduationCap, Menu , X} from 'lucide-react'

export default function Header() {

	const [open, setOpen] = useState(false);

    return (
		<header className='fixed top-0 w-full border-0 shadow-lg bg-tertiary z-10000'>
			<div className='justify-between px-4 py-4 mx-auto max-w-7xl sm:px-6 lg:px-8 flex-row-center'>
				
				<div className='flex gap-2'>
					<GraduationCap className='text-primary w-9 h-9'/>
				
					<h1 className='text-2xl font-bold'>
						Study Buddy
					</h1>
				</div>
                 
				{/* Desktop Menu */}
                <div className='hidden md:flex-row-center gap-11'>
					<Link href="#guestFeatures"><span className='nav-link hover-underline'>Guest Features</span></Link>

					<Link href="#communityFeatures"><span className='nav-link hover-underline'>Community Features</span></Link>

					<Link href="#howItWorks"><span className='nav-link hover-underline'>How it Works</span></Link>

					<Link href="#reviews"><span className='nav-link hover-underline'>Reviews</span></Link>

					<Link href="/register">
						<button className='btn-sign'>
							Register
						</button>
					</Link>
				</div>

				{/* Mobile Button */}
				<button className='mr-3 md:hidden' onClick={() => setOpen(!open)}>
					{open ? <X size={30}/> : <Menu size={30}/>}
				</button>

			</div>

			{/* Mobile Menu */}
			{open && (
				<div className='gap-3 px-6 pb-4 shadow-lg md:hidden bg-tertiary flex-col-center '>
					<Link href="#freeFeatures" onClick={ () => setOpen(false)}>
						<span className='nav-link'>Guest Features</span>
					</Link>

					<Link href="#communityFeatures" onClick={ () => setOpen(false)}>
						<span className='nav-link'>Community Features</span>
					</Link>

					<Link href="#howItWorks" onClick={ () => setOpen(false)}>
						<span className='nav-link'>How it Works</span>
					</Link>

					<Link href="#reviews" onClick={ () => setOpen(false)}>
						<span className='nav-link'>Reviews</span>
					</Link>

				</div>
			)}

		</header>
    )
}
