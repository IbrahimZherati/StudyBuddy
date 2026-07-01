import useLocalStorage from "./useLocalStorage";
import { useEffect } from "react";
import getFileSummary from "@/utils/Files/getFileSummary";

export default function useGetSummary(fileId) {
    const [summary, setSummary] = useLocalStorage(`file_summary_${fileId}`, null, true);

    useEffect(() => {
        if(!fileId)
            return;

        if(summary && summary.isSuccess)
            return;
        
        const fetchData = async () => {
            const fileSummary = await getFileSummary(fileId);
            setSummary(fileSummary);
        };
        fetchData();

    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [fileId]);

    return summary?.value;
}