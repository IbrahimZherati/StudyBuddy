"use client";

import { useEffect, useState } from "react";
import useGetUserIdNoCache from "./useGetUserIdNoCache";

export default function useLocalStorage(key, initialValue, cacheResult = true) {
    const [value, setValue] = useState(initialValue);

    const userId = useGetUserIdNoCache();
    const uniqueKey = key + "_" + userId;

    useEffect(() => {
        if(!cacheResult || !userId)
            return;

        try {
            const stored = localStorage.getItem(uniqueKey);
            if (stored) {
                // eslint-disable-next-line react-hooks/set-state-in-effect
                setValue(JSON.parse(stored));
            }
        } 
        catch (error) {
            console.error("Error reading localStorage", error);
        }
    }, [uniqueKey, cacheResult, userId]);

    const setStoredValue = (newValue) => {
        try {
            setValue(newValue);
            if(cacheResult) {
                localStorage.setItem(uniqueKey, JSON.stringify(newValue));
            }
        } 
        catch (error) {
            console.error("Error writing localStorage", error);
        }
    };

    return [value, setStoredValue];
}