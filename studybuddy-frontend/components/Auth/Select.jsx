import React from 'react'
import { useState, useEffect, useRef } from 'react';

export default function Select({
    name, value, options = [], style, placeholder, handleChange, handleFocus, label,
    isSearchable = true, hasError, triedToSubmit, errorMessage }
) {

    const [open, setOpen] = useState(false);
    const [search, setSearch] = useState("");
    const dropdownRef = useRef(null);

    const selectedItem = options.find(item => item.id === value);

    const filteredOptions = isSearchable
        ? options.filter(item =>
            item.name.toLowerCase().includes(search.toLowerCase())
        )
        : options;

    useEffect(() => {
        const handleClickOutside = (event) => {
            if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
                setOpen(false);
            }
        };

        document.addEventListener("mousedown", handleClickOutside);
        return () => {
            document.removeEventListener("mousedown", handleClickOutside);
        };
    }, []);

    return (
        <div ref={dropdownRef} className={style?.container || "flex flex-col relative"}>
            <span className={`${style?.label || "input-span"}`}>
                {label}
            </span>

            <div>
                {/* Selected */}
                <button
                    type="button"
                    onClick={() => { setOpen(!open); handleFocus(); }}
                    className={`
                    flex  
                    input-box
                    ${style?.input} 
                    ${!selectedItem ? "text-gray-500" : ""}
                    ${open ? "outline-2 -outline-offset-2 outline-blue-200" : ""}
                    ${hasError && triedToSubmit ? "input-error" : ""}
                `}
                >
                    {selectedItem ? selectedItem.name : placeholder}
                </button>

                {/* Dropdown */}
                {open && (
                    <div className="absolute w-full top-20 bg-[#F5F7FF] rounded-2xl shadow-lg z-10 overflow-hidden">

                        {/* Search Input */}
                        {isSearchable && (
                            <div className="p-2">
                                <input
                                    type="text"
                                    placeholder="Search..."
                                    value={search}
                                    onChange={(e) => setSearch(e.target.value)}
                                    className="w-full px-3 py-2 text-[#1f2044] rounded-xl border focus:outline-secondary"
                                />
                            </div>
                        )}

                        {/* Options */}
                        <div className="max-h-48 overflow-y-auto">
                            {filteredOptions.length > 0 ? (filteredOptions.map(item => (
                                <div key={item.id}
                                    onClick={() => {
                                        handleChange({ target: { name, value: item.id } });
                                        setOpen(false);
                                        setSearch(""); // reset search
                                    }}
                                    className="px-4 py-2 hover:bg-[#E0E7FF] text-[#1f2044] cursor-pointer"
                                >
                                    {item.name}
                                </div>
                            ))) : (
                                <div className="px-4 py-2 text-gray-400">
                                    No results found
                                </div>
                            )}
                        </div>
                    </div>
                )}

                {(errorMessage) &&
                    <p className={`error-message ${errorMessage ? "visible" : "invisible"}`}>
                        {errorMessage || "placeholder"}
                    </p>
                }

                {(!errorMessage) &&
                    <p className="note mb-2">
                        { }
                    </p>
                }
            </div>

        </div>
    );
}

