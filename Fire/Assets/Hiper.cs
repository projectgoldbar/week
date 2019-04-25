using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiper
{
    public class Hiper : MonoBehaviour
    {
        public TrailRenderer Trail;

        public Move m;

        private void Awake()
        {
            Trail = GetComponent<TrailRenderer>();
            m = Utility.Instance.playerTr.GetComponent<Move>();
        }

        private void Update()
        {
            if (m.agent.speed <= 30)
            {
                if (Trail.time >= 0)
                {
                    float HiperTrailTIme = m.agent.speed / 30;
                    Trail.time -= HiperTrailTIme * 0.1f;
                }
                else
                {
                    Trail.time = 0;
                }
            }
            else if (m.agent.speed > 30)
            {
                float HiperTrailTIme = m.agent.speed / 30;
                Trail.time += HiperTrailTIme * 0.1f;
            }
        }
    }
}