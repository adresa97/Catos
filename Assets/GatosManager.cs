using Unity.VisualScripting;
using UnityEngine;

public class GatosManager : MonoBehaviour
{
    [SerializeField]
    private GameEvents catEvents;

    public Gato[] gatos;

    public void OnEnable()
    {
        catEvents.AddListener(CatEventsCallback);
    }

    public void OnDisable()
    {
        catEvents.RemoveListener(CatEventsCallback);
    }

    private void CatEventsCallback(object data)
    {
        if(data is SendCatEvent)
        {
            ChangeCat((data as SendCatEvent).catIndex, (data as SendCatEvent).hatIndex, (data as SendCatEvent).shirtIndex);
        }
    }

    private bool ChangeCat(int catIndex, int hatIndex, int shirtIndex)
    {
        if (catIndex >= gatos.Length)
        {
            Debug.LogWarning("⚠️ Índice de gato fuera de rango");
            return false;
        }

        if (gatos[catIndex] == null)
        {
            Debug.LogWarning("⚠️ Gato no asignado");
            return false;
        }

        gatos[catIndex].ActualizarRopa(hatIndex, shirtIndex);
        return true;
    }
}
