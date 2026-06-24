import axios from "axios";
import { apiUrl } from "./domainUrl";

const urlPrefix = apiUrl;

const put = async (reqData, urlSuffix, optionalParam) => {
    const url = urlPrefix + urlSuffix;
    const response = await axios.put(
        url,
        reqData,
        {
            params: {
                ...(optionalParam?.key
                    ? { [optionalParam.key]: optionalParam.value }
                    : {})
            },
            withCredentials: true,
        }
    );
    const data = response.data;
    return data;
};

export default put;