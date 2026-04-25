import React from 'react'
import Select from './Select';

export default function SelectField(props) {
    const customStyles = {
        container:"flex flex-col gap-2 relative",
        label:"block text-xl font-bold",
        input:"py-2 px-3 bg-[#d0d7fb] rounded-2xl focus:outline-none"
    }

    return (
        <Select
            {...props}
            style={customStyles}
        />
    )
}

