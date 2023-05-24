# Unity SO Architecture

This package provides the base building blocks for working with a ScriptableObjects Architecture in Unity.

Based on the talk by Ryan Hipple's in Unite 2017 https://www.youtube.com/watch?v=raQ3iHhE_Kk

I put together this package for reusability to meet my needs and later decided to make it public. This was not meant to be a package that covers all possible scenarios, but to be extendable so it can.

## Installation

> ### NOTE: If you are using `Assembly Definitions` in Unity, You need to reference the editor assembly for the drawers to be used.
>
> ### This is due to the use of `dynamic` keyword which is not supported in unity builds.

### Package Manager Installation

Use the URL in the package manager window > Add... Add package from git URL...

> `https://github.com/mariodebono/Unity-SO-Architecture.git#release/stable`

![Add Package from Git URL](./Documentation~/Resources/Add%20package%20from%20gitURL.jpg)

Modify your `manifest.json` file found at `/PROJECTNAME/Packages/manifest.json` and add the following line:

```json
{
  "dependencies": {
    ...
    "com.mariodebono.so-architecture": "https://github.com/mariodebono/Unity-SO-Architecture.git#release/stable",
    ...
  }
}
```

---

## Features

### **Events**

There are two types of events: `EmptyGameEvent` and `BaseGameEvent<>`. These events can be extended to accommodate arguments.

#### **EmptyGameEvent**

These are just events that do not carry any arguments and are just that.

#### **BaseGameEvent<>**

These are events that can be extended to pass custom arguments.

```csharp
  // PoseEventArgs.cs

  [Serializable]
  public struct PoseEventArgs
  {
    public Vector3 position;
    public Vector3 rotation;
  }
```

```csharp
  // PoseGameEvent.cs

  public class PoseGameEvent : BaseGameEvent<PoseEventArgs>
  {
  }
```

### **Event Listeners**

While the `EmptyGameEvent` comes with a paired `EmptyGameEventListener` and you do not need to do anything, for the generic `GameEvents<>` you need to create your own listener. This is super simple.

Following the above example:

```csharp
  // PoseEventListener.cs


  public class PoseEventListener : BaseGameEventListener<PoseEventArgs> {
    public override void OnEventRaised(PoseEventArgs args)
    {
      // extend the logic in this method
      // event will not trigger if you don't call the base
      // you can access the base event and action here also.
      base.OnEventRaised(args);
    }
  }
```

### **Editor**

The listener comes with a custom editor that allows you to trigger the event.

There are two modes to trigger: `Raise Event` and `Raise Local`.

![Listener Editor UI](./Documentation~/Resources/Listener%20Editor%20UI.jpg)

The `Raise Event` button will trigger the event on the Event Scriptable object, which means all registered listeners will receive the event.

The `Raise Local Event` on the _Event Listener_ will only trigger the `UnityEvent<> eventResponse` on the component it is clicked on.

> When `Raise Local Event` or `Raise Event` are triggered from the listener, the `Args` in the editor window will be the args used in the event.

---

### **Variables**

There are no preset variables shipped in this package, I prefer to create only what I need, and it is very simple to add more.

> Variables and References included in package:
>
> - FloatVariable
> - Vector3Variable
> - IntVariable
> - BoolVariable

There are two classes that can be extended: `BaseVariable<>` and `BaseVariableReference<,>`

#### **BaseVariable<>**

This class provides the basic structure for a variable, and can be extended with the required type.

> Types can be any struct type to handle complex values.

```csharp
  // FloatVar.cs

  [CreateAssetMenu(menuName = "Variables/Pose Var")]
  public class FloatVar : BaseVariable<float>
  { }
```

or a more complex type

```csharp
  // PoseVar.cs

  [Serializable]
  public struct Pose
  {
    public Vector3 position;
    public Vector3 rotation;
  }


  [CreateAssetMenu(menuName = "Variables/Pose Var")]
  public class PoseVar : BaseVariable<Pose>
  { }
```

#### **BaseVariableReference<,>**

This class provides the base for working with variables.

- Switch between constant or variable values.
- Custom Inspector to keep a single-line interface
- Multiline Inspector for Complex Types (Generics)

You can simply create a specific type:

```csharp
  // PoseReference.cs

  [Serializable]
  public class PoseReference : BaseVariableReference<PoseVar, Pose>
  { }
```

> This takes 2 generic types, the first is to mark the actual variable, and the second type argument is used to generate a generic constant type.

Simple Types (most built-in, like `Vector3`, `float`):

![Variable Inspector](./Documentation~/Resources/Variables%20Editor%20UI.jpg)

Complex types expand below:

![Complex Example](./Documentation~/Resources/Complex%20Variable%20UI.jpg)

#### **Disabled GUI (ReadOnly)**

When `GUI.enabled` is set to `false`, the property will be rendered as disabled and fixed to `useConstant = false` (use variable)

The property is still accessible through code.

![Disabled Variable Reference, empty](./Documentation~/Resources/Disabled%20Variable%20Reference.png)

or

![Disabled Variable Reference, With variable](./Documentation~/Resources/Disabled%20Variable%20Reference%2Bvariable.png)

---
