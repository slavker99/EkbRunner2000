using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool AutoChangePos = true;
    [SerializeField] private Transform CameraObj;
    [SerializeField] private Transform Cameras;
    [SerializeField] private Transform Positions;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private int startPos = 1;
    [SerializeField] private float cameraSpeed = 0.1f;


    [Header("PostProcessing")]
    [SerializeField] private PostProcessVolume postProcessing;
    [SerializeField] private float distortionMin = 0;
    [SerializeField] private float distortionMax = -50;
    [Header("Коэффициенты смещения камеры от скорости")]
    [SerializeField] private float camYcoef = 20;
    [SerializeField] private float camZcoef = 20;
    private LensDistortion distortion;
    private float lastSpeed = 0;
    private Queue<float> lastSpeeds = new Queue<float>();

    [SerializeField] private PlayerController playerController;
    private bool isInitialize = false;
    private int nextPos = 0;
    private bool cameraMoving = false;

    [SerializeField] private List<Transform> PosHardLayer0;
    [SerializeField] private List<Transform> FreePositions;

    private Transform newCameraPos;
    private float cameraZpos;
    private float cameraYpos;

    private void Awake()
    {
        Cameras.parent = playerTransform;
        newCameraPos = CameraObj;
        postProcessing.profile.TryGetSettings(out distortion);
        isInitialize = true;
        CameraObj.localPosition = PosHardLayer0[startPos].localPosition;
    }

    private void Start()
    {
        cameraZpos = CameraObj.localPosition.z;
        cameraYpos = CameraObj.localPosition.y;
    }

    public void CustomUpdate()
    {
        SpeedAnimations();
        UpdatePosition();
    }

    private void SpeedAnimations()
    {
        var curSpeed = playerController.PlayerModel.GetCurrentSpeed();
        var maxSpeed = playerController.PlayerModel.GetMaxSpeed();
        var middleSpeed = GetMiddleSpeed();
        var newDistVal = Mathf.Lerp(distortionMin, distortionMax, middleSpeed / maxSpeed);
        distortion.intensity.value = Mathf.Lerp(lastSpeed, newDistVal, 1f);
        lastSpeed = curSpeed;

        CameraObj.localPosition = new Vector3(CameraObj.localPosition.x, 
            cameraYpos + middleSpeed / camYcoef, 
            cameraZpos - middleSpeed / camZcoef);
    }

    private float GetMiddleSpeed()
    {
        var curSpeed = playerController.PlayerModel.GetCurrentSpeed();
        float middleSpeed = 0;
        lastSpeeds.Enqueue(curSpeed);
        if (lastSpeeds.Count > 10)
            lastSpeeds.Dequeue();
        foreach (float num in lastSpeeds)
            middleSpeed += num;
        middleSpeed = middleSpeed / lastSpeeds.Count;
        return middleSpeed;
    }

    public void UpdatePosition()
    {
        if (cameraMoving)
        {
            var newXpos = Mathf.Lerp(CameraObj.transform.localPosition.x, newCameraPos.localPosition.x, cameraSpeed);
            CameraObj.transform.localPosition = new Vector3(newXpos, CameraObj.transform.localPosition.y, CameraObj.transform.localPosition.z);
            //CameraObj.transform.localPosition = Vector3.MoveTowards(CameraObj.transform.localPosition, newCameraPos.localPosition, cameraSpeed);
        }

        if (CameraObj.localPosition.x == newCameraPos.localPosition.x)
            cameraMoving = false;
    }

    public void ChangePos(int numPos, float speed = 0.1f)
    {
        if (!isInitialize)
            Awake();
        //cameraSpeed = speed;
        nextPos = numPos;
        cameraMoving = true;
        newCameraPos = PosHardLayer0[nextPos];
    }

    public void ChangeFreePos(int numPos, float speed = 0.1f) // выбор позиции из заданного массива
    {
        if ((0 <= numPos) && (numPos < FreePositions.Count))
        {
            cameraSpeed = speed;
            newCameraPos = FreePositions[numPos];
            cameraMoving = true;
            AutoChangePos = false;
        }
    }

    public void ChangeFreePos(Transform pos, float speed = 0.1f) // выбор позиции с кастомными координатами
    {
        cameraSpeed = speed;
        newCameraPos = pos;
        cameraMoving = true;
        AutoChangePos = false;
    }



    public void DetachCameraFromPlayer()
    {
        Cameras.parent = this.transform;
    }

}
