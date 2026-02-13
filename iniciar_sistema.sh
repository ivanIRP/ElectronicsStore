#!/bin/bash

echo "========================================"
echo "Electronics Store - Sistema Completo"
echo "========================================"
echo ""
echo "Este script iniciará la API y la aplicación web"
echo ""

# Iniciar la API en una nueva terminal
echo "[1/2] Iniciando API REST..."
if [[ "$OSTYPE" == "darwin"* ]]; then
    # macOS
    osascript -e 'tell app "Terminal" to do script "cd '"$(pwd)/API"' && dotnet run"'
else
    # Linux
    gnome-terminal -- bash -c "cd API && dotnet run; exec bash"
fi

sleep 3

# Iniciar la Web Admin en otra terminal
echo "[2/2] Iniciando Aplicación Web..."
if [[ "$OSTYPE" == "darwin"* ]]; then
    # macOS
    osascript -e 'tell app "Terminal" to do script "cd '"$(pwd)/WebAdmin"' && dotnet run"'
else
    # Linux
    gnome-terminal -- bash -c "cd WebAdmin && dotnet run; exec bash"
fi

echo ""
echo "========================================"
echo "Sistema iniciado correctamente!"
echo "========================================"
echo ""
echo "API disponible en: http://localhost:5000"
echo "Swagger en: http://localhost:5000/swagger"
echo "Web Admin en: http://localhost:5001"
echo ""
