# GestionUsuariosConfiamed

Este repositorio contiene dos microservicios desarrollados en ASP.NET Core 8:

- **GestionClientes**: API para la gestión de usuarios/clientes.
- **ItemsTrabajos**: API para la gestión de items de trabajo y asignaciones.

## Arquitectura y separación de capas

Ambos microservicios siguen una arquitectura en capas, con una clara separación de responsabilidades:

## Estructura del proyecto

```
GestionUsuariosConfiamed/
├── GestionClientes/
│   ├── Controllers/
│   ├── Models/
│   ├── Repositories/
│   ├── Services/
│   └── Program.cs
├── ItemsTrabajos/
│   ├── Controllers/
│   ├── Models/
│   ├── Repositories/
│   ├── Services/
│   └── Program.cs
├── README.md
└── ...otros archivos
```

## Nota sobre la arquitectura y advertencias de análisis

La arquitectura de este repositorio está basada en microservicios y separación de capas (controladores, modelos, servicios, repositorios). Si alguna herramienta de análisis (como SonarQube) muestra advertencias sobre la arquitectura, puede deberse a:

- El tiempo de análisis insuficiente o problemas de configuración.
- La estructura del repositorio, que separa cada microservicio en carpetas independientes.
- Falta de información sobre dependencias externas o archivos de configuración.

Esto no implica necesariamente un problema real en la arquitectura, sino que puede ser una limitación de la herramienta utilizada.

## Modelo de endpoints principales

### GestionClientes

| Método | Endpoint                      | Descripción                                   |
| ------ | ----------------------------- | --------------------------------------------- |
| GET    | /api/usuario                  | Lista todos los usuarios                      |
| GET    | /api/usuario/mejor-disponible | Devuelve el usuario menos saturado            |
| GET    | /api/usuario/{id}/saturado    | Verifica si el usuario está saturado          |
| GET    | /api/usuario/menos-items      | Devuelve el usuario con menos items asignados |

### ItemsTrabajos

| Método | Endpoint                         | Descripción                                      |
| ------ | -------------------------------- | ------------------------------------------------ |
| GET    | /api/item                        | Lista todos los items                            |
| GET    | /api/item/{id}                   | Devuelve un item por su id                       |
| GET    | /api/item/pendientes/{usuarioId} | Lista los items pendientes de un usuario         |
| GET    | /api/item/saturado/{usuarioId}   | Verifica si el usuario está saturado (por items) |

Estos endpoints cubren las operaciones principales de cada microservicio y pueden ser extendidos según necesidades del negocio.
Esta estructura modular facilita la escalabilidad y el mantenimiento, permitiendo agregar nuevas funcionalidades o cambiar la fuente de datos sin afectar otras capas.

- **Modularidad**: Cada microservicio está organizado en carpetas por responsabilidad, lo que permite un desarrollo y pruebas independientes.
- **Escalabilidad**: El uso de servicios y repositorios desacoplados permite escalar la lógica de negocio y el acceso a datos fácilmente.
- **Claridad**: La separación de capas es evidente y cada clase tiene una única responsabilidad.
- **Comunicación entre microservicios**: El microservicio `ItemsTrabajos` utiliza un cliente HTTP para comunicarse con `GestionClientes`, siguiendo buenas prácticas de integración.

#### Posibles mejoras arquitectónicas

- Actualmente los repositorios usan almacenamiento en memoria; para producción se recomienda implementar persistencia en base de datos.
- Considerar el uso de interfaces para los servicios y repositorios, facilitando pruebas unitarias y extensibilidad.
- Revisar la gestión de dependencias para evitar el uso excesivo de singletons donde no sea necesario.

En resumen, el proyecto sigue un enfoque en capas correctamente separado, lo que favorece la mantenibilidad y escalabilidad. No se detectan problemas graves de arquitectura, pero se recomienda avanzar hacia una persistencia real y mayor uso de interfaces.

## Requisitos previos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Cómo iniciar cada proyecto

### GestionClientes

1. Abre una terminal en la carpeta `GestionClientes`.
2. Ejecuta:
   ```sh
   dotnet run
   ```
3. La API estará disponible en `https://localhost:5001` (o el puerto configurado).
4. Accede a la documentación Swagger en `https://localhost:5001/swagger`.

### ItemsTrabajos

1. Abre una terminal en la carpeta `ItemsTrabajos`.
2. Ejecuta:
   ```sh
   dotnet run
   ```
3. La API estará disponible en `https://localhost:5002` (o el puerto configurado).
4. Accede a la documentación Swagger en `https://localhost:5002/swagger`.

## Imágenes de funcionamiento

A continuación se muestran ejemplos visuales del funcionamiento de cada API:

### GestionClientes

![GestionClientes](clientes.png)

### ItemsTrabajos

![ItemsTrabajos](items.png)

---

Ambos proyectos pueden configurarse mediante los archivos `appsettings.json` y exponen endpoints documentados automáticamente con Swagger.

Para cualquier duda, revisa los controladores en las carpetas `Controllers/` de cada microservicio.
