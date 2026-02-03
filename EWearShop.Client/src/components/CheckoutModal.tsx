import type { FC } from "react"
import type { Product } from "@/components/ProductCard.tsx"
import * as Dialog from "@radix-ui/react-dialog"
import { X } from "lucide-react"
import { useCallback, useMemo } from "react"
import { useForm } from "react-hook-form"
import api from "@/api/api-client.ts"
import { getFullImageUrl } from "@/utils/getFullImageUrl.ts"
import { useCartStore } from "../store/useCartStore"

interface CheckoutFormData {
    firstName: string
    lastName: string
    fatherName?: string
    email: string
    phoneNumber: string
    address: {
        country: string
        city: string
        street: string
        zipCode: string
        houseNumber: string
        apartmentNumber?: string
        additionalInfo?: string
    }
}

export const CheckoutModal: FC<{ isOpen: boolean, onOpenChange: (open: boolean) => void }> = ({ isOpen, onOpenChange }) => {
    const { items, clearCart } = useCartStore()
    const { register, handleSubmit, formState: { errors, isSubmitting } } = useForm<CheckoutFormData>()

    const groupedItems = useMemo(() => items.reduce((acc, item) => {
        const existing = acc.find(i => i.id === item.id)
        if (existing) {
            existing.quantity += 1
        }
        else {
            acc.push({ ...item, quantity: 1 })
        }
        return acc
    }, [] as (Product & { quantity: number })[]), [items])

    const totalPrice = items.reduce((sum, item) => sum + item.price, 0)

    const onSubmit = useCallback(async (data: CheckoutFormData) => {
        const request = {
            customerInfo: {
                firstName: data.firstName,
                lastName: data.lastName,
                fatherName: data.fatherName,
                email: data.email,
                phoneNumber: data.phoneNumber,
                address: data.address,
            },
            items: groupedItems.map(item => ({
                productId: item.id,
                quantity: item.quantity,
            })),
        }

        try {
            await api.post("/api/orders", request)
            clearCart()
            onOpenChange(false)
        }
        catch (error) {
            console.error("Order submission failed", error)
        }
    }, [groupedItems])

    return (
        <Dialog.Root open={isOpen} onOpenChange={onOpenChange}>
            <Dialog.Portal>
                <Dialog.Overlay className="fixed inset-0 bg-black/10 backdrop-blur-md z-100" />
                <Dialog.Content className="fixed top-0 right-0 h-full w-full max-w-xl bg-white p-8 z-101 shadow-2xl overflow-y-auto focus:outline-none">

                    <div className="flex justify-between items-center mb-10">
                        <Dialog.Title className="text-[14px] uppercase tracking-[0.3em] font-light">Your Order</Dialog.Title>
                        <Dialog.Close className="text-gray-400 hover:text-black">
                            <X size={20} strokeWidth={1} />
                        </Dialog.Close>
                    </div>

                    <section className="mb-12">
                        <div className="space-y-4">
                            {groupedItems.map(item => (
                                <div key={item.id} className="flex gap-4 border-b border-gray-50 pb-4">
                                    <img
                                        src={getFullImageUrl(item.imageUrl)}
                                        className="w-16 h-16 object-cover bg-gray-50"
                                        alt={item.name}
                                    />
                                    <div className="flex-1 flex flex-col justify-between">
                                        <div className="flex justify-between items-start">
                                            <h4 className="text-[10px] uppercase tracking-wider font-medium">{item.name}</h4>
                                            <span className="text-[10px] font-semibold">
                                                {item.price}
                                                {" "}
                                                {item.currency}
                                            </span>
                                        </div>
                                        <div className="flex justify-between items-end">
                                            <span className="text-[9px] text-gray-400 uppercase tracking-tighter">
                                                Qty:
                                                {item.quantity}
                                            </span>
                                            <span className="text-[10px] text-gray-400">
                                                Subtotal:
                                                {item.price * item.quantity}
                                                {" "}
                                                {item.currency}
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            ))}
                        </div>
                        <div className="mt-6 flex justify-between items-center border-t border-black pt-4">
                            <span className="text-[11px] uppercase tracking-[0.2em] font-bold">Total Amount</span>
                            <span className="text-[14px] font-bold">
                                {totalPrice}
                                {" "}
                                RUB
                            </span>
                        </div>
                    </section>

                    <form onSubmit={handleSubmit(onSubmit)} className="space-y-10">
                        <section>
                            <h3 className="text-[10px] uppercase tracking-widest text-gray-400 mb-6 border-b pb-1">Delivery Details</h3>
                            <div className="grid grid-cols-2 gap-x-6 gap-y-6">
                                <InputField label="First Name" register={register("firstName", { required: true })} error={errors.firstName} />
                                <InputField label="Last Name" register={register("lastName", { required: true })} error={errors.lastName} />
                                <div className="col-span-2">
                                    <InputField label="Email Address" register={register("email", { required: true })} error={errors.email} />
                                </div>
                                <div className="col-span-2">
                                    <InputField label="Phone" register={register("phoneNumber", { required: true })} error={errors.phoneNumber} />
                                </div>
                                <InputField label="Country" register={register("address.country", { required: true })} error={errors.address?.country} />
                                <InputField label="City" register={register("address.city", { required: true })} error={errors.address?.city} />
                                <div className="col-span-2">
                                    <InputField label="Street Address" register={register("address.street", { required: true })} error={errors.address?.street} />
                                </div>
                                <InputField label="House Number" register={register("address.houseNumber", { required: true })} error={errors.address?.houseNumber} />
                                <InputField label="Zip Code" register={register("address.zipCode", { required: true })} error={errors.address?.zipCode} />
                            </div>
                        </section>

                        <button
                            disabled={isSubmitting || items.length === 0}
                            type="submit"
                            className="w-full bg-black text-white py-5 text-[11px] uppercase tracking-[0.3em] hover:bg-zinc-800 transition-all disabled:bg-gray-100 disabled:text-gray-400"
                        >
                            {isSubmitting ? "Processing..." : "Place Order"}
                        </button>
                    </form>
                </Dialog.Content>
            </Dialog.Portal>
        </Dialog.Root>
    )
}

function InputField({ label, register, error }: any) {
    return (
        <div className="flex flex-col gap-1">
            <label className="text-[9px] uppercase tracking-tighter text-gray-500">{label}</label>
            <input
                {...register}
                className={`border-b ${error ? "border-red-500" : "border-gray-200"} focus:border-black outline-none py-1 text-[12px] transition-colors`}
            />
        </div>
    )
}
