using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ChaseGenerator : GeneratorBase
{
    private NavMeshPath Path;
    private WaitForSeconds second = new WaitForSeconds(0.02f);

    private void OnEnable()
    {
        Path = new NavMeshPath();
        StartCoroutine(AgentSetPosition());
    }

    public override void Initiate()
    {
        base.Initiate();
        Process();
    }

    public override void Exit()
    {
    }

    private void Process()
    {
        unit.state = StateIndex.CHASE;
    }

    private void Update()
    {
        NavMesh.CalculatePath(transform.position, Ref.Instance.playerTr.position, NavMesh.AllAreas, Path);

        state.Agent.SetPath(Path);
    }

    public IEnumerator AgentSetPosition()
    {
        for (; ; )
        {
            yield return second;
        }
    }
}