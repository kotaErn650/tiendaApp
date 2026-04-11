import { products, clients, categories, getCategoryName, formatPrice } from './data.js';

// ── DOM refs ───────────────────────────────────────────────
const tabs        = document.querySelectorAll('.tab');
const pages       = document.querySelectorAll('.page');
const tabBar      = document.getElementById('tab-bar');
const btnBack     = document.getElementById('btn-back');
const pageTitle   = document.getElementById('page-title');

// Summary
const countCat    = document.getElementById('count-categories');
const countProd   = document.getElementById('count-products');
const countCli    = document.getElementById('count-clients');

// Products
const searchInput = document.getElementById('search-input');
const productList = document.getElementById('products-list');

// Detail
const pickerProd    = document.getElementById('picker-products');
const detailNombre  = document.getElementById('detail-nombre');
const detailDesc    = document.getElementById('detail-descripcion');
const detailCat     = document.getElementById('detail-categoria');
const detailPrecio  = document.getElementById('detail-precio');
const labelCantidad = document.getElementById('label-cantidad');
const btnMinus      = document.getElementById('btn-minus');
const btnPlus       = document.getElementById('btn-plus');
const detailTotal   = document.getElementById('detail-total');
const btnAddCart    = document.getElementById('btn-add-cart');

// Clients
const clientList  = document.getElementById('clients-list');

// Toast
const toast       = document.getElementById('toast');

// ── State ──────────────────────────────────────────────────
let cantidad = 1;
let currentProduct = null;
let toastTimer = null;

// ── Navigation ─────────────────────────────────────────────
const PAGE_TITLES = {
  summary:        'ShopApp',
  products:       'Productos',
  'product-detail': 'Detalle',
  clients:        'Clientes',
};

function showPage(pageId, fromTab = false) {
  pages.forEach(p => p.classList.remove('active'));
  document.getElementById(`page-${pageId}`).classList.add('active');

  pageTitle.textContent = PAGE_TITLES[pageId] ?? 'ShopApp';

  const isDetail = pageId === 'product-detail';
  btnBack.classList.toggle('hidden', !isDetail);
  tabBar.classList.toggle('hidden', isDetail);

  if (fromTab) {
    tabs.forEach(t => t.classList.remove('active'));
    const activeTab = document.querySelector(`.tab[data-tab="${pageId}"]`);
    if (activeTab) activeTab.classList.add('active');
  }
}

// Tab clicks
tabs.forEach(tab => {
  tab.addEventListener('click', () => showPage(tab.dataset.tab, true));
});

// Back button
btnBack.addEventListener('click', () => showPage('products', true));

// ── Summary ─────────────────────────────────────────────────
countCat.textContent  = categories.length;
countProd.textContent = products.length;
countCli.textContent  = clients.length;

// ── Products list ───────────────────────────────────────────
function renderProducts(list) {
  productList.innerHTML = '';
  if (list.length === 0) {
    productList.innerHTML = '<li class="empty-state">No se encontraron productos.</li>';
    return;
  }
  list.forEach(p => {
    const li = document.createElement('li');
    li.className = 'item-card';
    const priceClass = p.precio <= 100 ? 'low' : 'high';
    li.innerHTML = `
      <span class="item-name">${p.nombre}</span>
      <span class="item-desc">${p.descripcion}</span>
      <span class="item-price ${priceClass}">${formatPrice(p.precio)}</span>
    `;
    li.addEventListener('click', () => openProductDetail(p.id));
    productList.appendChild(li);
  });
}
renderProducts(products);

searchInput.addEventListener('input', () => {
  const q = searchInput.value.trim().toLowerCase();
  renderProducts(q ? products.filter(p => p.nombre.toLowerCase().includes(q)) : products);
});

// ── Product detail ──────────────────────────────────────────
// Populate picker
products.forEach(p => {
  const opt = document.createElement('option');
  opt.value = p.id;
  opt.textContent = p.nombre;
  pickerProd.appendChild(opt);
});

function openProductDetail(productId) {
  pickerProd.value = productId;
  cantidad = 1;
  labelCantidad.textContent = '1';
  updateDetail();
  showPage('product-detail');
}

function updateDetail() {
  const id = parseInt(pickerProd.value, 10);
  currentProduct = products.find(p => p.id === id) ?? null;
  if (!currentProduct) return;

  detailNombre.textContent  = currentProduct.nombre;
  detailDesc.textContent    = currentProduct.descripcion;
  detailCat.textContent     = getCategoryName(currentProduct.categoryId);

  const priceClass = currentProduct.precio <= 100 ? 'low' : 'high';
  detailPrecio.textContent  = formatPrice(currentProduct.precio);
  detailPrecio.className    = `detail-price ${priceClass}`;

  calcTotal();
}

function calcTotal() {
  if (!currentProduct) return;
  detailTotal.textContent = formatPrice(currentProduct.precio * cantidad);
}

pickerProd.addEventListener('change', () => {
  cantidad = 1;
  labelCantidad.textContent = '1';
  updateDetail();
});

btnMinus.addEventListener('click', () => {
  if (cantidad > 1) {
    cantidad--;
    labelCantidad.textContent = cantidad;
    calcTotal();
  }
});

btnPlus.addEventListener('click', () => {
  cantidad++;
  labelCantidad.textContent = cantidad;
  calcTotal();
});

btnAddCart.addEventListener('click', () => {
  if (!currentProduct) return;
  showToast(`✅ ${cantidad} × "${currentProduct.nombre}" agregado(s).\nTotal: ${formatPrice(currentProduct.precio * cantidad)}`);
});

// Initialize detail with first product
if (products.length > 0) {
  pickerProd.value = products[0].id;
  updateDetail();
}

// ── Clients list ────────────────────────────────────────────
clients.forEach(c => {
  const li = document.createElement('li');
  li.className = 'item-card client';
  li.innerHTML = `
    <span class="item-name">${c.nombre}</span>
    <span class="item-address">📍 ${c.direccion}</span>
  `;
  clientList.appendChild(li);
});

// ── Toast ───────────────────────────────────────────────────
function showToast(msg) {
  toast.textContent = msg;
  toast.classList.remove('hidden');
  clearTimeout(toastTimer);
  toastTimer = setTimeout(() => toast.classList.add('hidden'), 3000);
}

// ── PWA service worker ──────────────────────────────────────
if ('serviceWorker' in navigator) {
  window.addEventListener('load', () => {
    navigator.serviceWorker.register('/sw.js').catch(() => {});
  });
}
