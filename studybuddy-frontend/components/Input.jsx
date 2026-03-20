import React from 'react'

const Input = ({label, fieldName, value, handleFocus, handleChange, errorMessage,
                type="text", placeholder="", optional=false, 
                hasError=false, triedToSubmit=false}) => {

    const isCheckbox = type === "checkbox";

    return (
        <label className={`${isCheckbox? "flex items-center gap-2": ""}`}>
            <span className={`
                        input-span 
                        ${optional? "text-gray-700 text-[1rem]": ""}
                        ${isCheckbox? "inline": ""}
                    `}>
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
                className={`${isCheckbox? "input-checkbox": "input-box"} 
                            ${hasError && triedToSubmit? "input-error": ""}`}
            /> 

            <p className={`error-message ${errorMessage? "visible": "invisible"}`}>
                {errorMessage || "placeholder"}
            </p>
        </label>
    )
}

export default Input;