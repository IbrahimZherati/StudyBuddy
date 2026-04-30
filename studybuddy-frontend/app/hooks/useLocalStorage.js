"use client";

import { useEffect, useState } from "react";

export default function useLocalStorage(key, initialValue) {
    const [value, setValue] = useState(initialValue);

    useEffect(() => {
        try {
            const stored = localStorage.getItem(key);
            if (stored) {
                // eslint-disable-next-line react-hooks/set-state-in-effect
                setValue(JSON.parse(stored));
            }
        } 
        catch (error) {
            console.error("Error reading localStorage", error);
        }
    }, [key]);

    const setStoredValue = (newValue) => {
        try {
            setValue(newValue);
            localStorage.setItem(key, JSON.stringify(newValue));
        } 
        catch (error) {
            console.error("Error writing localStorage", error);
        }
    };

    return [value, setStoredValue];
}