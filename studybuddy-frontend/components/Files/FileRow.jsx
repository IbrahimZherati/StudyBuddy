import React from 'react';
import { Trash2, Sparkles, FileText } from 'lucide-react';
import Link from 'next/link';
import DateAndTime from '@/components/DateAndTime';
import apiDelete from '@/utils/API/delete';
import { notify } from '@/utils/notify';

export default function FileRow({ file }) {

    const onDelete = async () => {
        try {
            console.log(file.id);
            await apiDelete(`ClientFile/${file.id}`);
            window.location.reload();
        }
        catch(error) {
            const errorReason = error?.response?.data?.error;
            console.log("Error deleting file", errorReason);

            if(errorReason) {
                notify({
                    title: "Error",
                    message: errorReason,
                    sound: false,
                    error: true
                })
            }
        }
    }

    return (
        <div className="flex flex-col sm:flex-row sm:items-center justify-between p-5 bg-tertiary 
                        rounded-xl shadow-md cursor-pointer hover:bg-[#E9EAFF] gap-4"
        >
            <div className="flex items-center gap-2 min-w-0">
                <div className="p-2 text-primary shrink-0">
                    <FileText size={20} />
                </div>

                <span className="font-medium text-black truncate text-xl sm:text-base">
                    {file.title}
                </span>
            </div>

            <div className="flex items-center justify-between sm:justify-end gap-6 shrink-0">
                <div className="flex items-center gap-3">
                    <div className="relative group">
                        <Link href={`/files/${file.id}`}>
                            <button 
                                className="p-2 text-purple-600 hover:bg-purple-100 rounded-lg transition-colors duration-150"
                            >
                                <Sparkles size={20} />
                            </button>
                        </Link>
                        
                        <div className="absolute bottom-full mb-2 left-1/2 -translate-x-1/2 
                                        hidden group-hover:block bg-gray-900 text-white text-xs 
                                        px-2 py-1 rounded shadow-lg whitespace-nowrap z-10"
                        >
                            Ai Analysis
                        </div>
                    </div>

                    <div className="relative group">
                        <button 
                            onClick={onDelete}
                            className="p-2 text-red-500 hover:bg-red-100 rounded-lg 
                                        transition-colors duration-150"
                        >
                            <Trash2 size={20} />
                        </button>

                        <div className="absolute bottom-full mb-2 left-1/2 -translate-x-1/2 hidden 
                                        group-hover:block bg-gray-900 text-white text-xs px-2 py-1 
                                        rounded shadow-lg whitespace-nowrap z-10"
                        >
                            Delete this file
                        </div>
                    </div>
                </div>

                <DateAndTime 
                    dateAndTime={file.createDate} 
                />
            </div>
        </div>
    );
}