# Persistence-v2

A collection of Unity components for convenient game data serialization.  
Repository contains a demo project.

## Components

`PersistentModelContainer<T>` is responsible for saving (caching, scheduling, etc.) data models. It can be used together with any supported serialization method. By now, there are:
- XML (`XmlModelSerializer`)
- Binary (`BinaryModelSerializer`)
- Binary Encrypted (`SecureModelSerializer`) with either Rijndael or Base64 encryption.

Also, a `ModelSerializationLog` component is available.

## Example

Define a model (it has to have a parameterless constructor):
```c#
public class PlayerData
{
    public string Name = "Player";
    public int Level = 0;
    public string[] Items = {"Sword"}; 
}
```

Create a container component:
```c#
public class PlayerDataContainer : PersistentModelContainer<PlayerData> { }
```

Create a script for working with data:
```c#
public class DemoScript : MonoBehaviour 
{
    private void Start()
    {
        var container = GetComponent<IModelContainer<PlayerData>>();
        // read
        var playerData = container.Model;
        // write
        container.Model.Name = "Some other player name";
        container.SaveChanges();
    }
}
```

In Unity, create a GameObject with the following components attached:
- Desired serializer (e.g. `BinaryModelSerializer`)
- `PlayerDataContainer`
- `DemoScript`

That's it! Run the game and verify that data is indeed saved.
(On Windows, check `C:\\Users\[user_name]\AppData\\LocalLow\[company_name]\[product_name]`) 
