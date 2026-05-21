import get from "../API/get";

export default async function getAll(url, param) {
    console.log(`Fetching ${url} ${param? `with param = ${param.value}`: ""}`);
    let data;
    try {
        const firstRequestResult = await get({
            skip: 0,
            take: 0
        }, url, param);
        const dataCount = firstRequestResult.value.count;

        data = await get({
            skip: 0,
            take: dataCount < 1000? dataCount: 1000
        }, url, param);
    }
    catch(error) {
        console.log("Error fetching data:", error);
    }

    try {
        return data?.value?.data || [];
    }
    catch(error) {
        console.log("Error extracting data:", error);
    }
}