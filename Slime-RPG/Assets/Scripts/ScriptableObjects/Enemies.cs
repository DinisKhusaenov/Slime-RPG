using UnityEngine;

[CreateAssetMenu(fileName = "new enemy", menuName = "Enemies", order = 51)]
public class Enemies : ScriptableObject
{
    [SerializeField] private string _label;
    [SerializeField] private int _hp;
    [SerializeField] private int _damage;
    [SerializeField] private GameObject _prefab;

}
