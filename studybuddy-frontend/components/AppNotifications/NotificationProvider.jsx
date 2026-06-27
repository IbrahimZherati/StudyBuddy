"use client";

import { Toaster } from "react-hot-toast";
import NotificationLimiter from "./NotificationLimiter";

export default function NotificationProvider() {
    return (
        <>
            <Toaster
                position="top-center"
                toastOptions={{
                    duration: 5000,
                }}
            />
            <NotificationLimiter />
        </>
    );
}