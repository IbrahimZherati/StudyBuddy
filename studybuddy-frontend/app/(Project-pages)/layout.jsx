import Sidebar from "@/components/Layout/Sidebar";
import Navbar from "@/components/Layout/Navbar";

export default function RootLayout({ children }) {
    return (
        <div className="flex h-screen bg-white">
            <Sidebar />

            <div className="flex-1 flex flex-col">
                <Navbar />

                <main className="p-6 overflow-y-auto">
                    {children}
                </main>
            </div>
        </div>
    )
}