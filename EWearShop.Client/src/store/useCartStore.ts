import type { Product } from "../components/ProductCard"
import { create } from "zustand"
import { persist } from "zustand/middleware"

interface CartState {
    items: Product[]
    addToCart: (product: Product) => void
    totalCount: () => number
}

export const useCartStore = create<CartState>()(
    persist(
        (set, get) => ({
            items: [],
            addToCart: product => set(state => ({ items: [...state.items, product] })),
            totalCount: () => get().items.length,
        }),
        { name: "ewear-cart" },
    ),
)
