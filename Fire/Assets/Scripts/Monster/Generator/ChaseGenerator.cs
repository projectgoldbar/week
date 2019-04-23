using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(-500)]
public class ChaseGenerator : GeneratorBase
{
    protected WaitForSeconds second = new WaitForSeconds(0.3f);

    private float timer = 3;

    private float CurrentTime = 0;

    private void OnEnable()
    {
    }

    public override void Awake()
    {
        base.Awake();
        Path = new NavMeshPath();
    }

    public override void Initiate()
    {
        Process();
        ComponentOnOff();
        StartCoroutine(CalculatePath());
    }

    public virtual void Process()
    {
        unit.state = StateIndex.CHASE;
        CurrentTime = 0;
        timer = Random.Range(3.0f, 5.0f);
        unit.Anim.SetBool("RushAttack", false);
        unit.Anim.Play("Zombie_Walk");
    }

    private void ComponentOnOff()
    {
        state.Agent.enabled = true;
        state.Agent.updateRotation = true;
    }

    public override void Execution() //Update
    {
        CoolDown();
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