using UnityEngine;

[CreateAssetMenu(fileName = "new enemy", menuName = "Enemies", order = 51)]
public class Enemies : ScriptableObject
{
    [SerializeField] private string _label;
    [SerializeField] private int _hp;
    [SerializeField] private int _damage;
    [SerializeField] private int _attackSpeed;

    public int HP => _hp;
    public int Damage => _damage;
    public int AttackSpeed => _attackSpeed;
}
