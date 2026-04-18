import useLocalStorage from "./useLocalStorage";
import { useEffect } from "react";
import getGroupInfo from "@/utils/Chat/GroupChat/getGroupInfo";

export default function useGetGroupInfo(groupId) {
    const [groupInfo, setGroupInfo] = useLocalStorage(`groupInfo:${groupId}`, null);

    useEffect(() => {
        if(groupInfo)
            return;
        
        const fetchData = async () => {
            const groupInfo = await getGroupInfo(groupId);
            setGroupInfo(groupInfo);
        };
        fetchData();

    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [groupInfo, groupId]);

    return groupInfo;
}