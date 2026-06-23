import React from 'react'

const Input = ({ label, name, value, style, handleFocus, handleChange,
    errorMessage, note,
    type = "text", placeholder = "", optional = false,
    hasError = false, triedToSubmit = false }) => {

    const isCheckbox = type === "checkbox";

    return (
        <label className={isCheckbox ? "flex items-center gap-2" : style?.container}>
            <span className={`
                        input-span
                        ${style?.label} 
                        ${optional ? "text-gray-700 text-[1rem]" : ""}
                        ${isCheckbox ? "inline" : ""}
                    `}>
                {label}
            </span>

            <div className='flex flex-col'>
                <input
                    type={type}
                    name={name}
                    placeholder={placeholder}
                    value={isCheckbox? undefined: (value || "")}
                    checked={isCheckbox? value: undefined}
                    onChange={handleChange}
                    onFocus={handleFocus}
                    className={`${style?.input || (isCheckbox ? "input-checkbox" : "input-box")}
                            ${hasError && triedToSubmit ? "border-2 border-red-400" : ""}
                        `}
                />

                {(!isCheckbox && (!note || errorMessage)) &&
                    <p className={`error-message ${errorMessage ? "visible" : "invisible"}`}>
                        {errorMessage || "placeholder"}
                    </p>
                }

                {(note && !errorMessage) &&
                    <p className="note mb-2">
                        {note}
                    </p>
                }
            </div>
        </label>
    )
}

export default Input;