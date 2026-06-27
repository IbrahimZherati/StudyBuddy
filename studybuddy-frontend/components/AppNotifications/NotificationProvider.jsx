"use client";

import { Toaster } from "react-hot-toast";

export default function NotificationProvider() {
    return (
        <Toaster
            position="top-center"
            toastOptions={{
                duration: 3000,
            }}
        />
    );
}