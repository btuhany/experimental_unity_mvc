# Experimental Unity MVC

This repository is my attempt to bring MVC/MVVM architecture patterns into Unity. It mainly uses Zenject for dependency injection.

There are several experimental (for me) ideas inside. I used both a command/event bus system and UniRx for reactive programming to connect the Model and View layers. Most communication between controllers, views, and models is done through an event bus or a command manager. However, some parts of the code are now hard to follow. Using a shared Context class which exist in the project could be a better way to manage communication.

One experimental idea I now regret is making each MVC entity’s installer a ScriptableObject. I thought it would help avoid Git conflicts in team projects, but trying to solve a problem that didn’t really exist just made the system more complicated.

I also created a small tool for memory tracking on the Mono/C# side.

Right now, the project includes a simple counter that starts on scene load and an unfinished snake game. I plan to finish the snake game later, but the MVC framework is mostly complete and ready to use.

## Memory Tracking Tools

![mvc_ss_1](https://github.com/user-attachments/assets/04cd7fa0-8ee0-4a73-99c8-005a9dab0bb8)
![mvc_ss_2](https://github.com/user-attachments/assets/2c3e4b48-35ec-4305-87e5-71cb9d893bd2)
