import React from 'react';
import { Trash2, Sparkles, FileText } from 'lucide-react';
import Link from 'next/link';
import DateAndTime from '@/components/DateAndTime';
import apiDelete from '@/utils/API/delete';

export default function FileRow({ file }) {

    const onDelete = async () => {
        try {
            await apiDelete(null, `ClientFile/${file.id}`);
        }
        catch(error) {
            console.log("Error deleting file", error?.response?.data);
        }
        finally {
            window.location.reload();
        }
    }

    return (
        <div className="flex flex-col sm:flex-row sm:items-center justify-between p-5 bg-tertiary 
                        rounded-xl shadow-md cursor-pointer hover:-translate-y-1 hover:shadow-lg gap-4"
        >
            <div className="flex items-center gap-2 min-w-0">
                <div className="p-2 text-primary shrink-0">
                    <FileText size={20} />
                </div>

                <span className="font-medium text-black truncate text-xl sm:text-base">
                    {file.name}
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
                    dateAndTime={file.uploadDateTime} 
                />
            </div>
        </div>
    );
}