
export default function CardContainer({children}) {
    return (
        <div className={`grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-5`}>
            {children}
        </div>
    )
}