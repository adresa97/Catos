using UnityEngine;

public class GatosManager : MonoBehaviour
{
    public Gato[] gatos;

    public void processData(string data)
    {
        if (data.Length == 3 &&
            int.TryParse(data[0].ToString(), out int gatoIndex) &&
            int.TryParse(data[1].ToString(), out int sombreroIndex) &&
            int.TryParse(data[2].ToString(), out int camisaIndex))
        {
            Debug.Log($"🎯 Cambiar gato {gatoIndex}: Sombrero {sombreroIndex}, Camisa {camisaIndex}");

            if (gatoIndex >= 0 && gatoIndex < gatos.Length && gatos[gatoIndex] != null)
            {
                gatos[gatoIndex].ActualizarRopa(sombreroIndex, camisaIndex);
            }
            else
            {
                Debug.LogWarning("⚠️ Índice de gato fuera de rango o gato no asignado.");
            }
        }
        else
        {
            Debug.LogWarning("⚠️ Código no válido recibido: " + data);
        }
    }

}
