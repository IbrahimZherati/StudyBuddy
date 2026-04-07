import get from "./get";

const getUserId = async () => {
    try {
        const data = await get(null, 'Auth/UserInfo');
        return data.userId;
    }
    catch(error) {
        throw(error);
    }
};

export default getUserId;