const urlPrefix = "http://localhost:5203/api/";

const post = async (reqData, urlSuffix) => {
    const url = urlPrefix + urlSuffix;
    const response = await fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(reqData)
    });

    const data = await response.json();
    return data;
};

export default post;