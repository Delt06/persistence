#  Persistence

[![Version](https://img.shields.io/github/v/release/Delt06/persistence?sort=semver)](https://github.com/Delt06/persistence/releases)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A library for saving player's progress in Unity.

> Developed and tested with Unity 2020.3.16f1

## Table of contents

- [Installation](#installation)
- [Usage](#usage)

## Installation
### Option 1
- Open Package Manager through Window/Package Manager
- Click "+" and choose "Add package from git URL..."
- Insert the URL: https://github.com/Delt06/persistence.git?path=Packages/com.deltation.persistence

### Option 2  
Add the following line to `Packages/manifest.json`:
```
"com.deltation.persistence": "https://github.com/Delt06/persistence.git?path=Packages/com.deltation.persistence",
```

## Usage

- Create a persistent model:
```csharp
using System;

[Serializable]
public class DemoModel
{
    public string PlayerName = "Player";
    public string[] Data = { "1", "2", "3" };
}
```

- Define a component that will manage the persistent state:
```csharp
using DELTation.Persistence;
using DELTation.Persistence.Building;

public class DemoPersistentStateMono : PersistentStateMonoBase<DemoModel>
{
    protected override void ConstructPersistentState(PersistentStateBuilder<DemoModel> builder)
    {
        builder
            .WithJsonUtilitySerializer() // use JSON
            .WithDefaultModelFallback() // use default constructor if no data was stored
            .WithPeriodicWrite(1f) // save once a second automatically
            ;
    }
}
```

- Use the component to manage persistent data
```csharp
using DELTation.Persistence;
using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
    [SerializeField] private InputField _text;

    private PersistentStateMonoBase<DemoModel> _modelContainer;

    private void Awake()
    {
        // get access to the component
        _modelContainer = GetComponentInChildren<PersistentStateMonoBase<DemoModel>>();
    }

    public void Read()
    {
        // read a value from the model 
        _text.text = _modelContainer.Model.PlayerName;
    }

    public void Write()
    {
        // write a value to the model and then save it (mark as dirty)
        _modelContainer.Model.PlayerName = _text.text;
        _modelContainer.Save();
    }
}
```