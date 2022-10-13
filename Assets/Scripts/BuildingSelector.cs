using ReferenceSharing.Variables;
using UnityEngine;

public class BuildingSelector : MonoBehaviour
{
    [SerializeField] private Variable<double> consumptionRef;
    [SerializeField] private Variable<int> peopleRef;
    [SerializeField] private Variable<bool> consumingRef;
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
        if (_currentBuilding)
            _currentBuilding.GetComponent<Outline>().enabled = false;

        panel.SetActive(true);
        _currentBuilding = building;
        _currentBuilding.GetComponent<Outline>().enabled = true;
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
}