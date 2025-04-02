window.initSwiperSliders = () => {
    // HERO SWIPER
    const heroEl = document.querySelector('.card-wrapper');
    if (heroEl) {
        try {
            if (window.heroSwiper) {
                window.heroSwiper.destroy(true, true);
            }
            window.heroSwiper = new Swiper(heroEl, {
                autoplay: {
                    delay: 4000,
                    disableOnInteraction: false,
                },
                loop: true,
                spaceBetween: 30,
                pagination: {
                    el: '.swiper-pagination',
                    clickable: true,
                    dynamicBullets: true,
                },
                navigation: {
                    nextEl: '.swiper-button-next',
                    prevEl: '.swiper-button-prev',
                },
                breakpoints: {
                    0: { slidesPerView: 1 },
                    768: { slidesPerView: 1 },
                    1024: { slidesPerView: 1 }
                }
            });
        } catch (e) {
            console.warn("Hero Swiper failed to initialize:", e);
        }
    }

    // PRODUCT SWIPER
    const swiper = new Swiper('.product-swiper', {
        slidesPerView: 1,
        spaceBetween: 20,
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        breakpoints: {
            640: {
                slidesPerView: 2,
            },
            768: {
                slidesPerView: 3,
            },
            1024: {
                slidesPerView: 5,
            },
        }
    })
};
