using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ChaseGenerator : GeneratorBase
{
    private NavMeshPath Path;
    private WaitForSeconds second = new WaitForSeconds(0.02f);

    private float timer = 3;

    private float CurrentTime = 0;

    private void OnEnable()
    {
        Path = new NavMeshPath();
    }

    public override void Initiate()
    {
        base.Initiate();
        Process();
        timer = Random.Range(3.0f, 5.0f);
    }

    public override void Exit()
    {
    }

    private void Process()
    {
        unit.state = StateIndex.CHASE;
    }

    public override void Execution() //Update
    {
        if (unit.Distance <= 20.0f)
        {
            CurrentTime += Time.deltaTime;
            if (CurrentTime >= timer)
            {
                CurrentTime = 0;
                state.ChangeState(StateIndex.ATTACK);
            }
        }

        NavMesh.CalculatePath(transform.position, Ref.Instance.playerTr.position, NavMesh.AllAreas, Path);

        state.Agent.SetPath(Path);
    }
}