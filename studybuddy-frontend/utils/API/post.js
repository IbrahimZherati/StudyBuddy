import axios from "axios";
import { apiUrl } from "./domainUrl";

const urlPrefix = apiUrl;

const post = async (reqData, urlSuffix, optionalParam, fullUrl=undefined) => {
    const url = fullUrl ?? urlPrefix + urlSuffix;
    const response = await axios.post(
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

export default post;