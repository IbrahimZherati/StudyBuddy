import Sidebar from "@/components/Layout/Sidebar";
import Navbar from "@/components/Layout/Navbar";

export default function MainLayout({ children }) {
    return (
        <div className="flex h-screen overflow-hidden bg-white">
            <Sidebar />

            <div className="flex-1 min-w-0 h-full min-h-0 overflow-hidden flex flex-col">
                <Navbar />

                <main className="h-full min-h-0 overflow-y-auto overflow-x-hidden md:ml-56 mt-16 flex-1">
                    {children}
                </main>
            </div>
        </div>
    )
}