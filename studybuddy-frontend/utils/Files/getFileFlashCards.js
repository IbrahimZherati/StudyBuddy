import get from "../API/get";
import { notify } from "../notify";

const getFileFlashCards = async (fileId, take) => {
    try {
        const data = await get({
            Id: fileId,
            take
        }, 
        'ClientFile/GetFlashCards');

        return {
            isSuccess: true,
            value: data.value
        }
    }
    catch(error) {
        const errorReason = error?.response?.data?.error;
        console.log("Error requesting file flash cards:", errorReason);

        notify({
            title: "Error",
            message: errorReason,
            error: true,
            sound: false
        })

        return {
            isSuccess: false,
            value: "Sorry. Error occured while generating flash cards. Refreshing the page may fix the issue."
        }
    }
};

export default getFileFlashCards;