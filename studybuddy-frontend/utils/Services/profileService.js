import get from "../API/get";
import put from "../API/put";
import getUserId from "../API/getUserId";

const unwrapValue = (response) => response?.value ?? response?.Value ?? response;

const unwrapList = (response) => {
    const value = unwrapValue(response);
    return value?.data ?? value?.Data ?? [];
};

const getList = (endpoint) => async () => {
  return unwrapList(await get({}, endpoint));
};

export const getProfile = async () => {
    const userId = await getUserId();
    if (!userId) return null;

    const response = await get({ userId }, "ClientUser/GetProfile");
    return unwrapValue(response);
};

export const updateProfile = (data) => put(data, "ClientUser");

export const getCountries = getList("Country");
export const getCities = getList("City");
export const getUniversities = getList("University");
export const getMajors = getList("Major");
export const getDays = getList("Day");
