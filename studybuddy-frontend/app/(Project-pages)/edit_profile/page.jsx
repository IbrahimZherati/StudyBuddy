'use client';

//TODO:
// - URGENT: Fix City and University resting bug (restore handleChange() when done)
// - Automatic focus in Select
// - Separte dataStorage between different accounts (done?)

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

    const data = {
        universities: useGetDataList("University"),
        countries: useGetDataList("Country"),
        cities: useGetDataList("City/GetCitiesForCountry", {key: "countryId", value: form.countryId}),
        majors: useGetDataList("Major"),
        days: useGetDataList("Day")
    }

    // ================= HELPERS =================

    const findIdByName = (items, name) => {
        if (!name || !items) return null;

        const item = items.find(
            (i) => (i.name || "").toLowerCase() === String(name).toLowerCase()
        );

        return item? item.id : null;
    };

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

    // ================= FETCH =================

    const [profile, setProfile] = useGetUserInfo(true, profileUpdated);
    // console.log("Profile", profile);

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

    // console.log("cities:", data.allCities);

    const processedProfile = processProfile();

    const unSavedChanges = !compare(form, processedProfile);
    // console.log("Form", form);
    // console.log("ProcessedProfile", processedProfile);

    const isFirstLoadOfSaved = useRef(true);
    const isFirstLoadOfCurrent = useRef(true);
    
    const [loadedData, setLoadedData] = useState(false);
    const [savedChanges, setSavedChanges] = useLocalStorage("editProfileChanges", null);

    // console.log("Saved Changes", savedChanges);

    useEffect(() => {
        if(loadedData) {
            if (isFirstLoadOfSaved.current && savedChanges) {
                // setForm(savedChanges);
    
                isFirstLoadOfSaved.current = false;
                isFirstLoadOfCurrent.current = false;
            }
            else if (isFirstLoadOfCurrent.current && profile) {
                console.log(processedProfile);
                setForm(processedProfile);
    
                isFirstLoadOfCurrent.current = false;
            }
        }

        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [savedChanges, profile, isFirstLoadOfSaved, isFirstLoadOfCurrent, loadedData]);

    useEffect(() => {
        const saveChangesInterval = setInterval(() => {
            if (loadedData && form != savedChanges) {
                setSavedChanges(form);
            }
        }, 2000);

        return () => clearInterval(saveChangesInterval);
    }, [form, savedChanges, setSavedChanges, loadedData]);

    // ================= HANDLERS =================
    const handleChange = (e) => {
        const { name, value } = e.target;
        handleFormChange(setForm, name, value);
        // if(name == "countryId")
        //     setForm(prev => ({...prev, cityId: null}));
    };

    // ================= SUBMIT =================

    const handleDiscard = () => {
        setForm(processedProfile);
        setSavedChanges(form);
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

    let isDataStillLoading = false;
    for (const [key, value] of Object.entries(data)) {
        if (!value) {
            // if(key !== "cities" || form.countryId) 
                isDataStillLoading = true;
        }
    }

    if(!isDataStillLoading && !loadedData)
        setLoadedData(true);

    if (!form)
        isDataStillLoading = true;

    if(profileUpdated && !unSavedChanges)
        setProfileUpdated(false);

    if (isDataStillLoading)
        return <Loading />

    return (
        <div className="p-4">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-10">

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
