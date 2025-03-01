const storageKeyLanguage = "languageStorage";
const storageKeySkill = "skillStorage";
const storageKeyTimestamp = 'fetchedDate';
const serverUrl = window.serverUrl;
const apiAllLanguage = serverUrl + "/api/language";
const apiAllSkill = serverUrl + "/api/skill";

// Check current date with the last fetched date
function validateFetchData() {
    const lastFetch = localStorage.getItem(storageKeyTimestamp);
    if (!lastFetch) return false;   //If null, false...

    const lastFetchDate = new Date(lastFetch).toDateString();
    const todayDate = new Date().toDateString();

    return lastFetchDate === todayDate; // Only fetch if today is newer lastFetch
}

async function fetchData() {
    try {
        console.log("Fetching Skill and Language data...");
        //Promis.all - (call both fetch at the same time)
        const [languagesResponse, skillsResponse] = await Promise.all([
            fetch(apiAllLanguage),
            fetch(apiAllSkill)
        ]);

        //Check API Response
        if (!languagesResponse.ok) {
            throw new Error("API Language request failed!");
        }
        if (!skillsResponse.ok) {
            throw new Error("API Skill request failed!");
        }

        const [languages, skills] = await Promise.all([
            languagesResponse.json(),
            skillsResponse.json()
        ]);

        // Save data into local storage
        localStorage.setItem(storageKeyLanguage, JSON.stringify(languages));
        localStorage.setItem(storageKeySkill, JSON.stringify(skills));
        localStorage.setItem(storageKeyTimestamp, new Date().toISOString());
    } catch (error) {
        console.error("Error fetching data:", error);
    }
}

// Kiểm tra và fetch dữ liệu nếu cần
if (validateFetchData() === false) {
    fetchData();
}