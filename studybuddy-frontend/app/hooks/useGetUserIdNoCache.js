import getUserId from "@/utils/API/getUserId";
import { useEffect, useState } from "react";

export default function useGetUserIdNoCache() {
    const [userId, setUserId] = useState("");
    
    useEffect(() => {
        if(userId)
            return;
        
        const fetchData = async () => {
            const userId = await getUserId();
            setUserId(userId);
        };
        fetchData();

    }, [userId]);

    return userId;
}