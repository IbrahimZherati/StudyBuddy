import Sidebar from "@/components/Layout/Sidebar";
import Navbar from "@/components/Layout/Navbar";

export default function MainLayout({ children }) {
    return (
        <div className="flex h-screen bg-white">
            <Sidebar />

            <div className="flex-1 min-w-0 flex flex-col gap-6">
                <Navbar />

                <main className="md:ml-56 mt-16 p-6 flex-1">
                    {children}
                </main>
            </div>
        </div>
    )
}