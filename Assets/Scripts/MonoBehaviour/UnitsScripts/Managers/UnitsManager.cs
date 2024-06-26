using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UnitsManager : MonoBehaviour, IInteractible
{
    [SerializeField] private Button UnitsSlot;
    [SerializeField] private Button UnitsBack;

    private CanvasGroup Units;
    public GameObject UnitsSimulation;
    public static UnitsManager instance;

    void Awake() 
    {
        Units = GameObject.FindGameObjectWithTag("Units").GetComponent<CanvasGroup>();
        instance = this;
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        UnitsSlot.onClick.AddListener(() =>
        {
            SoundManager.PlaySound(SoundName.ButtonPressUI);
            OpenUnits();
        });

        UnitsBack.onClick.AddListener(() =>
        {
            SoundManager.PlaySound(SoundName.ButtonPressUI, 0.6f);
            CloseUnits();
        });
    }

	public List<IShowable> GetUnitsType()
	{
		List<IShowable> unitList = new List<IShowable>();
		GameObject[] unitPrefabs = Resources.LoadAll<GameObject>("Units Objects");

		foreach (GameObject unitPrefab in unitPrefabs)
		{
			if (unitPrefab != null)
			{
				IShowable showableUnit = unitPrefab.GetComponentInChildren<IShowable>();
				if (showableUnit != null)
				{
					unitList.Add(showableUnit);
				}
			}
		}
		return unitList;
	}

	public void SetButtonInteractive (bool isInteractive)
    {
        Color fadedColor = new Color(200/255f, 198/255f, 192/255f);

        if (GetUnitImage() != null) //UnitSlot isn't empty
        {
            GetUnitImage().GetComponent<Image>().color = !isInteractive ? fadedColor: Color.white;
            GetButtonFromUnitImage().interactable = isInteractive;
        }
        else //UnitSlot is empty
        {
            GetButtonFromUnitSlot().interactable = isInteractive;
        }
    }
    public bool UnitSlotHasChildren() => GetUnitSlot().transform.childCount > 1;
	public GameObject GetActiveUnit()
	{
		if (UnitSlotHasChildren())
		{
			GameObject unitImage = GetUnitImage();
			return Resources.Load<GameObject>("Units Objects/" + unitImage.name);
		}
		return null;
	}
	public GameObject GetUnitImage()
    {
        if (UnitSlotHasChildren())
        {
            return GetUnitSlot().transform.GetChild(1).gameObject;
        }
        return null; 
    }
    public Button GetButtonFromUnitImage()
    {
      if(UnitSlotHasChildren())
        {
            return GetUnitImage().GetComponent<Button>();
        } 
      return null; 
    }
    public GameObject GetUnitSlot() => GameObject.FindGameObjectWithTag("UnitSlot");
    public Button GetButtonFromUnitSlot() => GetUnitSlot().transform.GetChild(0).GetComponent<Button>();

    public void CloseUnits()
    {
        Units.enabled = true;
    }
    public void OpenUnits()
    {
        Units.enabled = false;
    }

    public void CloseUnitsSimulation() => UnitsSimulation.SetActive(false);
    public void OpenUnitsSimulation() => UnitsSimulation.SetActive(true);
}
