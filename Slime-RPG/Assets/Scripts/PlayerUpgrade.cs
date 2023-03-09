using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    [SerializeField] private int _attackIncrease = 1;
    [SerializeField] private int _upgrateAttackCost = 10;
    [SerializeField] private int _upgrateHPCost = 10;

    public void EnhanceAttack()
    {
        if (Player.instance != null)
        {
            if (PlayerPrefs.GetInt("Money") >= _upgrateAttackCost)
            {
                Player.instance.EnhanceDamage(_attackIncrease);
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - _upgrateAttackCost);
            }
        }
    }

    public void EnhanceHP()
    {
        if (Player.instance != null)
        {
            if (PlayerPrefs.GetInt("Money") >= _upgrateHPCost)
            {
                Player.instance.EnhanceHP(_attackIncrease);
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - _upgrateHPCost);
            }
        }
    }
}
