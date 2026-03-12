'use client'
import React, { useState } from 'react'
import Input from '@/components/Input/page';

const RegisterPage = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [passwordConfirmation, setPasswordConfirmation] = useState("");

    const passwordsMatch = password === passwordConfirmation;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const isEmail = emailRegex.test(email);
    const canSubmit = isEmail && passwordsMatch;

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
        console.log("Data:", data);
    }

    return (
        <div className='page'>
            <form onSubmit={handleSubmit}
                className='flex flex-col justify-evenly border-2 items-center h-120 aspect-square'>

                <label>
                    <span className='input-span'>Email:</span>
                    <Input 
                        value={email}
                        onChange={e => {setEmail(e.target.value)}}
                    /> 
                </label>

                {(!isEmail && email) &&
                    <p className='error'>Please enter a valid email</p>
                }

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
                    <p className='error'>Passwords do not match</p>
                }

                <button type="submit" className={`${!canSubmit? "unavailable": ""}`} 
                        disabled={!canSubmit || !isEmail}>
                    Register
                </button>
            </form>
        </div>
    )
}

export default RegisterPage;