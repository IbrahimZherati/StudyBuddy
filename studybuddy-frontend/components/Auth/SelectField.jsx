import React from 'react'

export default function SelectField({ name, value, options, placeholder, onChange, label }) {
    return (
        <label className='flex flex-col gap-2'>
            <span className='block text-xl font-bold'>
                {label}
            </span>
            
            <select
                name={name}
                value={value || ""}
                onChange={onChange}
                className="py-2 px-3 bg-[#d0d7fb] rounded-2xl focus:outline-none"
            >
                <option value="" disabled className="bg-tertiary font-bold rounded-2xl">
                    {placeholder}
                </option>

                {options.map(item => (
                    <option key={item.id} value={item.id} className="bg-tertiary">
                        {item.name}
                    </option>
                ))}
            </select>
            
        </label>
    )
}
