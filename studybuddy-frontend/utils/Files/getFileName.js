import get from "../API/get";

const getFileName = async (fileId) => {
    try {
        const data = await get(null, `ClientFile/${fileId}`);
        return data.value;
    }
    catch(error) {
        console.log("Error requesting file info:", error?.response?.data);
    }
};

export default getFileName;