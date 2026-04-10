'use client'
import { useState } from 'react';
import Input from '@/components/Auth/Input';
import handleFormChange from '@/utils/forms/handleChange';
import handleFormSubmit from '@/utils/forms/handleSubmit';
import Link from 'next/link';
import GoBackButton from '@/components/Auth/GoBackButton';

const RegisterPage = () => {
    const initialValue = {
        email: "",
        userName: "",
        password: "",
        passwordConfirmation: ""
    }

    const [formData, setFormData] = useState(initialValue);

    const [triedToSubmit, setTriedToSubmit] = useState(false);

    const minimumPasswordLength = 4;
    const passwordLongEnough = formData.password.length >= minimumPasswordLength;
    const passwordsMatch = formData.password === formData.passwordConfirmation;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const isEmail = emailRegex.test(formData.email);
    const minimumUserNameLength = 3;
    const userNameLongEnough = formData.userName.length >= minimumUserNameLength;
    const canSubmit = isEmail && userNameLongEnough && passwordsMatch && passwordLongEnough;

    const handleFocus = () => {
        setTriedToSubmit(false);
    }

    const handleChange = (fieldName, fieldValue) => {
        handleFormChange(setFormData, fieldName, fieldValue);
    }

    const handleSubmit = async (e) => {
        try {
            const data = await handleFormSubmit(e, canSubmit, setTriedToSubmit, 
                formData, setFormData, initialValue, "auth/register");
                
            if (data)
                console.log("Data:", data);
        }
        catch (error) {
            console.log("An Error Occured with POST request:", error);
        }
    }

    return (
        <section className='card-sign'>
            <div className='hidden md:block'>
                <GoBackButton />
            </div>

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

                <Input label="User Name:" fieldName="userName" type="text"
                    placeholder='Enter Your Name' value={formData.userName}
                    handleFocus={handleFocus}
                    handleChange={handleChange}
                    hasError={!userNameLongEnough}
                    triedToSubmit={triedToSubmit}
                    errorMessage={
                        (triedToSubmit && !userNameLongEnough)
                            ? `User Name must be no less than ${minimumUserNameLength} characters` : "" 
                    }
                    note="User Name is going to be public. Please do not add any personal info."
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

                    <button type="submit" className="btn" >
                        Register
                    </button>
                </div>
            </form>
        </section>
    )
}

export default RegisterPage;