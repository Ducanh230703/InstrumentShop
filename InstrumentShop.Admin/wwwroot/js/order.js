// API endpoint
function loadOrders() {
    console.log("Loading orders...");
    fetch('https://localhost:7236/api/Orders')
        .then(response => {
            console.log("Orders loaded successfully:", response);
            return response.json();
        })
        .then(data => {
            const ordersList = document.getElementById('order-list');
            ordersList.innerHTML = '';
            data.forEach(order => {
                const row = document.createElement('tr');
                row.innerHTML = `
                    <td>${order.orderId}</td>
                    <td>${order.orderDate}</td>
                    <td>${order.orderStatus}</td>
                    <td>${order.totalAmount}</td>
                    <td>
                        <button onclick="showOrderDetail(${order.orderId})">Detail</button>
                    </td>
                `;
                ordersList.appendChild(row);
            });
        })
        .catch(error => {
            console.error("Error loading orders:", error);
        });
}

function showOrderDetail(id) {
    console.log("Showing order detail for order ID:", id);
    fetch(`https://localhost:7236/api/OrderDetails/order/${id}`) // Lấy OrderDetails thay vì Orders
        .then(response => {
            console.log("Order details loaded successfully:", response);
            return response.json();
        })
        .then(details => {
            displayOrderDetails(details);
            $('#orderDetailModal').modal('show');

            const closeModalButton = document.querySelector('#orderDetailModal .btn-secondary');
            closeModalButton.addEventListener('click', () => {
                $('#orderDetailModal').modal('hide');
                console.log('Modal đã đóng');
            });
        })
        .catch(error => {
            console.error("Error loading order details:", error);
        });
}
function displayOrderDetails(orderDetails) {
    const orderDetailContent = document.getElementById("order-detail-content");
    orderDetailContent.innerHTML = "";

    if (orderDetails.length === 0) {
        orderDetailContent.innerHTML = "<p>Không có chi tiết đơn hàng.</p>";
        return;
    }

    const table = document.createElement("table");
    table.classList.add("table");
    table.innerHTML = `
        <thead>
            <tr>
                <th>Sản phẩm</th>
                <th>Số lượng</th>
                <th>Giá</th>
            </tr>
        </thead>
        <tbody id="order-detail-list">
        </tbody>
    `;
    orderDetailContent.appendChild(table);

    const orderDetailList = document.getElementById("order-detail-list");
    orderDetails.forEach(detail => {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${detail.instrumentName}</td>
            <td>${detail.quantity}</td>
            <td>${detail.price}</td>
        `;
        orderDetailList.appendChild(row);
    });
}


loadOrders();