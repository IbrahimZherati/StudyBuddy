import axios from "axios";

const urlPrefix = "http://localhost:5203/api/";

const get = async (reqData, urlSuffix, optionalParam) => {
    const url = urlPrefix + urlSuffix;
    const response = await axios.get(url, {
        params: {...reqData, ...(optionalParam?.key ? {[optionalParam.key]:optionalParam.value}: {})},
        withCredentials:true,
    });
    const data = response.data;
    return data;
};

export default get;