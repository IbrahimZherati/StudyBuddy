import React from 'react'

const Input = ({label, name, value, handleFocus, handleChange, 
                errorMessage, note,
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
                name={name}
                placeholder={placeholder}
                value={isCheckbox? undefined: value}  
                checked={isCheckbox? value: undefined}  
                onChange={handleChange}
                onFocus={handleFocus}
                className={`${isCheckbox? "input-checkbox": "input-box"} 
                            ${hasError && triedToSubmit? "input-error": ""}`}
            /> 

            {(!note || errorMessage) && 
                <p className={`error-message ${errorMessage? "visible": "invisible"}`}>
                    {errorMessage || "placeholder"}
                </p>
            }

            {(note && !errorMessage) && 
                <p className="note mb-2">
                    {note}
                </p>
            }
        </label>
    )
}

export default Input;