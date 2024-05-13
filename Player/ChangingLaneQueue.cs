using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс ChangingLaneQueue
/// Очередь нужна для обработки нескольких одновременных перестроений игрока
/// </summary>

public class ChangingLaneQueue: MonoBehaviour
{
    public Queue<Direction> ActionsQueue = new Queue<Direction>();
    private Direction queueType = Direction.Left;
    private PlayerController controller;

    public void Initialization(PlayerMovement mov, PlayerController contr)
    {
        controller = contr;
    }

    public void ClearQueue()
    {
        ActionsQueue.Clear();
    }

    public void AddAction(Direction action)
    {
        if (action != queueType)
        {
            ClearQueue();
            queueType = action;
        }
        ActionsQueue.Enqueue(action);
    }

    private void FixedUpdate()
    {
        if ((controller.isChangingLane == Direction.None) && (ActionsQueue.Count != 0))
        {
            var action = ActionsQueue.Dequeue();
            if (action == Direction.Left)
                controller.PlayerMovement.MoveLeft();
            else if (action == Direction.Right)
                controller.PlayerMovement.MoveRight();
        }
    }
}

public enum Direction
{
    None,
    Left,
    Right
}
