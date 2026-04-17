import get from "../../API/get";

const getGroupInfo = async (groupId) => {
    try {
        const data = await get({groupId}, `GroupChat/${groupId}`);
        return data.value.id;
    }
    catch(error) {
        console.log("Error requesting group's Id:", error);
    }
};

export default getGroupInfo;