"use client";

import { useEffect, useState } from "react";

export default function useLocalStorage(key, initialValue, cacheResult = true) {
    const [value, setValue] = useState(initialValue);

    useEffect(() => {
        if(!cacheResult)
            return;

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
    }, [key, cacheResult]);

    const setStoredValue = (newValue) => {
        try {
            setValue(newValue);
            if(cacheResult)
                localStorage.setItem(key, JSON.stringify(newValue));
        } 
        catch (error) {
            console.error("Error writing localStorage", error);
        }
    };

    return [value, setStoredValue];
}