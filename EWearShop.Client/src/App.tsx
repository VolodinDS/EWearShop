import { BrowserRouter, Route, Routes } from "react-router-dom"
import { Toaster } from "sonner"
import BottomNav from "@/components/BottomNav.tsx"
import CatalogPage from "@/pages/CatalogPage.tsx"
import ManagementPage from "@/pages/ManagementPage.tsx"
import { useCartStore } from "@/store/useCartStore.ts"
import "./App.css"

function App() {
    const cartItemsCount = useCartStore(state => state.totalCount())

    return (
        <>
            <Toaster
                position="top-center"
                toastOptions={{
                    style: {
                        borderRadius: "0px",
                        textTransform: "uppercase",
                        fontSize: "10px",
                        letterSpacing: "0.1em",
                        fontFamily: "inherit",
                    },
                }}
            />
            <BrowserRouter>
                <div className="min-h-screen flex flex-col">
                    <main className="grow container mx-auto">
                        <Routes>
                            <Route path="/" element={<CatalogPage />} />
                            <Route path="/management" element={<ManagementPage />} />
                        </Routes>
                    </main>
                    <BottomNav cartItemsCount={cartItemsCount} />
                </div>
            </BrowserRouter>
        </>
    )
}

export default App
