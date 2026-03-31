
export default function Card({children, additionalStyles}) {
    return (
        <div className={`h-full p-4 flex gap-6 rounded-2xl shadow-lg bg-[#F5F6FF]
                        hover:-translate-y-1 hover:shadow-xl hover:bg-[#FAFAFF]
                        transition-all duration-300 
                        ${additionalStyles}`}
        >
            {children}
        </div>
    )
}