import type { FC } from "react"
import { motion } from "framer-motion"
import { getFullImageUrl } from "@/utils/getFullImageUrl.ts"

export interface Product {
    id: string
    name: string
    description: string
    category: string
    price: number
    currency: string
    imageUrl: string
}

interface ProductCardProps {
    index: number
    product: Product
    onAddToCart: (product: Product) => void
}

const ProductCard: FC<ProductCardProps> = ({ index, product, onAddToCart }) => {
    const delay = (index % 4) * 0.1

    return (
        <motion.div
            initial={{ opacity: 0, y: 30 }}
            whileInView={{ opacity: 1, y: 0 }}
            transition={{
                duration: 0.8,
                delay,
                ease: [0.21, 0.47, 0.32, 0.98],
            }}
            viewport={{ once: true }}
            className="group flex flex-col w-full"
        >

            <div className="group flex flex-col w-full animate-fade-in">
                <div className="relative aspect-square overflow-hidden bg-[#F2F2F2]">
                    <div className="absolute top-2 left-2 z-10 bg-white/95 px-2 h-5 flex items-center justify-center shadow-sm">
                        <span className="text-[8px] uppercase tracking-[0.2em] font-bold text-black leading-none translate-y-[0.5px]">
                            {product.category}
                        </span>
                    </div>

                    <img
                        src={getFullImageUrl(product.imageUrl)}
                        alt={product.name}
                        loading="lazy"
                        className="w-full h-full object-cover object-center transition-transform duration-1000 group-hover:scale-105"
                    />
                    <button
                        onClick={(e) => {
                            e.stopPropagation()
                            onAddToCart(product)
                        }}
                        className="absolute bottom-0 left-0 right-0 bg-white/80 backdrop-blur-md py-4 text-[10px] uppercase tracking-[0.2em]
                   transition-all duration-500 active:bg-black active:text-white
                   opacity-100 translate-y-0
                   md:opacity-0 md:translate-y-4 md:group-hover:opacity-100 md:group-hover:translate-y-0"
                    >
                        Add to cart
                    </button>
                </div>

                <div className="mt-4 flex flex-col px-2 pb-2">
                    <div className="flex justify-between items-start gap-4">
                        <h3 className="text-[11px] uppercase tracking-wider text-black leading-tight font-medium">
                            {product.name}
                        </h3>
                        <span className="text-[11px] font-semibold whitespace-nowrap">
                            {product.price}
                            {" "}
                            {product.currency}
                        </span>
                    </div>

                    <p className="mt-2 text-[10px] text-gray-400 leading-relaxed text-left line-clamp-2 uppercase tracking-tight">
                        {product.description}
                    </p>
                </div>
            </div>
        </motion.div>
    )
}

export default ProductCard
