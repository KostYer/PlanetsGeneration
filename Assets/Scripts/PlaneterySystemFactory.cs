using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Planets
{
    interface IPlaneterySystemFactory
    {
        IPlaneterySystem Create(float mass);
    }
    public class PlaneterySystemFactory: MonoBehaviour, IPlaneterySystemFactory
    {
        [SerializeField] private GameObject _planetPrefab;
        [SerializeField] private PlanetsConfigsSO _planetsConfigs;
        [SerializeField] private int _minPlanets = 2;
        [SerializeField] private int _maxPlanets = 20;
        [SerializeField] private float _totalMass = 140000.65f;
        [SerializeField] private PlanetsPositioner _planetsPositioner;
        private IPlaneterySystem _planeterySystem;

        private readonly string planetSystemName = "PlaneterySystem";
        private float _starScale = 16f;
        private Vector3 _starPos = Vector3.zero;


        [SerializeField] protected Button _genetateButton;
        private GameObject _centerStar;
        private bool _isFirstGeneration = true;
        private void Start()
        {
            _genetateButton.onClick.AddListener(GenetatePlanets);
        }


        private void GenetatePlanets()
        {
            TryDestoryPlanets();
            var planetSystem=  Create(_totalMass);
            CreateCenterStar();
            _planetsPositioner.Init(planetSystem.Planets.ToList());
            _isFirstGeneration = false;

        }
        
        
        private void TryDestoryPlanets()
        {
            if (_isFirstGeneration)
            {
                return;
            }

            var planets = _planeterySystem.Planets;
            foreach (var p in planets)
            {
              var  planet = (PlanetObject) p;
              planet.gameObject.SetActive(false);
              Destroy(planet);
            }

            _planeterySystem.Planets = null;
         

        }

        public IPlaneterySystem Create(float mass)
        {
            if (_isFirstGeneration)
            {
                _planeterySystem = new GameObject(planetSystemName).AddComponent<PlaneterySystem>();
            }


      
            var planets = GeneratePlanets();
            _planeterySystem.Planets = planets;
            return _planeterySystem;
        }
        
      
        private  List<IPlaneteryObject> GeneratePlanets()
        {
            List<IPlaneteryObject> planets = new List<IPlaneteryObject>();
            var planetsCount = GetPlanetsCount();
  
            var massLeft = _totalMass;
            for (int i = 0; i < planetsCount-1; i++)
            {
                
                var minAllowedMass = GetLoverMassBound(massLeft, planetsCount, i);
                var maxAllowedMass = GetUpperMassBound(massLeft);
                var mass = Random.Range(minAllowedMass, maxAllowedMass);
                var planet = GetPlanetObject(mass);
                planets.Add(planet);
                massLeft -= mass;

            }
             
            var lastPlanet = GetPlanetObject(massLeft);
            planets.Add(lastPlanet);


            return planets;
            
            
        }

        private float GetLoverMassBound(float massLeft, int planetsTotal,  int planetsLeft)
        {
            var result = 0f;
            var minPlanetMass = _planetsConfigs.GetMinPlanetMass();
            var maxPlanetMass = _planetsConfigs.GetMaxPlanetMass();
            if (massLeft<maxPlanetMass)
            {
                result = minPlanetMass;
            }
            else
            {
                result = massLeft/(planetsTotal-planetsLeft);
            }
            return result;
        }

        private float GetUpperMassBound(float totalMass)
        {
            var maxPlanetMass = _planetsConfigs.GetMaxPlanetMass();
            return Mathf.Min(maxPlanetMass, totalMass);
        }


        private int GetPlanetsCount()
        {
            var maxPlanetMass = _planetsConfigs.GetMaxPlanetMass();
            var countOfHaviestPlanets = (int)(_totalMass / maxPlanetMass);
            var minPlanetCount = _minPlanets;
            minPlanetCount = countOfHaviestPlanets > _minPlanets ? countOfHaviestPlanets : _minPlanets;
            
            var planetsCount = Random.Range(minPlanetCount, _maxPlanets);
            return planetsCount;
        }

        private PlanetObject GetPlanetObject(float mass)
        {
            mass =(float) Math.Round(mass,6);
            var planetData = _planetsConfigs.GetPlanetData(mass);
            var radius = planetData.CalcRadius(mass);
            var planetGo = Instantiate(_planetPrefab);
            var planet = planetGo.GetComponent<PlanetObject>().InitPlanet(planetData.PlanetType, mass, radius);
            planet.transform.parent = transform;
            return planet;
        }


        private void CreateCenterStar()
        {
            if (!_isFirstGeneration)
            {
                return; 
            }

            var planetGo = Instantiate(_planetPrefab, _starPos, Quaternion.identity);
            planetGo.transform.localScale = new Vector3(_starScale, _starScale, _starScale);
            planetGo.name = "CenterStar";
            _centerStar = planetGo;
        }

        private void OnDestroy()
        {
            _genetateButton.onClick.RemoveListener(GenetatePlanets);
        }
    }
}