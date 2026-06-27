"use client";

import { useEffect } from "react";
import toast, { useToasterStore } from "react-hot-toast";

const TOAST_LIMIT = 3;

export default function NotificationLimiter() {
    const { toasts } = useToasterStore();

    useEffect(() => {
        const visibleToasts = toasts.filter((t) => t.visible);

        visibleToasts
        .slice(TOAST_LIMIT)
        .forEach((t) => toast.dismiss(t.id));
    }, [toasts]);

    return null;
}