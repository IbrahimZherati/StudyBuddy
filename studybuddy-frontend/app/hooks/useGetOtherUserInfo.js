import useLocalStorage from "./useLocalStorage";
import { useEffect } from "react";
import getProfile from "@/utils/ClientUser/getProfile";
import useGetUserId from "./useGetUserId";

export default function useGetOtherUserInfo(userId, cacheResult = false) {
    const [userInfo, setUserInfo] = useLocalStorage("otherUserInfo", null, cacheResult);

    useEffect(() => {
        if(userInfo && cacheResult)
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