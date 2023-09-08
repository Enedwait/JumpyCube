# JumpyCube
#### Jumpy Cube ECS Demo Game.
Play: https://play.google.com/store/apps/details?id=com.okrt.jumpycube (Android)

#### Brief:
It's a Flappy Bird look-alike game made in Unity with use of ECS.

#### Main features:
- ECS (Leoecs-lite).
- A little bit of Zenject.
- Everything else is from the scratch.
- My very original (author's) music. xD

#### Core Gameplay:
- I guess, it's like in Flappy Bird)

#### Requirements
- Unity 2022
- Visual Studio 2019
- [Zenject](https://github.com/modesttree/Zenject)

#### Extra-code
This should be added to EcsUguiActionBase.cs from [LeoECS Lite uGui Bindings](https://github.com/Leopotam/ecslite-unity-ugui)

```
public void SetWidgetName(string widgetName)
{
    if (string.IsNullOrWhiteSpace(widgetName))
        return;

    if (_emitter)
        _emitter.SetNamedObject(_widgetName, null);

    _widgetName = widgetName;

    ValidateEmitter();
    _emitter.SetNamedObject(_widgetName, gameObject);
}
```

Regards, Oleg [Knight Rider] Tolmachev.
