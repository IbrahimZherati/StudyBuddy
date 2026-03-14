import React from 'react'

const Input = ({label, fieldName, value, handleChange, type="text"}) => {
    const isCheckbox = type === "checkbox";
    
    return (
        <label>
            <span className='input-span'>{label}</span>
            <input 
                type={type}
                value={isCheckbox? undefined: value}  
                checked={isCheckbox? value: undefined}  
                onChange={e => {handleChange(fieldName, isCheckbox? e.target.checked: e.target.value)}}
                className='border-2 text-blue-500'
            /> 
        </label>
    )
}

export default Input;