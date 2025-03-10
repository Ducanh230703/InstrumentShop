document.getElementById('login-form').addEventListener('submit', function(event) {
        event.preventDefault();

        const email = document.getElementById('email').value;
        const password = document.getElementById('password').value;

    fetch('https://localhost:7236/api/Admins/authenticate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email, password })
        })
            .then(response => {
                if (response.status === 200) {
                    // Đăng nhập thành công
                     window.location.href = '/Home/Index';
                } else if (response.status === 401) {
                    // Sai email hoặc password
                    document.getElementById('error-message').textContent = 'Invalid email or password.';
                } else {
                    // Lỗi server
                    console.error('Server error:', response.status);
                    document.getElementById('error-message').textContent = 'An error occurred.';
                }
            })
        .catch(error => {
            console.error('Error:', error);
            document.getElementById('error-message').textContent = 'An error occurred.';
        });
    });