using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Player
{
    public NavMeshAgent agent;

    private Vector3 destination;

    public bool IsDestination => Vector3.Distance(destination, Vector3.right*transform.position.x + Vector3.forward*transform.position.z) < 0.1f;

    protected override void Start()
    {
        base.Start();
        ChangeState(new PatrolState());
    }

    IState<Bot> currentState;

    public void SetDestination(Vector3 position)
    {
        destination = position;
        destination.y = 0;
        agent.SetDestination(position);
    }

    private void Update()
    {
        if(currentState != null)
        {
            currentState.OnExcute(this);

            CanMove(transform.position);
        }
    }
    public void ChangeState(IState<Bot> state)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    ChangeColor(colorType);

    //    NavMeshAgent agent = GetComponent<NavMeshAgent>();
    //    agent.SetDestination(target.position);

    //}

    
   
}
