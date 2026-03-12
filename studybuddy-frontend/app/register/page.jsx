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

                <Input label="Email:" value={email} onChangeFunc={setEmail} />

                {(email && !isEmail) &&
                    <p className='error'>Please enter a valid email</p>
                }

                <Input label="Password:" type="password" value={password} 
                    onChangeFunc={setPassword} />
                <Input label="Confirm Password" type="password" value={passwordConfirmation} 
                    onChangeFunc={setPasswordConfirmation} />

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