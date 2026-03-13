'use client'
import React, { useState } from 'react';
import Input from '@/components/Input/Input';
import handleFormChange from '@/utils/forms/handleChange';
import handleFormSubmit from '@/utils/forms/handleSubmit';

const RegisterPage = () => {
    const [formData, setFormData] = useState({
        email: "yazankhalil@gmail.com",
        password: "ree@@311",
        passwordConfirmation: "ree@@311"
    });

    const passwordsMatch = formData.password === formData.passwordConfirmation;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const isEmail = emailRegex.test(formData.email);
    const canSubmit = isEmail && passwordsMatch;

    const handleChange = (fieldName, fieldValue) => {
        handleFormChange(setFormData, fieldName, fieldValue);
    }

    const handleSubmit = async (e) => {
        const data = await handleFormSubmit(e, formData, "auth/register");
        console.log("Data:", data);
    }

    return (
        <div className='page'>
            <form onSubmit={handleSubmit}
                className='flex flex-col justify-evenly border-2 items-center h-120 aspect-square'>

                <Input label="Email:" fieldName="email" type="email"
                    value={formData.email} 
                    handleChange={handleChange} />

                {(formData.email && !isEmail) &&
                    <p className='error'>Please enter a valid email</p>
                }

                <Input label="Password:" fieldName="password" type="password" 
                    value={formData.password} 
                    handleChange={handleChange} />

                <Input label="Confirm Password" fieldName="passwordConfirmation" type="password" 
                    value={formData.passwordConfirmation} 
                    handleChange={handleChange} />

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