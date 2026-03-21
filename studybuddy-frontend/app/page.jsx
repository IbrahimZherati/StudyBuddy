import CommunityFeatures from "@/components/CommunityFeatures";
import Footer from "@/components/Footer";
import FreeTools from "@/components/FreeTools";
import Header from "@/components/Header";
import Hero from "@/components/Hero";
import HowItWorks from "@/components/HowItWorks";
import Review from "@/components/Review";

export default function Home() {
	return (
		<div className="flex flex-col gap-4">
           <Header/>
           <Hero/>
		   <FreeTools/>
		   <CommunityFeatures/>
		   <HowItWorks/>
		   <Review/>
		   <Footer/>
		</div>
	);
}
