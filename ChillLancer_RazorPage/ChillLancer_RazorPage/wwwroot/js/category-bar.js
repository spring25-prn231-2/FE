document.addEventListener('DOMContentLoaded', function () {
    const storageKeyCategory = 'categoryStorage';
    const storageKeyTimestamp = 'fetchedDate';
    const serverUrl = window.serverUrl;
    const apiUrl = serverUrl + '/api/category';

    // Check current date with the last fetched date
    function validateFetchData() {
        const lastFetch = localStorage.getItem(storageKeyTimestamp);
        if (!lastFetch) return false;   //If null, false...

        const lastFetchDate = new Date(lastFetch).toDateString();
        const todayDate = new Date().toDateString();

        return lastFetchDate === todayDate; // Only fetch if today is newer lastFetch
    }

    // Check data in localStorage
    if (validateFetchData() === false) {
        fetchAndStoreCategories();
    } else {
        const savedData = localStorage.getItem(storageKeyCategory);
        if (savedData) {
            renderNavCategories(JSON.parse(savedData));
            renderNavCategoriesMobile(JSON.parse(savedData));
        } else {
            console.log("No cached category data found.");
            fetchAndStoreCategories(); // Fetch nếu dữ liệu bị mất
        }
    }

    // Fetch API Category Method
    function fetchAndStoreCategories() {
        console.log("Fetching category data...");
        fetch(apiUrl)
            .then(response => response.json())
            .then(data => {
                // Save to localStorage
                localStorage.setItem(storageKeyCategory, JSON.stringify(data));
                localStorage.setItem(storageKeyTimestamp, new Date().toISOString());
                renderNavCategories(data);
                renderNavCategoriesMobile(data);
            })
            .catch(error => console.error('Error fetching categories:', error));
    }

    function renderNavCategories(categories) {
        const container = document.querySelector('.categories-nav');
        if (!container) return;

        // Group categories by MajorName and BriefName
        const grouped = categories.reduce((cat, category) => {
            const major = category['major-name'];
            const brief = category['brief-name'];
            const specialized = category['specialized-name'];

            if (!cat[major]) cat[major] = {};
            if (!cat[major][brief]) cat[major][brief] = [];

            cat[major][brief].push(specialized);
            return cat;
        }, {});

        // Create HTML structure
        let html = '';
        for (const [majorName, briefGroups] of Object.entries(grouped)) {
            html += `
                    <li class="category-item">
                        <a href="/Categories?majorName=${encodeURIComponent(majorName)}" class="major-link">${majorName}</a>
                        <ul class="sub-categories-list four-columns-menu">`;

            for (const [briefName, specializedNames] of Object.entries(briefGroups)) {
                html += `
                        <li class="sub-categories-item category-brief-name">${briefName}
                            <ul class="group-sub-categories">
                            ${specializedNames.map(specName =>
                    `<li class="sub-categories-item"><a href="#" class="specialized-link">${specName}</a></li>`
                ).join('')}
                            </ul>
                        </li>`;
            }
            html += `</ul></li>`;
        }

        container.innerHTML = html;
    }

    function renderNavCategoriesMobile(categories) {
        const container = document.querySelector('.categories-mobile-nav');
        if (!container) return;

        // Tạo Set để lấy các MajorName duy nhất
        const majorNames = [...new Set(categories.map(category => category['major-name']))];

        // Tạo HTML structure
        let html = '';
        majorNames.forEach(majorName => {
            html += `
            <li>
                <a href="/Categories?majorName=${encodeURIComponent(majorName)}">${majorName}</a>
            </li>`;
        });

        container.innerHTML = html;
    }
});

//=================================[ Category Slide Tray ]=================================
document.addEventListener('DOMContentLoaded', function () {
    const wrapper = document.querySelector('.categories-navbar');
    const listItems = document.querySelector('.categories-nav');
    const leftScrollBtn = document.querySelector('.left-arrow');
    const rightScrollBtn = document.querySelector('.right-arrow');
    const itemWidth = 300;

    //Update display arrow
    function updateArrows() {
        const maxscroll = wrapper.scrollWidth - wrapper.clientWidth;
        leftScrollBtn.classList.toggle('d-none', wrapper.scrollLeft <= 0);
        rightScrollBtn.classList.toggle('d-none', wrapper.scrollLeft >= maxscroll - 1);
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