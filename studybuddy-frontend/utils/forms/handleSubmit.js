import post from "../API/post";

const handleSubmit = async (e, formData, url) => {
    e.preventDefault();
    try {
        const data = await post(formData, url);
        return data;
    }
    catch(error) {
        throw(error);
    }
}

export default handleSubmit;