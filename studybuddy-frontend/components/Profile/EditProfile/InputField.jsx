import React from 'react'

export default function InputField({ name, placeholder, value, onChange ,label}) {
    return (
        <label className='flex flex-col gap-2'>
            <span className='block text-xl font-bold'>
                {label}
            </span>
            
            <input
                name={name}
                value={value || ""}
                placeholder={placeholder}
                onChange={onChange}
                className="py-2 px-3 bg-[#d0d7fb] rounded-2xl focus:outline-none"
            />
        </label>
  );
}
