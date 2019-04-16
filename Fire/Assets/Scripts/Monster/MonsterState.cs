using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    public NavMeshAgent Agent;
    public MonoBehaviour mono;

    [Header("안넣어도됨 상태머신에서 집어넣음.")]
    public GeneratorBase StateBase = null;

    [Header("자기본인 넣어주면됨")]
    public PatrolGenerator Patrol;

    public ChaseGenerator Chase;
    public AttackGenerator Attack;

    private List<GeneratorBase> generators = new List<GeneratorBase>();

    private void OnEnable()
    {
        ChangeState(StateIndex.CHASE);
    }

    private void Awake()
    {
        //StateSetting();
        ListAddSetting();
    }

    private void StateSetting()
    {
        Patrol = GetComponent<PatrolGenerator>();
        Chase = GetComponent<ChaseGenerator>();
        Attack = GetComponent<AttackGenerator>();
    }

    private void ListAddSetting()
    {
        generators.Add(Patrol);
        generators.Add(Chase);
        generators.Add(Attack);
    }

    private void Update()
    {
        if (StateBase != null)
        {
            StateBase.Execution();
        }
    }

    public void ChangeState(StateIndex nextState)
    {
        if (StateBase != null)
            StateBase.Exit();

        StateBase = CreateStateInstance(nextState);

        StateBase.Initiate();
    }

    public GeneratorBase CreateStateInstance(StateIndex NextState)
    {
        GeneratorBase generator = null;

        switch (NextState)
        {
            case StateIndex.PATROL:
                generator = Generator_activation(generator, Patrol);
                Generator_Inactive(Patrol);
                break;

            case StateIndex.CHASE:
                Agent.speed = 10f;
                generator = Generator_activation(generator, Chase);
                Generator_Inactive(Chase);
                Agent.avoidancePriority = 50;
                break;

            case StateIndex.ATTACK:
                Agent.speed = 0;
                generator = Generator_activation(generator, Attack);
                Generator_Inactive(Attack);
                Agent.avoidancePriority = 51;
                break;
        }
        return generator;
    }

    private GeneratorBase Generator_activation(GeneratorBase generator, GeneratorBase Base)
    {
        generator = Base;
        generator.enabled = true;

        return generator;
    }

    private void Generator_Inactive(GeneratorBase Base)
    {
        var list = generators.FindAll(x => x != Base);

        foreach (var item in list)
        {
            item.enabled = false;
        }
    }
}