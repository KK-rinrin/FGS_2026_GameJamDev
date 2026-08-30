using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{

    [Header("チェックポイントの位置")]
    [SerializeField] private List<Transform> checkPoints = new List<Transform>();

    [Header("プレイヤーのTransform")]
    [SerializeField] private Transform playerPos;

    private int currentCheckPointIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Transform currentCheckPoint = checkPoints[currentCheckPointIndex];

        if(currentCheckPoint.transform.position.x <= playerPos.transform.position.x)
        {
            if(currentCheckPointIndex < checkPoints.Count - 1)
            {
                currentCheckPointIndex++;
            }
        }
    }

    public Transform GetCheckPoint()
    {
        return checkPoints[currentCheckPointIndex - 1];
    }
}
