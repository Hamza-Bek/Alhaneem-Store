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

let map, marker;

window.initLeafletMap = function (dotNetHelper) {
    map = L.map('map').setView([30.0444, 31.2357], 13); // Default center (Cairo)

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png').addTo(map);

    map.on('click', function (e) {
        setMarker(e.latlng.lat, e.latlng.lng, dotNetHelper);
    });
};

window.detectUserLocation = function (dotNetHelper) {
    if (!navigator.geolocation) {
        alert("Geolocation is not supported.");
        return;
    }

    navigator.geolocation.getCurrentPosition(function (position) {
        const lat = position.coords.latitude;
        const lng = position.coords.longitude;
        map.setView([lat, lng], 13);
        setMarker(lat, lng, dotNetHelper);
    }, function (error) {
        alert("Failed to detect your location.");
        console.error(error);
    });
};

function setMarker(lat, lng, dotNetHelper) {
    if (!marker) {
        marker = L.marker([lat, lng]).addTo(map);
    } else {
        marker.setLatLng([lat, lng]);
    }
    dotNetHelper.invokeMethodAsync('SetLocation', lat, lng);
}


window.showToast = function (message, type) {
    const toast = document.createElement("div");
    toast.textContent = message;
    toast.className = "blazor-toast " + type;

    document.body.appendChild(toast);

    setTimeout(() => {
        toast.classList.add("show");
    }, 100);

    setTimeout(() => {
        toast.classList.remove("show");
        setTimeout(() => toast.remove(), 300);
    }, 3000);
};

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