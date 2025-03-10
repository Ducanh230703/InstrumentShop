function loadInstrumentCategories() {
    console.log("Loading instrument categories...");
    fetch(`https://localhost:7236/api/InstrumentCategories`)
        .then(response => {
            console.log("Instrument categories loaded successfully:", response);
            return response.json();
        })
        .then(data => {
            const categoriesList = document.getElementById('categories-list');
            categoriesList.innerHTML = '';
            data.forEach(category => {
                const row = document.createElement('tr');
                row.innerHTML = `
                    <td>${category.instrumentCategoryId}</td>
                    <td>${category.categoryName}</td>
                    <td>
                        <button onclick="showEditForm(${category.instrumentCategoryId})">Edit</button>
                        <button onclick="deleteInstrumentCategory(${category.instrumentCategoryId})">Delete</button>
                    </td>
                `;
                categoriesList.appendChild(row);
            });
        })
        .catch(error => {
            console.error("Error loading instrument categories:", error);
        });
}

// Show create form
function showCreateForm() {
    console.log("Showing create form...");
    document.getElementById('instrumentCategoryId').value = '';
    document.getElementById('categoryName').value = '';
    document.getElementById('modal-title').textContent = 'Create Instrument Category';
    document.getElementById('category-modal').style.display = 'block';
    clearErrors();
}

// Show edit form
function showEditForm(id) {
    console.log("Showing edit form for category ID:", id);
    fetch(`https://localhost:7236/api/InstrumentCategories/${id}`)
        .then(response => {
            console.log("Instrument category details loaded successfully:", response);
            return response.json();
        })
        .then(category => {
            // Tạo bản sao mới của đối tượng category
            const newCategory = { ...category };

            document.getElementById('instrumentCategoryId').value = newCategory.instrumentCategoryId;
            document.getElementById('categoryName').value = newCategory.categoryName;
            document.getElementById('modal-title').textContent = 'Edit Instrument Category';
            document.getElementById('category-modal').style.display = 'block';
            clearErrors();
        })
        .catch(error => {
            console.error("Error loading instrument category details:", error);
        });
}

// Delete instrument category
function deleteInstrumentCategory(id) {
    console.log("Deleting instrument category ID:", id);
    if (confirm('Are you sure?')) {
        fetch(`https://localhost:7236/api/InstrumentCategories/${id}`, { method: 'DELETE' })
            .then(response => {
                console.log("Instrument category deleted successfully:", response);
                loadInstrumentCategories();
            })
            .catch(error => {
                console.error("Error deleting instrument category:", error);
            });
    }
}

// Close modal
function closeModal() {
    console.log("Closing modal...");
    document.getElementById('category-modal').style.display = 'none';
}

// Clear errors
function clearErrors() {
    document.getElementById('categoryName-error').textContent = '';
}

// Validate form
function validateForm() {
    console.log("Validating form...");
    clearErrors();
    let isValid = true;
    const categoryName = document.getElementById('categoryName').value;

    if (!categoryName) {
        document.getElementById('categoryName-error').textContent = 'Category Name is required';
        isValid = false;
    }

    console.log("Form validation result:", isValid);
    return isValid;
}

// Submit form
document.getElementById('category-form').addEventListener('submit', function (event) {
    event.preventDefault();
    if (validateForm()) {
        let category = {
            categoryName: document.getElementById('categoryName').value
        };

        const categoryId = document.getElementById('instrumentCategoryId').value;
        const method = categoryId ? 'PUT' : 'POST';
        const url = categoryId ? `https://localhost:7236/api/InstrumentCategories/${categoryId}` : `https://localhost:7236/api/InstrumentCategories`;

        // Chỉ thêm categoryId vào object category khi cập nhật (PUT request)
        if (method === 'PUT') {
            category = {
                instrumentCategoryId: categoryId,
                categoryName: document.getElementById('categoryName').value
            };
        }

        console.log("Submitting form with data:", category);
        fetch(url, {
            method: method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(category)
        })
            .then(response => {
                console.log("Form submitted successfully:", response);
                closeModal();
                loadInstrumentCategories();
            })
            .catch(error => {
                console.error("Error submitting form:", error);
            });
    }
});
loadInstrumentCategories();