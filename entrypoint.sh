#!/bin/bash
set -e

echo "=== INICIANDO MIGRACIONES DE BASE DE DATOS ==="

echo "Migrando Catalog..."
./bundle-catalog --connection "$ConnectionStrings__PostgresConnection"

echo "Migrando Workspace..."
./bundle-workspace --connection "$ConnectionStrings__PostgresConnection"

echo "Migrando Workflow..."
./bundle-workflow --connection "$ConnectionStrings__PostgresConnection"

echo "Migrando IAM..."
./bundle-iam --connection "$ConnectionStrings__PostgresConnection"

echo "=== MIGRACIONES COMPLETADAS CON EXITO ==="

echo "Iniciando la API..."
exec dotnet Web.API.dll