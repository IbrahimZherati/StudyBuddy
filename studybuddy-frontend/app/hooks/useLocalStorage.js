"use client";

import { useEffect, useState } from "react";

export default function useLocalStorage(key, initialValue) {
    const [value, setValue] = useState(initialValue);
    const [isLoaded, setIsLoaded] = useState(false);

    useEffect(() => {
        try {
            const stored = localStorage.getItem(key);
            if (stored !== null) {
                // eslint-disable-next-line react-hooks/set-state-in-effect
                setValue(JSON.parse(stored));
            }
        } catch (err) {
            console.error("Error reading localStorage", err);
        }
        setIsLoaded(true);
    }, [key]);

    const setStoredValue = (newValue) => {
        try {
            setValue(newValue);
            localStorage.setItem(key, JSON.stringify(newValue));
        } catch (err) {
            console.error("Error writing localStorage", err);
        }
    };

    return [value, setStoredValue, isLoaded];
}