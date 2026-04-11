# ShopApp

Aplicación de tienda de tecnología disponible en **tres versiones**:

| Versión | Tecnología | Descripción |
|---------|-----------|-------------|
| 💻 **Consola (.NET)** | .NET 9 Console App | Ejecutable directamente con `dotnet run` en cualquier OS |
| 🌐 **Web (PWA)** | Vite + Vanilla JS | Funciona en cualquier navegador y se puede instalar en Android |
| 📱 **Android nativo** | .NET MAUI 10 | APK nativa para Android vía Visual Studio |

---

## 💻 Versión Consola — Ejecutar con `dotnet run`

La forma más rápida de probar la lógica de negocio sin necesitar Visual Studio ni un dispositivo Android.

### Requisitos
- [.NET 9 SDK](https://dotnet.microsoft.com/download) (o superior)

### Ejecución

```bash
cd ShopApp.Console
dotnet run
```

La aplicación muestra en consola:
- **Resumen**: conteo de categorías, productos y clientes
- **Categorías**: lista completa (7 categorías)
- **Productos**: tabla con nombre, precio y color indicador (✅ ≤ $100 · 🟣 > $100)
- **Clientes**: listado con nombre y dirección
- **Detalle de producto**: información completa con simulación de pedido (cantidad × precio)
- **Búsqueda filtrada**: productos con precio > $100

### Arquitectura de la solución .NET

```
ShopApp.sln
├── ShopApp.Core/          # Biblioteca de clases (net9.0) — lógica reutilizable
│   └── DataAccess/
│       ├── Entities.cs    # Records: Product, Client, Category
│       └── ShopDbContext.cs  # EF Core InMemory + Seed data
│
├── ShopApp.Console/       # Proyecto ejecutable (net9.0) — punto de entrada
│   └── Program.cs         # Main: instancia ShopDbContext y muestra datos
│
└── ShopApp/               # Proyecto MAUI (net10.0-android) — app Android
    └── ...
```

`ShopApp.Console` referencia `ShopApp.Core`, que contiene toda la lógica de datos.
El proyecto MAUI comparte la misma lógica conceptual y puede ser refactorizado para usar
`ShopApp.Core` en el futuro.

---

## 🌐 Versión Web — Ejecutar en el navegador

### Requisitos
- [Node.js 18+](https://nodejs.org/) y npm

### Instalación

```bash
npm install
```

### Iniciar en modo desarrollo

```bash
npm run dev
```

El servidor arranca en `http://localhost:5173` y muestra automáticamente la URL para acceder desde la red local:

```
  ➜  Local:   http://localhost:5173/
  ➜  Network: http://192.168.x.x:5173/

  📱  Desde tu celular Android (misma red WiFi):
      http://192.168.x.x:5173
```

### Build de producción

```bash
npm run build      # genera la carpeta dist/
npm run preview    # sirve el build en red local
```

---

## 📱 Abrir desde un celular Android (misma red WiFi)

1. Asegúrate de que el PC y el celular estén en la **misma red WiFi**.
2. Ejecuta `npm run dev` en el PC.
3. En el celular, abre el navegador (Chrome recomendado) y escribe la URL mostrada en la consola, por ejemplo:  
   `http://192.168.1.100:5173`
4. La app carga completa. Para **instalarla como PWA**:
   - Toca el ícono de menú (⋮) → **"Agregar a pantalla de inicio"** (o "Instalar app").
   - La app quedará en el escritorio de Android con ícono propio y funciona sin barra del navegador.

---

## ✨ Funcionalidades (versión web)

- **Resumen**: contador de categorías, productos y clientes.
- **Productos**: lista con precio codificado por color (≤ $100 → verde, > $100 → violeta) y búsqueda en tiempo real.
- **Detalle de producto**: selector de producto, contador de cantidad y cálculo de total.
- **Clientes**: listado con nombre y dirección.
- **PWA**: funciona offline (shell) y es instalable en Android.

---

## 📱 Versión Android Nativa (.NET MAUI)

### Requisitos
- Visual Studio 2022/2026 con carga de trabajo **.NET MAUI**
- SDK de Android instalado

### Pasos
1. Abrir `ShopApp.sln`
2. Restaurar paquetes NuGet
3. Establecer `ShopApp` como proyecto de inicio
4. Seleccionar emulador o dispositivo Android físico
5. Ejecutar con **F5**

---

## Estructura del repositorio

```
tiendaApp/
├── index.html              # Punto de entrada web
├── vite.config.js          # Configuración Vite (host 0.0.0.0)
├── package.json            # Scripts npm
├── src/
│   ├── main.js             # Lógica de la app web
│   ├── data.js             # Datos (productos, clientes, categorías)
│   └── style.css           # Estilos
├── public/
│   ├── manifest.json       # PWA manifest
│   ├── sw.js               # Service Worker
│   └── icons/              # Iconos PWA
└── ShopApp/                # Proyecto .NET MAUI
    ├── ShopApp.csproj
    └── ...
```


Aplicación de tienda generada con **.NET MAUI 10.0** orientada a Android, siguiendo la arquitectura de las clases 1-9 del curso.

## Estructura del Proyecto

```
ShopApp/
├── ShopApp.csproj                          # Proyecto MAUI → net10.0-android
├── MauiProgram.cs                          # Host, DI, rutas de navegación
├── App.xaml / App.xaml.cs                  # ResourceDictionary global (converter + DataTemplate)
├── AppShell.xaml / AppShell.xaml.cs        # Shell con Flyout + 3 tabs
├── MainPage.xaml / MainPage.xaml.cs        # Resumen (Count de entidades)
├── BindingUtilObject.cs                    # Base INotifyPropertyChanged
│
├── DataAccess/
│   ├── Entities.cs                         # record: Product, Client, Category
│   └── ShopDbContext.cs                    # EF Core InMemory + Seed data
│
├── Converters/
│   └── PrecioParaColorConverter.cs         # Precio ≤100 → LimeGreen, >100 → DarkViolet
│
├── Pages/
│   ├── ProductsPage.xaml / .cs             # CollectionView + ObservableCollection
│   ├── ProductoBusquedaHandler.cs          # SearchHandler con filtro y navegación
│   ├── ProductDetailPage.xaml / .cs        # IQueryAttributable, Picker, Stepper, FlexLayout
│   ├── ClientsPage.xaml / .cs             # Lista de clientes
│
├── Resources/
│   ├── Styles/
│   │   ├── Colors.xaml                     # Paleta de colores de la tienda
│   │   └── Styles.xaml                     # Estilos globales
│   ├── AppIcon/                            # Icono de la app
│   ├── Splash/                             # Pantalla de carga
│   ├── Fonts/                              # Fuentes tipográficas
│   └── Images/                             # Imágenes (shop_header.png, tab_*.png)
│
└── Platforms/
    └── Android/
        ├── MainActivity.cs
        ├── MainApplication.cs
        └── AndroidManifest.xml
```

## Fases Implementadas

| Fase | Descripción | Estado |
|------|-------------|--------|
| 1 | Arquitectura de Proyecto y Punto de Entrada | ✅ |
| 2 | Persistencia de Datos con EF Core InMemory | ✅ |
| 3 | Lógica de Binding y Utilidades | ✅ |
| 4 | Interfaces de Usuario y Navegación | ✅ |
| 5 | Estilización y Temas | ✅ |

## Seed Data

- **7 Categorías**: Computadoras, Monitores, Teclados, Ratones, Auriculares, Impresoras, Almacenamiento
- **7 Productos** con precios variados ($49.99 — $1299.99)
- **2 Clientes**: Ana García, Carlos López

## Dependencias NuGet

```xml
<PackageReference Include="Microsoft.Maui.Controls" Version="10.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="9.0.0" />
```

## Imágenes requeridas (agregar manualmente en Resources/Images)

- `shop_header.png` — Imagen del encabezado del Flyout
- `tab_home.png` — Ícono tab Resumen
- `tab_products.png` — Ícono tab Productos
- `tab_clients.png` — Ícono tab Clientes

## Cómo abrir en Visual Studio 2026

1. Abrir `ShopApp.sln`
2. Restaurar paquetes NuGet
3. Establecer `ShopApp` como proyecto de inicio
4. Seleccionar emulador/dispositivo Android
5. Ejecutar (F5)
