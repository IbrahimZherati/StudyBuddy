import get from "../API/get";

const getProfileFromId = async (clientId) => {
    try {
        const data = await get({clientId}, 'ClientUser/GetProfileByClientId');
        return data.value;
    }
    catch(error) {
        console.log("Error requesting profile info:", error?.response?.data);
    }
};

export default getProfileFromId;