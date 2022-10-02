
using UnityEngine;

namespace Planets
{
    [CreateAssetMenu(fileName = "PlanetsConfigsSO", menuName = "ScriptableObjects/PlanetsConfigsSO")]
    public class PlanetsConfigsSO : ScriptableObject
    {
        [SerializeField] private PlanetMassConfig[] _planetsData;



        public PlanetMassConfig GetPlanetData(float mass)
        {
            PlanetMassConfig planetData =  _planetsData[0];
            for (int i =_planetsData.Length-1; i >= 0; i--)
            {
                var planetConf = _planetsData[i];
                if (planetConf.GetMinMass()<=mass)
                {
                    planetData = planetConf;
                    break;
                }
            }
            return planetData;

        }
        public float GetMinPlanetMass()
        {
            var result = float.MaxValue;
            for (int i =0; i< _planetsData.Length; i ++)
            {
                var unitMinMass = _planetsData[i].GetMinMass();
                if (unitMinMass < result)
                {
                    result = unitMinMass;
                }
            }
            return result;
        }

        public float GetMaxPlanetMass()
        {
            var result = 0f;
            for (int i =0; i< _planetsData.Length; i ++)
            {
                var unitMaxMass = _planetsData[i].GetMaxMass();
                if (unitMaxMass > result)
                {
                    result = unitMaxMass;
                }
            }
            return result;
        }
        
        
        
    }
}