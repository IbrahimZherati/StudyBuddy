import Link from "next/link";
import Card from "../Card";

export default function FeatureCard({icon, title, desc, href}) {
	const additionalStyles = "active:bg-gray-200 active:translate-y-1";
    return (
		<Link href={href}>
			<Card additionalStyles={additionalStyles}>
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

					<p className="text-blue-600 font-semibold mt-auto">
						Try it →
					</p>
				</div>
			</Card>
		</Link>
    )
}