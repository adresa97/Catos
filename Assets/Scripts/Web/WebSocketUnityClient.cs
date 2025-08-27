using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using WebSocketSharp;

public class WebSocketUnityClient : MonoBehaviour
{
    [SerializeField]
    private GameEvents catEvents;

    private WebSocket ws;

    private Queue<System.Action> TODO = new Queue<System.Action>();

    void Start()
    {
        ws = new WebSocket("wss://websocket-unity-railway-production.up.railway.app");

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("✅ Conectado al WebSocket de Railway");
        };

        ws.OnMessage += (sender, e) =>
        {
            try
            {
                Debug.Log("📨 Mensaje recibido desde la web: " + e.Data);
                string message = e.Data;
                ProcessData(message);
            } 
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        };

        ws.OnError += (sender, e) =>
        {
            Debug.LogError("❌ Error en WebSocket: " + e.Message);
        };

        ws.ConnectAsync();
    }

    private void Update()
    {
        lock(TODO)
        {
            while(TODO.Count > 0)
            {
                TODO.Dequeue()();
            }
        }
    }

    void OnDestroy()
    {
        if (ws != null && ws.IsAlive)
            ws.Close();
    }

    private bool ProcessData(string data)
    {
        if (data.Length < 1)
        {
            Debug.LogWarning("⚠️ Código vacío");
            return false;
        }

        int.TryParse(data[0].ToString(), out int msgType);
        if (msgType == 1)
        {
            Debug.Log("Código (" + data + ") recibido corresponde a un pescado");
            lock (TODO)
            {
                TODO.Enqueue(() =>
                {
                    catEvents.Emit(new SendFoodEvent());
                });
            }
            return true;
        }

        if (data.Length < 5)
        {
            Debug.LogWarning("⚠️ Código inválido: " + data);
            return false;
        }

        int.TryParse(data[1].ToString(), out int catIndex);
        int.TryParse(data[2].ToString(), out int hatIndex);
        int.TryParse(data[3].ToString(), out int shirtIndex);
        int.TryParse(data[4].ToString(), out int shoeIndex);

        if (catIndex < 0 || hatIndex < 0 || shirtIndex < 0 || shoeIndex < 0)
        {
            Debug.LogWarning("⚠️ Los valores no deben ser negativos");
            return false;
        }

        Debug.Log("Código (" + data + ") recibido corresponde a un gato");
        lock (TODO)
        {
            TODO.Enqueue(() =>
            {
                catEvents.Emit(new SendCatEvent(catIndex, hatIndex, shirtIndex, shoeIndex));
            });
        }
        return true;
    }
}


