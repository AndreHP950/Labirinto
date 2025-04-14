using UnityEngine;

public class CoinControll : MonoBehaviour
{
    public int points = 10; // Pontos que esta moeda vale
    private Interface gameInterface; // Referência ao script Interface

    private void Start()
    {
        // Encontra o script Interface na cena
        gameInterface = Object.FindFirstObjectByType<Interface>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Adiciona os pontos e remove a moeda
            if (gameInterface != null)
            {
                gameInterface.AddPoints(points);
            }
            Destroy(gameObject);
        }
    }
}
