# ShopApp — .NET 10 MAUI para Android

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
