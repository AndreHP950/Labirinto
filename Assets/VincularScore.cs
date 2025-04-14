using UnityEngine;
using TMPro;

public class VincularScore : MonoBehaviour
{
    void Start()
    {
        if (Interface.Instance != null)
        {
            Interface.Instance.RegistrarTextoPontuacao(GetComponent<TextMeshProUGUI>());
        }
    }
}
