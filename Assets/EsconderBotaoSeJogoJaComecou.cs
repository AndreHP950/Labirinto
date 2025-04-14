using UnityEngine;

public class EsconderBotaoSeJogoJaComecou : MonoBehaviour
{
    void Start()
    {
        if (Interface.Instance != null && Interface.Instance.JogoJaComecou())
        {
            gameObject.SetActive(false);
        }
    }
}
