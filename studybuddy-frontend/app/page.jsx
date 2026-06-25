import CommunityFeatures from "@/components/HomePage/CommunityFeatures";
import Footer from "@/components/HomePage/Footer";
import Header from "@/components/HomePage/Header";
import Hero from "@/components/HomePage/Hero";
import HowItWorks from "@/components/HomePage/HowItWorks";

export default function Home() {
	return (
		<div className="flex flex-col gap-4">
           <Header/>
		   <main className="flex flex-col gap-4 p-2">
				<Hero/>
				<CommunityFeatures/>
				<HowItWorks/>
		   </main>
		   <Footer/>
		</div>
	);
}
