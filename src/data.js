// ── Data (mirrors ShopDbContext seed) ─────────────────────
export const categories = [
  { id: 1, nombre: 'Computadoras' },
  { id: 2, nombre: 'Monitores' },
  { id: 3, nombre: 'Teclados' },
  { id: 4, nombre: 'Ratones' },
  { id: 5, nombre: 'Auriculares' },
  { id: 6, nombre: 'Impresoras' },
  { id: 7, nombre: 'Almacenamiento' },
];

export const products = [
  { id: 1, nombre: 'Laptop Pro 15',     descripcion: 'Laptop de alto rendimiento, 16GB RAM, 512GB SSD',     precio: 1299.99, categoryId: 1 },
  { id: 2, nombre: 'Monitor 4K 27"',    descripcion: 'Monitor Ultra HD con panel IPS y 144Hz',               precio: 499.00,  categoryId: 2 },
  { id: 3, nombre: 'Teclado Mecánico',  descripcion: 'Teclado mecánico retroiluminado RGB, switches Blue',   precio: 89.99,   categoryId: 3 },
  { id: 4, nombre: 'Ratón Inalámbrico', descripcion: 'Ratón ergonómico con DPI ajustable hasta 16000',       precio: 49.99,   categoryId: 4 },
  { id: 5, nombre: 'Auriculares BT',    descripcion: 'Auriculares bluetooth con cancelación de ruido',       precio: 149.00,  categoryId: 5 },
  { id: 6, nombre: 'Impresora Láser',   descripcion: 'Impresora láser monocromática 40ppm',                  precio: 199.99,  categoryId: 6 },
  { id: 7, nombre: 'SSD 1TB',           descripcion: 'Unidad de estado sólido NVMe PCIe 4.0',                precio: 99.50,   categoryId: 7 },
];

export const clients = [
  { id: 1, nombre: 'Ana García',    direccion: 'Calle Mayor 10, Madrid' },
  { id: 2, nombre: 'Carlos López',  direccion: 'Av. Libertad 55, Barcelona' },
];

export function getCategoryName(categoryId) {
  return categories.find(c => c.id === categoryId)?.nombre ?? '—';
}

export function formatPrice(amount) {
  return new Intl.NumberFormat('es-ES', { style: 'currency', currency: 'USD' }).format(amount);
}
