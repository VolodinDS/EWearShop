import type { FC } from "react"
import { Lock } from "lucide-react"
import * as React from "react"

interface LoginViewProps {
    onLogin: () => void
}

const LoginView: FC<LoginViewProps> = ({ onLogin }) => {
    const handleSubmit = (e: React.SubmitEvent<HTMLFormElement>) => {
        e.preventDefault()
        const formData = new FormData(e.currentTarget)
        const key = formData.get("mgmt_key") as string

        if (key) {
            localStorage.setItem("mgmt_key", key)
            onLogin()
        }
    }

    return (
        <div className="min-h-screen bg-white flex items-center justify-center px-4">
            <div className="w-full max-w-[320px] animate-fade-in">
                <header className="text-center mb-12">
                    <div className="flex justify-center mb-6">
                        <div className="w-12 h-12 border border-gray-100 flex items-center justify-center">
                            <Lock size={18} strokeWidth={1} className="text-gray-400" />
                        </div>
                    </div>
                    <h1 className="text-[12px] uppercase tracking-[0.4em] font-light">
                        Restricted Access
                    </h1>
                    <p className="text-[9px] text-gray-400 uppercase mt-2 tracking-widest leading-relaxed">
                        Please provide your management key
                        {" "}
                        <br />
                        {" "}
                        to synchronize with the terminal
                    </p>
                </header>

                <form onSubmit={handleSubmit} className="flex flex-col gap-6">
                    <div className="flex flex-col gap-2">
                        <label className="text-[8px] uppercase tracking-widest text-gray-400">
                            Management Key
                        </label>
                        <input
                            name="mgmt_key"
                            type="password"
                            required
                            autoFocus
                            placeholder="••••••••"
                            className="border-b border-gray-200 py-3 text-[12px] tracking-[0.3em] outline-none focus:border-black transition-colors placeholder:text-gray-200"
                        />
                    </div>

                    <button
                        type="submit"
                        className="bg-black text-white py-4 text-[10px] uppercase tracking-[0.3em] hover:bg-zinc-800 transition-all active:scale-[0.98]"
                    >
                        Authenticate
                    </button>
                </form>
            </div>
        </div>
    )
}

export default LoginView
