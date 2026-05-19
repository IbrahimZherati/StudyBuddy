import getAll from "@/utils/DataLists/getAll";
import useLocalStorage from "./useLocalStorage";
import { useEffect } from "react";

export default function useGetDataList(listName, param) {
    const [dataList, setDataList] = useLocalStorage(listName, null);

    useEffect(() => {
        if(dataList && dataList.length !== 0)
            return;
        if(param)
            console.log(param);
        // if(param && param.key && !param.value)
        //     return;
        
        const fetchData = async () => {
            const dataList = await getAll(listName, param);
            setDataList(dataList);
        };
        fetchData();

    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [dataList]);

    return dataList;
}