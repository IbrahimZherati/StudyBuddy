'use client'
import React, { useState } from 'react';
import Input from '@/components/Auth/Input';
import handleFormChange from '@/utils/forms/handleChange';
import handleFormSubmit from '@/utils/forms/handleSubmit';
import Link from 'next/link';
import GoBackButton from '@/components/Auth/GoBackButton';
import { useRouter } from 'next/navigation';
import { LoaderCircle } from 'lucide-react';

export default function LoginPage() {
    const initialValue = {
        email: "",
        password: "",
        rememberMe: false
    }
    const [formData, setFormData] = useState(initialValue);

    const [triedToSubmit, setTriedToSubmit] = useState(false);

    const [isLoading, setIsLoading] = useState(false);

    const minimumPasswordLength = 4;
    const passwordLongEnough = formData.password.length >= minimumPasswordLength;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const isEmail = emailRegex.test(formData.email);
    const canSubmit = isEmail && passwordLongEnough;

    const handleFocus = () => {
        setTriedToSubmit(false);
    }

    const handleChange = (e) => {
        const {name, value, checked, type} = e.target;
        handleFormChange(setFormData, name, type == "checkbox"? checked: value);
    }

    const router = useRouter();

    const handleSubmit = async (e) => {
        setIsLoading(true);
        try {
            const data = await handleFormSubmit(e, canSubmit, setTriedToSubmit, 
                formData, setFormData, "Auth/Login", "post", initialValue);
                
            if(data)    
                console.log(data.value);
            if (data?.isSuccess)
                router.push('/posts');
        }
        catch (error) {
            console.log("An Error Occured with POST request:", error?.response?.data);
        }
        finally {
            setIsLoading(false);
        }
    }

    return (
        <div className='card-sign'>

            <GoBackButton />

            <h1 className='title'>
                Login
            </h1>

            <form noValidate onSubmit={handleSubmit} className='custom-form'>

                <Input label="Email:" name="email" type="email"
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

                <Input label="Password:" name="password" type="password"
                    placeholder="Enter Your Password" value={formData.password}
                    handleFocus={handleFocus}
                    handleChange={handleChange}
                    hasError={!passwordLongEnough}
                    triedToSubmit={triedToSubmit}
                    errorMessage={
                        (triedToSubmit && !passwordLongEnough)
                            ? `Password must be no less than ${minimumPasswordLength} characters` : ""
                    }
                />

                <Input label="Remember me" name="rememberMe" type="checkbox"
                    optional={true}
                    value={formData.rememberMe}
                    handleChange={handleChange}
                />

                <p className='sign-p'>Don&#39;t have an account?
                    <Link href="/register" className='sign-p-link'>
                        Create one
                    </Link>
                </p>

                <button type="submit" className="btn mx-auto flex-row-center gap-1.5" disabled={isLoading}>
                    Login
                    {isLoading &&
                        <LoaderCircle 
                            className="h-4 w-4 animate-spin
                                     text-white drop-shadow-[0_0_6px_rgba(255,255,255,0.8)]" 
                            strokeWidth={3}
                        />
                    }
                </button>
            </form>
        </div>
    )
}