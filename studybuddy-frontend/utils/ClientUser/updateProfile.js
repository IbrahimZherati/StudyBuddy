import put from "../API/put";

export default async function updateProfile(data) {
    try {
        const response = await put(data, "Client/User");
    }
    catch(error) {
        console.log("Error updating profile info", error);
    }
}