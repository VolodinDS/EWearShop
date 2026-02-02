import { BrowserRouter, Route, Routes } from "react-router-dom"
import BottomNav from "@/components/BottomNav.tsx"
import "./App.css"

function App() {
    return (
        <BrowserRouter>
            <div className="min-h-screen flex flex-col">
                <main className="grow container mx-auto">
                    <Routes>
                        <Route path="/" element={<div className="p-10 text-center uppercase tracking-zara">Контент каталога</div>} />
                        <Route path="/management" element={<div className="p-10 text-center">Admin Zone</div>} />
                    </Routes>
                </main>
                <BottomNav cartItemsCount={0} />
            </div>
        </BrowserRouter>
    )
}

export default App
