document.addEventListener('DOMContentLoaded', function () {
    const wrapper = document.querySelector('.categories-navbar');
    const listItems = document.querySelector('.categories-list');
    const leftScrollBtn = document.querySelector('.left-arrow');
    const rightScrollBtn = document.querySelector('.right-arrow');
    const itemWidth = 200;

    //Update display arrow
    function updateArrows() {
        const maxscroll = listItems.scrollWidth - wrapper.clientWidth;
        leftScrollBtn.classList.toggle('d-none', wrapper.scrollLeft <= 0);
        rightScrollBtn.classList.toggle('d-none', wrapper.scrollLeft >= maxscroll);
    }

    rightScrollBtn.addEventListener('click', () => {
        wrapper.scrollBy({
            left: itemWidth,
            behavior: 'smooth'
        });
    });

    leftScrollBtn.addEventListener('click', () => {
        wrapper.scrollBy({
            left: -itemWidth,
            behavior: 'smooth'
        });
    });

    wrapper.addEventListener('scroll', updateArrows);
    window.addEventListener('resize', updateArrows);
    updateArrows(); // Khởi tạo ban đầu
});