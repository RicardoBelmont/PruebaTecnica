# PruebaTecnica

## Dominio
- Sin dependencias y con el enfoque mas puro posible, contiene nuestra logica de aplicacion a nivel de definicion (interfaces) asi como nuestros modelos de datos 
- Consta de la siguiente estructura (A nivel de Ezquema o definicion, nunca implementacion):
  - Entities: Modelos de datos
  - Repositories: Interfaces para acceso a datos
  - Services: Capa de acceso a datos externos (como clientes http o integraciones externas)
  - User Cases: Casos de usos del dominio

## Application
- Contiene la logica y validaciones (Casos de uso) de nuestra aplicacion, tales como validaciones de textos o procesos en forma de implementacion del Dominio

## Infraestructure
- Se centra en el acceso mas enfocado a bajo nivel, tal como implementacion de bases de datos locales o acceso a la capa de red (HTTP), inclusive acceso a secure storage etc

## Presentation
- En este caso, nos enfocamos exclusivamente en la aplicacion (views y viewmodel), tratando de segregar todo lo demas a las demas capas

## Notas

### Enfoque del proyecto en:

- Arquitectura limpia (Clean Architecture)
- Patrón MVVM (con CommunityToolkit.MVVM) 
Consumo eficiente de API REST usando HttpClient
Autenticación mediante OAuth 2.0 usando MSAL
- Almacenamiento seguro de tokens (SecureStorage): Manejado por Storage.SecureStorage
- Integración con autenticación biométrica (Plugin.Fingerprint o Essentials)
  - Se opto por una implementacion nativa con la finalidad de evitar usar paquetes en version alfa asi como mejor manejo de excepciones y mensajes, android 28++

- Internacionalización (mínimo Español/Inglés)
  - Uso de archivos Resx
Pruebas Unitarias (mínimo dos pruebas, usando xUnit y Moq)
Integración de mapas con Google Maps o similares con optimización de rendimiento (marcadores agrupados/clustering)
- Archivo YAML o instrucciones para automatización CI/CD (Fastlane, Azure DevOps, o GitHub Actions)
  - En este caso, no hay un sitio de deployment, por lo que me limite unicamente a la configuracion basica del manifest/info.plist