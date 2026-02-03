import type { Product } from "../components/ProductCard"
import { create } from "zustand"
import { persist } from "zustand/middleware"

interface CartState {
    items: Product[]
    addToCart: (product: Product) => void
    totalCount: () => number
    clearCart: () => void
    removeFromCart: (productId: string) => void
}

export const useCartStore = create<CartState>()(
    persist(
        (set, get) => ({
            items: [],
            addToCart: product => set(state => ({ items: [...state.items, product] })),
            totalCount: () => get().items.length,
            clearCart: () => set({ items: [] }),
            removeFromCart: productId => set(state => ({
                items: state.items.filter(item => item.id !== productId),
            })),
        }),
        { name: "ewear-cart" },
    ),
)
