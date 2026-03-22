import Card from "./Card";
import StarRow from "./StarRow";

export default function ReviewCard({userName, userMajor, reviewText, rating}) {
    const styles = {
        "flexDirection": "column",
        "gap": 16   
    };
    
    return (
        <Card styles={styles}>
            <div className="flex flex-col gap-0.5">
                <h4 className='flex-row-center justify-start font-bold text-xl flex gap-2'>
                    {userName}
                    <StarRow numOfStars={rating} />
                </h4>

                <p className="text-sm text-gray-500">
                    {userMajor}
			    </p>
            </div>
						
			<p className="text-gray-700">
				{reviewText}
			</p>
        </Card>
    )
}