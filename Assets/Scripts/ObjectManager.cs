using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance { get; private set; }

    [SerializeField]
    private List<ObjectData> objects;
    private int currentIndex = 0;
    public GameObject currentLoadedObject;

    public ObjectData currentLoadedObjectData;

    [HideInInspector]
    public string objectName;

    [HideInInspector]
    public string nextObjectName;

    public ClippingPlane clippingPlane;

    public UIManager UIManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadObject(0);
    }

    // public void SwitchObject(int index)
    // {
    //     currentIndex = index;
    //     LoadObject(currentIndex);
    // }

    public void SwitchToNextObject()
    {
        currentIndex = (currentIndex + 1) % objects.Count;
        LoadObject(currentIndex);
    }

    private void LoadObject(int index)
    {
        if (index < 0 || index >= objects.Count)
        {
            Debug.LogError("Object index is out of range.");
            return;
        }

        if (currentLoadedObject != null)
        {
            Destroy(currentLoadedObject);
        }

        currentIndex = index;

        currentLoadedObjectData = objects[currentIndex];
        currentLoadedObject = Instantiate(currentLoadedObjectData.modelPrefab);

        currentLoadedObjectData.Abilities = new List<IAbility>();

        if (currentLoadedObject.GetComponents<IAbility>() != null)
        {
            foreach (var ability in currentLoadedObject.GetComponents<IAbility>())
            {
                if (!currentLoadedObjectData.Abilities.Contains(ability))
                {
                    currentLoadedObjectData.Abilities.Add(ability);
                }
            }
        }

        objectName = currentLoadedObjectData.objectName;
        nextObjectName = objects[(currentIndex + 1) % objects.Count].objectName;

        clippingPlane.mat = objects[currentIndex].modelPrefab.GetComponent<Renderer>().sharedMaterial;
        clippingPlane.transform.DOMoveX(5, 0.2f);

        UIManager.RefreshObjectInfo();
    }

    public void ActivateAbility(int abilityIndex)
    {
        if (currentIndex < 0 || currentIndex >= objects.Count)
        {
            Debug.LogError("Current index is out of range.");
            return;
        }

        ObjectData currentObject = objects[currentIndex];
        if (abilityIndex < 0 || abilityIndex >= currentObject.Abilities.Count)
        {
            Debug.LogError("Ability index is out of range.");
            return;
        }

        IAbility ability = currentObject.Abilities[abilityIndex];
        if (ability == null)
        {
            Debug.LogError("Ability is null.");
            return;
        }

        foreach (var parentAbility in currentLoadedObject.GetComponents<IAbility>())
        {
            if (parentAbility.Name.Equals(ability.Name))
            {
                parentAbility.ActivateAbility();
            }
        }
    }
}
