# Stage 1: Build
FROM node:22-alpine AS builder
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build --prod

# Stage 2: Serve con Nginx
FROM nginx:alpine

# Copiamos build de Angular
COPY --from=builder /app/dist/nayaview-anime-front /usr/share/nginx/html

# Copiamos configuración de Nginx con SSL
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Montaje de certificados externos desde docker-compose
# EXPOSE 80 y 443
EXPOSE 80 443

CMD ["nginx", "-g", "daemon off;"]
