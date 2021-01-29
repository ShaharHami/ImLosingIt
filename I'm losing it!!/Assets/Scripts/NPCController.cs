using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public enum NPCState
{
    Moving, Working, Annoying, Idling, Follow
}
public class NPCController : MonoBehaviour
{
    private CharacterMover characterMover;

    public NPCState state = NPCState.Idling;
    private CharacterController characterController;

    public PointsOfInterest[] pointsOfInterest;
    public int currentPointIdx = -1;
    public PointsOfInterest target;
    public Transform followee;

    private Transform _transform;
    private Vector2 direction;

    public bool allowDebug = false;
    private Tween lastStretch;
    
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        _transform = this.transform;
        characterMover = GetComponent<CharacterMover>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (allowDebug)
            {
                BecomePlayable();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (allowDebug)
            {
                StartFollowingPlayerToTarget(target);
            }
        }
    }

    public void BecomePlayable()
    {
        StopAllCoroutines();
        this.enabled = false;
        characterController.enabled = true;
        GameObject.FindObjectOfType<CinemachineVirtualCamera>().Follow = this.transform;
        lastStretch.Kill();
    }

    public void SetState(NPCState state)
    {
        this.state = state;
        switch (state)
        {
                case NPCState.Idling:
                    break;
                case NPCState.Moving:
                    MoveToNextPoint();
                    break;
                case NPCState.Working:
                    break;
                case NPCState.Annoying:
                    break;
                case NPCState.Follow:
                    FollowPlayer();
                    break;
        }
    }

    private void OnDone()
    {
        if (pointsOfInterest[currentPointIdx].waypointOnly)
        {
            MoveToNextPoint();
            return;
        }

        if (pointsOfInterest[currentPointIdx].taskTime > 0)
        {
            //start task
            return;
        }

        if (pointsOfInterest[currentPointIdx].annoyingFactor > 0)
        {
            return;
        }
    }
    
    public void MoveToNextPoint()
    {
        currentPointIdx = (currentPointIdx + 1) % pointsOfInterest.Length;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (Vector3.Distance(pointsOfInterest[currentPointIdx].transform.position,_transform.position) >= 0.1f)
        {
            direction = pointsOfInterest[currentPointIdx].transform.position - _transform.position;
            characterMover.Move(direction.normalized);
            yield return null;
        }

        lastStretch = transform.DOMove(pointsOfInterest[currentPointIdx].transform.position, 0.03f).OnComplete(OnDone);
    }

    public void StartFollowingPlayerToTarget(PointsOfInterest target)
    {
        this.target = target;
        SetState(NPCState.Follow);
    }
    
    public void FollowPlayer()
    {
        followee = GameObject.FindObjectOfType<CinemachineVirtualCamera>().Follow;
        StartCoroutine(FollowToTarget());
    }
    
    IEnumerator FollowToTarget()
    {
        
        while (state == NPCState.Follow)
        {
            if (Vector3.Distance(_transform.position, target.transform.position) > 1)
            {
                if (Vector3.Distance(_transform.position, followee.position) > 1)
                {
                    direction = followee.position - _transform.position;
                    characterMover.Move(direction.normalized);
                    yield return null;
                }
                else
                {
                    characterMover.Move(Vector3.zero);
                    yield return null;
                }
            }
            else
            {
                characterMover.Move(Vector3.zero);
                if (Vector3.Distance(_transform.position, target.transform.position) > 0.5f)
                {
                    transform.DOMove(target.transform.position, 0.5f);
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    yield return null;
                }

            }
        }
    }

    public void StopFollowing()
    {
        SetState(NPCState.Idling);
    }
}
