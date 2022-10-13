using ReferenceSharing;
using UnityEngine;

public class BuildingSelector : MonoBehaviour
{
    [SerializeField] private Reference<double> consumptionRef;
    [SerializeField] private Reference<int> peopleRef;
    [SerializeField] private Reference<bool> consumingRef;
    [SerializeField] private GameObject panel;
    private Camera _mainCam;
    private Building _currentBuilding;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100.0f))
            {
                SelectBuilding(hit.transform.GetComponent<Building>());
            }
        }
    }

    private void SelectBuilding(Building building)
    {
        if (!building) return;
        if (_currentBuilding)
        {
            _currentBuilding.GetComponent<Outline>().enabled = false;
            _currentBuilding.OnPeopleUpdate -= UpdatePeople;
        }

        panel.SetActive(true);
        _currentBuilding = building;
        _currentBuilding.GetComponent<Outline>().enabled = true;
        _currentBuilding.OnPeopleUpdate += UpdatePeople;
        consumptionRef.Value = building.getConsumption();
        peopleRef.Value = building.getPeopleCount();
        consumingRef.Value = building.getIsConsumming();
    }

    public void ToggleLight(bool on)
    {
        if (!_currentBuilding) return;
        _currentBuilding.setIsConsumming(on);
        consumingRef.Value = on;
    }

    private void UpdatePeople(int ppl)
    {
        peopleRef.Value = ppl;
    }

    private void OnDisable()
    {
        _currentBuilding.OnPeopleUpdate -= UpdatePeople;
    }
}