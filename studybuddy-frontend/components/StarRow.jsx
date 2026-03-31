import { StarIcon } from "lucide-react";

export default function StarRow({numOfStars}) {
    return (
        <span className="text-yellow-500 text-2xl flex">
			{[...Array(numOfStars)].map((_, i) => (
			    <StarIcon key={i}/>
			))}
		</span>
    )
}