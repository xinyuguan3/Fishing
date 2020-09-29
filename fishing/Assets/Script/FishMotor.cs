﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///鱼运动类：移动（随机速度、随机位置）、旋转
///</summary>
public class FishMotor : MonoBehaviour
{
    public Transform[] wayPoint;
    private int currentIndex;
    public int randomIndex;

    public float maxSpeed = 0.5f;
    public float minSpeed = 0.1f;
    private float speed;
    public float rotateSpeed = 0.1f;

    private float startTime;
    public float timeInterval = 5;

    private void Start()
    {
        wayPoint = transform.parent.GetComponentInChildren<WayPoint>().wayPoint;
    }

    public void Pathfinding()
    {
        if (startTime < Time.time)
        {
            GenerateRandomSpeedAndIndex();
            startTime = Time.time + timeInterval;
        }

        RotateToTargetPoint(wayPoint[currentIndex].position, rotateSpeed);
        MoveToTargetPoint(currentIndex, speed);
    }

    public int RandomSelectWayPoint()
    {
        return Random.Range(0, wayPoint.Length); 
    }

    public void GenerateRandomSpeedAndIndex()
    {
        while (randomIndex == currentIndex)
        {
            randomIndex = RandomSelectWayPoint();
        }
        print(randomIndex);
        currentIndex = randomIndex;
        speed = Random.Range(minSpeed, maxSpeed);
    }

    public void MoveToTargetPoint(int index, float speed)
    {
        this.transform.Translate(wayPoint[index].position * Time.deltaTime * speed);
    }

    public void RotateToTargetPoint(Vector3 targetPoint, float speed)
    {
        Quaternion dir = Quaternion.LookRotation(targetPoint - this.transform.position);
       this.transform.rotation = Quaternion.Lerp(this.transform.rotation, dir, speed);
    }
}
