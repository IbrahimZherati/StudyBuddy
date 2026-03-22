'use client'
import { useState, useEffect } from 'react';
import Input from '@/components/Input';
import handleFormChange from '@/utils/forms/handleChange';
import handleFormSubmit from '@/utils/forms/handleSubmit';
import Link from 'next/link';
import GoBackButton from '@/components/GoBackButton';

const RegisterPage = () => {
    const [formData, setFormData] = useState({
        email: "",
        password: "",
        passwordConfirmation: ""
    });

    const [triedToSubmit, setTriedToSubmit] = useState(false);

    const minimumPasswordLength = 4;
    const passwordLongEnough = formData.password.length >= minimumPasswordLength;
    const passwordsMatch = formData.password === formData.passwordConfirmation;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const isEmail = emailRegex.test(formData.email);
    const canSubmit = isEmail && passwordsMatch && passwordLongEnough;

    const handleFocus = () => {
        setTriedToSubmit(false);
    }

    const handleChange = (fieldName, fieldValue) => {
        handleFormChange(setFormData, fieldName, fieldValue);
    }

    const handleSubmit = async (e) => {
        try {
            const data = await handleFormSubmit(e, canSubmit, setTriedToSubmit, formData, "auth/register");
            if(data)
                console.log("Data:", data);
        }
        catch(error) {
            console.log("An Error Occured with POST request:", error);
        }
    }

    const [isMobile, setIsMobile] = useState(window.matchMedia('(max-width: 768px)').matches);

    useEffect(() => {
        const mediaQuery = window.matchMedia('(max-width: 768px)');
        const handleWindowChange = (e) => setIsMobile(e.matches);
        mediaQuery.addEventListener('change', handleWindowChange);

        return () => mediaQuery.removeEventListener('change', handleWindowChange);
    }, []);

    return (
        <div className='page-sign'>
            <div className='card-sign'>

                {!isMobile && 
                    <GoBackButton/>
                }

                <h1 className='title'>
                    Register
                </h1>

                <form noValidate onSubmit={handleSubmit} className='custom-form'>

                    <Input label="Email:" fieldName="email" type="email"
                        placeholder="Enter Your Email" value={formData.email} 
                        handleFocus={handleFocus}
                        handleChange={handleChange} 
                        hasError={!isEmail}
                        triedToSubmit={triedToSubmit}
                        errorMessage={
                            (triedToSubmit && !isEmail)
                               ? "Please enter a valid email" : ""
                        }      
                    />

                    <Input label="Password:" fieldName="password" type="password" 
                        placeholder="Enter Your Password" value={formData.password} 
                        handleFocus={handleFocus}
                        handleChange={handleChange} 
                        hasError={!passwordLongEnough || !passwordsMatch}
                        triedToSubmit={triedToSubmit} 
                        errorMessage={
                            (triedToSubmit && !passwordLongEnough)
                               ? `Password must be no less than ${minimumPasswordLength} characters` : ""
                        }          
                    />

                    <Input label="Confirm Password:" fieldName="passwordConfirmation" type="password" 
                        placeholder="Confirm Your Password" value={formData.passwordConfirmation} 
                        handleFocus={handleFocus}
                        handleChange={handleChange} 
                        hasError={!passwordLongEnough || !passwordsMatch}
                        triedToSubmit={triedToSubmit}
                        errorMessage={
                            (!passwordsMatch && (!triedToSubmit || passwordLongEnough))
                               ? "Passwords do not match" : ""
                        }            
                    />

                    <div className='flex-col-center'>
                        <p className='sign-p'>Have an account?
                            <Link href="/login" className='sign-p-link'>
                                log in
                            </Link>
                        </p>

                        <button type="submit" className="btn-sign" >
                            Register
                        </button>
                    </div>
                </form>
            </div>
        </div>
    )
}

export default RegisterPage;