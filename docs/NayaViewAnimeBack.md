# NayaViewBack

## Descripción.

Este proyecto es una aplición API que maneja la apliacción. 

NayaView es una aplicación de tipo agenda/anotador de anime/manga que permite a los usuarios llevar un registro de las series que están viendo o leyendo, así como de su progreso y calificaciones. La aplicación ofrece una interfaz intuitiva y fácil de usar, permitiendo a los usuarios agregar nuevas series, actualizar su estado (por ejemplo, "viendo", "completado", "en pausa"), y calificar las series que han terminado. Además, NayaView proporciona recomendaciones personalizadas basadas en las preferencias del usuario y su historial de visualización/lectura. La aplicación también incluye funciones sociales, como la posibilidad de compartir listas con amigos y seguir a otros usuarios para descubrir nuevas series.


## Tecnologías Utilizadas
- Node.js
- NestJS
- TypeScript
- PostgreSQL
- Prisma ORM
- JWT para autenticación
- Docker para la contenedorización

## Modelo de datos

- User:
```mermaid
classDiagram
    class User {
        +id: UUID
        +email: string
        +password: Password
        oauthProvider: string
        oauthId: string
        +createdAt: DateTime
        +updatedAt: DateTime
    }
```

- Password:
```mermaid
classDiagram
    class Password {
        +id: UUID
        +hash: string
        +user: User
        +active: boolean
        +createdAt: DateTime
        +updatedAt: DateTime
    }
```

- Media:
```mermaid
classDiagram
    class Media {
        +id: UUID
        +title: string
        +type: MediaType
        +status: MediaStatus
        +episodes: int
        +tags: List~Tag~
        +synopsis: string
        +coverImageUrl: string
        +createdAt: DateTime
        +updatedAt: DateTime
    }
```

- UserMedia:
```mermaid
classDiagram
    class UserMedia {
        +id: UUID
        +user: User
        +media: Media
        +status: UserMediaStatus
        +progress: int
        +score: int
        +createdAt: DateTime
        +updatedAt: DateTime
    }
```
- Tags:
```mermaid
classDiagram
    class Tag {
        +id: UUID
        +name: string
        +createdAt: DateTime
        +updatedAt: DateTime
    }
```
- MediaType Enum:
```mermaid
classDiagram
    class MediaType {
        <<enumeration>>
        +ANIME
        +MANGA
        +FILM
        +SERIES
    }
```

- MediaStatus Enum:
```mermaid
classDiagram
    class MediaStatus {
        <<enumeration>>
        +RELEASING
        +FINISHED
        +NOT_YET_RELEASED
        +CANCELLED
    }
```

- UserMediaStatus Enum:
```mermaid
classDiagram
    class UserMediaStatus {
        <<enumeration>>
        +PLANNING
        +WATCHING
        +COMPLETED
        +ON_HOLD
        +DROPPED
    }
```

### Diagrama de Entidades y Relaciones (ERD)

```mermaid
erDiagram
    USER ||--o{ PASSWORD : has
    USER ||--o{ USER_MEDIA : owns
    MEDIA ||--o{ USER_MEDIA : is_tracked_in
    MEDIA ||--o{ TAG : has
```