export const defaultProfilePhotoPath = "/images/avatar-default.svg";
export const defaultGroupPhotoPath = "/images/group-default.svg";

export const fileToBase64 = async (file) => {
    const dataUrl = await new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onload = () => resolve(reader.result);
        reader.onerror = () => reject(new Error("Failed to read image file"));
        reader.readAsDataURL(file);
    });

    return String(dataUrl).split(",")[1];
};

export const fileFromBase64 = (photo, defaultPhoto) => {
    if(!photo)
        return defaultPhoto;
    
    if (typeof photo === "string") {
        return photo.startsWith("data:")
            ? photo
            : `data:image/jpeg;base64,${photo}`;
    }

    if (Array.isArray(photo) && photo.length > 0) {
        const binary = photo.map((byte) => String.fromCharCode(byte)).join("");
        return `data:image/jpeg;base64,${btoa(binary)}`;
    }

    return defaultPhoto;
}