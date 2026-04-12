import post from "../API/post";

const handleSubmit = async (e, canSubmit, setTriedToSubmit, formData, setFormData, initialState, url) => {
    e.preventDefault();
    if(!canSubmit) {
        setTriedToSubmit(true);
        return;
    }
    
    const data = await post(formData, url);
    setFormData(initialState);
    return data;
}

export default handleSubmit;