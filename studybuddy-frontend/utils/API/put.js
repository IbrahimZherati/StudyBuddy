import axios from "axios";
import { apiUrl } from "./domainUrl";

const urlPrefix = apiUrl;

const put = async (reqData, urlSuffix, optionalParam) => {
    const url = urlPrefix + urlSuffix;
    try {
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
    }
    catch(error) {
        console.log(error?.response?.data);
    }
};

export default put;