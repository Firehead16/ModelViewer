using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Unbind : MonoBehaviour, IAbility
{
    [SerializeField]
    private List<Transform> parts = new List<Transform>();

    private List<Vector3> startPos = new List<Vector3>();

    public bool isUnbinded = false;

    [SerializeField]
    private float multiplier = 2f;

    private string abilityName = "Разобрать";

    string IAbility.Name => abilityName;

    public void ActivateAbility()
    {
        if (isUnbinded)
        {
            for (var i = 0; i < parts.Count; i = i + 1)
            {
                parts[i].transform.DOLocalMove(startPos[i], 1f);
            }
            abilityName = "Разобрать";
        }
        else
        {
            foreach (var part in parts)
            {
                part.transform.DOLocalMove(part.transform.position * multiplier, 1f);
            }
            abilityName = "Собрать";
        }

        ObjectManager.Instance.UIManager.RefreshObjectInfo();

        isUnbinded = !isUnbinded;
    }

    private void Start()
    {
        foreach (var part in parts)
        {
            startPos.Add(part.transform.localPosition);
        }
    }
}
