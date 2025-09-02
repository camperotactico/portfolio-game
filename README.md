> WIP This document is not finished and is being updated as new features are added.

## Balance Hero

In this small casual game, the objective is to achieve the highest score by getting different color shapes into the goals at the sides before running out of time.

## About this project

This is a simple video game built using `Unity3D` that I am building to showcase my game development skills using this tool.

The reason why I have decided to create this project is because the last relevant game on my `GitHub` profile was over three years old (right before I started my previous job).

Ever since, my abilities as a developer have improved, and I wanted to build a vertical slice to reflect them, as I am not able to share any of the source code I worked on during my previous employment.

## Technical features

- **Unit and integration tests**: Key parts of the codebase have been built following test driven development practices, and have been thoroughly tested using the built-in `Unity Test Framework`. 

- **Object Pooling**: During gameplay, many shapes constantly appear and disappear from the play area. The game makes use of object pooling to recycle instances of these shapes to reduce hiccups caused by memory allocation and garbage collection. It relies on `Unity3D`'s built-in pool system as it is thoroughly tested and flexible enough for the needs of this title.

- **Advanced use of Scriptable Objects**: Scriptable objects are used in various places accross the project for different purposes: 
    - **To store data**: which allows the code to follow the `flyweight` pattern, and also allows designers to easily tweak the parameters of the game.
    - **As runtime sets**: which allow easy access to "global data" during the execution of the game. Similar to a `Singleton` in concept, but offering better decoupling which is essential for testing.
    - **As event channels**: which allow events to be contained in files which then can be accessed globally. Unlike, static delegates, using SO: removes the dependency of the `emitter` from the `observer`, which reduces decoupling which simplifies the testing process. It also allows for events to be _drag & dropped_ in the inspector, which allows Designer and Artists to create responses for them without programming. 

- **Other Design Patterns**: Even thought the scope of this prototype is small, I have implemented many of its features following common design patterns to ensure the codebase remains as maintainable, robust and scalable as possible. An example of this is the class `ShapeSpawner`, which handles the spawning of the different shapes in the game.

> During a level, shapes can be spawned following different strategies: they can be spawn at a steady rate, spawned in bursts, spawned once a certain score is achieved... On top of that, each level in the game might feature a different combination of shapes and strategies.
> To achieve this, the class uses:
>
> - the `Component` pattern to handle different spawning strategies.
> - `Factories` to instantiate strategies from serialised data.
> - An `EventQueue` to aggregate and perform the spanwing operations.

- **Addressables and AssetBundles**: The game is designed to make use of Addressables to load new content (in the form of levels and skins) once the user launches the game.

- **Physics Interpolation**: Due to the nature of the game, a lot of physics interactions will take place at all times. These can be very taxing on mobile devices, so I changed the Physics engine update interval to run only 20 times per second against the default 50. The movement of physics based object is then interpolated to be displayed at the target FPS.
