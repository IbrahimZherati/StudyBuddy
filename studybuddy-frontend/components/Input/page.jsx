import React from 'react'

const Input = ({label, value, onChangeFunc, type="text"}) => {
    return (
        <label>
            <span className='input-span'>{label}</span>
            <input 
                type={type}
                value={value}
                onChange={e => {onChangeFunc(e.target.value)}}
                className='border-2 text-blue-500'
            /> 
        </label>
    )
}

export default Input;