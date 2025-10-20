# Description
This library adds an ability to edit configs for mods (that support this) inside a lobby.

![](https://github.com/KingEnderBrine/-RoR2-InLobbyConfig/blob/master/Screenshots/1.png?raw=true)

![](https://github.com/KingEnderBrine/-RoR2-InLobbyConfig/blob/master/Screenshots/2.png?raw=true)

# For developers
Dependency attribute `[BepInDependency("com.KingEnderBrine.InLobbyConfig")]`

## TL;DR
Easiest (but limited) way to add config based on BepInEx config is to use helper method
```cs
var configEntry = InLobbyConfig.Fields.ConfigFieldUtilities.CreateFromBepInExConfigFile(Config, "Display name");
InLobbyConfig.ModConfigCatalog.Add(configEntry);
```
There is a special field. If your config has a `bool` field called `Enabled` (ignored case) it will be shown next to the mod name like so:

![](https://github.com/KingEnderBrine/-RoR2-InLobbyConfig/blob/master/Screenshots/3.png?raw=true)

This behavior can be disabled if you add `false` to parameters for `CreateFromBepInExConfigFile`.

Or you can create `InLobbyConfig.ModConfigEntry` and add fields and section manually 
```cs
var configEntry = new InLobbyConfig.ModConfigEntry();
configEntry.DisplayName = "Display name";
//configEntry.EnableField = new BooleanConfigField(...);
configEntry.SectionFields.Add("Section name", new List<IConfigField>
{
    new InLobbyConfig.Fields.BooleanConfigField("display name", () => ConfigField.Value, (newValue) => ConfigField.Value = newValue),
    InLobbyConfig.Fields.ConfigFieldUtilities.CreateFromBepInExConfigEntry<int>(ConfigField)
});
InLobbyConfig.ModConfigCatalog.Add(configEntry);
```

## Documentation
### Supported field types
|Type|Field class|
|----|---|
|bool|BooleanConfigField|
|enum|EnumConfigField|
|int|IntConfigField|
|float|FloatConfigField|
|string|StringConfigField|
|UnityEngine.Color|ColorConfigField|
|Collection|SelectListField|

Or you can create your own `ConfigField` with your own prefab, for example:

![](https://github.com/KingEnderBrine/-RoR2-InLobbyConfig/blob/master/Screenshots/4.png?raw=true)

### Field constructor parameters description
##### BaseConfigField (BooleanConfigField, EnumConfigField)

* displayName - name of a field
* tooltip - text to display when hover over field name.
* valueAccessor - a function which will be used to retrieve actual value of config.
* onValueChanged - a function which will be called when the value of a field is changed. Value in config doesn't automatically change, this is up to you what you want to do with the changed value.

##### SelectConfigField
Same as `BaseConfigField` +

* optionsAccessor - a function which will be used to retrieve available values for selection.

##### BaseInputConfigField (ColorConfigField, StringConfgiField)
Same as `BaseConfigField` +

* onEndEdit - a function which will be called when a user ended changing field. Unlike `onValueChange` which called with every change. Value in config doesn't automatically change, this is up to you what you want to do with the changed value.

##### BaseNumberInputConfigField (IntConfigField, FloatConfigField)
Same as `BaseInputConfigField` +

* minimum - minimum for a field value.
* maximum - maximum for a filed value.

##### SelectListField
Same as `BaseConfigField` + 

* OnItemAddedCallback - a function which will be called when a new item added to a collection.
* OnItemRemovedCallback - a function which will be called when an item removed from a collection.
* OptionsAccessor - a function which will be used to retrieve options for selection.

### Custom config field
Mandatory things that you need to create a custom config field:

* Create a class that implements `InLobbyConfig.Fields.IConfigField` (or inherits form `InLobbyConfig.Fields.BaseConfigField<T>`)
* Create a class that inherites `InLobbyConfig.FieldControllers.ConfigFieldController`
* Create a prefab that has your `ConfigFieldController` in a root object

From this point, you can do whatever you want. For examples of how I set up fields, you can go to [InLobbyConfig GitHub](https://github.com/KingEnderBrine/-RoR2-InLobbyConfig) or [ArtifactsRandomizer GitHub](https://github.com/KingEnderBrine/-RoR2-ArtifactsRandomizer).