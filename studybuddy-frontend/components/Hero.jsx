import React from 'react'
import Link from 'next/link'

export default function Hero() {
    return (
        <section>
            <div className='my-7 ml-12'>
                <h1 className='text-4xl font-bold mb-3'>
                    Study Together and Make Achievements
                </h1>

                <p className='sub-title'>
                    Connect with students in your major. Find your perfect study partner today and boost your GPA.
                </p>
            </div>
            
            <div className='flex-col-center gap-2 bg-[#00041A] mx-auto mt-5 w-fit rounded-xl p-3'>
                <div className='flex-col-center text-white'>
                    <p className='font-bold text-center'>
                        Join us now to find your study buddy!
                    </p>

                    <p>
                        Signing up is required for that.
                    </p>
                </div>

                <div className='flex justify-between'>
                    <Link href='/login'>
                        <button className="btn-sign">
                            Login
                        </button>
                    </Link>

                    <Link href='/register'>
                        <button className="btn-sign ml-2">
                            Register
                        </button>
                    </Link>
                </div>
            </div>
        </section>
    )
}
