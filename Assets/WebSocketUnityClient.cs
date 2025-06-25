using UnityEngine;
using WebSocketSharp;

public class WebSocketUnityClient : MonoBehaviour
{
    private WebSocket ws;
    public GatosManager gatosManager;

    void Start()
    {
        ws = new WebSocket("wss://websocket-unity-railway-production.up.railway.app");

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("✅ Conectado al WebSocket de Railway");
        };

        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("📨 Mensaje recibido desde la web: " + e.Data);
            gatosManager.processData(e.Data);
        };

        ws.OnError += (sender, e) =>
        {
            Debug.LogError("❌ Error en WebSocket: " + e.Message);
        };

        ws.ConnectAsync();
    }

    void OnDestroy()
    {
        if (ws != null && ws.IsAlive)
            ws.Close();
    }
}


