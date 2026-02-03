import axios from "axios"

const api = axios.create({
    baseURL: import.meta.env.VITE_API_TARGET || "http://localhost:5000",
})

api.interceptors.request.use((config) => {
    const key = localStorage.getItem("mgmt_key")
    if (key) {
        config.headers["X-Admin-Key"] = key
    }
    return config
})

export default api
