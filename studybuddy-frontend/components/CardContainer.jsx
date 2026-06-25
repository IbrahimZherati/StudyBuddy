
export default function CardContainer({children, additionalStyles, ref, onScroll}) {
    return (
        <div 
            ref={ref}
            onScroll={onScroll}
            className={`grid gap-5 grid-cols-[repeat(auto-fit,minmax(250px,1fr))] auto-rows-fr
                        ${additionalStyles}`}>
            {children}
        </div>
    )
}