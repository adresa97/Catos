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

        int.TryParse(data[0].ToString(), out int catIndex);
        if (catIndex < 0)
        {
            lock(TODO)
            {
                TODO.Enqueue(() =>
                {
                    catEvents.Emit(new SendFoodEvent());
                });
            }
            return true;
        }

        if (data.Length < 3)
        {
            Debug.LogWarning("⚠️ Código inválido: " + data);
            return false;
        }

        int.TryParse(data[1].ToString(), out int hatIndex);
        int.TryParse(data[2].ToString(), out int shirtIndex);

        if (hatIndex < 0 || shirtIndex < 0)
        {
            Debug.LogWarning("⚠️ Los valores de ropa no deben ser negativos");
            return false;
        }

        lock(TODO)
        {
            TODO.Enqueue(() =>
            {
                catEvents.Emit(new SendCatEvent(catIndex, hatIndex, shirtIndex));
            });
        }
        return true;
    }
}


