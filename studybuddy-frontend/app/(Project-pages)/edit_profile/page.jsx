'use client';

import React, { useState, useEffect } from 'react';
import InputField from '@/components/Profile/EditProfile/InputField';
import ImageUpload from '@/components/Profile/ImageUpload';
import SelectField from '@/components/Auth/SelectField';
import AdjustAvailableDays from '@/components/Profile/EditProfile/AdjustAvailableDays';
import AddStudyInterests from '@/components/Profile/EditProfile/AddStudyInterests';
import handleFormChange from '@/utils/forms/handleChange';
import useGetDataList from '@/app/hooks/useGetDataList';
import useGetUserInfo from '@/app/hooks/useGetUserInfo';
import updateProfile from '@/utils/ClientUser/updateProfile';
import useLocalStorage from '@/app/hooks/useLocalStorage';

export default function EditProfile() {

    const [isSaving, setIsSaving] = useState(false);

    const [form, setForm] = useState({
        userName: "",
        bio: "",
        majorId: "",
        universityId: "",
        cityId: "",
        countryId: "",
        gender: "",
        photo: null,
        availableDays: [],
        studyInterests: []
    });

    const data = {
        universities:useGetDataList("University"),
        countries:useGetDataList("Country"),
        cities:useGetDataList("City"),
        majors:useGetDataList("Major"),
        days:useGetDataList("Day")
    }

    const profile = useGetUserInfo();

    // ================= HELPERS =================

    const findIdByName = (items, name) => {
        if (!name) return "";

        const item = items.find(
            (i) => (i.name || "").toLowerCase() === String(name).toLowerCase()
        );

        return item ? String(item.id) : "";
    };

    const getDayIdsFromProfile = (profileDays, dayOptions) => {
        if (!Array.isArray(profileDays)) return [];

        return profileDays.map((dayName) => {
            const day = dayOptions.find(
                d => (d.name || "").toLowerCase() === String(dayName).toLowerCase()
            );
            return day ? day.id : null;
        }).filter((id) => id !== null);
    };

    const getProfilePhotoPreview = (photo) => {
        if (!photo) return "/images/avatar-default.svg";

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
    };

    const [savedChanges, setSavedChanges] = useLocalStorage("editProfileChanges", null);

    useEffect(() => {
        setForm(savedChanges || profile);
        setSavedChanges(form);
    }, [savedChanges, profile, setSavedChanges, form]);

    // ================= FETCH =================

    useEffect(() => {
        async function fetchData() {
            const profileData = profile || {};
            
            setForm((prev) => ({
                ...prev,
                userName: profileData.userName || "",
                bio: profileData.bio || "",
                majorId: findIdByName(data.majors, profileData.major),
                universityId: findIdByName(data.universities, profileData.university),
                cityId: findIdByName(data.cities, profileData.city),
                countryId: findIdByName(data.countries, profileData.country),
                gender: String(profileData.gender ?? true),
                availableDays: getDayIdsFromProfile(
                    profileData.avaiableDays || profileData.availableDays,
                    data.days
                ),
                studyInterests: profileData.studyInterests || [],
            }));            
        }

        fetchData();
    }, []);

    // ================= HANDLERS =================
    const handleChange = (e) => {
        const { name, value } = e.target;
        handleFormChange(setForm, name, value);
        setSavedChanges(form);
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
        const toNullableInt = (value) => {
            if (value === "" || value === null || value === undefined) return null;
            const parsed = Number(value);
            return Number.isNaN(parsed) ? null : parsed;
        };

        try {
            setIsSaving(true);

            const selectedDays = (form.availableDays || [])
                .map((id) => data.days.find((day) => day.id === id || String(day.id) === String(id)))
                .filter(Boolean)
                .map((day) => ({ id: day.id, name: day.name }));

            const payload = {
                userName: form.userName,
                bio: form.bio,
                majorId: toNullableInt(form.majorId),
                universityId: toNullableInt(form.universityId),
                cityId: toNullableInt(form.cityId),
                countryId: toNullableInt(form.countryId),
                gender: form.gender === true || form.gender === "true",
                availableDays: selectedDays,
            };

            if (form.photo instanceof File) {
                payload.photo = await fileToBase64(form.photo);
            }

            await updateProfile(payload);
            alert("Edits saved successfully");

        } 
        catch (error) {
            console.log("Error updating your profile:", error);
        } 
        finally {
            setIsSaving(false);
        }
    };

    // ================= UI =================

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

                    <AddStudyInterests
                        value={form.studyInterests}
                        onChange={handleChange}
                    />

                    <AdjustAvailableDays
                        value={form.availableDays}
                        onChange={handleChange}
                        dayOptions={data.days}
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
                        label="City" 
                        name="cityId" 
                        placeholder="Select City" 
                        value={form.cityId} 
                        options={data.cities} 
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

                    <button onClick={handleSubmit} disabled={isSaving} className="btn mr-0">
                        {isSaving ? "Saving..." : "Save"}
                    </button>

                </div>
            </div>
        </div>
    );
}