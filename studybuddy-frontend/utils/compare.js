const compare = (obj1, obj2) => {
    if (obj1 == null || obj2 == null)
        return obj1 == obj2;

    if (typeof obj1 !== 'object' || typeof obj2 !== 'object') {
        return String(obj1) === String(obj2);
    }

    if (Array.isArray(obj1) !== Array.isArray(obj2))
        return false;

    if (Array.isArray(obj1)) {
        if (obj1.length !== obj2.length)
            return false;
        return obj1.every((item, index) => compare(item, obj2[index]));
    }

    const keys1 = Object.keys(obj1);
    const keys2 = Object.keys(obj2);

    if (keys1.length !== keys2.length) return false;

    return keys1.every(key => compare(obj1[key], obj2[key]));
}

export default compare;