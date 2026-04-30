import get from "../API/get";

const getProfile = async (userId) => {
    try {
        const data = await get({userId}, 'ClientUser/GetProfile');
        console.log("Profile feteched");
        return data.value;
    }
    catch(error) {
        console.log("Error requesting profile info:", error);
    }
};

export default getProfile;