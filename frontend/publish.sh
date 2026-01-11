#!/bin/bash

set -e

echo "🔖 Informe a versão da imagem (ex: backend-v1.0.9):"
read VERSION

if [ -z "$VERSION" ]; then
  echo "❌ Versão não informada. Operação cancelada."
  exit 1
fi

IMAGE_NAME="priekamoto/teste07:frontend-v"

echo "🐳 Buildando imagem $IMAGE_NAME:$VERSION..."
docker build -t $IMAGE_NAME$VERSION .

echo "🚀 Enviando imagem para o Docker Hub..."
docker push $IMAGE_NAME$VERSION

echo "✅ Publicação concluída com sucesso!"