import Link from 'next/link'
import React from 'react'
import { GraduationCap } from 'lucide-react'

export default function Header() {
    return (
		<header className='flex-row-center justify-evenly py-4 border-0 shadow-lg bg-[#F5F6FF]'>
			<div className='flex gap-2'>
				<GraduationCap className='text-[#002CFF] w-9 h-9'/>
				
				<h1 className='font-bold text-2xl'>
					Study Buddy
				</h1>
			</div>

			<div className='flex-row-center gap-11'>
				<Link href=""><span className='nav-link'>Free Tools</span></Link>

				<Link href=""><span className='nav-link'>Community Features</span></Link>

				<Link href=""><span className='nav-link'>How it Works</span></Link>

				<Link href=""><span className='nav-link'>Reviews</span></Link>

				<Link href="/register">
					<button className='btn-sign'>
						Sign Up
					</button>
				</Link>
			</div>
		</header>
    )
}
