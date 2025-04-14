using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb;
    public float forceBase = 1000f; // força inicial
    public float velocidadePorPonto = 10f; // quanto cada ponto aumenta a força

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float pontos = Interface.Instance != null ? Interface.Instance.GetPontos() : 0;
        float forceAmount = forceBase + (pontos * velocidadePorPonto);

        Vector3 direcao = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            direcao += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            direcao += Vector3.back;
        if (Input.GetKey(KeyCode.D))
            direcao += Vector3.right;
        if (Input.GetKey(KeyCode.A))
            direcao += Vector3.left;

        rb.AddForce(direcao.normalized * forceAmount * Time.deltaTime);
    }
}
