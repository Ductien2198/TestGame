using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        Transform target = GameObject.Find("Finish").transform;
        t.SetDestination(target.position);
    }

    public void OnExcute(Bot t)
    {
        if(t.BrickCount == 0)
        {
            t.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot t)
    {
    }

}
