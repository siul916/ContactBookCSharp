# ContactBookCsharp

Proyecto de consola en C# basado en la playlist del profesor **How to Make a Contact Book (C#) [Spanish]**.

## Purpose and Value

Este proyecto demuestra una libreta de contactos hecha con C# y programacion orientada a objetos. El valor principal es practicar como dividir un problema grande en partes pequenas: primero la clase `Contact`, luego la arquitectura, las pantallas, la paginacion, la busqueda, el ordenamiento, la deduplicacion, el merge y finalmente Disjoint-Set-Union.

Conceptos incluidos:

- OOP: encapsulacion, interfaces y clases responsables de una sola tarea.
- Paginacion y sorting.
- Modularizacion y arquitectura.
- Enfoque top-down y bottom-up.
- Trabajar con una cosa a la vez.
- GitHub: repositorio, commits y push.
## Estructura

```text
ContactBookCsharp
+- .vscode
¦  +- launch.json
¦  +- settings.json
¦  +- tasks.json
+- src
¦  +- ContactBook
¦     +- Contact.cs
¦     +- ContactBook.cs
¦     +- ContactBook.csproj
¦     +- ContactComparer.cs
¦     +- ContactMerger.cs
¦     +- ContactSeed.cs
¦     +- Program.cs
¦     +- Union.cs
+- tests
¦  +- ContactBook.Test
¦     +- ContactBook.Test.csproj
¦     +- ContactBookTest.cs
¦     +- ContactTests.cs
¦     +- Program.cs
¦     +- UnionTest.cs
+- .gitignore
+- ContactBookCSharp.sln
+- LICENSE
+- README.md
```
## Como correr el proyecto

```powershell
dotnet run --project src/ContactBook
```

En VS Code tambien puedes ir a **Run and Debug** y seleccionar **ContactBook**. La configuracion en `.vscode/launch.json` usa `externalTerminal`, para que la app de consola abra en una terminal externa.

## Como correr las pruebas

```powershell
dotnet run --project tests/ContactBook.Test
```

## Como compilar la solucion

```powershell
dotnet build ContactBookCSharp.sln
```
## Temas de los videos cubiertos

1. **Contact Book - 01 - Contact + Contact Test**
   - Clase `Contact`.
   - Clase de prueba `ContactTests`.
   - `ToString`, `Equals`, `GetHashCode`, operadores `==` y `!=`.

2. **Contact Book - 02 - Architecture + Show Welcome Screen + Show Contacts**
   - Clase `ContactBook` para controlar la aplicacion.
   - Pantalla de bienvenida.
   - Tabla para mostrar contactos.

3. **Contact Book - 03 - Show Input Options + Show Exit Screen**
   - Menu con opciones `N`, `P`, `C`, `R`, `F`, `O`, `D`, `X`.
   - Pantalla de salida.
   - Validacion de input.
4. **Contact Book - 04 - Next Page + Create Contact**
   - Paginacion con next/previous page.
   - Crear contactos desde consola.

5. **Contact Book - 05 - Review Contact + Find Contacts**
   - Revisar contacto por indice.
   - Confirmacion antes de borrar.
   - Busqueda con `FindAll`.

6. **Contact Book - 06 - Order Contacts + Deduplicate Contacts**
   - Ordenamiento con `ContactComparer` e `IComparer<Contact>`.
   - Deteccion de contactos duplicados.

7. **ContactBook - 07 - Deduplicate + Merge Contacts**
   - `ContactMerger` permite escoger que dato usar de cada duplicado.
   - Pantalla de duplicate contacts y merged contact.

8. **ContactBook - 08 - Disjoint-Set-Union + Find-Union Algorithm**
   - `Union<T>` implementa la estructura DSU.
   - `Find` usa compresion de caminos.
   - `Join` usa union por rango.
   - `UnionTest.cs` prueba el algoritmo.
## Estructuras de datos y algoritmos

- `List<Contact>` para guardar contactos.
- `FindAll` para buscar contactos.
- `IComparer<Contact>` para ordenar.
- `Skip` y `Take` para paginacion.
- `Union<T>` para Disjoint-Set-Union.
- Find-Union para agrupar duplicados antes de unirlos.

## Future Work

Estas mejoras quedan como trabajo futuro, igual que en la introduccion del profesor:

- Load/Save to file.
- Last modified timestamps.
- More contact fields.
- Data persistence.
- Web API.
- Web app.
## Checklist antes de entregar

Antes de entregar el enlace de GitHub, se puede verificar el proyecto con estos comandos:

```powershell
dotnet build ContactBookCSharp.sln
dotnet run --project tests/ContactBook.Test
```

Tambien se puede correr la aplicacion principal con:

```powershell
dotnet run --project src/ContactBook
```
## Opciones del menu final

La aplicacion principal muestra una tabla de contactos y estas opciones:

- `[+]` Next Page
- `[-]` Prev Page
- `[G]` Goto Page
- `[C]` Create Contact
- `[R]` Review Contact
- `[U]` Update Contact
- `[D]` Delete Contact
- `[F]` Find Contacts
- `[O]` Order Contacts
- `[M]` Deduplicate Contacts
- `[S]` Change Page Size
- `[X]` Exit

El flujo de `M` muestra contactos duplicados, permite escoger el first name, last name, phone y email que formaran el merged contact, y luego confirma si se deben unir.

