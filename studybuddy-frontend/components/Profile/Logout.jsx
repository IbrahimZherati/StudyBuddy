import post from '@/utils/API/post';
import { useRouter } from 'next/navigation';
import React from 'react'

export default function Logout({additionalStyles}) {

    const router = useRouter();

    const logout = async () => {
        await post({}, "Auth/Logout");
        router.push("/login");
    }

    return (
        <button 
            className={`btn bg-red-500 ${additionalStyles}`}
            onClick={logout}
        >
            Log Out
        </button>
    )
}
