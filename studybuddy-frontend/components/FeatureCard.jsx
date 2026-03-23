import Link from "next/link";
import Card from "./Card";

export default function FeatureCard({icon, title, desc, href}) {
    return (
        <Card>
            <div>
				{icon}
			</div>

			<div className="flex flex-col gap-6">
				<div>
					<h4 className="text-xl font-semibold">
						{title}
					</h4>

					<p className="text-gray-600">
						{desc}
					</p>
				</div>

				<Link className="text-blue-600 font-semibold mt-auto" href={href}>
					Try it →
				</Link>
			</div>
        </Card>
    )
}