import useLocalStorage from "./useLocalStorage";
import { useEffect } from "react";
import getProfileFromId from "@/utils/ClientUser/getProfileFromId";

export default function useGetOtherUserInfo(Id, cacheResult = false) {
    const [userInfo, setUserInfo] = useLocalStorage(`otherUserInfo_${Id}`, null, cacheResult);

    useEffect(() => {
        if(!Id)
            return;

        if(userInfo && cacheResult)
            return;
        
        const fetchData = async () => {
            const userInfo = await getProfileFromId(Id);
            setUserInfo(userInfo);
        };
        fetchData();

    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [Id, cacheResult]);

    return userInfo;
}