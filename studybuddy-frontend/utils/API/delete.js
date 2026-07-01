import axios from "axios";
import { apiUrl } from "./domainUrl";

const urlPrefix = apiUrl;

const apiDelete = async (urlSuffix, optionalParam, fullUrl) => {
    const url = fullUrl ?? urlPrefix + urlSuffix;
    const response = await axios.delete(
        url,
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

export default apiDelete;