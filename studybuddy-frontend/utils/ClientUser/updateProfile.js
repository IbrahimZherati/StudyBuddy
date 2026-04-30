import put from "../API/put";

export default async function updateProfile(data) {
    try {
        const response = await put(data, "ClientUser");
    }
    catch(error) {
        console.log("Error updating profile info", error.response.data);
    }
}