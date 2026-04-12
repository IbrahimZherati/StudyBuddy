import axios from "axios";
import get from "../API/get";

export const getProfile = () => get({}, "ClientUser");

export const updateProfile = (data) => {
    return axios.put("http://localhost:5203/api/ClientUser", data, {
        headers: {
            "Content-Type": "multipart/form-data",
        },
    });
};

export const getCountries = () => get({}, "Country");
export const getCities = () => get({}, "City");
export const getUniversities = () => get({}, "University");
export const getMajors = () => get({}, "Major");
//export const getDays = () => get({}, "Day");