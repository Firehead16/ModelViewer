using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text currentObjectName;

    [SerializeField]
    private TMP_Text description;

    [SerializeField]
    private Button nextObject;

    [SerializeField]
    private Button abilityButtonPrefab;

    [SerializeField]
    private Transform abilitiesParent;

    [HideInInspector]
    //[SerializeField]
    private List<Button> abilityButtons = new List<Button>();

    private void Start()
    {
        nextObject.onClick.AddListener(SetNextObject);
    }

    private void SetNextObject()
    {
        ObjectManager.Instance.SwitchToNextObject();
    }

    public void SetDescription(string textOfDescription)
    {
        description.text = textOfDescription;
    }

    public string GetDescription()
    {
        return description.text;
    }

    public void RefreshObjectInfo()
    {
        if (abilityButtons.Count != 0)
        {
            for (int i = 0; i < abilityButtons.Count; i++)
            {
                Destroy(abilityButtons[i].gameObject);
            }
        }

        abilityButtons = new List<Button>();

        for (int i = 0; i < ObjectManager.Instance.currentLoadedObjectData.Abilities.Count; i++)
        {
            IAbility ability = ObjectManager.Instance.currentLoadedObjectData.Abilities[i];
            Button abilityButton = Instantiate(abilityButtonPrefab, parent: abilitiesParent);
            abilityButtons.Add(abilityButton);
            abilityButton.GetComponentInChildren<TMP_Text>().text = ability.Name;
            abilityButton.onClick.AddListener(ObjectManager.Instance.currentLoadedObjectData.Abilities[i].ActivateAbility);
        }
        currentObjectName.text = ObjectManager.Instance.objectName;
        nextObject.GetComponentInChildren<TMP_Text>().text = ObjectManager.Instance.nextObjectName;
    }

    private void OnDestroy()
    {
        nextObject.onClick.RemoveAllListeners();
    }
}
