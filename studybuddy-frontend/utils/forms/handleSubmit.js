import post from "../API/post";

const handleSubmit = async (e, canSubmit, setTriedToSubmit, formData, url) => {
    e.preventDefault();
    if(!canSubmit) {
        setTriedToSubmit(true);
        return;
    }
    try {
        const data = await post(formData, url);
        return data;
    }
    catch(error) {
        throw(error);
    }
}

export default handleSubmit;