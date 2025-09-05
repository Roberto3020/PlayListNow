# PlayListNow - Contenerización y Ejecución

## Requisitos y Arquitectura
- Frontend y backend dockerizados en imágenes separadas.
- Ejecución local en un único host con Docker.
- Comunicación entre servicios por nombre de servicio (no localhost).
- Orquestación con Docker Compose.
- Red bridge personalizada para los servicios.
- Puertos sugeridos: Frontend en 3000, Backend en 8080.

## Estructura del Proyecto
- `WebApi/` - Backend .NET
- `WebUI/` - Frontend Angular
- `docker-compose.yml` - Orquestación de servicios

## Construcción de Imágenes Individuales

### Backend (.NET)
```powershell
cd <WebApi>
docker build -t playlistnow-webapi -f WebApi/Dockerfile .
```

### Frontend (Angular)
```powershell
cd <WebUI>
docker build -t playlistnow-webui -f WebUI/Dockerfile .
```

## Ejecución Individual con Red Personalizada
```powershell
docker network create playlistnet
docker run -d --name webapi --network playlistnet -p 8080:8080 playlistnow-webapi
docker run -d --name webui --network playlistnet -p 3000:80 playlistnow-webui
```

## Orquestación con Docker Compose
```powershell
docker-compose up --build
```

- Accede al frontend: [http://localhost:3000](http://localhost:3000)
- Accede al backend Swagger: [http://localhost:8080/swagger](http://localhost:8080/swagger)

## Limpieza de Contenedores y Recursos
```powershell
docker-compose down
docker container prune
docker system prune -a
```

## Configuración de CORS en Backend
En `Program.cs`:
```csharp
builder.Services.AddCors();
app.UseCors(policy =>
    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
);
```

## Variables de Entorno en Angular
En `src/environments/environment.ts`:
```typescript
export const environment = {
  production: false,
  apiUrl: window.location.hostname === 'localhost'
    ? 'http://localhost:8080/song'
    : 'http://webapi:8080/song'
};
```

## Proceso de Verificación
1. Construye las imágenes y levanta los servicios con Docker Compose.
2. Accede al frontend y backend en los puertos configurados.
3. Verifica que el frontend consuma la API usando el nombre de servicio Docker (`webapi`).
4. Revisa los logs con:
   ```powershell
   docker-compose logs webapi
   docker-compose logs webui
   ```
5. Si hay errores de CORS, revisa la configuración en el backend.

## Migraciones automáticas de la base de datos

Al iniciar los servicios con Docker Compose, el backend ejecuta automáticamente las migraciones de Entity Framework Core antes de iniciar la API. Esto garantiza que la base de datos y las tablas se creen sin intervención manual.

No necesitas ejecutar comandos adicionales: solo usa

```powershell
docker-compose up --build
```

Esto funciona porque el contenedor del backend usa la imagen de .NET SDK y ejecuta:

```sh
sh -c "dotnet ef database update --project DAL --startup-project WebApi && dotnet WebApi.dll"
```

Así, cualquier persona que clone el repositorio tendrá la base de datos lista al levantar los servicios.

---


