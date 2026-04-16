'use client';
import React from 'react'
import { useState, useEffect } from 'react';
import InputField from '@/components/Profile/EditProfile/InputField';
import handleFormChange from '@/utils/forms/handleChange';
import ImageUpload from '@/components/Profile/ImageUpload';
import SelectField from '@/components/Auth/SelectField';
import AdjustAvailableDays from '@/components/Profile/EditProfile/AdjustAvailableDays';
import AddStudyInterests from '@/components/Profile/EditProfile/AddStudyInterests';
import {
    getProfile,
    updateProfile,
    getCountries,
    getCities,
    getUniversities,
    getMajors,
} from '@/utils/Services/profileService';

export default function EditProfile() {

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
    });

    useEffect(() => {
        async function fetchData() {
            const [profile, countries, cities, universities, majors] = await Promise.all([
                getProfile(),
                getCountries(),
                getCities(),
                getUniversities(),
                getMajors(),
            ]);
            
            setForm(prev => ({
                ...prev,
                ...profile
            }));

            setData({
                countries,
                cities,
                universities,
                majors
            });
            
        }

        fetchData();
    }, []);

    const handleChange = (e) => {
        const { name, value } = e.target;
        handleFormChange(setForm, name, value);
    }
    
    const handleImageChange = (file) => {
        setForm((prev) => ({ ...prev, photo: file }));
    };

    const handleDays = (days) => {
        setForm(prev => ({ ...prev, availableDays: days }));
    };

    const handleInterests = (data) => {
        setForm(prev => ({ ...prev, studyInterests: data }));
    };
    
    const handleSubmit = async () => {
        const formData = new FormData();

        Object.keys(form).forEach(key => {
            formData.append(key, form[key]);
        });

        await updateProfile(formData);
        alert("Saved!");
    };

    return (
        <div className="grid grid-cols-2 gap-10 p-6">
            
            {/* LEFT */}
            <div className="flex flex-col gap-6">
                <ImageUpload onChange={handleImageChange} />

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
                    value={form.majorId}
                    options={data.majors}
                    placeholder="Major"
                    onChange={handleChange}
                />

                <SelectField
                    label="University"
                    name="universityId"
                    value={form.universityId}
                    options={data.universities}
                    placeholder="University"
                    onChange={handleChange}
                />

                <SelectField
                    label="City"
                    name="cityId"
                    value={form.cityId}
                    options={data.cities}
                    placeholder="City"
                    onChange={handleChange}
                />

                <SelectField
                    label="Country"
                    name="countryId"
                    value={form.countryId}
                    options={data.countries}
                    placeholder="Country"
                    onChange={handleChange}
                />

                <SelectField
                    label="Gender"
                    name="gender"
                    value={form.gender}
                    options={[
                        { id: true, name: "Male" },
                        { id: false, name: "Female" }
                    ]}
                    placeholder="Gender"
                    onChange={handleChange}
                />

                <button
                    onClick={handleSubmit}
                    className='mr-0 btn'
                >
                    Save
                </button>
                
            </div>
            
        </div>
    )
}
