
export default function CardContainer({children, additionalStyles}) {
    return (
        <div className={`grid gap-5 grid-cols-[repeat(auto-fit,minmax(250px,1fr))]
                        ${additionalStyles}`}>
            {children}
        </div>
    )
}