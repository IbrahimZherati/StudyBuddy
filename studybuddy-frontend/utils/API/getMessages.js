import get from "./get";

const getMessages = async (toId, skip, take, orderBy = 1) => {
    try {
        const messages = await get({
            SecondClientId: toId,
            skip,
            take,
            orderBy
        }, 'Message');

        return [...messages.value.data].reverse();
    }
    catch(error) {
        console.log("Error requesting messages", error);
        return [];
    }
};

export default getMessages;