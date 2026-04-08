import getUserId from "@/utils/API/getUserId";
import useLocalStorage from "./useLocalStorage";
import { useEffect } from "react";

export default function useGetUserId() {
    const [userId, setUserId, isLoaded] = useLocalStorage("userId", null);
    
    useEffect(() => {
        if(userId)
            return;
        
        const fetchData = async () => {
            const userId = await getUserId();
            setUserId(userId);
        };
        fetchData();

    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [userId]);

    return userId;
}