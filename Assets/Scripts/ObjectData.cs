using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewModel", menuName = "Models")]
public class ObjectData : ScriptableObject
{
    public string objectName;
    public GameObject modelPrefab;
    public List<IAbility> Abilities;
}
