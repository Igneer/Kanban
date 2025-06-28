# Kanban Project Documentation

## Introducción
El proyecto propuesto es la realización de una aplicación Kanban. Se utiliza el patrón de diseño MVC(Model-View-Controller) construido con el framework ASP.NET de Microsoft y con el lenguaje de programación C#. 

## ¿Qué es Kanban?
Kanban es un método para gestionar el trabajo y los proyectos de manera visual y eficiente. Consiste principalmente de tableros que muestran las tareas en diferentes etapas del proceso, decididas por el equipo que vaya a utilizar esta metodología. Pero generalmente podemos encontrar las siguientes:

- Ideas
- To Do
- Doing
- Review
- Done

Mientras que las tareas se representan con tarjetas, que pueden ir moviéndose entre las columnas representando la etapa en la que se encuentran.

![Tablero](https://github.com/TallerDeLenguajes2/tl2-proyecto-2024-Igneer/blob/main/img/Tablero.PNG)

## Sobre la Aplicacion:

### Roles y Acceso

- __Roles__: Habrá dos __Niveles de Acceso__ las cuales son Administrador y Operador. El administrador tendrá control total al sistema, mientras que el operador podrá gestionar solo los tableros que haya creado o en los que tenga tareas asignadas.
- __Acceso al sistema__: Solo los usuarios logueados podrán utilizar la aplicacion. 

### Como usar la Aplicacion

1. **Registro e Ingreso**
- Los usuarios deben pedir al administrador la creacion de un usuario con nombre y contraseño. Luego, iniciar sesion en la aplicacion.

![InicioDeSesion](https://github.com/TallerDeLenguajes2/tl2-proyecto-2024-Igneer/blob/main/img/InicioDeSesion.PNG)

2. **Pagina Principal**
- Una vez iniciado sesion. Podran observar la pagina principal de la aplicacion donde se les mostrará todos sus tableros, como tambien la interacción con los mismos, ya sea para modificarlos o eliminarlos.

- Para crear una tarea, en la primera columna podran encontrar un boton de color verde con el cual crearan las tareas que perteneceran a ese tablero. Una vez creada la tarea podran observar cuatro botones, las flechas direccionales para mover la tarea a la etapa correspondiente en la que se encuentra, un boton de color rojo con un ícono de basurero para poder eliminarla, y otros dos botones, uno con ícono de lápiz para poder modificarla y otro para asignar la tarea a otro usuario de la aplicacion. Y en caso de que ustedes posean una tarea asignada por otro usuario en un tablero del cual no son propietario, solo podran moverla entre las columnas del tablero.

![Pagina Principal](https://github.com/TallerDeLenguajes2/tl2-proyecto-2024-Igneer/blob/main/img/PaginaPrincipal.PNG)

- Arriba a la izquierda podran ver su usuario, junto con su rol y un boton en color rojo para poder cerrar sesión.

- **Solo en caso de ser administrador**, podran observar una pestaña de usuarios, a los que podran modificar, eliminar y controlar sus tableros junto a las tareas que pertenecen a dicho tablero. 

3. **A tener en cuenta**
- No se pueden eliminar tableros que posean tareas, primero deberás eliminarlas.
- No se pueden eliminar usuarios que posean tableros, primero deberás eliminarlos.

4. **--Usario:admin--Contraseña:1234--**. Usuario administrador para probar la aplicacion.

## Arquitectura del Sistema
### Patrón MVC
Este es el patron utilizado en la realizacion de la aplicacion web, que es una forma de estructurar las aplicaciones de software en tres componentes principales: **Modelo(Model), Vista(View) y Controlador(Controller)**. Esta separación ayuda a gestionar el código de manera más eficiente y facilita su mantenimiento y escalabilidad.

- **Modelo(Model)**: Representa la lógica de negocio y los datos de la aplicación. Se encarga de acceder y gestionar los datos, ya sea desde una base de datos, una API u otra fuente.

- **Vista(View)**: Representa la interfaz de usuario. Muestra los datos del modelo al usuario y captura las entradas del usuario. Es independiente de la lógica de negocio

- **Controlador(Controller)**: Actúa como intermediario entre el modelo y la vista. Procesa las entradas del usuario y actualiza el lmodelo y la vista en consecuencia. Contiene la lógica de la aplicación.

## Tecnologías Utilizadas

- Microsoft ASP.NET Core
- C#
- Razor View Engine
- Bootstrap
- SQLite

## Instalación y ejecucion de la aplicacion

1. Descargar e instalar git. Luego ejecutar el comando **git clone https://github.com/TallerDeLenguajes2/tl2-proyecto-2024-Igneer.git** en el directorio deseado.

2. Descargar e instalar .NET Core SDK 8.0 o posterior. Luego, ejecutar el comando **dotnet run**. Y en el navegador de preferencia, localhost:5250.

## Anexos
Documentación:
- https://learn.microsoft.com/es-es/aspnet/core/mvc/views/razor?view=aspnetcore-9.0
- https://learn.microsoft.com/es-es/dotnet/fundamentals/
- https://learn.microsoft.com/es-es/aspnet/core/mvc/overview?view=aspnetcore-9.0
- https://www.sqlite.org/docs.html
