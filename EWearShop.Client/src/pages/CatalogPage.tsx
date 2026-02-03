import type { FC } from "react"
import type { Product } from "@/components/ProductCard.tsx"
import { useEffect, useState } from "react"
import api from "@/api/api-client.ts"
import ProductCard from "@/components/ProductCard.tsx"
import { useCartStore } from "@/store/useCartStore.ts"

const CatalogPage: FC = () => {
    const [products, setProducts] = useState<Product[]>([])
    const [loading, setLoading] = useState(true)
    const addToCart = useCartStore(state => state.addToCart)

    useEffect(() => {
        const fetchProducts = async () => {
            return await api.get<Product[]>("/api/products")
        }

        fetchProducts().then((response) => {
            setProducts(response.data)
        }).catch((error) => {
            console.error("Failed to fetch products", error)
        }).finally(() => {
            setLoading(false)
        })
    }, [])

    if (loading) {
        return (
            <div className="flex h-screen items-center justify-center">
                <span className="text-[10px] uppercase tracking-[0.3em] animate-pulse">Loading...</span>
            </div>
        )
    }

    return (
        <div className="min-h-screen bg-white pb-32">
            <header className="py-12 flex justify-center">
                <h1 className="text-2xl font-light tracking-[0.5em] uppercase">EWearShop</h1>
            </header>

            <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-px bg-gray-100 border-y border-gray-100">
                {products.map((product, index) => (
                    <div key={product.id} className="bg-white">
                        <ProductCard
                            index={index}
                            product={product}
                            onAddToCart={addToCart}
                        />
                    </div>
                ))}
            </div>

            {products.length === 0 && (
                <div className="py-20 text-center text-[10px] uppercase tracking-widest text-gray-400">
                    No products found.
                </div>
            )}
        </div>
    )
}

export default CatalogPage
