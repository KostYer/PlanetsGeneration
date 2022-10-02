
using System.Collections.Generic;
using UnityEngine;

namespace Planets
{
    public interface IPlaneterySystem
    {
        IEnumerable<IPlaneteryObject> Planets { get; set; }
        void OnUpdate(float timeStep);
    }
    public class PlaneterySystem : MonoBehaviour, IPlaneterySystem
    {
        public IEnumerable<IPlaneteryObject> Planets { get; set; }
         


        private void Update()
        {
            if (Planets == null)
            {
                return;
            }

            OnUpdate(Time.deltaTime);
        }

        public void OnUpdate(float timeStep)
        {
            foreach (var  p in Planets)
            {
                var planet = (PlanetObject) p;
                planet.transform.RotateAround(Vector3.zero, Vector3.forward, 2* planet.OrbitingSpeed *timeStep);
            }
        }
    }
}
