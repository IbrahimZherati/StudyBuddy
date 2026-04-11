import useLocalStorage from "./useLocalStorage";
import { useEffect } from "react";
import getProfile from "@/utils/ClientUser/getProfile";
import useGetUserId from "./useGetUserId";

export default function useGetUserInfo() {
    const [userInfo, setUserInfo] = useLocalStorage("userInfo", null);
    const userId = useGetUserId();

    useEffect(() => {
        if(userInfo)
            return;
        
        const fetchData = async () => {
            if(userId) {
                const userInfo = await getProfile(userId);
                setUserInfo(userInfo);
            }
        };
        fetchData();

    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [userInfo, userId]);

    return userInfo;
}