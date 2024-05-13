using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс DynamicPointMovement
/// Описывает движение игрока по радиальной траектории с перестроениями
/// </summary>

public class DynamicPointMovement : PlayerMovement
{
    [SerializeField] private bool autoAcceleration = false;
    [SerializeField] private Transform playerRotateTransform;
    [SerializeField] private List<Transform> lanes;
    private Transform currentLaneTransform;
    [SerializeField] private CameraController cameraController;

    private void Start()
    {
        if (controller.isMoving) ChangeLane(currentLane);
    }

    private void FixedUpdate()
    {
        if (controller.isMoving)
        {
            if (autoAcceleration)
                if (currentSpeed < controller.PlayerModel.GetMaxSpeed())
                    currentSpeed = currentSpeed + acceleration;
            playerRotateTransform.Rotate(new Vector3(0, -currentSpeed * speedCoef, 0));
            MoveLeftRight();
        }
        if (cameraController) 
            cameraController.CustomUpdate();
    }

    private void MoveLeftRight()
    {
        if (Math.Abs(playerTransform.localPosition.x - currentLaneTransform.localPosition.x) < 0.1f)
            controller.isChangingLane = Direction.None;
        else if (controller.isChangingLane != Direction.None)
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, currentLaneTransform.position, changeLaneSpeed);
    }

    private void ChangeLane(int lane)
    {
        currentLaneTransform = lanes[lane].transform;
        lastLane = currentLane;
        currentLane = lane;
        cameraController.ChangePos(lane);
    }

    public override void MoveToPoint(Transform point)
    {
        playerTransform.position = point.transform.position;
        playerTransform.rotation = point.transform.rotation;
    }

    public override void ToStartState(Transform point)
    {
        currentSpeed = controller.PlayerModel.GetStartSpeed();
        MoveToPoint(point);
    }

    public override void CrashState()
    {
        currentSpeed = controller.PlayerModel.GetCrashSpeed();
        if (controller.isChangingLane != Direction.None)
        {
            ChangeLane(lastLane);
        }
    }

    public override void GameOverState()
    {
        currentSpeed = 0;
    }

    public override void MoveUp(float value)
    {
        if ((!autoAcceleration) && (currentSpeed < controller.PlayerModel.GetMaxSpeed()))
            currentSpeed = currentSpeed + controller.PlayerModel.GetAccelerationCoef();
    }

    public override void MoveDown(float value)
    {
        if ((!autoAcceleration) && (currentSpeed > controller.PlayerModel.GetDefaultSpeed()))
            currentSpeed = currentSpeed + value * controller.PlayerModel.GetTurningCoef();
    }

    public override void MoveLeft()
    {
        var nextLane = currentLane - 1;
        if (0 <= nextLane && nextLane < lanes.Count)
        {
            ChangeLane(nextLane);
            controller.isChangingLane = Direction.Left;
            controller.PlayerView.ChangingLaneState(Direction.Left);
        }
    }

    public override void MoveRight()
    {
        var nextLane = currentLane + 1;
        if (0 <= nextLane && nextLane < lanes.Count)
        {
            ChangeLane(nextLane);
            controller.isChangingLane = Direction.Right;
            controller.PlayerView.ChangingLaneState(Direction.Right);
        }
    }

    public override void SetSpeed(float speed)
    {
        currentSpeed = speed;
    }
}
