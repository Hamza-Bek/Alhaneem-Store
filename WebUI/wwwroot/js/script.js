new Swiper('.card-wrapper', {
    autoplay: {
        delay: 2000, // Time in milliseconds
        disableOnInteraction: false,
    },
    loop: true,
    spaceBetween: 30,

    // If we need pagination
    pagination: {
        el: '.swiper-pagination',
        clickable: true,
        dynamicBullets: true,
    },

    // Navigation arrows
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
    },

    breakpoints : {
        0: {
            slidesPerView : 1
        },
        768: {
            slidesPerView : 1
        },
        1024: {
            slidesPerView : 1
        }
    }
});

window.copyTextToClipboard = function (text) {
    navigator.clipboard.writeText(text).then(function () {
        console.log("Copied to clipboard:", text);
    }, function (err) {
        console.error("Failed to copy:", err);
    });
};



//window.showToast = function (message, type) {
//    const toast = document.createElement("div");
//    toast.textContent = message;
//    toast.className = "blazor-toast " + type;

//    const container = document.getElementById("toast-container");
//    if (!container) {
//        console.error("Toast container not found.");
//        return;
//    }

//    container.appendChild(toast);

//    // Show the toast with animation
//    setTimeout(() => {
//        toast.classList.add("show");
//    }, 100);

//    // Auto-hide the toast
//    setTimeout(() => {
//        toast.classList.remove("show");
//        setTimeout(() => toast.remove(), 300);
//    }, 3000);
//};

window.showRejectConfirmation = async function () {
    return new Promise((resolve) => {
        const overlay = document.createElement('div');
        overlay.style.position = 'fixed';
        overlay.style.top = 0;
        overlay.style.left = 0;
        overlay.style.width = '100vw';
        overlay.style.height = '100vh';
        overlay.style.background = 'rgba(0,0,0,0.6)';
        overlay.style.display = 'flex';
        overlay.style.justifyContent = 'center';
        overlay.style.alignItems = 'center';
        overlay.style.zIndex = 10000;

        const box = document.createElement('div');
        box.style.background = '#fff';
        box.style.padding = '1rem 1.5rem';
        box.style.width = '400px'; 
        box.style.maxWidth = '90%';
        box.style.borderRadius = '10px';
        box.style.boxShadow = '0 0 10px rgba(0,0,0,0.2)';
        box.innerHTML = `
                <h3 style="margin-bottom: 1rem;">Are you sure you want to reject this order?</h3>
                <div style="display: flex; justify-content: space-between; gap: 1rem;">
                    <button id="confirmReject" style="background: #d9534f; color: white; padding: .5rem 1rem; border: none; border-radius: 5px;">Yes</button>
                    <button id="cancelReject" style="background: #5bc0de; color: white; padding: .5rem 1rem; border: none; border-radius: 5px;">Cancel</button>
                </div>
            `;

        overlay.appendChild(box);
        document.body.appendChild(overlay);

        document.getElementById('confirmReject').onclick = () => {
            document.body.removeChild(overlay);
            resolve(true);
        };

        document.getElementById('cancelReject').onclick = () => {
            document.body.removeChild(overlay);
            resolve(false);
        };
    });
}

