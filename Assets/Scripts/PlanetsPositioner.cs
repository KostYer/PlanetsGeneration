using System.Collections.Generic;
using UnityEngine;

namespace Planets
{
    public class PlanetsPositioner : MonoBehaviour
    {
     
        [SerializeField] private Vector3 _center = Vector3.zero;
        [SerializeField] private float _centerOffset = 10f;
        [SerializeField]  private float _planetsOffset = 15f;
        
        private List<IPlaneteryObject> _planets;

        public void Init(List<IPlaneteryObject> planets)
        {
            _planets = planets;
            PositionPlanets();
        }

        private void PositionPlanets()
        {
            var totalDistFromCenter = _center.x + _centerOffset;
            for (int i = 0; i < _planets.Count; i++)
            {
                var planet =(PlanetObject) _planets[i];
                var distanceStep = _planetsOffset + planet.Radius *2f;

                totalDistFromCenter += distanceStep;
                PlaceWithRandomAngle(planet.transform,  totalDistFromCenter, _center );

            }
        }


        private void PlaceWithRandomAngle(Transform planet, float distance, Vector3 center)
        {


            var angle = Random.Range(0f, 360f);
            var angRad = angle * Mathf.Deg2Rad;
            var z = center.z;
            var y =center.y + distance * Mathf.Cos(angRad);
            var x =center.x+ distance * Mathf.Sin(angRad);

            planet.position = new Vector3(x, y, z);


                

               
        }
    }
}