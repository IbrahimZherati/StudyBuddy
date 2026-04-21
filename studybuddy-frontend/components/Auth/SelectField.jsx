import React from 'react'
import { useState , useEffect , useRef } from 'react';

export default function SelectField({ name, value, options=[], placeholder, onChange, label , isSearchable = true}) {
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


<<<<<<< HEAD
export default function SelectField({ name, value, options, placeholder, onChange, label }) {
=======
>>>>>>> 9684887d11ca0be7a4a0779916bb79a4b88ae92e
    return (
        <div ref={dropdownRef} className="flex flex-col gap-2 relative">
            <span className="text-xl font-bold">
                {label}
            </span>

            {/* Selected */}
            <div onClick={() => setOpen(!open)}
                className="bg-[#d0d7fb] rounded-2xl px-4 py-2 cursor-pointer"
            >
                {selectedItem ? selectedItem.name : placeholder}
            </div>

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
                                className="w-full px-3 py-2 text-[#1f2044] rounded-xl border focus:outline-secondary "
                            />
                        </div>
                    )}

                    {/* Options */}
                    <div className="max-h-48 overflow-y-auto">
                        {filteredOptions.length > 0 ? (filteredOptions.map(item => (
                            <div key={item.id}
                                onClick={() => { 
                                    onChange({ target: { name, value: item.id } });
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
        </div>
    );
}

