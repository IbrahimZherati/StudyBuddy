'use client'
import React, { useState } from 'react'
import Input from '@/components/Input/page';

const RegisterPage = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [passwordConfirmation, setPasswordConfirmation] = useState("");

    const passwordsMatch = password === passwordConfirmation;

    const handleSubmit = async (e) => {
        e.preventDefault();
        const response = await fetch("http://localhost:5203/api/auth/register", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                email,
                password,
                passwordConfirmation
            })
        });

        const data = await response.json();
        console.log(data);
    }

    return (
        <div className='page'>
            <form className='flex flex-col justify-evenly border-2 items-center h-120 aspect-square'>
                <label>
                    <span className='input-span'>Email:</span>
                    <Input 
                        value={email}
                        onChange={e => {setEmail(e.target.value)}}
                    /> 
                </label>
                <label>
                    <span className='input-span'>Password:</span>
                    <Input 
                        value={password}
                        onChange={e => {setPassword(e.target.value)}}
                    /> 
                </label>
                <label>
                    <span className='input-span'>Confirm Password:</span>
                    <Input 
                        value={passwordConfirmation}
                        onChange={e => {setPasswordConfirmation(e.target.value)}}
                    /> 
                </label>

                {!passwordsMatch &&
                    <p className='text-red-500'>Passwords do not match</p>
                }

                <button onSubmit={handleSubmit}>
                    Register
                </button>
            </form>
        </div>
    )
}

export default RegisterPage;