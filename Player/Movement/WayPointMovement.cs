using System;
using System.Collections;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMovement : PlayerMovement
{
    [ReadOnly] public CarWayPoint NextPoint;
    [SerializeField] protected List<Transform> LanePositions;
    [SerializeField] protected float rotateCoef = 0.00045f;

    private void FixedUpdate()
    {
        if (currentSpeed < controller.PlayerModel.GetMaxSpeed())
            currentSpeed = currentSpeed + acceleration;
        if (controller.isMoving)
        {
            playerTransform.position = Vector3.MoveTowards(transform.position, NextPoint.transform.position, currentSpeed * speedCoef);
            playerTransform.rotation = Quaternion.RotateTowards(transform.rotation, NextPoint.transform.rotation, currentSpeed * rotateCoef);
        }
        if (controller.isChangingLane != Direction.None)
            ChangingLane();
    }

    private void SetNextLane(int lane, Direction dir)
    {
        if (4 <= lane && lane < LanePositions.Count)
        {
            if (lane > currentLane) controller.PlayerView.ChangingLaneState(Direction.Right);
            else controller.PlayerView.ChangingLaneState(Direction.Left);
            nextLane = lane;
            controller.isChangingLane = dir;
            //if (CameraController.AutoChangePos)
            //    CameraController.ChangePos(nextLane);
        }
    }

    private void ChangingLane()
    {
        if (Math.Abs(transform.position.x - LanePositions[nextLane].transform.position.x) < 0.1f)
            controller.isChangingLane = Direction.None;
        else
            playerTransform.position = Vector3.MoveTowards(transform.position, LanePositions[nextLane].transform.position, changeLaneSpeed);
    }


    public override void MoveToPoint(Transform point)
    {
        playerTransform.position = point.transform.position;
        playerTransform.rotation = point.transform.rotation;
        //if (point.NextPoint != null)
           // NextPoint = point.NextPoint;
    }

    public override void ToStartState(Transform point)
    {
        currentSpeed = controller.PlayerModel.GetStartSpeed();
        MoveToPoint(point);
    }

    public override void CrashState()
    {
        currentSpeed = controller.PlayerModel.GetStartSpeed();
    }

    public override void GameOverState()
    {
        currentSpeed = 0;
    }

    public override void MoveLeft()
    {
        if (NextPoint.Group.LeftGroup != null)
        {
            SetNextLane(nextLane - 1, Direction.Left);
            NextPoint = NextPoint.Group.LeftGroup.WayPoints[NextPoint.Number];
        }

    }

    public override void MoveRight()
    {
        if (NextPoint.Group.RightGroup != null)
        {
            SetNextLane(nextLane + 1, Direction.Right);
            NextPoint = NextPoint.Group.RightGroup.WayPoints[NextPoint.Number];
        }
    }
}
