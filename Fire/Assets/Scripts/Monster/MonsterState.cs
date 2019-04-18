﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MonsterState : MonoBehaviour
{
    public NavMeshAgent Agent;
    public MonoBehaviour mono;

    [Header("안넣어도됨 상태머신에서 집어넣음.")]
    public GeneratorBase StateBase = null;

    [Tooltip("자기자신오브젝트넣음")]
    public ChaseGenerator Chase = null;

    [Tooltip("자기자신오브젝트넣음")]
    public AttackGenerator Attack = null;

    [Tooltip("자기자신오브젝트넣음")]
    public TrapGenerator Trap = null;

    [Tooltip("자기자신오브젝트넣음")]
    public FlyingGenerator Flying = null;

    [System.NonSerialized]
    public List<GeneratorBase> generators = new List<GeneratorBase>();

    private void OnEnable()
    {
    }

    public virtual void Awake()
    {
        ListAddSetting();
        ChangeState(StateIndex.CHASE);
    }

    public void ListAddSetting()
    {
        generators.Add(Chase);
        generators.Add(Attack);
        generators.Add(Trap);
        generators.Add(Flying);
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

        StateBase = StateOnOff(nextState);

        StateBase.Initiate();
    }

    public GeneratorBase StateOnOff(StateIndex NextState)
    {
        GeneratorBase generator = null;

        switch (NextState)
        {
            case StateIndex.CHASE:
                generator = State_activation(generator, Chase);
                State_Inactive(Chase);
                Agent.avoidancePriority = 50;
                break;

            case StateIndex.ATTACK:
                generator = State_activation(generator, Attack);
                State_Inactive(Attack);
                Agent.avoidancePriority = 51;
                break;

            case StateIndex.TRAP:
                generator = State_activation(generator, Trap);
                State_Inactive(Trap);
                break;

            case StateIndex.Gallery:
                generator = State_activation(generator, Chase);
                State_Inactive(Chase);
                break;

            case StateIndex.Flaying:
                generator = State_activation(generator, Flying);
                State_Inactive(Flying);
                break;
        }
        return generator;
    }

    protected GeneratorBase State_activation(GeneratorBase generator, GeneratorBase Base)
    {
        generator = Base;
        generator.enabled = true;

        return generator;
    }

    protected void State_Inactive(GeneratorBase Base)
    {
        var list = generators.FindAll(x => x != Base);

        foreach (var item in list)
        {
            item.enabled = false;
        }
    }
}