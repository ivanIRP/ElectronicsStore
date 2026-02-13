# Electronics Store - Sistema Completo

Sistema completo de tienda de productos electrónicos con API REST, aplicación web de administración y aplicación móvil Android para clientes.

## 📁 Estructura del Proyecto

```
ElectronicsStore/
├── API/                    # API REST en .NET Core
├── WebAdmin/              # Aplicación Web ASP.NET MVC (Administrador)
└── AndroidApp/            # Aplicación Android (Clientes)
```

## 🚀 Características

### API REST (.NET Core)
- Gestión completa de productos electrónicos (CRUD)
- Sistema de compras con actualización automática de inventario
- Base de datos SQLite
- Documentación Swagger
- CORS habilitado para acceso web y móvil

### Aplicación Web (Administrador)
- Dashboard con estadísticas de ventas
- CRUD completo de productos
- Visualización de stock en tiempo real
- Interfaz moderna y responsive
- Alertas de productos con bajo stock

### Aplicación Android (Clientes)
- Visualización de productos disponibles
- Sistema de compras integrado
- Actualización automática de stock
- Interfaz Material Design
- Pull-to-refresh para actualizar productos

## 📋 Requisitos Previos

### Para la API y Web Admin
- .NET 8.0 SDK o superior
- Visual Studio 2022 o Visual Studio Code (opcional)

### Para la App Android
- Android Studio Hedgehog o superior
- JDK 8 o superior
- Android SDK (API Level 24 o superior)
- Dispositivo Android o emulador

## 🔧 Instalación y Configuración

### 1. API REST

```bash
# Navegar a la carpeta de la API
cd API

# Restaurar paquetes NuGet
dotnet restore

# Compilar el proyecto
dotnet build

# Ejecutar la API (puerto 5000 por defecto)
dotnet run
```

La API estará disponible en: `http://localhost:5000`
Documentación Swagger en: `http://localhost:5000/swagger`

**Endpoints principales:**
- `GET /api/Productos` - Obtener todos los productos
- `GET /api/Productos/disponibles` - Obtener productos en stock
- `GET /api/Productos/{id}` - Obtener un producto
- `POST /api/Productos` - Crear producto
- `PUT /api/Productos/{id}` - Actualizar producto
- `DELETE /api/Productos/{id}` - Eliminar producto
- `POST /api/Compras` - Realizar compra
- `GET /api/Compras/reporte` - Obtener reporte de ventas

### 2. Aplicación Web (Administrador)

```bash
# Navegar a la carpeta WebAdmin
cd WebAdmin

# Restaurar paquetes
dotnet restore

# Compilar
dotnet build

# Ejecutar (puerto 5001 por defecto)
dotnet run
```

La aplicación web estará disponible en: `http://localhost:5001`

**IMPORTANTE:** Asegúrate de que la API esté ejecutándose antes de iniciar la aplicación web.

Si la API corre en un puerto diferente, actualiza `appsettings.json`:
```json
{
  "ApiSettings": {
    "BaseUrl": "http://localhost:PUERTO/api"
  }
}
```

### 3. Aplicación Android

#### Configuración de la URL de la API

**IMPORTANTE:** Antes de compilar la app Android, debes configurar la URL correcta de la API:

1. Abre el archivo: `AndroidApp/app/src/main/java/com/electronicsstore/client/api/ApiClient.java`

2. Modifica la variable `BASE_URL` según tu caso:

**Para Emulador Android:**
```java
private static final String BASE_URL = "http://10.0.2.2:5000/api/";
```

**Para Dispositivo Físico:**
```java
// Reemplaza 192.168.1.100 con la IP local de tu computadora
private static final String BASE_URL = "http://192.168.1.100:5000/api/";
```

Para encontrar tu IP local:
- **Windows:** `ipconfig` en CMD
- **Mac/Linux:** `ifconfig` en Terminal

#### Compilar y ejecutar

1. Abre Android Studio
2. Abre el proyecto desde la carpeta `AndroidApp`
3. Espera a que Gradle sincronice las dependencias
4. Conecta un dispositivo Android o inicia un emulador
5. Haz clic en "Run" (▶️) o presiona `Shift + F10`

## 📱 Uso del Sistema

### Flujo de trabajo completo:

1. **Iniciar la API**
   ```bash
   cd API
   dotnet run
   ```

2. **Iniciar la aplicación web de administración**
   ```bash
   cd WebAdmin
   dotnet run
   ```

3. **Gestionar productos desde la web**
   - Accede a `http://localhost:5001`
   - Crea, edita o elimina productos
   - Visualiza el dashboard con estadísticas

4. **Realizar compras desde la app móvil**
   - Abre la app en tu dispositivo Android
   - Visualiza el catálogo de productos
   - Haz clic en "Comprar" en cualquier producto
   - Ingresa la cantidad deseada
   - Confirma la compra

5. **Ver actualizaciones en tiempo real**
   - El stock se actualiza automáticamente en la web
   - Refresca la app móvil para ver los cambios

## 🗄️ Base de Datos

El sistema usa SQLite con datos de ejemplo pre-cargados:

**Productos de ejemplo:**
1. Laptop Dell XPS 15 - $1,299.99 (10 unidades)
2. iPhone 15 Pro - $999.99 (15 unidades)
3. Samsung Galaxy S24 - $899.99 (20 unidades)

La base de datos se crea automáticamente al ejecutar la API por primera vez.

Ubicación: `API/electronics.db`

## 🔒 Seguridad

- La API usa CORS configurado para aceptar todas las solicitudes (desarrollo)
- Para producción, configura CORS con orígenes específicos
- Considera agregar autenticación JWT para mayor seguridad

## 🐛 Solución de Problemas

### Error: "No se puede conectar a la API"

**En la app Android:**
1. Verifica que la API esté ejecutándose
2. Confirma que la URL en `ApiClient.java` sea correcta
3. Si usas emulador, usa `10.0.2.2` en lugar de `localhost`
4. Si usas dispositivo físico, asegúrate de estar en la misma red Wi-Fi

**En la aplicación web:**
1. Verifica que la API esté ejecutándose
2. Revisa la URL en `appsettings.json`

### Error: "La base de datos no se crea"

Elimina el archivo `electronics.db` (si existe) y reinicia la API.

### Error de compilación en Android

1. Limpia el proyecto: `Build > Clean Project`
2. Reconstruye: `Build > Rebuild Project`
3. Invalida caché: `File > Invalidate Caches / Restart`

## 📦 Preparación para GitHub

El proyecto está listo para ser subido a GitHub:

```bash
# Inicializar repositorio (en la carpeta ElectronicsStore)
git init

# Agregar archivos
git add .

# Primer commit
git commit -m "Initial commit - Electronics Store System"

# Conectar con tu repositorio remoto
git remote add origin https://github.com/TU_USUARIO/electronics-store.git

# Subir cambios
git push -u origin main
```

### Archivos importantes para GitHub

Se recomienda agregar un `.gitignore`:

```gitignore
# .NET
bin/
obj/
*.db
*.db-shm
*.db-wal
.vs/
*.user

# Android
*.iml
.gradle
.idea/
local.properties
build/
captures/
.externalNativeBuild
.cxx
```

## 🤝 Contribuciones

Este es un proyecto de ejemplo completamente funcional. Siéntete libre de:
- Hacer fork del repositorio
- Crear nuevas características
- Reportar bugs
- Mejorar la documentación

## 📝 Licencia

Este proyecto es de código abierto y está disponible bajo la licencia MIT.

## 👨‍💻 Autor

Sistema desarrollado como ejemplo de aplicación completa .NET + Android.

## 📞 Soporte

Para preguntas o problemas:
1. Revisa la sección de "Solución de Problemas"
2. Verifica que todos los servicios estén ejecutándose
3. Revisa los logs de la consola para errores específicos

## 🎯 Próximas Mejoras Sugeridas

- [ ] Autenticación y autorización (JWT)
- [ ] Imágenes de productos
- [ ] Carrito de compras
- [ ] Historial de compras del cliente
- [ ] Notificaciones push
- [ ] Reportes avanzados en PDF
- [ ] Búsqueda y filtros avanzados
- [ ] Integración con pasarelas de pago
- [ ] Sistema de usuarios y roles
- [ ] API de versiones (versionado de endpoints)

---

**¡El sistema está 100% funcional y listo para usar! 🚀**
