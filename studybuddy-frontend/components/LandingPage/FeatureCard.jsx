import ClickableCard from "../ClickableCard";

export default function FeatureCard({icon, title, desc, href}) {
    return (
		<ClickableCard href={href}>
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
		</ClickableCard>
    )
}