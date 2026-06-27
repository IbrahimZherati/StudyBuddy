import { defaultProfilePhotoPath, fileFromBase64 } from "./fileHandling";

export const processNotification = (not) => {
    return {
        id: not.id,
        type: not.type,
        from: not.fromClientUserId,
        name: not.fromClientUserName,
        photo: fileFromBase64(not.fromClientPhoto, defaultProfilePhotoPath),
        content: not.description,
        time: not.createDate
    }
};