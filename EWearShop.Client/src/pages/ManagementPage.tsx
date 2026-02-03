import type { FC } from "react"
import { AnimatePresence, motion } from "framer-motion"
import { Lock, LogOut } from "lucide-react"
import { useEffect, useState } from "react"
import api from "@/api/api-client.ts"

interface Order {
    id: string
    customerName: string
    totalAmount: number
    createdAt: string
    status: string
}

const ManagementPage: FC = () => {
    const [key, setKey] = useState(localStorage.getItem("mgmt_key") || "")
    const [isAuthenticated, setIsAuthenticated] = useState(!!key)
    const [orders, setOrders] = useState<Order[]>([])
    const [loading, setLoading] = useState(false)

    const handleLogin = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault()
        const formData = new FormData(e.currentTarget)
        const inputKey = formData.get("key") as string
        if (inputKey) {
            localStorage.setItem("mgmt_key", inputKey)
            setKey(inputKey)
            setIsAuthenticated(true)
        }
    }

    const handleLogout = () => {
        localStorage.removeItem("mgmt_key")
        setIsAuthenticated(false)
        setKey("")
    }

    useEffect(() => {
        if (isAuthenticated) {
            setLoading(true)
            api.get<Order[]>("/api/admin/orders")
                .then(res => setOrders(res.data))
                .catch(() => {
                    // Если API вернул 401, сбрасываем авторизацию
                    handleLogout()
                })
                .finally(() => setLoading(false))
        }
    }, [isAuthenticated])

    return (
        <div className="min-h-screen bg-white pt-20 pb-32 px-4 md:px-10">
            <AnimatePresence mode="wait">
                {!isAuthenticated
                    ? (
                            <motion.div
                                key="login"
                                initial={{ opacity: 0, y: 20 }}
                                animate={{ opacity: 1, y: 0 }}
                                exit={{ opacity: 0, y: -20 }}
                                className="max-w-sm mx-auto mt-20 text-center"
                            >
                                <Lock className="mx-auto mb-6 text-gray-300" size={32} strokeWidth={1} />
                                <h2 className="text-[12px] uppercase tracking-[0.3em] mb-8">Access Restricted</h2>
                                <form onSubmit={handleLogin} className="flex flex-col gap-4">
                                    <input
                                        name="key"
                                        type="password"
                                        placeholder="ENTER MANAGEMENT KEY"
                                        className="border-b border-black py-2 text-center text-[11px] tracking-widest outline-none placeholder:text-gray-300"
                                        autoFocus
                                    />
                                    <button className="bg-black text-white py-3 text-[10px] uppercase tracking-widest hover:bg-gray-900 transition-colors">
                                        Authenticate
                                    </button>
                                </form>
                            </motion.div>
                        )
                    : (
                            <motion.div
                                key="dashboard"
                                initial={{ opacity: 0 }}
                                animate={{ opacity: 1 }}
                                className="max-w-6xl mx-auto"
                            >
                                <div className="flex justify-between items-end mb-12 border-b border-gray-100 pb-6">
                                    <div>
                                        <h1 className="text-[14px] uppercase tracking-[0.4em] font-light">Management Terminal</h1>
                                        <p className="text-[10px] text-gray-400 uppercase mt-2">Overseeing all incoming orders</p>
                                    </div>
                                    <button onClick={handleLogout} className="text-[10px] uppercase tracking-widest text-gray-400 hover:text-black flex items-center gap-2">
                                        <LogOut size={14} />
                                        {" "}
                                        Exit
                                    </button>
                                </div>

                                {loading
                                    ? (
                                            <div className="text-[10px] uppercase tracking-widest animate-pulse">Synchronizing...</div>
                                        )
                                    : (
                                            <div className="overflow-x-auto">
                                                <table className="w-full text-left">
                                                    <thead>
                                                        <tr className="border-b border-gray-100 text-[10px] uppercase tracking-widest text-gray-400">
                                                            <th className="py-4 font-normal">Order ID</th>
                                                            <th className="py-4 font-normal">Date</th>
                                                            <th className="py-4 font-normal">Amount</th>
                                                            <th className="py-4 font-normal text-right">Status</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody className="divide-y divide-gray-50">
                                                        {orders.map(order => (
                                                            <tr key={order.id} className="text-[11px] tracking-tight hover:bg-gray-50 transition-colors">
                                                                <td className="py-6 font-mono text-gray-500">
                                                                    {order.id.slice(0, 8)}
                                                                    ...
                                                                </td>
                                                                <td className="py-6 text-gray-600">
                                                                    {new Date(order.createdAt).toLocaleDateString("en-GB")}
                                                                </td>
                                                                <td className="py-6 font-medium">
                                                                    {order.totalAmount}
                                                                    {" "}
                                                                    RUB
                                                                </td>
                                                                <td className="py-6 text-right">
                                                                    <span className="bg-black text-white px-2 py-1 text-[9px] uppercase tracking-tighter">
                                                                        {order.status}
                                                                    </span>
                                                                </td>
                                                            </tr>
                                                        ))}
                                                    </tbody>
                                                </table>
                                            </div>
                                        )}
                            </motion.div>
                        )}
            </AnimatePresence>
        </div>
    )
}

export default ManagementPage
