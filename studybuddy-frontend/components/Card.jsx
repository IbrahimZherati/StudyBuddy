
export default function Card({children, styles}) {
    return (
        <div className="p-4 flex gap-6 rounded-2xl shadow-lg bg-[#F5F6FF]"
            style={styles}>
            {children}
        </div>
    )
}