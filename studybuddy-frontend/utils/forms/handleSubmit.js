import post from "../API/post";
import put from "../API/put";

const handleSubmit = async (e, canSubmit, setTriedToSubmit, formData, setFormData, url, method, initialState) => {
    e.preventDefault();
    if(!canSubmit) {
        setTriedToSubmit(true);
        return;
    }
    
    const data = await (method === "post"?  post(formData, url): put(formData, url));
    if(initialState)
        setFormData(initialState);
    return data;
}

export default handleSubmit;