import useLocalStorage from "./useLocalStorage";
import { useEffect } from "react";
import getFileFlashCards from "@/utils/Files/getFileFlashCards";

export default function useGetFlashCards(fileId, take) {
    const [flashCards, setFlashCards] = useLocalStorage(`file_flashCards_${fileId}`, null, true);

    useEffect(() => {
        if(!fileId)
            return;

        if(flashCards && flashCards.isSuccess && flashCards.value.length == take)
            return;
        
        const fetchData = async () => {
            const fileFlashCards = await getFileFlashCards(fileId, take);
            setFlashCards(fileFlashCards);
        };
        fetchData();

    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [fileId, take]);

    const refresh = async () => {
        const fileFlashCards = await getFileFlashCards(fileId, take);
        setFlashCards(fileFlashCards);
    }

    return [flashCards?.value, refresh];
}