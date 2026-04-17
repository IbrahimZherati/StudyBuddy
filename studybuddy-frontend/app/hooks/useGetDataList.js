import getAll from "@/utils/DataLists/getAll";
import useLocalStorage from "./useLocalStorage";
import { useEffect } from "react";

export default function useGetDataList(listName) {
    const [dataList, setDataList] = useLocalStorage(listName, null);

    useEffect(() => {
        if(dataList)
            return;
        
        const fetchData = async () => {
            const dataList = await getAll(listName);
            setDataList(dataList);
        };
        fetchData();

    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [dataList]);

    return dataList;
}