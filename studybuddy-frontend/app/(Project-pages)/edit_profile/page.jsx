'use client';

import React, { useState, useEffect, useRef, useMemo } from 'react';
import InputField from '@/components/Profile/EditProfile/InputField';
import ImageUpload from '@/components/Profile/ImageUpload';
import SelectField from '@/components/Auth/SelectField';
import AdjustAvailableDays from '@/components/Profile/EditProfile/AdjustAvailableDays';
import StudyInterests from '@/components/Profile/EditProfile/StudyInterests';
import handleFormChange from '@/utils/forms/handleChange';
import useGetDataList from '@/app/hooks/useGetDataList';
import useGetUserInfo from '@/app/hooks/useGetUserInfo';
import updateProfile from '@/utils/ClientUser/updateProfile';
import useLocalStorage from '@/app/hooks/useLocalStorage';
import Loading from '@/components/Loading';

export default function EditProfile() {

    const [isSaving, setIsSaving] = useState(false);

    const [form, setForm] = useState({
        userName: "",
        bio: "",
        majorId: null,
        universityId: null,
        cityId: null,
        countryId: null,
        gender: true,
        photo: null,
        availableDays: [],
        studyInterests: []
    });

    const data = {
        universities: useGetDataList("University"),
        countries: useGetDataList("Country"),
        cities: useGetDataList("City"),
        majors: useGetDataList("Major"),
        days: useGetDataList("Day")
    }

    // ================= HELPERS =================

    const findIdByName = (items, name) => {
        if (!name) return "";

        const item = items.find(
            (i) => (i.name || "").toLowerCase() === String(name).toLowerCase()
        );

        return item ? String(item.id) : "";
    };

    const getDayIdsFromProfile = (profileDays) => {
        if (!data.days)
            return [];

        return profileDays.map(
            (dayName) => findIdByName(data.days, dayName)
        );
    };

    const profilePhotoPreview = useMemo(() => {
        const photo = form?.photo;

        if (!photo)
            return "/images/avatar-default.svg";

        if (typeof photo === "string") {
            return photo.startsWith("data:")
                ? photo
                : `data:image/jpeg;base64,${photo}`;
        }

        if (Array.isArray(photo) && photo.length > 0) {
            const binary = photo.map((byte) => String.fromCharCode(byte)).join("");
            return `data:image/jpeg;base64,${btoa(binary)}`;
        }

        return "/images/avatar-default.svg";
    }, [form?.photo]);

    // ================= FETCH =================

    const profile = useGetUserInfo();
    // console.log("Component Rendered");

    const processProfile = () => {
        if (!profile)
            return null;
        console.log("Profile", profile);

        return {
            userName: profile.userName,
            bio: profile.bio,
            majorId: findIdByName(data.majors, profile.major),
            universityId: findIdByName(data.universities, profile.university),
            cityId: findIdByName(data.cities, profile.city),
            countryId: findIdByName(data.countries, profile.country),
            gender: profile.gender,
            photo: profile.photo,
            availableDays: getDayIdsFromProfile(profile.avaiableDays), //REMEMBER TO FIX THIS LATER!!!!!
            studyInterests: profile.studyInterests
        }
    }

    const isFirstLoadOfSaved = useRef("true");
    const isFirstLoadOfCurrent = useRef("true");
    const [savedChanges, setSavedChanges] = useLocalStorage("editProfileChanges", null);

    useEffect(() => {
        if (isFirstLoadOfSaved.current && savedChanges) {
            console.log("Loaded saved changes");
            setForm(savedChanges);

            isFirstLoadOfSaved.current = false;
            isFirstLoadOfCurrent.current = false;
        }
        else if (isFirstLoadOfCurrent.current && profile) {
            console.log("Loaded current profile info");
            const processedProfile = processProfile();
            setForm(processedProfile);

            isFirstLoadOfCurrent.current = false;
        }

    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [savedChanges, profile, isFirstLoadOfSaved, isFirstLoadOfCurrent]);

    // let lastSaveRef = useRef(Date.now());
    // const timeNow = Date.now();
    // if((timeNow - lastSaveRef.current) / 1000 >= 10) {
    //     console.log("Saving Chnages now...");
    //     setSavedChanges(form);
    //     lastSaveRef.current = timeNow;
    // }

    // ================= HANDLERS =================
    const handleChange = (e) => {
        const { name, value } = e.target;
        handleFormChange(setForm, name, value);
    };

    const fileToBase64 = async (file) => {
        const dataUrl = await new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.onload = () => resolve(reader.result);
            reader.onerror = () => reject(new Error("Failed to read image file"));
            reader.readAsDataURL(file);
        });

        return String(dataUrl).split(",")[1];
    };

    // ================= SUBMIT =================

    const handleSubmit = async () => {
        if (isSaving)
            return;

        try {
            setIsSaving(true);

            // if (form.photo instanceof File) {
            //     payload.photo = await fileToBase64(form.photo);
            // }

            const processedForm = Object.fromEntries(
                Object.entries(form).map(([key, value]) => {
                    return value ? [key, value] : [key, null];
                })
            )

            await updateProfile(processedForm);
        }
        catch (error) {
            console.log("Error updating your profile:", error);
        }
        finally {
            setIsSaving(false);
        }
    };

    // ================= UI =================

    let isDataStillLoading = false;
    for (const [, value] of Object.entries(data)) {
        if (!value)
            isDataStillLoading = true;
    }
    if (!form)
        isDataStillLoading = true;

    if (isDataStillLoading)
        return <Loading />

    return (
        <div className="p-6">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-10">

                {/* LEFT */}
                <div className="flex flex-col gap-6">
                    <ImageUpload
                        onChange={handleChange}
                        initialPreview={profilePhotoPreview}
                    />

                    <InputField
                        label="Bio"
                        name="bio"
                        placeholder="Enter Your Bio"
                        value={form.bio}
                        onChange={handleChange}
                    />

                    <StudyInterests
                        value={form.studyInterests}
                        onChange={handleChange}
                    />

                    <AdjustAvailableDays
                        value={form.availableDays}
                        dayOptions={data.days}
                        onChange={handleChange}
                    />
                </div>

                {/* RIGHT */}
                <div className="flex flex-col gap-6">

                    <InputField
                        label="Name"
                        name="userName"
                        placeholder="Enter Your Name"
                        value={form.userName}
                        onChange={handleChange}
                    />

                    <SelectField
                        label="Major"
                        name="majorId"
                        placeholder="Select Major"
                        value={form.majorId}
                        options={data.majors}
                        onChange={handleChange}
                    />

                    <SelectField
                        label="University"
                        name="universityId"
                        placeholder="Select University"
                        value={form.universityId}
                        options={data.universities}
                        onChange={handleChange}
                    />

                    <SelectField
                        label="Country"
                        name="countryId"
                        placeholder="Select Country"
                        value={form.countryId}
                        options={data.countries}
                        onChange={handleChange}
                    />

                    <SelectField
                        label="City"
                        name="cityId"
                        placeholder="Select City"
                        value={form.cityId}
                        options={[]}
                        onChange={handleChange}
                    />

                    <SelectField
                        label="Gender"
                        name="gender"
                        placeholder="Select Gender"
                        value={form.gender}
                        options={[
                            { id: true, name: "Male" },
                            { id: false, name: "Female" }
                        ]}
                        onChange={handleChange}
                    />

                    <button onClick={handleSubmit} disabled={isSaving}
                        className={`btn mr-0 ${isSaving ? "disabled" : ""}`}
                    >
                        {isSaving ? "Saving..." : "Save"}
                    </button>

                </div>
            </div>
        </div>
    );
}