# Dockerfile para Angular (WebUI)
FROM node:20 AS build

# Establece el directorio de trabajo
WORKDIR /app

# Copia el archivo de dependencias
COPY package.json ./

# Instala las dependencias
RUN npm install

# Copia el resto de los archivos de la aplicación
COPY . .

RUN npm run build --configuration=docker

# Etapa para servir la aplicación con Nginx
FROM nginx:alpine

# Copia los archivos construidos al directorio de Nginx
COPY --from=build /app/dist/WebUI/browser /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
