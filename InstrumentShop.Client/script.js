
function fetchAndDisplayProducts() {
    fetch('https://localhost:7236/api/Instruments')
      .then(response => response.json())
      .then(data => {
        displayProducts(data);
      })
      .catch(error => {
        console.error('Lỗi khi lấy dữ liệu từ API:', error);
      });
  }
  

  function displayProducts(products) {
    const container = document.querySelector('.container');
    container.innerHTML = '';

    products.forEach(product => {
        const productDiv = document.createElement('div');
        productDiv.classList.add('product');
        productDiv.dataset.instrumentId = product.instrumentId;

        const imageUrl = `data:image/jpeg;base64,${product.imageData}`;

        productDiv.innerHTML = `
            <img src="${imageUrl}" alt="${product.name}">
            <h3>${product.name}</h3>
            <p>${product.description}</p>
            <p class="price">$${product.price}</p>
            <button class="buy-button">Thêm vào giỏ</button>
        `;

        container.appendChild(productDiv);
    });

    attachAddToCartListeners();
}

function fetchInstrumentCategories() {
  fetch('https://localhost:7236/api/InstrumentCategories')
      .then(response => response.json())
      .then(data => {
          displayInstrumentCategories(data);
      })
      .catch(error => {
          console.error('Lỗi khi lấy dữ liệu instrumentCategory:', error);
      });
}

function displayInstrumentCategories(categories) {
  const dropdown = document.querySelector('.dropdown');
  dropdown.innerHTML = ''; // Xóa nội dung cũ

  categories.forEach(category => {
      const li = document.createElement('li');
      const a = document.createElement('a');
      a.href = '#'; // Thêm liên kết nếu cần
      a.textContent = category.categoryName;
      li.appendChild(a);
      dropdown.appendChild(li);
  });
}

window.onload = function() {
  fetchInstrumentCategories();
};
  
  function attachAddToCartListeners() {
    const buyButtons = document.querySelectorAll('.buy-button');
    buyButtons.forEach(button => {
        button.addEventListener('click', function() {
            const productDiv = this.parentElement;
            const product = {
                id: productDiv.dataset.instrumentId,
                name: productDiv.querySelector('h3').textContent,
                price: parseFloat(productDiv.querySelector('.price').textContent.replace('$', '').replace(',', '')),
                image: productDiv.querySelector('img').src,
                quantity: 1
            };
            addToCart(product);
        });
    });
}

function addToCart(product) {
  let cartData = JSON.parse(localStorage.getItem('cart')) ;
  const existingProductIndex = cartData.findIndex(item => item.id === product.id); // Tìm theo id

  if (existingProductIndex !== -1) {
      cartData[existingProductIndex].quantity++;
  } else {
      cartData.push(product);
  }

  localStorage.setItem('cart', JSON.stringify(cartData));
  displayCart();
  updateCartIcon();
}

function displayCart() {
    const cartItems = document.getElementById('cart-items');
    const cartTotal = document.getElementById('cart-total');
    cartItems.innerHTML = '';
    let cartData = JSON.parse(localStorage.getItem('cart')) || [];
    let total = 0;

    cartData.forEach((item, index) => {
        const li = document.createElement('li');
        li.innerHTML = `
            <img src="${item.image}" style="width: 50px; height: 50px; margin-right: 10px;">
            <span>${item.name} - $${item.price} x ${item.quantity}</span>
            <button class="increase-quantity" data-index="${index}">+</button>
            <button class="decrease-quantity" data-index="${index}">-</button>
            <button class="remove-item" data-index="${index}">Xóa</button>
        `;
        cartItems.appendChild(li);
        total += item.price * item.quantity;
    });

    cartTotal.textContent = total;
    attachCartItemListeners();
}

function attachCartItemListeners() {
    document.querySelectorAll('.increase-quantity').forEach(button => {
        button.addEventListener('click', function() {
            const index = parseInt(this.dataset.index);
            increaseQuantity(index);
        });
    });

    document.querySelectorAll('.decrease-quantity').forEach(button => {
        button.addEventListener('click', function() {
            const index = parseInt(this.dataset.index);
            decreaseQuantity(index);
        });
    });

    document.querySelectorAll('.remove-item').forEach(button => {
        button.addEventListener('click', function() {
            const index = parseInt(this.dataset.index);
            removeItem(index);
        });
    });
}

function increaseQuantity(index) {
    let cartData = JSON.parse(localStorage.getItem('cart')) || [];
    cartData[index].quantity++;
    localStorage.setItem('cart', JSON.stringify(cartData));
    displayCart();
    updateCartIcon();
}

function decreaseQuantity(index) {
    let cartData = JSON.parse(localStorage.getItem('cart')) || [];
    if (cartData[index].quantity > 1) {
        cartData[index].quantity--;
        localStorage.setItem('cart', JSON.stringify(cartData));
        displayCart();
        updateCartIcon();
    }
}

function removeItem(index) {
    let cartData = JSON.parse(localStorage.getItem('cart')) || [];
    cartData.splice(index, 1);
    localStorage.setItem('cart', JSON.stringify(cartData));
    displayCart();
    updateCartIcon();
}

function updateCartIcon() {
    let cartData = JSON.parse(localStorage.getItem('cart')) || [];
    cartIcon.textContent = `Giỏ hàng (${cartData.reduce((total, item) => total + item.quantity, 0)})`;
}

window.onload = function() {
    fetchAndDisplayProducts();
    updateCartIcon();
};

const cartIcon = document.getElementById('cart-icon');
const cart = document.getElementById('cart');
const closeCart = document.getElementById('close-cart');
const checkoutButton = document.getElementById('checkout-button');
const checkoutForm = document.getElementById('checkout-form');

cartIcon.addEventListener('click', function() {
    cart.style.display = 'block';
    displayCart();
});

closeCart.addEventListener('click', function() {
    cart.style.display = 'none';
});

checkoutButton.addEventListener('click', () => {
    checkoutForm.style.display = 'block';
});

const closeCheckoutFormButton = document.getElementById('close-checkout-form');

closeCheckoutFormButton.addEventListener('click', () => {
    checkoutForm.style.display = 'none';
});

const customerForm = document.getElementById('customer-form');
customerForm.addEventListener('submit', (event) => {
  event.preventDefault();

  const firstName = document.getElementById('firstName').value;
  const lastName = document.getElementById('lastName').value;
  const email = document.getElementById('email').value;
  const address = document.getElementById('address').value;
  const phoneNumber = document.getElementById('phoneNumber').value;

  console.log(JSON.stringify({
      firstName: firstName,
      lastName: lastName,
      email: email,
      address: address,
      phoneNumber: phoneNumber
  }));

  fetch('https://localhost:7236/api/Customers', {
      method: 'POST',
      headers: {
          'Content-Type': 'application/json',
      },
      body: JSON.stringify({
          firstName: firstName,
          lastName: lastName,
          email: email,
          address: address,
          phoneNumber: phoneNumber,
      }),
  })
  .then(response => {
      if (response.ok) {
          console.log('Thêm khách hàng thành công');
      } else {
          console.error('Lỗi khi thêm khách hàng');
          response.text().then(text => console.error('Response body:', text)); // In response body để xem chi tiết lỗi từ server
      }
  })
  .catch(error => {
      console.error('Lỗi khi thêm khách hàng:', error);
  });
});
