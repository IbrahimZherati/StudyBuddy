import Input from '@/components/Auth/Input';
import React from 'react'

export default function InputField(props) {
    const customStyles = {
        container:"flex flex-col gap-2",
        label:"block text-xl font-bold",
        input:"py-2 px-3 bg-[#d0d7fb] rounded-2xl focus:outline-none"
    }

    return (
        // <label className='flex flex-col gap-2'>
        //     <span className='block text-xl font-bold'>
        //         {label}
        //     </span>
            
        //     <input
        //         name={name}
        //         value={value || ""}
        //         placeholder={placeholder}
        //         onChange={onChange}
        //         className=""
        //     />
        // </label>
        <Input 
            {...props}
            style={customStyles}
        />
  );
}
