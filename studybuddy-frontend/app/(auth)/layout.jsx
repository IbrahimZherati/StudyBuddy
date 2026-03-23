export default function RootLayout({ children }) {
	return (
		<main className="w-full min-h-dvh overflow-y-auto 
						bg-linear-90 from-[#111b71] via-[#7b8eef] to-[#111b71]"
		>

			<div className="flex justify-center items-start md:items-center w-full min-h-dvh px-4 py-8">
				{children}
			</div>
		</main>
	);
}
