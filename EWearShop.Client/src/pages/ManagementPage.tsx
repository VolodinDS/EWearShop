import type { FC } from "react"
import * as Accordion from "@radix-ui/react-accordion"
import { ChevronDown, LogOut, Package, User } from "lucide-react"
import { useEffect, useState } from "react"
import api from "@/api/api-client.ts"
import LoginView from "@/components/LoginView.tsx"
import { getFullImageUrl } from "@/utils/getFullImageUrl.ts"

interface OrderItem {
    id: string
    quantity: number
    name: string
    description: string
    price: number
    currency: string
    imageUrl: string
}

interface AdminOrder {
    id: string
    orderDate: string
    customerInfo: {
        firstName: string
        lastName: string
        email: string
        phoneNumber: string
    }
    items: OrderItem[]
}

const ManagementPage: FC = () => {
    const [isAuthenticated, setIsAuthenticated] = useState(!!localStorage.getItem("mgmt_key"))
    const [orders, setOrders] = useState<AdminOrder[]>([])
    const [loading, setLoading] = useState(false)

    useEffect(() => {
        if (isAuthenticated) {
            setLoading(true)
            api.get<AdminOrder[]>("/api/admin/orders")
                .then(res => setOrders(res.data))
                .catch(() => setIsAuthenticated(false))
                .finally(() => setLoading(false))
        }
    }, [isAuthenticated])

    if (!isAuthenticated) {
        return <LoginView onLogin={() => setIsAuthenticated(true)} />
    }

    return (
        <div className="min-h-screen bg-[#F9F9F9] pt-20 pb-32 px-4 md:px-10">
            <div className="max-w-5xl mx-auto">
                <header className="flex justify-between items-end mb-12 border-b border-gray-200 pb-8">
                    <div>
                        <h1 className="text-[16px] uppercase tracking-[0.4em] font-light">Order Management</h1>
                        <p className="text-[10px] text-gray-400 uppercase mt-2 tracking-widest">System Terminal / Live Updates</p>
                    </div>
                    <button
                        onClick={() => {
                            localStorage.removeItem("mgmt_key")
                            setIsAuthenticated(false)
                        }}
                        className="text-[10px] uppercase tracking-widest text-gray-400 hover:text-black flex items-center gap-2 transition-colors"
                    >
                        <LogOut size={14} />
                        {" "}
                        Exit Session
                    </button>
                </header>

                {loading
                    ? (
                            <div className="text-[10px] uppercase tracking-[0.3em] animate-pulse py-20 text-center">Data Synchronization...</div>
                        )
                    : (
                            <Accordion.Root type="single" collapsible className="space-y-4">
                                {orders.map(order => (
                                    <Accordion.Item key={order.id} value={order.id} className="bg-white border border-gray-100 overflow-hidden shadow-sm hover:shadow-md transition-shadow">
                                        <Accordion.Header>
                                            <Accordion.Trigger className="w-full flex flex-wrap md:flex-nowrap items-center justify-between p-6 gap-4 group text-left">
                                                <div className="flex items-center gap-6">
                                                    <div className="flex flex-col gap-1">
                                                        <span className="text-[9px] text-gray-400 uppercase tracking-tighter font-bold">Order Reference</span>
                                                        <span className="text-[11px] font-mono uppercase tracking-tighter">
                                                            #
                                                            {order.id.slice(0, 8)}
                                                        </span>
                                                    </div>
                                                    <div className="h-8 w-px bg-gray-100 hidden md:block" />
                                                    <div className="flex flex-col gap-1">
                                                        <span className="text-[9px] text-gray-400 uppercase tracking-tighter font-bold">Date</span>
                                                        <span className="text-[11px] uppercase">{new Date(order.orderDate).toLocaleDateString("en-GB")}</span>
                                                    </div>
                                                </div>

                                                <div className="flex items-center gap-8 ml-auto">
                                                    <div className="flex flex-col gap-1 items-end">
                                                        <span className="text-[9px] text-gray-400 uppercase tracking-tighter font-bold">Customer</span>
                                                        <span className="text-[11px] uppercase">
                                                            {order.customerInfo.firstName}
                                                            {" "}
                                                            {order.customerInfo.lastName}
                                                        </span>
                                                    </div>
                                                    <div className="flex flex-col gap-1 items-end min-w-20">
                                                        <span className="text-[9px] text-gray-400 uppercase tracking-tighter font-bold">Total Items</span>
                                                        <span className="text-[11px] font-bold">{order.items.length}</span>
                                                    </div>
                                                    <ChevronDown className="text-gray-300 group-data-[state=open]:rotate-180 transition-transform duration-300" size={16} />
                                                </div>
                                            </Accordion.Trigger>
                                        </Accordion.Header>

                                        <Accordion.Content className="data-[state=open]:animate-slide-down data-[state=closed]:animate-slide-up overflow-hidden bg-gray-50 border-t border-gray-50">
                                            <div className="p-6">
                                                <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
                                                    <div className="space-y-4">
                                                        <h4 className="text-[10px] uppercase tracking-widest text-black border-b border-gray-200 pb-2 flex items-center gap-2">
                                                            <User size={12} />
                                                            {" "}
                                                            Contact Info
                                                        </h4>
                                                        <div className="text-[11px] space-y-2 text-gray-600">
                                                            <p className="flex justify-between">
                                                                <span>Email:</span>
                                                                {" "}
                                                                <span className="text-black">{order.customerInfo.email}</span>
                                                            </p>
                                                            <p className="flex justify-between">
                                                                <span>Phone:</span>
                                                                {" "}
                                                                <span className="text-black">{order.customerInfo.phoneNumber}</span>
                                                            </p>
                                                        </div>
                                                    </div>

                                                    <div className="md:col-span-2 space-y-4">
                                                        <h4 className="text-[10px] uppercase tracking-widest text-black border-b border-gray-200 pb-2 flex items-center gap-2">
                                                            <Package size={12} />
                                                            {" "}
                                                            Order Items
                                                        </h4>
                                                        <div className="space-y-3">
                                                            {order.items.map(item => (
                                                                <div key={item.id} className="flex items-center gap-4 bg-white p-3 border border-gray-100">
                                                                    <img src={getFullImageUrl(item.imageUrl)} className="w-12 h-12 object-cover" alt="" />
                                                                    <div className="flex-1">
                                                                        <p className="text-[10px] uppercase font-medium">{item.name}</p>
                                                                        <p className="text-[9px] text-gray-400 uppercase tracking-tighter">
                                                                            Qty:
                                                                            {item.quantity}
                                                                            {" "}
                                                                            ×
                                                                            {item.price}
                                                                            {" "}
                                                                            {item.currency}
                                                                        </p>
                                                                    </div>
                                                                    <span className="text-[11px] font-bold">
                                                                        {item.quantity * item.price}
                                                                        {" "}
                                                                        {item.currency}
                                                                    </span>
                                                                </div>
                                                            ))}
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </Accordion.Content>
                                    </Accordion.Item>
                                ))}
                            </Accordion.Root>
                        )}
            </div>
        </div>
    )
}

export default ManagementPage
