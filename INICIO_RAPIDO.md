# 🚀 Guía de Inicio Rápido

## Ejecutar el sistema completo en 3 pasos:

### Paso 1: Iniciar la API (Terminal 1)

```bash
cd API
dotnet restore
dotnet run
```

✅ La API estará corriendo en `http://localhost:5000`
✅ Swagger disponible en `http://localhost:5000/swagger`

---

### Paso 2: Iniciar la Aplicación Web (Terminal 2)

```bash
cd WebAdmin
dotnet restore
dotnet run
```

✅ Aplicación web disponible en `http://localhost:5001`

Accede con tu navegador y:
- Ver dashboard
- Agregar productos
- Editar inventario
- Ver estadísticas de ventas

---

### Paso 3: Configurar y ejecutar la App Android

#### A. Configurar la URL de la API

**📱 Para Emulador:**

Edita `AndroidApp/app/src/main/java/com/electronicsstore/client/api/ApiClient.java`:

```java
private static final String BASE_URL = "http://10.0.2.2:5000/api/";
```

**📱 Para Dispositivo Físico:**

1. Encuentra tu IP local:
   - Windows: `ipconfig` en CMD (busca "IPv4 Address")
   - Mac/Linux: `ifconfig` en Terminal (busca "inet")

2. Edita el mismo archivo:
```java
// Reemplaza con TU IP
private static final String BASE_URL = "http://192.168.1.XXX:5000/api/";
```

#### B. Compilar y ejecutar

1. Abre **Android Studio**
2. Open Project → Selecciona la carpeta `AndroidApp`
3. Espera que Gradle sincronice (primera vez puede tardar)
4. Conecta tu dispositivo o inicia un emulador
5. Click en **Run ▶️** (o `Shift + F10`)

---

## ✅ Verificar que todo funciona

### Desde la aplicación web:
1. Ve a `http://localhost:5001`
2. Crea un nuevo producto
3. Verifica que aparezca en la lista

### Desde la app Android:
1. Abre la app
2. Desliza hacia abajo para refrescar (pull-to-refresh)
3. Deberías ver el producto que acabas de crear
4. Haz clic en "Comprar"
5. Ingresa una cantidad y confirma

### Verificar actualización de stock:
1. Vuelve a la aplicación web
2. Refresca la página
3. El stock del producto debería haber disminuido ✨

---

## 🆘 ¿Problemas?

### "Cannot connect to API" en Android

- ✅ Verifica que la API esté corriendo
- ✅ Si usas emulador: usa `10.0.2.2`
- ✅ Si usas dispositivo físico:
  - Asegúrate de estar en la misma red Wi-Fi
  - Usa la IP correcta de tu PC
  - Verifica que el firewall no bloquee el puerto 5000

### La web no carga productos

- ✅ Verifica que la API esté corriendo en el puerto 5000
- ✅ Revisa la consola de la API para ver si recibe las peticiones

### Error de compilación en Android

```bash
# En Android Studio:
Build > Clean Project
Build > Rebuild Project
```

---

## 📊 Datos de Ejemplo

La API viene con 3 productos pre-cargados:

1. **Laptop Dell XPS 15** - $1,299.99 (10 unidades)
2. **iPhone 15 Pro** - $999.99 (15 unidades)
3. **Samsung Galaxy S24** - $899.99 (20 unidades)

---

## 🎯 Flujo de Trabajo Recomendado

```
1. Iniciar API
   ↓
2. Iniciar Web Admin
   ↓
3. Crear/Editar productos desde la web
   ↓
4. Ejecutar app Android
   ↓
5. Ver productos y realizar compras
   ↓
6. Verificar actualización de stock en la web
```

---

## 📝 URLs de Referencia Rápida

| Servicio | URL |
|----------|-----|
| API | http://localhost:5000 |
| Swagger Docs | http://localhost:5000/swagger |
| Web Admin | http://localhost:5001 |

---

## 🔗 Comandos Git para subir a GitHub

```bash
# En la carpeta ElectronicsStore
git init
git add .
git commit -m "Initial commit - Electronics Store System"
git branch -M main
git remote add origin https://github.com/TU_USUARIO/TU_REPO.git
git push -u origin main
```

---

**¡Listo! El sistema está completamente funcional 🎉**
