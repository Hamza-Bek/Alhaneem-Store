window.sessionHelper = {
    getSessionId: function () {
        const cookies = document.cookie.split("; ");
        for (const cookie of cookies) {
            const [name, value] = cookie.split("=");
            if (name.trim() === "guestSessionId") {
                return value;
            }
        }
        return null;
    },
    setSessionId: function (sessionId) {
        const expires = new Date();
        expires.setDate(expires.getDate() + 30); // 30 days
        document.cookie = `guestSessionId=${sessionId}; expires=${expires.toUTCString()}; path=/`;
    }
};
