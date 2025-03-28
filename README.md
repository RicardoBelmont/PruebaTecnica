# Prueba técnica

## Dominio
- Sin dependencias y con el enfoque mas puro posible, contiene nuestra lógica de aplicación a nivel de definición (interfaces) así como nuestros modelos de datos 
- Consta de la siguiente estructura (A nivel de Esquema o definición, nunca implementación):
  - Entities: Modelos de datos
  - Repositories: Interfaces para acceso a datos
  - Services: Capa de acceso a datos externos (como clientes http o integraciones externas)
  - User Cases: Casos de usos del dominio

## Application
- Contiene la lógica y validaciones (Casos de uso) de nuestra aplicación, tales como validaciones de textos o procesos en forma de implementación del Dominio

## Infraestructure
- Se centra en el acceso mas enfocado a bajo nivel, tal como implementación de bases de datos locales o acceso a la capa de red (HTTP), inclusive acceso a secure storage etc

## Presentation
- En este caso, nos enfocamos exclusivamente en la aplicacion (views y viewmodel), tratando de segregar todo lo demás a las demás capas

## Notas

### Enfoque del proyecto en:

- Arquitectura limpia (Clean Architecture)
- Patron MVVM (con CommunityToolkit.MVVM) 
- Autenticación mediante OAuth 2.0 usando MSAL
  - implementación por MSAL Package
- Almacenamiento seguro de tokens (Secure Storage): 
  - Manejado por Storage.SecureStorage, puede ser visto en BiometricLogin (Activate Biometric Login)
- Integración con autenticación biométrica (Plugin.Fingerprint o Essentials)
  - Se opto por una implementación nativa con la finalidad de evitar usar paquetes en versión alfa asi como mejor manejo de excepciones y mensajes, Android 28++
- Internacionalización (mínimo español/Inglés)
  - Uso de archivos Resx

- Archivo YAML o instrucciones para automatización CI/CD (Fastlane, Azure DevOps, o GitHub Actions)
  - En este caso, no hay un sitio de deployment, por lo que me limite únicamente a la configuración básica del manifest/info.plist
- Pendiente de implementación
  - Consumo eficiente de API REST usando HttpClient
  - Pruebas Unitarias (mínimo dos pruebas, usando xUnit y Moq)
  - Integración de mapas con Google Maps o similares con optimización de rendimiento (marcadores agrupados/clustering)
