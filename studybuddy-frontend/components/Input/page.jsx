import React from 'react'

const Input = ({onChange}) => {
    return (
        <input 
            onChange={onChange}
            className='border-2 text-blue-500'
        />
    )
}

export default Input;