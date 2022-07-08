using Assets.Scripts.Utils;
using UnityEngine;

public class HPUtil
{
    public float TotalHPWidth { private set; get; }
    public float CurrentHPBarWidth { private set; get; }
    public GameObject HPBarCurrentObject { private set; get; }
    public float MaxHP { set; get; }
    public float CurrentHP { private set; get; }

    public HPUtil(GameObject hpBar, float maxHP)
    {
        HPBarCurrentObject = hpBar;

        TotalHPWidth = hpBar.transform.localScale.x;
        
        MaxHP = maxHP;

        CurrentHP = maxHP;
    }

    public void DamageCalculation(int dmgAmount)
    {
        if (HPBarCurrentObject == null) return;
        if (CurrentHP == 0) return;

        CurrentHP -= dmgAmount;
        CurrentHPBarWidth = TotalHPWidth * (CurrentHP / MaxHP);
        UpdateHPBar();
    }

    public void UpdateHPBar()
    {
        if (HPBarCurrentObject == null) return;

        var pos = HPBarCurrentObject.transform.localPosition;

        var scale = HPBarCurrentObject.transform.localScale;

        HPBarCurrentObject.transform.localScale = new Vector2(CurrentHPBarWidth, scale.y);
        HPBarCurrentObject.transform.localPosition = new Vector2((CurrentHPBarWidth - TotalHPWidth) / 2, pos.y);
    }
}
