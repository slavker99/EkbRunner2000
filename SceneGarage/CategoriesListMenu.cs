using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;

public class CategoriesListMenu : MonoBehaviour
{
    [SerializeField] private List<TuningObject> categories;
    public UnityEvent<string> ButtonEvent;

    public void SetContent(List<TuningObject> objects)
    {
        categories = objects;
    }

    public void ButtonListClick(int i)
    {
        ButtonEvent.Invoke(categories[i].GetId());
    }
}
