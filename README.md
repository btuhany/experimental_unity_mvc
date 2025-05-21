# Experimental Unity MVC

Video: https://www.youtube.com/watch?v=Q0M85VoMDrQ

This repository is my attempt to bring MVC/MVVM architecture patterns into Unity. It mainly uses Zenject for dependency injection.

My main goal was to integrate only the view layer with Unity and keep the controller and model parts in Mono/C# layers. 
There are several experimental (for me) ideas inside. I used both a command/event bus system and UniRx for reactive programming to connect the Model and View layers. Most communication between controllers, views, and models is done through an event bus or a command manager. However, some parts of the code are now hard to follow. Using a shared Context class which exist in the project could be a better way to manage communication.

The structure is built on top of Zenject. I use ISceneLifeCycleManaged interfaces to connect classes and listen to Unity callbacks. In this system, non-MonoBehaviour class instances are managed inside the scene through a SceneLifeCycleManager.
Of course, you can also use MonoBehaviours to create model and controllers if you prefer.

One experimental idea I now regret is making each MVC entity’s installer a ScriptableObject. For the objects I call "MVC entities", I’m using ScriptableObject-based installers to load them in zenject step. However, it’s also totally possible to load everything from a single installer. 
I thought it would help avoid Git conflicts in team projects, but trying to solve a problem that didn’t really exist just made the system more complicated.

Also, each scene needs a SceneLifeCycleManager (for keeping the instances alive). I created a small tool to validate that.
I also created a small tool for memory tracking on the Mono/C# side, to inspect which objects are still alive in the scene and app layer.

For demonstration, I made a simple time counter and snake game to test the MVC flow, I kept these in the same project to test scene transitions.

## Memory Tracking Tools

![mvc_ss_1](https://github.com/user-attachments/assets/04cd7fa0-8ee0-4a73-99c8-005a9dab0bb8)
![mvc_ss_2](https://github.com/user-attachments/assets/2c3e4b48-35ec-4305-87e5-71cb9d893bd2)
