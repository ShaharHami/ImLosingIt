using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleManager : MonoBehaviour
{
    public float checkInterval;
    public float obstacleProbability;
    public int maxObstacles;
    public List<Obstacle> obstacles;
    public float minDistance;
    public Player player;
    [HideInInspector] public bool canInteract = true;

    private Obstacle currentCoolDown;

    private void Start()
    {
        StartCoroutine(ObstacleLoop());
    }

    private IEnumerator ObstacleLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            foreach (var obstacle in GetObstaclesByState(Obstacle.State.Idle))
            {
                if (GetObstaclesByNotState(Obstacle.State.Idle).Count < maxObstacles && Random.Range(0f, 1f) < obstacleProbability)
                {
                    obstacle.Annoyed();
                    break;
                }
            }
        }
    }

    public List<Obstacle> GetObstaclesByState(Obstacle.State state)
    {
        return obstacles.Where(obstacle => obstacle.state == state).ToList();
    }
    
    public List<Obstacle> GetObstaclesByNotState(Obstacle.State state)
    {
        return obstacles.Where(obstacle => obstacle.state != state).ToList();
    }

    private void Update()
    {
        if (canInteract && Input.GetKey(KeyCode.E))
        {
            if (currentCoolDown != null && currentCoolDown.coolDownMeter > 0)
            {
                currentCoolDown.ShowPrompt();
                currentCoolDown.CoolDown();
            }
            else if (GetObstaclesByNotState(Obstacle.State.Idle).Count > 0)
            {
                currentCoolDown = GetClosestObstacle(GetObstaclesByNotState(Obstacle.State.Idle)); 
            }
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            currentCoolDown = null;
            canInteract = true;
        }
        else
        {
            var closestObstacle = GetClosestObstacle(GetObstaclesByNotState(Obstacle.State.Idle));
            if (closestObstacle != null)
            {
                closestObstacle.ShowPrompt();
            }
        }
        player.ErodeSanity(GetObstaclesByNotState(Obstacle.State.Idle).Count);
    }

    public Obstacle GetClosestObstacle(List<Obstacle> filteredObstacles)
    {
        Obstacle closest = null;
        foreach (var obstacle in filteredObstacles)
        {
            var distance = Vector2.Distance(obstacle.transform.position, player.transform.position);
            if (distance < minDistance)
            {
                if (closest == null || distance < Vector2.Distance(closest.transform.position, player.transform.position))
                {
                    closest = obstacle;
                }
            }
            obstacle.HidePrompt();
        }
        
        return closest;
    }
}