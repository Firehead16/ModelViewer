using DG.Tweening;
using UnityEngine;

public class CutHalf : MonoBehaviour, IAbility
{
    private string abilityName = "Разрез";

    string IAbility.Name => abilityName;

    public void ActivateAbility()
    {
        ObjectManager.Instance.clippingPlane.transform.DOLocalMoveX(0, 0.2f, true);
    }
}
