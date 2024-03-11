using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject targetBall;
    public Action OnChangingPath { get; set; }
    public Action OnReachingNextPath { get; set; }
    bool inChangeState;
    Vector3 offset;
    // Start is called before the first frame update
    private void Start()
    {
        offset = transform.position - targetBall.transform.position;
    }
    void OnEnable()
    {
        OnChangingPath += FollowTarget;
        OnReachingNextPath += StopFollowing;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 offset = transform.position - targetBall.transform.position;
        //if (inChangeState)
        //{
        //    Vector3 pos = Vector3.Lerp(transform.position, targetBall.transform.position + offset, 0.04f);
        //    transform.position = pos;
        //}
        Vector3 pos = Vector3.Lerp(transform.position, targetBall.transform.position + offset, 0.04f);
        transform.position = pos;
    }

    private void FollowTarget()
    {
        inChangeState = true;
    }

    private void StopFollowing()
    {
        inChangeState = false;
    }
    void OnDisable()
    {
        OnChangingPath -= FollowTarget;
        OnReachingNextPath -= StopFollowing;
    }
}
