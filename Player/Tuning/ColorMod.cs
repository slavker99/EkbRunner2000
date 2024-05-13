using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMod : Modification
{
    [Header("Цвет")]
    [SerializeField] private Material color;
    [SerializeField] private MeshRenderer coloredObjectMesh;

    public Material GetMaterial() => color;

    public ColorMod()
    {
        modType = ModType.ColorMod;
    }

    public override void SetVisible(bool value)
    {
        if (value)
        {
            coloredObjectMesh.material = color;
            if (category == CategoryType.Colors)
                foreach (var mod in tuningView.GetRecolorMods())
                    mod.Recolor(color);
        }
    }

    public override void Recolor(Material material) { }
}
