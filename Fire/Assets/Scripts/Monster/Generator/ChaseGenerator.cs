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

    private void OnEnable()
    {
    }

    public override void Awake()
    {
        base.Awake();
        unit.ChaseTarget = Utility.Instance.playerTr;
        Path = new NavMeshPath();
    }

    public override void Initiate()
    {
        Process();
        unit.Anim.SetBool("RushAttack", false);

        unit.Anim.Play("Zombie_Walk");
        state.Agent.updateRotation = true;
        StartCoroutine(CalculatePath());
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
        //CurrentTime += Time.deltaTime;
        if (CurrentTime >= timer)
        {
            CurrentTime = 0;
            state.ChangeState(StateIndex.ATTACK);
        }
    }

    private IEnumerator CalculatePath()
    {
        var a = Utility.Instance.playerTr.GetComponent<TestTarget>();
        while (true)
        {
            state.Agent.ResetPath();
            state.Agent.CalculatePath(a.target, Path);
            state.Agent.SetPath(Path);
            yield return second;
        }
    }
}