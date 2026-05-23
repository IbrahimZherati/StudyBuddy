'use client';

import { useState } from "react";
import { Minus, Plus } from "lucide-react";

export default function StudyInterests({ name, interests, handleChange, handleFocus }) {
    const [input, setInput] = useState("");
    const [showInput, setShowInput] = useState(false);

    const addInterest = () => {
        handleFocus();

        const interest = input.trim();
        if (!interest) 
            return;

        if (interests.includes(interest)){
            alert("Interest already exists");
            return;
        }

        const updated = [...interests, interest];
        handleChange({
            target: {
                name,
                value: updated
            }
        });

        setInput("");
        setShowInput(false);
    };

    const removeInterest = (item) => {
        handleFocus();

        const updated = interests.filter(i => i !== item);
        handleChange({
            target: {
                name,
                value: updated
            }
        });
    };

    return (
        <div className="flex flex-col gap-2">

            <div className="flex items-center gap-2">
                <h3 className="text-xl font-bold">Study Interests</h3>

                {!showInput && 
                    <button
                        onClick={() => {
                            setShowInput(true);
                            handleFocus();
                        }}
                        className="p-1 bg-[#B2C0FF] rounded-full cursor-pointer"
                    >
                        <Plus size={16}/>
                    </button>
                }
                {showInput && 
                    <button
                        onClick={() => {
                            setShowInput(false);
                            handleFocus();
                        }}
                        className="p-1 bg-[#B2C0FF] rounded-full cursor-pointer"
                    >
                        <Minus size={16}/>
                    </button>
                }
            </div>

            {showInput && (
                <div className="flex flex-wrap gap-2">
                    <input
                        value={input}
                        onChange={(e) => setInput(e.target.value)}
                        onFocus={handleFocus}
                        onKeyDown={(e) => {
                            if(e.code == "Enter")
                                addInterest();
                        }}
                        placeholder="Enter interest"
                        className="p-2 shadow outline-none bg-tertiary rounded-xl"
                    />

                    <button
                        onClick={addInterest}
                        className="btn bg-[#B2C0FF] text-black mx-0"
                    >
                        Add
                    </button>
                </div>
            )}

            <div className="flex flex-wrap gap-2">
                {interests.map((item, index) => (
                    <span
                        key={index}
                        className="px-3 py-1 rounded-full cursor-pointer bg-secondary"
                        onClick={() => removeInterest(item)}
                    >
                        {item} &times;
                    </span>
                ))}
            </div>
        </div>
    );
}