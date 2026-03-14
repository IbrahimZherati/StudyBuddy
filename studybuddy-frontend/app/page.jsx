import Link from "next/link";

export default function Home() {
	return (
		<div className="page">
			<h1 className='title'>Study Together and make achievements</h1>
			<h2 className="sub-title">Login or Create an account</h2>
			<Link href="/login">
				<button className="btn-sign">Login</button>
			</Link>
			<Link href="/register">
				<button className="btn-sign">Register</button>
			</Link>
		</div>
	);
}
