# Ejecutar CRM junto con PortalCorporativo

## Estrategia

1. Clonar PortalCorporativo.
2. Clonar CRM en carpeta paralela.
3. Levantar servicios transversales desde PortalCorporativo.
4. Levantar servicios CRM con docker-compose.crm.yml.
5. Configurar variables CRM para apuntar a las URLs del portal.

## Ejemplo

```bash
git clone https://github.com/christyepez/PortalCorporativo.git
git clone https://github.com/christyepez/CRM.git
cd PortalCorporativo
docker compose up -d
cd ../CRM
docker compose -f docker-compose.crm.yml up -d
```
