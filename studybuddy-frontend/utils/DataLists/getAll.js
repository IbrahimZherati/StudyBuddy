import get from "../API/get";

export default async function getAll(url) {
    try {
        const firstRequestResult = await get({
            skip: 0,
            take: 0
        }, url);
        const dataCount = firstRequestResult.value.count;

        const data = await get({
            skip: 0,
            take: dataCount
        }, url);

        return data.value.data;
    }
    catch(error) {
        console.log("Error fetching data:", error.response.data);
    }
}