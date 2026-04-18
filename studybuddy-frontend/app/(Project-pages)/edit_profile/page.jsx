'use client';

import React, { useState, useEffect } from 'react';
import InputField from '@/components/Profile/EditProfile/InputField';
import ImageUpload from '@/components/Profile/ImageUpload';
import SelectField from '@/components/Auth/SelectField';
import AdjustAvailableDays from '@/components/Profile/EditProfile/AdjustAvailableDays';
import AddStudyInterests from '@/components/Profile/EditProfile/AddStudyInterests';
import handleFormChange from '@/utils/forms/handleChange';
import { getProfile, updateProfile, getCountries, getCities, getUniversities, getMajors, getDays } from '@/utils/Services/profileService';

export default function EditProfile() {

    const [isSaving, setIsSaving] = useState(false);
    const [originalBio, setOriginalBio] = useState("");
    const [profilePhotoPreview, setProfilePhotoPreview] = useState("/images/avatar-default-2.png");

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

    const [data, setData] = useState({
        countries: [],
        cities: [],
        universities: [],
        majors: [],
        days: [],
    });

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
        if (!photo) return "/images/avatar-default-2.png";

        if (typeof photo === "string") {
            return photo.startsWith("data:")
                ? photo
                : `data:image/jpeg;base64,${photo}`;
        }

        if (Array.isArray(photo) && photo.length > 0) {
            const binary = photo.map((byte) => String.fromCharCode(byte)).join("");
            return `data:image/jpeg;base64,${btoa(binary)}`;
        }

        return "/images/avatar-default-2.png";
    };

    const getApiErrorMessage = (error) => {
        return (
            error?.response?.data?.error ||
            error?.response?.data?.Error ||
            error?.response?.data?.message ||
            error?.response?.data?.Message ||
            error?.message ||
            "An error occurred while saving data"
        );
    };

    const isAiServiceFailedError = (error) => {
        const message = getApiErrorMessage(error).toLowerCase();
        return message.includes("ai service failed");
    };

    // ================= FETCH =================

    useEffect(() => {
        async function fetchData() {
            const [profile, countries, cities, universities, majors, days] = await Promise.all(
                  [getProfile(), getCountries(), getCities(), getUniversities(), getMajors(), getDays()]
            );

            const profileData = profile || {};
            setOriginalBio(profileData.bio || "");
            setProfilePhotoPreview(getProfilePhotoPreview(profileData.photo));
            
            setForm((prev) => ({
                ...prev,
                userName: profileData.userName || "",
                bio: profileData.bio || "",
                majorId: findIdByName(majors, profileData.major),
                universityId: findIdByName(universities, profileData.university),
                cityId: findIdByName(cities, profileData.city),
                countryId: findIdByName(countries, profileData.country),
                gender: String(profileData.gender ?? true),
                availableDays: getDayIdsFromProfile(
                    profileData.avaiableDays || profileData.availableDays,
                    days
                ),
                studyInterests: profileData.studyInterests || [],
            }));

            setData({ countries, cities, universities, majors, days });
            
        }

        fetchData();
    }, []);

    // ================= HANDLERS =================
    const handleChange = (e) => {
        const { name, value } = e.target;
        handleFormChange(setForm, name, value);
    };

    const handleImageChange = (file) => {
        setForm(prev => ({ ...prev, photo: file }));
    };

    const handleDays = (days) => {
        setForm(prev => ({ ...prev, availableDays: days }));
    };

    const handleInterests = (interests) => {
        setForm(prev => ({ ...prev, studyInterests: interests }));
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

        } catch (error) {

            if (isAiServiceFailedError(error)) {
                try {
                    const toNullableInt = (value) => {
                        if (value === "" || value === null || value === undefined) return null;
                        const parsed = Number(value);
                        return Number.isNaN(parsed) ? null : parsed;
                    };

                    const selectedDays = (form.availableDays || [])
                        .map((id) => data.days.find((day) => day.id === id || String(day.id) === String(id)))
                        .filter(Boolean)
                        .map((day) => ({ id: day.id, name: day.name }));

                    const fallbackPayload = {
                        userName: form.userName,
                        bio: originalBio,
                        majorId: toNullableInt(form.majorId),
                        universityId: toNullableInt(form.universityId),
                        cityId: toNullableInt(form.cityId),
                        countryId: toNullableInt(form.countryId),
                        gender: form.gender === true || form.gender === "true",
                        availableDays: selectedDays,
                    };

                    if (form.photo instanceof File) {
                        fallbackPayload.photo = await fileToBase64(form.photo);
                    }

                    await updateProfile(fallbackPayload);
                    setForm((prev) => ({ ...prev, bio: originalBio }));
                    alert("Edits saved successfully, but Bio was not updated due to a temporary AI service outage.");
                    return;

                } catch (fallbackError) {
                    alert(getApiErrorMessage(fallbackError));
                    return;
                }
            }
            alert(getApiErrorMessage(error));

        } finally {
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
                        onChange={handleImageChange}
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
                        onChange={handleInterests}
                    />

                    <AdjustAvailableDays
                        value={form.availableDays}
                        onChange={handleDays}
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
                        isSearchable={false}
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