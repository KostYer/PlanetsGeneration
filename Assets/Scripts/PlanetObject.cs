
 using UnityEngine;

 namespace Planets
{
    public interface IPlaneteryObject
    {
        PlanetType PlanetType { get; set; }
        float Mass { get; set; }
        float Radius { get; set; }
    }


    public class PlanetObject : MonoBehaviour, IPlaneteryObject
    {
        [field: SerializeField] public PlanetType PlanetType { get; set; }
        [field: SerializeField] public float Mass { get; set; }
        [field: SerializeField] public float Radius { get; set; }

        private Vector2 _speedMinMax = new Vector2(20f, 7f);
        private Vector2 _massMinMax = new Vector2(0f, 70f);
        private Vector2 _speedRandomizer = new Vector2(-5,5);
        [field: SerializeField]  public float OrbitingSpeed { get; set; }



        public PlanetObject InitPlanet(PlanetType type, float mass, float radius)
        {
            PlanetType = type;
            Mass = mass;
            Radius = radius;
            ResizeSphere(Radius);
            OrbitingSpeed= CalcOrbitingSpeed(Mass);
            GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            return this;
        }


        private void ResizeSphere(float radius)
        {
            var diameter = radius * 2f;
            transform.localScale = new Vector3(diameter, diameter, diameter);
        }

        public float CalcOrbitingSpeed(float mass)
        {
            mass = Mathf.Clamp(mass, _massMinMax.x, _massMinMax.y);
            var massT = Mathf.InverseLerp(_massMinMax.x, _massMinMax.y, mass);
            var speed = Mathf.Lerp(_speedMinMax.x, _speedMinMax.y, massT);
            var rnd = Random.Range(_speedRandomizer.x, _speedRandomizer.y);
            return speed+rnd;
        }

    }


    public enum PlanetType
    {
        Asteroidan,
        Mercurian,
        Subterran,
        Terran,
        Superterran,
        Neptunian,
        Jovian
    }
}
