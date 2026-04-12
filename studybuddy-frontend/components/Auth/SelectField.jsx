import React from 'react'

export default function SelectField({ name, value, options=[], placeholder, onChange, label }) {
    return (
        <label className='flex flex-col gap-2'>
            <span className='block text-xl font-bold'>
                {label}
            </span>
            
            <select
                name={name}
                value={value || ""}
                onChange={onChange}
                className="p-2 bg-[#B2C0FF] rounded-2xl focus:outline-none"
            >
                <option value="">
                    {placeholder}
                </option>

                {options.map(item => (
                    <option key={item.id} value={item.id}>
                        {item.name}
                    </option>
                ))}
            </select>
            
        </label>
    )
}
