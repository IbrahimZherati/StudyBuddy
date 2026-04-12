import get from "./get";

const getUserId = async () => {
    try {
        const data = await get(null, 'Auth/UserInfo');
        return data.userId;
    }
    catch(error) {
        console.log("Error requesting user's userId:", error);
    }
};

export default getUserId;