using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private Transform _begin;
    [SerializeField] private Transform _end;

    public Transform End => _end;
    public Transform Begin => _begin;
}
