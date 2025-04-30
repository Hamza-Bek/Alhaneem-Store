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

