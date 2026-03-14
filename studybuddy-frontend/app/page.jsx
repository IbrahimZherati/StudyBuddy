import Link from "next/link";

export default function Home() {
	return (
		<div className="page">
			<h1 className='title'>Welcome to Study Buddy!</h1>
			<h2 className="sub-title">Login or Create an account</h2>
			<Link href="/login">
				<button className="mb-4 text-2xl">Login</button>
			</Link>
			<Link href="/register">
				<button className="text-2xl">Register</button>
			</Link>
		</div>
	);
}
