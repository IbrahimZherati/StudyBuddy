import post from "../API/post";

const handleSubmit = async (e, formData, url) => {
    e.preventDefault();
    const data = await post(formData, url);
    return data;
}

export default handleSubmit;