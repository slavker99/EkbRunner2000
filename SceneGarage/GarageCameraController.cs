using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageCameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform cameraRotateTransform;
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;
    [SerializeField] private Transform mainPos;
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private List<CategoryType> posCategories;
    [SerializeField] private List<Transform> posTransforms;
    private bool camRotate = false;

    private Dictionary<CategoryType, Transform> camPositions = new Dictionary<CategoryType, Transform>();
    public bool isCameraMoving = false;

    private void Awake()
    {
        if (posCategories.Count == posTransforms.Count)
            for (int i = 0; i < posCategories.Count; i++)
                camPositions.Add(posCategories[i], posTransforms[i]);
        else
            Debug.Log("GarageCamera: Ошибка при иницализации позиций");
            
    }

    public void ChangeCameraPos(Transform pos, Transform target = null)
    {
        if (cameraRotateTransform)
        {
            cameraRotateTransform.rotation = Quaternion.Euler(Vector3.zero);
            camRotate = false;
        }
        if (pos != null)
            cameraTransform.position = pos.position;
        if (target != null)
            cinemachineCamera.LookAt = target;
    }

    public void RotateCamera()
    {
        camRotate = !camRotate;
        //cameraRotateTransform.Rotate(0, 1, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown("q"))
            RotateCamera();

        if (camRotate)
            cameraRotateTransform.Rotate(0,0.7f,0);
    }
}