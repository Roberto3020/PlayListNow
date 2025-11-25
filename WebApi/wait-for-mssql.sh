#!/bin/sh
echo "Verificando sqlcmd..."
which sqlcmd || echo "sqlcmd no está instalado o no está en el PATH"
echo "Intentando conectar a SQL Server en db:1433..."
until /opt/mssql-tools/bin/sqlcmd -S tcp:playlistnow-db,1433 -U sa -P "Your_password123" -Q "SELECT 1"; do  echo "Esperando a que SQL Server esté disponible..."
  sleep 3
done

# Crea la base de datos si no existe
echo "Verificando si existe la base de datos PlayListDB..."
sqlcmd -S tcp:playlistnow-db,1433 -U sa -P "Your_password123" -Q "IF DB_ID('PlayListDB') IS NULL CREATE DATABASE PlayListDB"
exec "$@"

##imagen 

# # imagen
# #!/bin/sh
# echo "Verificando sqlcmd..."
# which sqlcmd || echo "sqlcmd no está instalado o no está en el PATH"

# echo "Intentando conectar a SQL Server en playlist-sql.playlist-sql-ns.svc.cluster.local:1433..."
# until /opt/mssql-tools/bin/sqlcmd -S tcp:playlist-sql.playlist-sql-ns.svc.cluster.local,1433 -U sa -P "TuPassword123*" -Q "SELECT 1"; do
#   echo "Esperando a que SQL Server esté disponible..."
#   sleep 3
# done

# # Crea la base de datos si no existe
# echo "Verificando si existe la base de datos PlaylistDB..."
# sqlcmd -S tcp:playlist-sql.playlist-sql-ns.svc.cluster.local,1433 -U sa -P "TuPassword123*" -Q "IF DB_ID('PlaylistDB') IS NULL CREATE DATABASE PlaylistDB"

# exec "$@"
