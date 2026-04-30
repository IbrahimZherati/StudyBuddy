'use client';

import React, { useState, useEffect, useRef, useMemo } from 'react';
import InputField from '@/components/Profile/EditProfile/InputField';
import ImageUpload from '@/components/Profile/ImageUpload';
import SelectField from '@/components/Auth/SelectField';
import AvailableDays from '@/components/Profile/EditProfile/AvailableDays';
import StudyInterests from '@/components/Profile/EditProfile/StudyInterests';
import handleFormChange from '@/utils/forms/handleChange';
import handleFormSubmit from '@/utils/forms/handleSubmit';
import useGetDataList from '@/app/hooks/useGetDataList';
import useGetUserInfo from '@/app/hooks/useGetUserInfo';
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

    const [triedToSubmit, setTriedToSubmit] = useState(false);

    const majorSelected = !form.majorId? false: true;
    const minimumUserNameLength = 3;
    const userNameLongEnough = form.userName.length >= minimumUserNameLength;
    const canSubmit = majorSelected && userNameLongEnough;

    const handleFocus = () => {
        setTriedToSubmit(false);
    }

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
            availableDays: getDayIdsFromProfile(profile.availableDays),
            studyInterests: profile.studyInterests
        }
    }

    const isFirstLoadOfSaved = useRef("true");
    const isFirstLoadOfCurrent = useRef("true");
    const [savedChanges, setSavedChanges] = useLocalStorage("editProfileChanges", null);

    useEffect(() => {
        if (isFirstLoadOfSaved.current && savedChanges) {
            console.log("Loaded saved changes", savedChanges);
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

    useEffect(() => {
        const saveChangesInterval = setInterval(() => {
            if(form != savedChanges) {
                console.log("Saving Chnages now...", form);
                setSavedChanges(form);
            }
        }, 2000);

        return () => clearInterval(saveChangesInterval);
    }, [form, savedChanges, setSavedChanges]);

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

    const handleSubmit = async (e) => {
        if(isSaving)
            return;

        try {
            setIsSaving(true);

            // if (form.photo instanceof File) {
            //     payload.photo = await fileToBase64(form.photo);
            // }

            try {
                console.log("Can Submit?", canSubmit);
                const data = await handleFormSubmit(e, canSubmit, setTriedToSubmit, form, setForm, "ClientUser", "put");
                if(data)
                    alert("Edits saved successfully");
            }
            catch(error) {
                console.log("Error updating profile info", error?.response?.data);
            }
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
                        label="Bio:"
                        name="bio"
                        placeholder="Enter Your Bio"
                        value={form.bio}
                        handleChange={handleChange}
                        handleFocus={handleFocus}
                    />

                    <StudyInterests
                        value={form.studyInterests}
                        handleChange={handleChange}
                        handleFocus={handleFocus}
                    />

                    <AvailableDays
                        value={form.availableDays}
                        dayOptions={data.days}
                        handleChange={handleChange}
                        handleFocus={handleFocus}
                    />
                </div>

                {/* RIGHT */}
                <div className="flex flex-col">

                    <InputField
                        label="Name:"
                        name="userName"
                        placeholder="Enter Your Name"
                        value={form.userName}
                        handleChange={handleChange}
                        handleFocus={handleFocus}
                        triedToSubmit={triedToSubmit}
                        hasError={!userNameLongEnough}
                        errorMessage={
                            (triedToSubmit && !userNameLongEnough)
                                ? `User Name must be no less than ${minimumUserNameLength} characters` : ""
                        }
                        note="User Name is going to be public. Please do not add any personal info."
                    />

                    <SelectField 
                        label="Major:" 
                        name="majorId" 
                        placeholder="Select Major" 
                        value={form.majorId} 
                        options={data.majors || []} 
                        handleChange={handleChange}
                        handleFocus={handleFocus}
                        triedToSubmit={triedToSubmit} 
                        hasError={!majorSelected}
                        errorMessage={
                            (triedToSubmit && !majorSelected)? "Please select your major": ""
                        }
                    />

                    <SelectField 
                        label="University:" 
                        name="universityId" 
                        placeholder="Select University" 
                        value={form.universityId} 
                        options={data.universities} 
                        handleChange={handleChange}
                        handleFocus={handleFocus}
                    />

                    <SelectField 
                        label="Country:" 
                        name="countryId" 
                        placeholder="Select Country" 
                        value={form.countryId} 
                        options={data.countries} 
                        handleChange={handleChange}
                        handleFocus={handleFocus}
                    />

                    <SelectField 
                        label="City:" 
                        name="cityId" 
                        placeholder="Select City" 
                        value={form.cityId} 
                        options={[]} 
                        handleChange={handleChange}
                        handleFocus={handleFocus}
                    />

                    <SelectField
                        label="Gender:"
                        name="gender"
                        placeholder="Select Gender"
                        value={form.gender}
                        isSearchable={false}
                        options={[
                            { id: true, name: "Male" },
                            { id: false, name: "Female" }
                        ]}
                        handleChange={handleChange}
                        handleFocus={handleFocus}
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