import get from "./get";

const getId = async (userId) => {
    try {
        const data = await get({userId}, 'ClientUser/GetProfile');
        return data.value.id;
    }
    catch(error) {
        console.log("Error requesting user's Id:", error);
    }
};

export default getId;