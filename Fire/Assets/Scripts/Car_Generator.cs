using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Car_Generator : MonoBehaviour
{
    private CarController controller;

    private GameObject Passenger;

    public CameraFallow cameraFallow;

    public NavMeshAgent agent;

    public float carMaxSpeed = 50.0f;
    public float carMinSpeed = 3.5f;

    public ParticleSystem explosion = null;

    public int explosion_Timer = 1;

    private BoxCollider Box;

    public bool PlayerIn = false;

    private void Awake()
    {
        Box = GetComponent<BoxCollider>();
        controller = GetComponent<CarController>();
        controller.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Utility.Instance.CarKey > 0)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Utility.Instance.CarKey--;
                PlayerIn = true;
                Passenger = other.gameObject;
                Passenger.transform.SetParent(transform);
                Passenger.SetActive(false);
                Passenger.transform.position = transform.position;

                controller.enabled = true;

                cameraFallow.target = transform;

                Box.center = new Vector3(Box.center.x, Box.center.y, 3.0f);
                Box.size = new Vector3(Box.size.x, Box.size.y, 4.0f);
                StartCoroutine(EndCar());
            }
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster") && PlayerIn)
        {
            var enemyAgent = other.gameObject.GetComponent<MonsterState>();

            enemyAgent.ChangeState(StateIndex.Flaying);
        }
    }

    private IEnumerator EndCar()
    {
        yield return new WaitForSeconds(10);
        PlayerIn = false;

        Passenger.transform.SetParent(null);
        Passenger.SetActive(true);

        Passenger.transform.rotation = transform.rotation;
        Passenger.transform.position = transform.position - transform.right * 5.0f;

        controller.enabled = false;

        cameraFallow.target = Passenger.transform;

        Box.center = new Vector3(Box.center.x, Box.center.y, -0.6f);
        Box.size = new Vector3(Box.size.x, Box.size.y, 11.0f);

        //차 폭발
        StartCoroutine(Car_Explosion());
        yield return null;
    }

    private IEnumerator Car_Explosion()
    {
        yield return new WaitForSeconds(explosion_Timer);

        explosion.Play();

        yield return new WaitForSeconds(5.0f);
        gameObject.SetActive(false);

        //사운드
        //카메라흔들림

        yield return null;
    }
}