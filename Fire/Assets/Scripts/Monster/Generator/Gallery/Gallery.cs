using UnityEngine;

[DefaultExecutionOrder(-250)]
public class Gallery : ChaseGenerator
{
    private void OnEnable()
    {
        StartCoroutine(CalculatePath(Utility.Instance.playerTr));
    }

    public override void Awake()
    {
        base.Awake();
    }

    public override void Initiate()
    {
        base.Initiate();
        second = new WaitForSeconds(0.2f);
    }

    public override void Execution()
    {
        CoolDown();
    }

    public override void CoolDown()
    {
        DistanceCheck();
    }

    public override void DistanceCheck()
    {
        if (unit.distance <= range)
        {
            state.ChangeState(StateIndex.ATTACK);
        }
        else return;
    }
}