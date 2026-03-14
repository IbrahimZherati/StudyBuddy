'use client'
import React, { useState } from 'react';
import Input from '@/components/Input/Input';
import handleFormChange from '@/utils/forms/handleChange';
import handleFormSubmit from '@/utils/forms/handleSubmit';

const LoginPage = () => {
    const [formData, setFormData] = useState({
        email: "",
        password: "",
        rememberMe: false
    });

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const isEmail = emailRegex.test(formData.email);
    const canSubmit = isEmail && formData.password.length > 3;

    const handleChange = (fieldName, fieldValue) => {
        handleFormChange(setFormData, fieldName, fieldValue);
    }

    const handleSubmit = async (e) => {
        try {
            const data = await handleFormSubmit(e, formData, "auth/login");
            console.log("Data:", data);
        }
        catch(error) {
            console.log("An Error Occured with POST request:", error);
        }
    }

    return (
        <div className='page'>
            <form onSubmit={handleSubmit} className='custom-form'>

                <Input label="Email:" fieldName="email" type="email"
                    value={formData.email} 
                    handleChange={handleChange} />

                {(formData.email && !isEmail) &&
                    <p className='error'>Please enter a valid email</p>
                }

                <Input label="Password:" fieldName="password" type="password" 
                    value={formData.password} 
                    handleChange={handleChange} />

                <Input label="remember me" fieldName="rememberMe" type="checkbox"
                    value={formData.rememberMe}
                    handleChange={handleChange} />

                <button type="submit" className={`${!canSubmit? "unavailable": ""}`} 
                        disabled={!canSubmit}>
                    Login
                </button>
            </form>
        </div>
    )
}

export default LoginPage;