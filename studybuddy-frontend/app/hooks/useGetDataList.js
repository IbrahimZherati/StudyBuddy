import getAll from "@/utils/DataLists/getAll";
import useLocalStorage from "./useLocalStorage";
import { useEffect } from "react";

export default function useGetDataList(listName, param) {
    const cacheKey = param? `${listName}_${param.key}_${param.value}`: listName;
    const [dataList, setDataList] = useLocalStorage(cacheKey, null);

    useEffect(() => {        
        const fetchData = async () => {
            const dataList = await getAll(listName, param);
            setDataList(dataList);
        };
        fetchData();

    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [cacheKey, param?.value]);

    return dataList;
}