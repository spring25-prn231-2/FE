document.addEventListener('DOMContentLoaded', function () {
    const wrapper = document.querySelector('.nav-items-wrapper');
    const navItems = document.querySelector('.nav-items');
    const leftArrow = document.querySelector('.left-arrow');
    const rightArrow = document.querySelector('.right-arrow');
    const itemWidth = 200; // Chiều rộng ước tính của mỗi nav-item

    function updateArrows() {
        const maxScroll = navItems.scrollWidth - wrapper.clientWidth;
        leftArrow.classList.toggle('d-none', wrapper.scrollLeft <= 0);
        rightArrow.classList.toggle('d-none', wrapper.scrollLeft >= maxScroll);
    }

    rightArrow.addEventListener('click', () => {
        wrapper.scrollBy({
            left: itemWidth,
            behavior: 'smooth'
        });
    });

    leftArrow.addEventListener('click', () => {
        wrapper.scrollBy({
            left: -itemWidth,
            behavior: 'smooth'
        });
    });

    wrapper.addEventListener('scroll', updateArrows);
    window.addEventListener('resize', updateArrows);
    updateArrows(); // Khởi tạo ban đầu
});