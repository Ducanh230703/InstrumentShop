function loadInstrumentCategories() {
    console.log("Loading instrument categories...");
    fetch(`https://localhost:7236/api/InstrumentCategories`)
        .then(response => {
            console.log("Instrument categories loaded successfully:", response);
            return response.json();
        })
        .then(data => {
            const categoriesList = document.getElementById('categoryId');
            categoriesList.innerHTML = '';
            data.forEach(category => {
                const option = document.createElement('option');
                option.value = category.instrumentCategoryId;
                option.text = category.categoryName;
                categoriesList.appendChild(option);
            });
        })
        .catch(error => {
            console.error("Error loading instrument categories:", error);
        });
}

function loadInstruments() {
    console.log("loadInstruments: Starting to load instruments...");
    fetch(`https://localhost:7236/api/Instruments`)
        .then(response => {
            console.log("loadInstruments: Response received:", response);
            if (!response.ok) {
                console.error("loadInstruments: Response not OK:", response.status, response.statusText);
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            console.log("Data:", data);
            const instrumentsListBody = document.getElementById('instruments-list').querySelector('tbody');
            instrumentsListBody.innerHTML = '';
            data.forEach(instrument => {
                const row = document.createElement('tr');

                // Ô ID
                const idCell = row.insertCell();
                idCell.textContent = instrument.instrumentId;

                // Ô Tên
                const nameCell = row.insertCell();
                nameCell.textContent = instrument.name;

                // Ô Danh mục
                const categoryCell = row.insertCell();
                categoryCell.textContent = instrument.categoryName;


                // Ô Hình ảnh
                const imageCell = row.insertCell();
                let imageSrc = '';
                if (instrument.imageData) {
                    console.log("Instrument ID:", instrument.instrumentId, "imageData:", instrument.imageData);
                    if (typeof instrument.imageData === 'string') {
                        imageSrc = `data:image/jpeg;base64,${instrument.imageData}`;
                    } else {
                        // Xử lý trường hợp instrument.imageData không phải là chuỗi Base64
                        console.error("Instrument Image Data is not a Base64 string:", instrument.imageData);
                        imageSrc = ''; // Hoặc một URL hình ảnh mặc định nào đó
                    }
                }

                const priceCell = row.insertCell();
                priceCell.textContent = instrument.price;

                const img = document.createElement('img');
                img.src = imageSrc;
                img.alt = instrument.name;
                img.style.maxWidth = '100px';
                img.onerror = function () {
                    console.error("Error loading image for Instrument ID:", instrument.instrumentId);
                    imageCell.textContent = "Không thể tải ảnh";
                };
                imageCell.appendChild(img);

                // Ô Hành động
                const actionCell = row.insertCell();
                const editButton = document.createElement('button');
                editButton.textContent = 'Edit';
                editButton.onclick = () => showEditForm(instrument.instrumentId);

                const deleteButton = document.createElement('button');
                deleteButton.textContent = 'Delete';
                deleteButton.onclick = () => deleteInstrument(instrument.instrumentId);
                const feedbackButton = document.createElement('button');
         
                actionCell.appendChild(editButton);
                actionCell.appendChild(deleteButton);

                instrumentsListBody.appendChild(row);
            });
        })
        .catch(error => {
            console.error("loadInstruments: Error loading instruments:", error);
        });
    console.log("loadInstruments: Finished loading instruments.");
}
function showFeedback(instrumentId) {
    console.log("Showing feedback for instrument ID:", instrumentId);
    fetch(`https://localhost:7236/api/Feedbacks/byInstrument/${instrumentId}`) // Thay đổi URL API
        .then(response => response.json())
        .then(feedbacks => {
            // Tạo modal hoặc phần tử HTML để hiển thị feedback
            const feedbackModal = document.getElementById('feedback-modal');
            const feedbackList = document.getElementById('feedback-list');
            feedbackList.innerHTML = ''; // Xóa nội dung cũ

            feedbacks.forEach(feedback => {
                const feedbackItem = document.createElement('li');
                feedbackItem.textContent = feedback.content; // Giả sử API trả về trường 'content'
                feedbackList.appendChild(feedbackItem);
            });

            feedbackModal.style.display = 'block'; // Hiển thị modal
        })
        .catch(error => {
            console.error("Error loading feedbacks:", error);
        });
}

function closeFeedbackModal() {
    document.getElementById('feedback-modal').style.display = 'none';
}
function showCreateForm() {
    console.log("Showing create form...");
    document.getElementById('instrumentId').value = '';
    document.getElementById('name').value = '';
    document.getElementById('description').value = '';
    document.getElementById('price').value = '';
    document.getElementById('image').value = '';
    document.getElementById('categoryId').value = '';
    document.getElementById('modal-title').textContent = 'Create Instrument';
    document.getElementById('instrument-modal').style.display = 'block';
    document.getElementById('instrument-image').style.display = 'none';
    clearErrors();
    loadInstrumentCategories();
}

function showEditForm(id) {
    console.log("Showing edit form for instrument ID:", id);
    fetch(`https://localhost:7236/api/Instruments/${id}`)
        .then(response => {
            console.log("Instrument details loaded successfully:", response);
            return response.json();
        })
        .then(instrument => {
            document.getElementById('instrumentId').value = instrument.instrumentId;
            document.getElementById('name').value = instrument.name;
            document.getElementById('description').value = instrument.description;
            document.getElementById('price').value = instrument.price;
            document.getElementById('categoryId').value = instrument.categoryId.toString();
            document.getElementById('modal-title').textContent = 'Edit Instrument';
            document.getElementById('instrument-modal').style.display = 'block';
            document.getElementById('instrument-image').style.display = 'block';
            clearErrors();
            loadInstrumentCategories();

            if (instrument.imageData) {
                const base64Image = btoa(String.fromCharCode(...new Uint8Array(instrument.imageData)));
                document.getElementById('instrument-image').src = `data:image/jpeg;base64,${base64Image}`;
            } else {
                document.getElementById('instrument-image').src = '';
            }
        })
        .catch(error => {
            console.error("Error loading instrument details:", error);
        });
}

function deleteInstrument(id) {
    console.log("Deleting instrument ID:", id);
    if (confirm('Are you sure?')) {
        fetch(`https://localhost:7236/api/Instruments/${id}`, { method: 'DELETE' })
            .then(response => {
                console.log("Instrument deleted successfully:", response);
                loadInstruments();
            })
            .catch(error => {
                console.error("Error deleting instrument:", error);
            });
    }
}

function closeModal() {
    console.log("Closing modal...");
    document.getElementById('instrument-modal').style.display = 'none';
}

function clearErrors() {
    document.getElementById('name-error').textContent = '';
    document.getElementById('description-error').textContent = '';
    document.getElementById('price-error').textContent = '';
    document.getElementById('image-error').textContent = '';
    document.getElementById('categoryId-error').textContent = '';
}

function validateForm() {
    console.log("Validating form...");
    clearErrors();
    let isValid = true;
    const name = document.getElementById('name').value;
    const price = document.getElementById('price').value;
    const categoryId = document.getElementById('categoryId').value;

    if (!name) {
        document.getElementById('name-error').textContent = 'Name is required';
        isValid = false;
    }

    if (!price) {
        document.getElementById('price-error').textContent = 'Price is required';
        isValid = false;
    }

    if (!categoryId) {
        document.getElementById('categoryId-error').textContent = 'Category is required';
        isValid = false;
    }

    console.log("Form validation result:", isValid);
    return isValid;
}

document.getElementById('instrument-form').addEventListener('submit', function (event) {
    event.preventDefault();
    if (validateForm()) {
        const imageInput = document.getElementById('image');
        const file = imageInput.files[0];

        const formData = new FormData();
        formData.append('name', document.getElementById('name').value);
        formData.append('description', document.getElementById('description').value);
        formData.append('price', document.getElementById('price').value);
        formData.append('categoryId', document.getElementById('categoryId').value);
        if (file) { // Chỉ thêm hình ảnh nếu có tệp được chọn
            formData.append('image', file);
        }

        const instrumentId = document.getElementById('instrumentId').value;
        const method = instrumentId ? 'PUT' : 'POST';
        const url = instrumentId ? `https://localhost:7236/api/Instruments/${instrumentId}` : `https://localhost:7236/api/Instruments`;

        fetch(url, {
            method: method,
            body: formData
        })
            .then(response => {
                console.log("Form submitted successfully:", response);
                closeModal();
                loadInstruments();
            })
            .catch(error => {
                console.error("Error submitting form:", error);
            });
    }
});

loadInstruments();