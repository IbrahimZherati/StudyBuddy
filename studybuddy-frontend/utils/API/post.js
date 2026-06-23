import axios from "axios";
import { apiUrl } from "./domainUrl";

const urlPrefix = apiUrl;

const post = async (reqData, urlSuffix, optionalParam) => {
    const url = urlPrefix + urlSuffix;
    try {
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
    }
    catch(error) {
        console.log(error?.response?.data);
    }
};

export default post;