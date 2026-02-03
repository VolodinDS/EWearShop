import type { FC } from "react"
import { Menu, ShoppingBag, UserRound } from "lucide-react"
import { useState } from "react"
import { NavLink } from "react-router-dom"
import { CheckoutModal } from "@/components/CheckoutModal.tsx"

interface BottomNavProps {
    cartItemsCount: number
}

const BottomNav: FC<BottomNavProps> = ({ cartItemsCount }) => {
    const [isCheckoutOpen, setIsCheckoutOpen] = useState(false)

    const linkStyles = ({ isActive }: { isActive: boolean }) =>
        `flex flex-col items-center gap-1 transition-colors ${
            isActive ? "text-black" : "text-gray-400 hover:text-gray-600"
        }`

    return (
        <div className="fixed bottom-6 left-1/2 -translate-x-1/2 z-50 w-[90%] max-w-md">
            <nav className="flex items-center justify-around bg-white/70 backdrop-blur-md border border-gray-200 px-6 py-3 rounded-full shadow-lg">

                <NavLink to="/" className={linkStyles}>
                    <Menu size={20} strokeWidth={1.5} />
                    <span className="text-[10px] uppercase tracking-widest font-medium">Catalog</span>
                </NavLink>

                <button className="relative flex flex-col items-center gap-1 text-gray-500 hover:text-black transition-colors" onClick={() => setIsCheckoutOpen(true)}>
                    <ShoppingBag size={20} strokeWidth={1.5} />
                    {cartItemsCount > 0 && (
                        <span className="absolute -top-1 -right-1 bg-black text-white text-[8px] w-4 h-4 rounded-full flex items-center justify-center">
                            {cartItemsCount}
                        </span>
                    )}
                    <span className="text-[10px] uppercase tracking-widest">Shopping Cart</span>
                </button>
                <CheckoutModal isOpen={isCheckoutOpen} onOpenChange={setIsCheckoutOpen} />

                <NavLink to="/management" className={linkStyles}>
                    <UserRound size={20} strokeWidth={1.5} />
                    <span className="text-[10px] uppercase tracking-widest font-medium">Management</span>
                </NavLink>

            </nav>
        </div>
    )
}

export default BottomNav
