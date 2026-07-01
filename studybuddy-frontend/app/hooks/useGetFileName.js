import useLocalStorage from "./useLocalStorage";
import { useEffect } from "react";
import getFileName from "@/utils/Files/getFileName";

export default function useGetFileName(Id) {
    const [fileName, setFileName] = useLocalStorage(`fileName_${Id}`, null);

    useEffect(() => {
        if(!Id)
            return;

        if(fileName)
            return;
        
        const fetchData = async () => {
            const fileInfo = await getFileName(Id);
            setFileName(fileInfo.title);
        };
        fetchData();

    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [Id]);

    return fileName;
}