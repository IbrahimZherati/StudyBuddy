import Sidebar from "@/components/Layout/Sidebar";
import Navbar from "@/components/Layout/Navbar";

export default function RootLayout({ children }) {
    return (
        <div className="flex h-screen bg-white">
            <Sidebar />

            <div className="flex-1 flex flex-col">
                <Navbar />

                <main className="md:ml-56 mt-16 p-6">
                    {children}
                </main>
            </div>
        </div>
    )
}