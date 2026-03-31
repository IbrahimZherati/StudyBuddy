const handleChange = (setFormData, fieldName, fieldValue) => {
    setFormData(prevState => ({
        ...prevState,
        [fieldName]: fieldValue
    }));
}

export default handleChange;