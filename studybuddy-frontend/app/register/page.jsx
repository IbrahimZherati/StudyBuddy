'use client'
import React, { useState } from 'react';
import Input from '@/components/Components/Input';
import handleFormChange from '@/utils/forms/handleChange';
import handleFormSubmit from '@/utils/forms/handleSubmit';
import Link from 'next/link';
import GoBackButton from '@/components/Components/GoBackButton';

const RegisterPage = () => {
    const [formData, setFormData] = useState({
        email: "",
        password: "",
        passwordConfirmation: ""
    });

    const passwordsMatch = formData.password === formData.passwordConfirmation;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const isEmail = emailRegex.test(formData.email);
    const canSubmit = isEmail && passwordsMatch && formData.password.length > 3;

    const handleChange = (fieldName, fieldValue) => {
        handleFormChange(setFormData, fieldName, fieldValue);
    }

    const handleSubmit = async (e) => {
        try {
            const data = await handleFormSubmit(e, formData, "auth/register");
            console.log("Data:", data);
        }
        catch(error) {
            console.log("An Error Occured with POST request:", error);
        }
    }

    return (
        <div className='page-sign'>
            <GoBackButton/>
            <div className='card-sign'>
                <h1 className='title'>Register</h1>
                <form onSubmit={handleSubmit} className='custom-form'>

                    <Input label="Email:" fieldName="email" type="email"
                        placeholder="Enter Your Email" value={formData.email} 
                        handleChange={handleChange} />

                    {(formData.email && !isEmail) &&
                        <p className='error'>Please enter a valid email</p>
                    }

                    <Input label="Password:" fieldName="password" type="password" 
                       placeholder="Enter Your Password" value={formData.password} 
                        handleChange={handleChange} />

                    <Input label="Confirm Password:" fieldName="passwordConfirmation" type="password" 
                     placeholder="Enter Your Confirm Password" value={formData.passwordConfirmation} 
                    handleChange={handleChange} />

                    {!passwordsMatch &&
                        <p className='error'>Passwords do not match</p>
                    }
                    
                    <Input label="Remember me" fieldName="rememberMe" type="checkbox"
                    value={formData.rememberMe}
                    handleChange={handleChange}/>

                    <p className='sign-p'>have an account? <Link href="/login" className='sign-p-link'>log in</Link></p>

                    <button type="submit" className={`${!canSubmit? 'unavailable':''} btn-sign`} 
                            disabled={!canSubmit}>
                        Register
                    </button>
                </form>
            </div>
        </div>
    )
}

export default RegisterPage;