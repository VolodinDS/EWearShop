const API_URL = import.meta.env.VITE_API_TARGET

export function getFullImageUrl(relativeUrl: string) {
    if (!relativeUrl) {
        return ""
    }
    if (relativeUrl.startsWith("http")) {
        return relativeUrl
    }
    const cleanRelative = relativeUrl.startsWith("/") ? relativeUrl : `/${relativeUrl}`
    return `${API_URL}${cleanRelative}`
}
