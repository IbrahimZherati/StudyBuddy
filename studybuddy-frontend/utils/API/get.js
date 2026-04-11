import axios from "axios";

const urlPrefix = "http://localhost:5203/api/";

const get = async (reqData, urlSuffix) => {
    const url = urlPrefix + urlSuffix;
    const response = await axios.get(url, {
        params: {...reqData},
        withCredentials:true,
    });
    const data = response.data;
    return data;
};

export default get;