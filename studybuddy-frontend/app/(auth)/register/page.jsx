'use client'
import { useState } from 'react';
import Input from '@/components/Auth/Input';
import Select from '@/components/Auth/Select';
import handleFormChange from '@/utils/forms/handleChange';
import handleFormSubmit from '@/utils/forms/handleSubmit';
import Link from 'next/link';
import GoBackButton from '@/components/Auth/GoBackButton';
import { useRouter } from 'next/navigation';
import useGetDataList from '@/app/hooks/useGetDataList';
import { LoaderCircle } from 'lucide-react';

export default function RegisterPage() {
    const initialValue = {
        email: "",
        userName: "",
        majorId: null,
        password: "",
        passwordConfirmation: ""
    }

    const [formData, setFormData] = useState(initialValue);

    const [triedToSubmit, setTriedToSubmit] = useState(false);

    const [isLoading, setIsLoading] = useState(false);

    const minimumPasswordLength = 4;
    const passwordLongEnough = formData.password.length >= minimumPasswordLength;
    const passwordsMatch = formData.password === formData.passwordConfirmation;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const isEmail = emailRegex.test(formData.email);
    const majorSelected = !formData.majorId? false: true;
    const minimumUserNameLength = 3;
    const userNameLongEnough = formData.userName.length >= minimumUserNameLength;
    const canSubmit = isEmail && majorSelected && userNameLongEnough && passwordsMatch && passwordLongEnough;

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
                formData, setFormData, "Auth/Register", "post", initialValue);

            if (data)
                console.log("Data:", data);
            if (data?.isSuccess)
                router.push('/login');
        }
        catch (error) {
            console.log("An Error Occured with POST request:", error?.response?.data);
        }
        finally {
            setIsLoading(false);
        }
    }

    const majors = useGetDataList("Auth/GetMajors");

    return (
        <section className='card-sign max-w-130'>
            <div className='hidden md:block'>
                <GoBackButton />
            </div>

            <h1 className='title'>
                Register
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

                <Input label="User Name:" name="userName" type="text"
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

                <Select label="Major:" name="majorId"
                    placeholder="Select Major" value={formData.majorId}
                    handleFocus={handleFocus}
                    handleChange={handleChange}
                    options={majors || []}
                    hasError={!majorSelected}
                    triedToSubmit={triedToSubmit}
                    errorMessage={
                        (triedToSubmit && !majorSelected)? "Please select your major": ""
                    }
                />

                <Input label="Password:" name="password" type="password"
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

                <Input label="Confirm Password:" name="passwordConfirmation" type="password"
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

                    <button type="submit" className="btn mx-auto flex-row-center gap-1.5" disabled={isLoading}>
                        <span>
                            Register
                        </span>
                        
                        {isLoading &&
                            <LoaderCircle
                                className="h-4 w-4 animate-spin
                                        text-white drop-shadow-[0_0_6px_rgba(255,255,255,0.8)]" 
                                strokeWidth={3}
                            />
                        }
                    </button>
                </div>
            </form>
        </section>
    )
}