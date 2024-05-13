using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ModCategory: TuningObject
{
    [SerializeField] private bool unicMod = true; // если одновременно может быть установлена только одна модификация
    public List<Modification> Mods = new List<Modification>();

    public bool GetUnicMod() => unicMod;

    public void Initialization()
    {
        Mods = (from m in gameObject.GetComponentsInChildren<Transform>()
               where m.tag == "Modification"
               select m.GetComponent<Modification>()).ToList();
    }

    public override void SetVisible(bool value)
    {

    }

    public void UnvisibleMods()
    {
        foreach (var mod in Mods)
            mod.SetVisible(false);
    }

    public void ResetMainMods()
    {
        foreach (var mod in Mods)
            mod.SetIsMain(false);
    }
}
