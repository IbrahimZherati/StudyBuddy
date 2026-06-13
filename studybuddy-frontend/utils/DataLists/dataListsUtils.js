export const findIdByName = (items, name) => {
    if (!name || !items) return null;

    const item = items.find(
        (i) => (i.name || "").toLowerCase() === String(name).toLowerCase()
    );

    return item ? item.id : null;
};

export const getDayIdsFromProfile = (profileDays, dayOptions) => {
    if (!dayOptions || !profileDays)
        return [];

    return profileDays.map(
        (dayName) => findIdByName(dayOptions, dayName)
    );
};