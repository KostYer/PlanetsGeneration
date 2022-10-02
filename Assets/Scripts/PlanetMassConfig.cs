using UnityEngine;

namespace Planets
{
    [System.Serializable]
    public class PlanetMassConfig
    {
        public PlanetType PlanetType;
        public Vector2 PlanetMass;
        public Vector2 PlanetRadius;



        public float GetMinMass()
        {
            return PlanetMass.x;
        }
        public float GetMaxMass()
        {
            return PlanetMass.y;
        }

        public float CalcRadius(float mass)
        {
            if (!IsMassValueValid(mass))
            {
                Debug.LogWarning("incorrect mass value");
                return 0f;
            }

            var massT = Mathf.InverseLerp(PlanetMass.x, PlanetMass.y, mass);
            var radius = Mathf.Lerp(PlanetRadius.x, PlanetRadius.y, massT);
            return radius;
        }

        private bool IsMassValueValid(float mass)
        {
            if (mass >PlanetMass.y )
            {
                return false;
            }

            if(mass <PlanetMass.x )
            {
                return false;
            }

            return true;
        }
    }
}