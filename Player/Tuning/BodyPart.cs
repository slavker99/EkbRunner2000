using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : Modification
{
    [SerializeField] private List<MeshRenderer> meshes = new List<MeshRenderer>();
    public BodyPart()
    {
        modType = ModType.BodyPart;
    }
    public List<MeshRenderer> GetMeshes()
    {
        if (meshes.Count == 0)
            Debug.Log("” мода " + name + " не установлены меши под перекрас");
        return meshes;
    }

    public override void SetVisible(bool value)
    {
        gameObject.SetActive(value);
    }

    public override void Recolor(Material material)
    {
        if (GetNeedRecolor())
            foreach (var mesh in meshes)
                mesh.material = material;
    }
}
