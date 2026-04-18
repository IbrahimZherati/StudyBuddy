import getId from "@/utils/ClientUser/getId";
import useLocalStorage from "./useLocalStorage";
import { useEffect } from "react";
import useGetUserId from "./useGetUserId";

export default function useGetId() {
    const [id, setId] = useLocalStorage("id", null);
    const userId = useGetUserId();
    
    useEffect(() => {
        if(id)
            return;
        
        const fetchData = async () => {
            if(userId) {
                const id = await getId(userId);
                setId(id);
            }
        };
        fetchData();

    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [id, userId]);

    return id;
}