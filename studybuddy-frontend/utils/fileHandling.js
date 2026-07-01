export const defaultProfilePhotoPath = "/images/avatar-default.svg";
export const defaultGroupPhotoPath = "/images/group-default.svg";

export const fileToBase64 = (file) => {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();

        reader.onload = () => {
            const result = reader.result;
            const base64 = result.split(",")[1];
            resolve(base64);
        };

        reader.onerror = reject;

        reader.readAsDataURL(file);
    });
}

export const fileFromBase64 = (photo, defaultPhoto) => {
    if (!photo)
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