using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(-500)]
public class ChaseGenerator : GeneratorBase
{
    private NavMeshPath Path;
    protected WaitForSeconds second = new WaitForSeconds(0.3f);

    private float timer = 3;

    private float CurrentTime = 0;

    public override void Awake()
    {
        base.Awake();
        Path = new NavMeshPath();
    }

    public override void Initiate()
    {
        Process();

        unit.Anim.SetBool("RushAttack", false);
        unit.Anim.Play("Zombie_Walk");
        StartCoroutine(CalculatePath());
        state.Agent.updateRotation = true;
    }

    private void OnEnable()
    {
    }

    private void Process()
    {
        unit.state = StateIndex.CHASE;
        state.Agent.enabled = true;
        CurrentTime = 0;
        timer = Random.Range(3.0f, 5.0f);
    }

    public override void Execution() //Update
    {
        //if (unit.Distance <= 20.0f)
        {
            CoolDown();
        }
    }

    public virtual void CoolDown()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime >= timer)
        {
            CurrentTime = 0;
            state.ChangeState(StateIndex.ATTACK);
        }
    }

    private IEnumerator CalculatePath()
    {
        while (true)
        {
            state.Agent.CalculatePath(Utility.Instance.playerTr.position, Path);
            state.Agent.SetPath(Path);
            yield return second;
        }
    }
}