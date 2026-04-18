import axios from "axios";

const urlPrefix = "http://localhost:5203/api/";

const put = async (reqData, urlSuffix) => {
    const url = urlPrefix + urlSuffix;
    const response = await axios.put(url, reqData, { withCredentials: true });
    const data = response.data;
    return data;
};

export default put;
