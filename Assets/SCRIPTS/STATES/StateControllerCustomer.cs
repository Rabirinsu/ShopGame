using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using System;


public class StateControllerCustomer : MonoBehaviour
{
    public CustomerSpawner SpawnerCustomer;
    public GameObject customerUI;
    public GameObject customerItemUI;
    public bool isExit;
    public bool isShop;
    private NavMeshPath path;
    public GameObject player;
    private Vector3 destinationPoint;
    public CustomerState currentState;
    public NavMeshSurface surface;
    public DestinationPoints destinationPoints;
    [HideInInspector] public CinemachineVirtualCamera Vcam;
    [HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public NavMeshObstacle obstacle;
    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        PathFind();
        isShop = false;
        animator = GetComponent<Animator>();
        Vcam = GameObject.Find("MainVcam").GetComponent<CinemachineVirtualCamera>();
        obstacle = GetComponent<NavMeshObstacle>();
        isExit = false;
    }

    private void Start()
    {
        Application.targetFrameRate = 30;
        player = GameObject.Find("Player");

    }
    // Update is called once per frame
    void Update()
    { // TODO :     FIX ISSUE AGENT STUCK DESTINATION 
            if (currentState.ExitState(this))
            {
                UpdateState();
                currentState.ExecuteState(this);
            }
            //   currentState.ExecuteState(this);
    }
    public void UpdateState()
    {
            foreach (var state in currentState.transitions) // Check every state transitions rules
            {

                if (state.CheckRules(this))
                    currentState = state;
            }
    }

    private void PathFind()
    {
        path = new NavMeshPath();


        while (path.status != NavMeshPathStatus.PathPartial)
        {
            path = new NavMeshPath();
            var index = UnityEngine.Random.Range(0, destinationPoints.destinationPoints.Count);
            destinationPoint = destinationPoints.destinationPoints[index];

            agent.CalculatePath(destinationPoint, path);


            if (path.status != NavMeshPathStatus.PathPartial)
            {
                agent.SetDestination(destinationPoint);
            }
            break;
            }
     
    }
    private void OnTriggerEnter(Collider other)
    {
        isShop = true;
    }
}
