using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(-250)]
public class ChaseGenerator : GeneratorBase
{
    protected WaitForSeconds second = new WaitForSeconds(0.3f);

    private float timer = 0;
    public float timerMin = 3;
    public float timerMax = 5;
    public float range = 10f;

    private float CurrentTime = 0;

    private void OnEnable()
    {
        //StartCoroutine(CalculatePath(Utility.Instance.playerTr));
    }

    public override void Awake()
    {
        base.Awake();
    }

    public override void Initiate()
    {
        Process();
        ComponentOn();
        StartCoroutine(CalculatePath(Utility.Instance.playerTr));
    }

    public virtual void Process()
    {
        unit.state = StateIndex.CHASE;
        CurrentTime = 0;
        state.Agent.radius = 0.4f;
        timer = Random.Range(timerMin, timerMax);
        unit.Anim.SetBool("RushAttack", false);
        unit.Anim.Play("Zombie_Walk");
    }

    private void ComponentOn()
    {
        state.Agent.enabled = true;
        state.Agent.updateRotation = true;
    }

    private void ComponentOnOff()
    {
        if (state.Agent.enabled == false)
        {
            state.Agent.enabled = true;
            state.Agent.updateRotation = true;
        }
        else
        {
            state.Agent.enabled = false;
            state.Agent.updateRotation = false;
        }
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
            DistanceCheck();
        }
    }

    public virtual void DistanceCheck()
    {
        if (unit.distance < range)
        {
            StopCoroutine(this.CalculatePath(Utility.Instance.playerTr));
            state.ChangeState(StateIndex.ATTACK);
        }
        else return;
    }

    public virtual IEnumerator CalculatePath(Transform target)
    {
        while (true)
        {
            state.Agent.ResetPath();
            state.Agent.CalculatePath(Utility.Instance.playerTr.position, Path);
            state.Agent.SetPath(Path);
            yield return second;
        }
    }
}