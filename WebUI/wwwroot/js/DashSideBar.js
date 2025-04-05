window.setupActionMenus = () => {
    const menus = document.querySelectorAll('.action-menu');

    menus.forEach(menu => {
        menu.addEventListener('click', function (e) {
            e.stopPropagation();
            menus.forEach(m => m.classList.remove('active'));
            this.classList.toggle('active');
        });
    });

    document.addEventListener('click', () => {
        menus.forEach(m => m.classList.remove('active'));
    });
};

window.sidebarHandler = {
    init: function () {
        const sidebar = document.getElementById("sidebar");
        const openBtn = document.getElementById("openBtn");
        const closeBtn = document.getElementById("closeBtn");
        const toggles = document.querySelectorAll(".submenu-toggle");
        const navLinks = document.querySelectorAll(".sidebar-nav .nav-link");

        // Open sidebar
        openBtn?.addEventListener("click", () => {
            sidebar.classList.remove("hidden");
            openBtn.style.display = "none";
        });

        // Close sidebar
        closeBtn?.addEventListener("click", () => {
            sidebar.classList.add("hidden");
            openBtn.style.display = "block";
        });

        // Toggle submenu
        toggles.forEach(toggle => {
            toggle.addEventListener("click", (e) => {
                e.preventDefault(); // prevent default if it's a <button>
                const submenu = toggle.nextElementSibling;
                submenu.style.display = submenu.style.display === "flex" ? "none" : "flex";
            });
        });

        // Close sidebar on real navigation (ONLY <a> elements)
        navLinks.forEach(link => {
            if (link.tagName.toLowerCase() === "a") {
                link.addEventListener("click", () => {
                    if (window.innerWidth <= 768) {
                        sidebar.classList.add("hidden");
                        openBtn.style.display = "block";
                    }
                });
            }
        });
    }
};


