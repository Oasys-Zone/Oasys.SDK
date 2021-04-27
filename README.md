# Welcome!
Welcome to the OASYS SDK Guide.

## About
OASYS SDK allows you to develop modules(scripts as we refer to) with our api equipped with vast amount of features. Our SDK is written in C#, and therefor you will be writing the modules in C#.  
Please follow the [C# Language and Coding Convention](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions).

## Prerequisites
- Your brain
- At minimum, foundation level of knowledge in C# 
- An IDE (Strongly recommend Visual Studio)
- .NET Framework 4.7.2 Runtime 
- .NET Framework 4.7.2 Developer Pack

## Installation
Head over to the [releases tab](https://github.com/Oasys-Zone/Oasys.SDK/releases) and download the latest release build.  
After unpacking the zip file, You will see a set of DLLs:
- **Oasys.SDK.dll**: is our primary API and module development library. This is what you will be using mainly.
- **Oasys.Common.dll**: is our bridging API library between the SDK and our Core.   
Although you won't be using it directly, it is required for namespace and class references.
- **SharpDX.dll**: is our primary third-party rendering library.
- **SharpDX.Direct3D9.dll**: same purpose as SharpDX.dll.
- **SharpDX.Mathematics.dll**: is our third-party maths library used in rendering and object positions. 
- **Newtonsoft.Json.dll**: is our third-party serialization library.

Those are the DLLs you are required to reference in your module project.

## Namespace and Class Documentation
For namespace and class definitions and code documentation, please refer to [here](https://oasys-zone.github.io/Oasys.SDK/).

## Creating A Module
To begin, create a blank .NET Framework Class Library project and add the DLLs you have downloaded earlier as references.  

Next, create a class named "Main" and reference the needed namespaces:  
```csharp
/*Default .NET BCL references here and above*/
using Oasys.SDK;
using Oasys.SDK.Events;

namespace SampleModule
{
    class Main
    {
    }
}
```

Create a public static method "Execute" and attach OasysModuleEntryPoint attribute to the method.

```csharp
namespace SampleModule
{
    class Main
    {
        [OasysModuleEntryPoint]
        public static void Execute()
        {
        }
    }
}
```  

Inside this method, you will need to subscribe to the required events:
- **GameEvents.OnGameLoadComplete**: this event is raised when the loading of the game finishes, where a user is inside the lobby. If the game has already started, then it is raised immediately after the core has initialized.
- **GameEvents.OnGameMatchComplete**: this event is raised when a game match finishes.
- **CoreEvents.OnCoreMainTick**: this event is raised multiple times per second. To be specific, it is raised every 10ms. This event is useful for custom caching and calculations executing every each of tick.
- **CoreEvents.OnCoreMainInput**: this event is raised whenever the main input(space key for default) is registered.
- **CoreEvents.OnCoreRender**: this event is raised whenever the rendering occurs. If you want to draw, this is the event you want to subscribe to.  

```csharp
namespace SampleModule
{
    class Main
    {
        [OasysModuleEntryPoint]
        public static void Execute()
        {
            GameEvents.OnGameLoadComplete += GameEvents_OnGameLoadComplete;
            GameEvents.OnGameMatchComplete += GameEvents_OnGameMatchComplete;
        }

        private static void GameEvents_OnGameLoadComplete()
        {
            //This is where you want to initialize your stuffs.

            if(UnitManager.MyChampion.ModelName == "ChampionName")
            {
                CoreEvents.OnCoreMainTick += CoreEvents_OnCoreMainTick;
                CoreEvents.OnCoreMainInput += CoreEvents_OnCoreMainInput;
                CoreEvents.OnCoreRender += CoreEvents_OnCoreRender;
            }
        }

        private static void GameEvents_OnGameMatchComplete()
        {
            //This is where you want to dispose and unload your stuffs.
        }

        private static void CoreEvents_OnCoreMainTick()
        {
            //This is where you want to cache and calculate your stuffs.
        }

        private static void CoreEvents_OnCoreMainInput()
        {
            //This is where you can do stuffs whenever the main input is registered.
        }

        private static void CoreEvents_OnCoreRender()
        {
            //This is where you want to draw your stuffs.
        }
    }
}
```

## Compiling
Compile as normal, and then rename the built .dll library extension to .omod in order for the loader to load the module.


