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
