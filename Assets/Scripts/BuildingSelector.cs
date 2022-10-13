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
    private string _lastSound = "";

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
            _currentBuilding.OnUpdate -= OnUpdate;
        }

        var isConsuming = building.getIsConsumming();
        var peopleCount = building.getPeopleCount();

        panel.SetActive(true);
        _currentBuilding = building;
        _currentBuilding.GetComponent<Outline>().enabled = true;
        _currentBuilding.OnUpdate += OnUpdate;
        consumptionRef.Value = building.getConsumption();
        peopleRef.Value = peopleCount;
        consumingRef.Value = isConsuming;
        OnUpdate(peopleCount, isConsuming);
    }

    public void ToggleLight(bool on)
    {
        if (!_currentBuilding) return;
        _currentBuilding.setIsConsumming(on);
        consumingRef.Value = on;
    }

    private void OnUpdate(int ppl, bool consuming)
    {
        peopleRef.Value = ppl;
        consumingRef.Value = consuming;

        var newSound = "";

        if (consuming)
        {
            newSound = ppl == 0 ? "empty" : _currentBuilding.name;
        }
        else if (ppl > 0)
        {
            newSound = "angry";
        }
        else if (_lastSound != "")
        {
            AudioManager.Instance.Stop(_lastSound);
            _lastSound = "";
            return;
        }

        if (newSound != _lastSound)
        {
            if (_lastSound != "")
                AudioManager.Instance.Stop(_lastSound);
            _lastSound = newSound;
            AudioManager.Instance.Play(_lastSound);
        }
    }

    private void OnDisable()
    {
        if (_currentBuilding)
            _currentBuilding.OnUpdate -= OnUpdate;
    }
}