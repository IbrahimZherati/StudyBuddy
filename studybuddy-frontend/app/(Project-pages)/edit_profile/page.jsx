'use client';

//TODO:
// - URGENT: Fix City and University resting bug
// - Automatic focus in Select
// - URGENT: Separte dataStorage between different accounts (done?)

import React, { useState, useEffect, useRef, useMemo, useCallback } from 'react';
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
import compare from '@/utils/compare';
import { fileFromBase64 } from '@/utils/fileHandling';

export default function EditProfile() {

    const [isSaving, setIsSaving] = useState(false);
    const [profileUpdated, setProfileUpdated] = useState(false);

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

    const majorSelected = !form.majorId ? false : true;
    const minimumUserNameLength = 3;
    const userNameLongEnough = form.userName.length >= minimumUserNameLength;
    const canSubmit = majorSelected && userNameLongEnough;

    const handleFocus = () => {
        setTriedToSubmit(false);
    }

    // ================= FETCH =================

    const [profile] = useGetUserInfo(true, profileUpdated);

    const universities = useGetDataList("University");
    const countries = useGetDataList("Country");
    const majors = useGetDataList("Major");
    const days = useGetDataList("Day");

    const findIdByName = (items, name) => {
        if (!name || !items) return null;

        const item = items.find(
            (i) => (i.name || "").toLowerCase() === String(name).toLowerCase()
        );

        return item ? item.id : null;
    };

    const profileCountryId = useMemo(() => {
        if (!profile || !countries) return null;

        return findIdByName(countries, profile.country);
    }, [profile, countries]);

    const cities = useGetDataList("City/GetCitiesForCountry", {key: "countryId", value: form.countryId || profileCountryId});

    const data = {
        universities,
        countries,
        cities,
        majors,
        days
    }

    // ================= HELPERS =================

    const getDayIdsFromProfile = (profileDays) => {
        if (!data.days || !profileDays)
            return [];

        return profileDays.map(
            (dayName) => findIdByName(data.days, dayName)
        );
    };

    const profilePhotoPreview = useMemo(() => {
        const photo = form?.photo;

        return fileFromBase64(photo, "/images/avatar-default.svg");
    }, [form?.photo]);

    const processProfile = () => {
        if (!profile)
            return null;

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

    const processedProfile = processProfile();

    const hasResolvedProfile = Boolean(
        processedProfile &&
        processedProfile.majorId !== null &&
        processedProfile.universityId !== null &&
        processedProfile.countryId !== null &&
        (!profile?.city || processedProfile.cityId !== null)
    );

    const hasRequiredDraftValues = useCallback((snapshot) => {
        if (!snapshot) return false;

        const countryChanged = processedProfile?.countryId !== null && snapshot.countryId !== processedProfile.countryId;
        const cityWasClearedIntentionally = countryChanged && snapshot.cityId === null;

        return (
            snapshot.majorId !== null &&
            snapshot.universityId !== null &&
            snapshot.countryId !== null &&
            (!profile?.city || snapshot.cityId !== null || cityWasClearedIntentionally)
        );
    }, [profile?.city, processedProfile?.countryId]);

    const unSavedChanges = !compare(form, processedProfile);

    const isFirstLoadOfCurrent = useRef("true");
    const hasHydratedProfile = useRef(false);
    const initialProfileRef = useRef(null);
    const [savedChanges, setSavedChanges] = useLocalStorage("editProfileChanges", null);

    useEffect(() => {
        if (isFirstLoadOfCurrent.current && hasResolvedProfile) {
            const initialProfile = processedProfile;

            if (hasRequiredDraftValues(savedChanges) && !compare(savedChanges, processedProfile)) {
                setForm(savedChanges);
            }
            else {
                setForm(initialProfile);
            }

            initialProfileRef.current = initialProfile;
            isFirstLoadOfCurrent.current = false;
            hasHydratedProfile.current = true;
        }

    }, [hasResolvedProfile, processedProfile, profile, savedChanges, hasRequiredDraftValues, data.majors, data.universities, data.cities, data.countries, data.days, isFirstLoadOfCurrent]);

    useEffect(() => {
        const saveChangesInterval = setInterval(() => {
            if (!hasHydratedProfile.current) {
                return;
            }

            if (form != savedChanges) {
                setSavedChanges(form);
            }
        }, 2000);

        return () => clearInterval(saveChangesInterval);
    }, [form, savedChanges, setSavedChanges]);

    // ================= HANDLERS =================
    const handleChange = (e) => {
        const { name, value } = e.target;
        handleFormChange(setForm, name, value);
        if(name == "countryId")
            setForm(prev => ({...prev, cityId: null}));
    };

    // ================= SUBMIT =================

    const handleDiscard = () => {
        const restoredProfile = initialProfileRef.current || processedProfile;
        setForm(restoredProfile);
        setSavedChanges(restoredProfile);
    }

    const handleSubmit = async (e) => {
        if (isSaving)
            return;

        const processedForm = {...form};
        for (let key in form) {
            if (!form[key] && key !== "gender")
                processedForm[key] = null;

            if(key === "studyInterests") {
                processedForm[key] = form[key].map(interest => ({
                    name: interest
                }))
            }

            if(key === "availableDays") {
                processedForm[key] = form[key].map(dayId => ({
                    name: "name", 
                    id: dayId
                }))
            }
        }

        console.log(processedForm, form);

        try {
            setIsSaving(true);

            try {
                const data = await handleFormSubmit(e, canSubmit, setTriedToSubmit,
                    processedForm, setForm, "ClientUser", "put");
                if (data) {
                    setSavedChanges(form);
                    initialProfileRef.current = form;
                    setProfileUpdated(true);
                    localStorage.removeItem("userInfo");
                    alert("Edits saved successfully");
                }
            }
            catch (error) {
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
    for (const [key, value] of Object.entries(data)) {
        if (!value) {
            if(key !== "cities" || form.countryId) 
                isDataStillLoading = true;
        }
    }
    if (!form)
        isDataStillLoading = true;

    if(profileUpdated && !unSavedChanges)
        setProfileUpdated(false);

    if (isDataStillLoading)
        return <Loading />

    return (
        <div className="p-4">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-10">

                {/* LEFT */}
                <div className="flex flex-col gap-6">
                    <ImageUpload
                        name="photo"
                        handleChange={handleChange}
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
                        name="studyInterests"
                        interests={form.studyInterests}
                        handleChange={handleChange}
                        handleFocus={handleFocus}
                    />

                    <AvailableDays
                        name="availableDays"
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
                            (triedToSubmit && !majorSelected) ? "Please select your major" : ""
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
                        options={data.cities || []}
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

                    <div className='flex justify-end items-center gap-4'>
                        {unSavedChanges &&
                            <p className='text-red-500 w-fit'>
                                You have unsaved changes
                            </p>
                        }

                        <button onClick={handleDiscard} disabled={!unSavedChanges}
                            className={`btn m-0 ${!unSavedChanges ? "disabled" : ""}`}
                        >
                            Discard
                        </button>

                        <button onClick={handleSubmit} disabled={isSaving}
                            className={`btn m-0 ${isSaving ? "disabled" : ""}`}
                        >
                            {isSaving ? "Saving..." : "Save"}
                        </button>
                    </div>

                </div>
            </div>
        </div>
    );
}
