import NormalNotification from "@/components/AppNotifications/NormalNotification";
import toast from "react-hot-toast";

function playNotificationSound() {
    const audio = new Audio("/sounds/notification.mp3");

    audio.play().catch(() => {
        // Browser may block sound before user interaction
    });
}

export function notify({title, userName, message, href, sound = true}) {
    if (sound) {
        playNotificationSound();
    }

    toast.custom((t) => (
        <NormalNotification
            visible={t.visible}
            title={title}
            userName={userName}
            message={message}
            href={href}
        />
    ));
}