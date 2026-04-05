import get from "../API/get";

const getProfile = async (userId) => {
    try {
        const data = await get({userId}, 'ClientUser/GetProfile');
        return data;
    }
    catch(error) {
        throw(error);
    }
};

export default getProfile;