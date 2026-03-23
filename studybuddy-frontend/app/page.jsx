import CommunityFeatures from "@/components/CommunityFeatures";
import Footer from "@/components/Footer";
import GuestFeatures from "@/components/GuestFeatures";
import Header from "@/components/Header";
import Hero from "@/components/Hero";
import HowItWorks from "@/components/HowItWorks";
import Review from "@/components/Review";

export default function Home() {
	return (
		<div className="flex flex-col gap-4">
           <Header/>
		   <main className="flex flex-col gap-4 p-2">
				<Hero/>
				<GuestFeatures/>
				<CommunityFeatures/>
				<HowItWorks/>
				<Review/>
		   </main>
		   <Footer/>
		</div>
	);
}
