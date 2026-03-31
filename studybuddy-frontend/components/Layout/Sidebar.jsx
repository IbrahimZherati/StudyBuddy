import React from 'react'
import { FileText, Timer, Book, Calculator, Calendar , GraduationCap} from "lucide-react";

export default function Sidebar() {
    const items = [
        {icon : <FileText/> , label : "Your Notes"},
        {icon : <Timer/> , label : "Pomodoro Timer"},
        {icon : <Book/> , label : "Learning Articles"},
        {icon : <Calculator/> , label : "Rate Calculator"},
        {icon : <Calendar/> , label : "Schedule Calendar"}
    ]
  return (
    <aside className='flex-col-center gap-4 p-2 w-56 bg-[#F5F6FF] border-r hidden md:block'>
        <div className='flex gap-2'>
            <GraduationCap className='text-[#002CFF] w-9 h-9'/>
                        
            <h1 className='font-bold text-2xl'>
                Study Buddy
            </h1>
        </div>

        <ul>
            {items.map((item, index) => (
                <li key={index} className='flex items-center gap-2 p-2 hover:bg-[#E0E4FF] cursor-pointer rounded-md'>
                    {item.icon}
                    <span>{item.label}</span>
                </li>
            ))}
        </ul>
    </aside>
  )
}
