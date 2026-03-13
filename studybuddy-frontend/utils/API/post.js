import axios from "axios";

const urlPrefix = "http://localhost:5203/api/";

const post = async (reqData, urlSuffix) => {
    const url = urlPrefix + urlSuffix;
    const response = await axios.post(url, reqData);
    const data = response.data;
    return data;
};

export default post;