import get from "../API/get";

const getProfile = async (userId) => {
    try {
        const data = get({userId}, 'ClientUser/GetProfile');
        return data;
    }
    catch(error) {
        throw(error);
    }
};

export default getProfile;