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


