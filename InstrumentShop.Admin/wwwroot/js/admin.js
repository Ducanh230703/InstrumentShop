// Load data
function loadAdmins() {
    console.log("Loading admins...");
    fetch('https://localhost:7236/api/Admins')
        .then(response => {
            console.log("Admins loaded successfully:", response);
            return response.json();
        })
        .then(data => {
            const adminsList = document.getElementById('admins-list');
            adminsList.innerHTML = '';
            data.forEach(admin => {
                const row = document.createElement('tr');
                row.innerHTML = `
                    <td>${admin.adminId}</td>
                    <td>${admin.email}</td>
                    <td>${admin.fullName}</td>
                    <td>
                        <button onclick="showEditForm(${admin.adminId})">Edit</button>
                        <button onclick="deleteAdmin(${admin.adminId})">Delete</button>
                    </td>
                `;
                adminsList.appendChild(row);
            });
        })
        .catch(error => {
            console.error("Error loading admins:", error);
        });
}

// Show create form
function showCreateForm() {
    console.log("Showing create form...");
    document.getElementById('adminId').value = '';
    document.getElementById('email').value = '';
    document.getElementById('passwordHash').value = '';
    document.getElementById('fullName').value = '';
    document.getElementById('modal-title').textContent = 'Create Admin';
    document.getElementById('admin-modal').style.display = 'block';
    clearErrors();
}

// Show edit form
function showEditForm(id) {
    console.log("Showing edit form for admin ID:", id);
    fetch(`https://localhost:7236/api/Admins/${id}`)
        .then(response => {
            console.log("Admin details loaded successfully:", response);
            return response.json();
        })
        .then(admin => {
            // Tạo bản sao mới của đối tượng admin
            const newAdmin = { ...admin };

            document.getElementById('adminId').value = newAdmin.adminId;
            document.getElementById('email').value = newAdmin.email;
            document.getElementById('passwordHash').value = newAdmin.passwordHash;
            document.getElementById('fullName').value = newAdmin.fullName;
            document.getElementById('modal-title').textContent = 'Edit Admin';
            document.getElementById('admin-modal').style.display = 'block';
            clearErrors();
        })
        .catch(error => {
            console.error("Error loading admin details:", error);
        });
}

// Delete admin
function deleteAdmin(id) {
    console.log("Deleting admin ID:", id);
    if (confirm('Are you sure?')) {
        fetch(`https://localhost:7236/api/Admins/${id}`, { method: 'DELETE' })
            .then(response => {
                console.log("Admin deleted successfully:", response);
                loadAdmins();
            })
            .catch(error => {
                console.error("Error deleting admin:", error);
            });
    }
}

// Close modal
function closeModal() {
    console.log("Closing modal...");
    document.getElementById('admin-modal').style.display = 'none';
}

// Clear errors
function clearErrors() {
    document.getElementById('email-error').textContent = '';
    document.getElementById('passwordHash-error').textContent = '';
    document.getElementById('fullName-error').textContent = '';
}

// Validate form
function validateForm() {
    console.log("Validating form...");
    clearErrors();
    let isValid = true;
    const email = document.getElementById('email').value;
    const passwordHash = document.getElementById('passwordHash').value;
    const fullName = document.getElementById('fullName').value;

    if (!email) {
        document.getElementById('email-error').textContent = 'Email is required';
        isValid = false;
    }

    if (!passwordHash) {
        document.getElementById('passwordHash-error').textContent = 'Password is required';
        isValid = false;
    }

    if (!fullName) {
        document.getElementById('fullName-error').textContent = 'Full Name is required';
        isValid = false;
    }

    console.log("Form validation result:", isValid);
    return isValid;
}

// Submit form
document.getElementById('admin-form').addEventListener('submit', function (event) {
    event.preventDefault();
    if (validateForm()) {
        let admin = {
            email: document.getElementById('email').value,
            passwordHash: document.getElementById('passwordHash').value,
            fullName: document.getElementById('fullName').value
        };

        const adminId = document.getElementById('adminId').value;
        const method = adminId ? 'PUT' : 'POST';
        const url = adminId ? `https://localhost:7236/api/Admins/${adminId}` : 'https://localhost:7236/api/Admins';

        // Chỉ thêm adminId vào object admin khi cập nhật (PUT request)
        if (method === 'PUT') {
            admin = {
                adminId: adminId,
                email: document.getElementById('email').value,
                passwordHash: document.getElementById('passwordHash').value,
                fullName: document.getElementById('fullName').value
            };
        }

        console.log("Submitting form with data:", admin);
        fetch(url, {
            method: method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(admin)
        })
            .then(response => {
                console.log("Form submitted successfully:", response);
                closeModal();
                loadAdmins();
            })
            .catch(error => {
                console.error("Error submitting form:", error);
            });
    }
});

// Load admins on page load
loadAdmins();