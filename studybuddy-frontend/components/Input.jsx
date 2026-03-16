import React from 'react'

const Input = ({label, fieldName, value, handleFocus, handleChange, errorMessage,
                type="text", placeholder="", hasError=false, triedToSubmit=false}) => {

    const isCheckbox = type === "checkbox";

    return (
        <label>
            <span className={`${isCheckbox? "input-check": "input-span"}`}>
                {label}
            </span>

            <input 
                type={type}
                name={fieldName}
                placeholder={placeholder}
                value={isCheckbox? undefined: value}  
                checked={isCheckbox? value: undefined}  
                onChange={e => {handleChange(fieldName, isCheckbox? e.target.checked: e.target.value)}}
                onFocus={handleFocus}
                className={`${isCheckbox? "input-check": "input-box"} 
                            ${hasError && triedToSubmit? "input-error": ""}`}
            /> 

            <p className='error-message'>
                {errorMessage}
            </p>
        </label>
    )
}

export default Input;