@echo off
echo ========================================
echo Electronics Store - Sistema Completo
echo ========================================
echo.
echo Este script iniciara la API y la aplicacion web
echo.

echo [1/2] Iniciando API REST...
start "API - Electronics Store" cmd /k "cd API && dotnet run"
timeout /t 5

echo [2/2] Iniciando Aplicacion Web...
start "Web Admin - Electronics Store" cmd /k "cd WebAdmin && dotnet run"

echo.
echo ========================================
echo Sistema iniciado correctamente!
echo ========================================
echo.
echo API disponible en: http://localhost:5000
echo Swagger en: http://localhost:5000/swagger
echo Web Admin en: http://localhost:5001
echo.
echo Presiona cualquier tecla para salir...
pause > nul
