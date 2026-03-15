import React from 'react'

const Input = ({label, fieldName, value, handleChange, type="text" , placeholder}) => {
    const isCheckbox = type === "checkbox";
    
    return (
        <label>
            <span className={`${isCheckbox ?"iput-check":"input-span"}`}>{label}</span>
            <input 
                type={type}
                placeholder={placeholder}
                value={isCheckbox? undefined: value}  
                checked={isCheckbox? value: undefined}  
                onChange={e => {handleChange(fieldName, isCheckbox? e.target.checked: e.target.value)}}
                className={`${isCheckbox ?"iput-check":"input-box"}`}
            /> 
        </label>
    )
}

export default Input;