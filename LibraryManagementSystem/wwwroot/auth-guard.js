(async function enforceAuthAndRole() {
    if (window.location.pathname.endsWith("/pages/forms/Login.html")) {
        return; // Don't check auth on login page
    }

    try {
        const response = await fetch('https://localhost:7262/api/User/role', {
            credentials: 'include'
        });

        if (!response.ok) {
            console.warn("User not authenticated, redirecting...");
            window.location.href = "/pages/forms/Login.html";
            return;
        }

        const data = await response.json();
        console.log("User role:", data);
    } catch (err) {
        console.error("Error checking authentication:", err);
        window.location.href = "/pages/forms/Login.html";
    }

    // Prevent back navigation after logout
    window.addEventListener("pageshow", function (event) {
        if (event.persisted || performance.getEntriesByType("navigation")[0].type === "back_forward") {
            location.reload();
        }
    });
})();
