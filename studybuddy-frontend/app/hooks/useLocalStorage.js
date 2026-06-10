"use client";

import { useEffect, useState } from "react";
import useGetUserIdNoCache from "./useGetUserIdNoCache";

export default function useLocalStorage(key, initialValue, cacheResult = true) {
    const [value, setValue] = useState(initialValue);
    const [isLoaded, setIsLoaded] = useState(false);

    const userId = useGetUserIdNoCache();
    const uniqueKey = key + "_" + userId;

    useEffect(() => {
        let cancelled = false;

        Promise.resolve().then(() => {
            if (cancelled) {
                return;
            }

            if (!cacheResult) {
                setIsLoaded(true);
                return;
            }

            if (!userId) {
                return;
            }

            if (cacheResult && userId) {
                try {
                    const stored = localStorage.getItem(uniqueKey);
                    if (stored) {
                        setValue(JSON.parse(stored));
                    }
                } 
                catch (error) {
                    console.error("Error reading localStorage", error);
                }
            }

            if (!cancelled) {
                setIsLoaded(true);
            }
        });

        return () => {
            cancelled = true;
        };
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

    return [value, setStoredValue, isLoaded];
}