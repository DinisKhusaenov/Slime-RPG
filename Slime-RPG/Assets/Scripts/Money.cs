using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private TMP_Text _textMeshPro;

    private void Update()
    {
        _textMeshPro.text = PlayerPrefs.GetInt("Money").ToString();
    }
}
