using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс PlayerMovement
/// Базовый класс для описания передвижения игрока
/// </summary>
/// 
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected Transform playerTransform;
    [SerializeField] protected PlayerController controller;
    [SerializeField] protected float currentSpeed = 0.1f;
    [SerializeField] protected float speedCoef = 0.002f;
    [SerializeField] protected float acceleration = 0.1f;
    [SerializeField] protected float changeLaneSpeed = 0.1f;

    [SerializeField] protected int lastLane = 0;
    [SerializeField] protected int currentLane = 1;
    [SerializeField] protected int nextLane = 1;


    public virtual void MoveToPoint(Transform point) { }

    public void SetNextPoint(Transform point) { }

    public virtual void ToStartState(Transform point) { }

    public virtual void CrashState() { }

    public virtual void GameOverState() { }

    public virtual void MoveUp(float value) { }
    public virtual void MoveDown(float value) { }
    public virtual void MoveLeft() { }

    public virtual void MoveRight() { }

    public virtual void SetSpeed(float speed) { }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

}
