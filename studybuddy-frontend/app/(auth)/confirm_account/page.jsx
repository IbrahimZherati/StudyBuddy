"use client"
import post from '@/utils/API/post';
import React, { useState } from 'react'
import { useSearchParams } from "next/navigation";
import { LoaderCircle } from 'lucide-react';

export default function ConfirmAccount() {

    const searchParams = useSearchParams();
    const email = searchParams.get("email");

    const [isLoading, setIsLoading] = useState(false);

    const [resendResult, setResendResult] = useState("");

    const resendEmail = async () => {
        if (!email)
            return;

        setResendResult("");
        setIsLoading(true);
        try {
            const data = await post(null, "Auth/SendToken", {
                key: "email",
                value: email
            })

            if (data?.isSuccess) {
                setResendResult("Email was resent");
            }
        }
        catch (error) {
            console.log("An Error Occured with POST request:", error?.response?.data);
            setResendResult("Error occured, email was not resent");
        }
        finally {
            setIsLoading(false);
        }
    }

    return (
        <div className="flex-col-center gap-8 text-amber-100 text-2xl">
            <div className="flex-col-center gap-5">
                <h1>Welcome aboard, <i className="font-semibold">buddy</i>!</h1>
                <h1>An Email was sent to your inbox. Please check it to confirm your account</h1>
            </div>

            <div className="flex-col-center gap-2">
                <h2>Couldn&apos;t find it? Please check your spam inbox</h2>
                <h2>Still not there?</h2>
            </div>

            <div className="flex-col-center gap-2">
                <button
                    onClick={resendEmail}
                    className={`btn mx-auto flex-row flex-row-center gap-1.5 w-32 text-[1.2rem]
                                ${isLoading ? "disabled" : ""}
                    `}
                    disabled={isLoading}
                >
                    <span>
                        Resend
                    </span>

                    {isLoading &&
                        <LoaderCircle
                            className="h-4 w-4 animate-spin
                                        text-white drop-shadow-[0_0_6px_rgba(255,255,255,0.8)]"
                            strokeWidth={3}
                        />
                    }
                </button>

                <div className={`flex-col-center text-lg text-sky-200
                                ${resendResult? "visible": "invisible"}`
                }>
                    <i>
                        {resendResult || "placeholder"}
                    </i>
                </div>

            </div>
        </div>
    )
}
