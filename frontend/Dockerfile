# ============================
# 1) Etapa de build
# ============================
FROM node:20-alpine AS builder

WORKDIR /app

COPY package*.json ./
COPY tsconfig*.json ./
COPY vite.config.* ./

RUN npm install

COPY . .

RUN npm run build


# ============================
# 2) Etapa de produção (nginx)
# ============================
FROM nginx:stable-alpine

# Remove config default
RUN rm -rf /usr/share/nginx/html/*

# Copia build do Vite
COPY --from=builder /app/dist /usr/share/nginx/html

# Copia config para Single Page Application (React Router)
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
