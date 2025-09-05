#!/bin/sh
echo "Verificando sqlcmd..."
which sqlcmd || echo "sqlcmd no está instalado o no está en el PATH"
echo "Intentando conectar a SQL Server en db:1433..."
until /opt/mssql-tools/bin/sqlcmd -S tcp:db,1433 -U sa -P "Your_password123" -Q "SELECT 1"; do
  echo "Esperando a que SQL Server esté disponible..."
  sleep 3
done
exec "$@"