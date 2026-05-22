import useLocalStorage from "./useLocalStorage";
import { useEffect } from "react";
import getProfile from "@/utils/ClientUser/getProfile";
import useGetUserId from "./useGetUserId";

export default function useGetUserInfo(cacheResult = true, infoChanged = false) {
    const [userInfo, setUserInfo] = useLocalStorage("userInfo", null, cacheResult);
    const userId = useGetUserId();
    console.log(infoChanged);
    useEffect(() => {
        const fetchData = async () => {
            if(userId) {
                const userInfo = await getProfile(userId);
                setUserInfo(userInfo);
            }
        };
        fetchData();

    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [userId, infoChanged]);

    return [userInfo, setUserInfo];
}