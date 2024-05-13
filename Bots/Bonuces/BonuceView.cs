using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonuceView : MonoBehaviour
{
    [SerializeField] private List<Transform> bodies;
    private Transform body;
    [SerializeField] private Material bodyMT;
    [SerializeField] private Transform bodyTrigger;
    //[SerializeField] private Light bodyLight;
    [SerializeField] private GameObject mesh;

    private void Awake()
    {
        int num = Random.Range(0,bodies.Count);
        for (int i = 0; i < bodies.Count; i++) 
        {
            if (i == num)
                body = bodies[i];
            else
                bodies[i].gameObject.SetActive(false);
        }
        body.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    private void Update()
    {
        if (body != null)
            body.transform.localRotation = Quaternion.Euler(0, body.transform.localRotation.eulerAngles.y + 1f, 0);
    }

    public IEnumerator QuitCorutine()
    {
        Destroy(body.gameObject);
        bodyTrigger.Translate(new Vector3(0, -3, 0));
        mesh.SetActive(false);
        yield return null;
    }
}
