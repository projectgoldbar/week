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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
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

                Passenger = other.gameObject;
                Passenger.transform.SetParent(transform);
                Passenger.SetActive(false);
                Passenger.transform.position = transform.position;

                controller.enabled = true;

                cameraFallow.target = transform;

                agent.speed = carMaxSpeed;
                agent.radius = 6;

                StartCoroutine(EndCar());
            }
        }
    }

    private IEnumerator EndCar()
    {
        yield return new WaitForSeconds(10);

        Passenger.transform.SetParent(null);
        Passenger.SetActive(true);

        Passenger.transform.rotation = transform.rotation;
        Passenger.transform.position = transform.position - transform.right * 5.0f;

        controller.enabled = false;

        cameraFallow.target = Passenger.transform;

        agent.speed = carMinSpeed;

        //agent.speed = 0;
        agent.velocity = Vector3.zero;

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