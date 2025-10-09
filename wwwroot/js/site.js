document.addEventListener('DOMContentLoaded', function () {
    const toggleButton = document.getElementById('darkModeToggle');
    const body = document.body;

    // Check localStorage for mode
    if (localStorage.getItem('darkMode') === 'enabled') {
        body.classList.add('dark-mode');
        toggleButton.innerHTML = '<i class="bi bi-sun"></i> Light Mode';
    }

    toggleButton.addEventListener('click', function () {
        body.classList.toggle('dark-mode');
        if (body.classList.contains('dark-mode')) {
            localStorage.setItem('darkMode', 'enabled');
            toggleButton.innerHTML = '<i class="bi bi-sun"></i> Light Mode';
        } else {
            localStorage.removeItem('darkMode');
            toggleButton.innerHTML = '<i class="bi bi-moon-stars"></i> Dark Mode';
        }
    });
});
