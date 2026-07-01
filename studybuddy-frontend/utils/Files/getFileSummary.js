import get from "../API/get";
import { notify } from "../notify";

const getFileSummary = async (fileId) => {
    try {
        const data = await get({
            Id: fileId
        }, 
        'ClientFile/GetSummary');

        return {
            isSuccess: true,
            value: data.value
        }
    }
    catch(error) {
        const errorReason = error?.response?.data?.error;
        console.log("Error requesting file summary:", errorReason);

        notify({
            title: "Error",
            message: errorReason,
            error: true,
            sound: false
        })

        return {
            isSuccess: false,
            value: "Sorry. Error occured while generating summary. Refreshing the page may fix the issue."
        }
    }
};

export default getFileSummary;