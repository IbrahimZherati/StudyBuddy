import Link from "next/link";
import Card from "./Card";

export default function FeatureCard({icon, title, desc, href}) {
    return (
        <Card>
            <div>
				{icon}
			</div>

			<div className="flex flex-col">
				<h4 className="text-xl font-semibold">
					{title}
				</h4>

				<p className="text-gray-600 h-28">
					{desc}
				</p>

				<Link className="text-blue-600 font-semibold mt-6" href={href}>
					Try it →
				</Link>
			</div>
        </Card>
    )
}