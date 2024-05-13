using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TuningObject : MonoBehaviour
{
    [SerializeField] protected string id;
    [SerializeField] protected string modName;
    [SerializeField] protected ModType modType = ModType.None;
    [SerializeField] protected CategoryType category = CategoryType.None;
    [SerializeField] protected string description = "";
    public Transform CameraPosition;
    public Transform CameraTarget;

    public string GetId() => id;
    public string GetModName() => modName;
    public ModType GetModType() => modType;
    public CategoryType GetCatType() => category;
    public abstract void SetVisible(bool value);

}

public enum ModType
{
    None,
    BodyPart,
    ColorMod
}

public enum CategoryType
{
    None,
    Bumpers,
    Reshetki,
    Headlights,
    Spoilers,
    Colors,
    TonerColors,
    Stickers,
    Diski
}
