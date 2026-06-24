import axios from "axios";
import { apiUrl } from "./domainUrl";

const urlPrefix = apiUrl;

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