'use client'
import React, { useState } from 'react';
import Input from '@/components/Auth/Input';
import handleFormChange from '@/utils/forms/handleChange';
import handleFormSubmit from '@/utils/forms/handleSubmit';
import Link from 'next/link';
import GoBackButton from '@/components/Auth/GoBackButton';
import getProfile from '@/utils/ClientUser/getProfile';


const LoginPage = () => {
    const [formData, setFormData] = useState({
        email: "",
        password: "",
        rememberMe: false
    });

    const [triedToSubmit, setTriedToSubmit] = useState(false);

    const minimumPasswordLength = 4;
    const passwordLongEnough = formData.password.length >= minimumPasswordLength;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const isEmail = emailRegex.test(formData.email);
    const canSubmit = isEmail && passwordLongEnough;

    const handleFocus = () => {
        setTriedToSubmit(false);
    }

    const handleChange = (fieldName, fieldValue) => {
        handleFormChange(setFormData, fieldName, fieldValue);
    }

    const handleSubmit = async (e) => {
        try {
            const data = await handleFormSubmit(e, canSubmit, setTriedToSubmit, formData, "auth/login");
            console.log(data.value);
            const userData = await getProfile(data.value);
            console.log("Data", userData);
            localStorage.setItem('id', userData.value.id);
        }
        catch (error) {
            console.log("An Error Occured with POST request:", error);
        }
    }

    return (
        <div className='card-sign'>

            <GoBackButton />

            <h1 className='title'>
                Login
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
                    hasError={!passwordLongEnough}
                    triedToSubmit={triedToSubmit}
                    errorMessage={
                        (triedToSubmit && !passwordLongEnough)
                            ? `Password must be no less than ${minimumPasswordLength} characters` : ""
                    }
                />

                <Input label="Remember me" fieldName="rememberMe" type="checkbox"
                    optional={true}
                    value={formData.rememberMe}
                    handleChange={handleChange}
                />

                <p className='sign-p'>Don&#39;t have an account?
                    <Link href="/register" className='sign-p-link'>
                        Create one
                    </Link>
                </p>

                <button type="submit" className="btn">
                    Login
                </button>
            </form>
        </div>
    )
}

export default LoginPage;