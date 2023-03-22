using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class NetworkController : MonoBehaviourPunCallbacks
{
    bool isConnected = false;
    bool isConnectedToRoom = false;

    float countTime = 0;

    [SerializeField] private TMP_InputField inputFieldRoom;
    [SerializeField] private TextMeshProUGUI textErrors;
    [SerializeField] private GameObject menuToHide;

    [SerializeField] private GameObject menuToShow;
    [SerializeField] private TextMeshProUGUI textInfos;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Update()
    {
        if (isConnected)
        {
            countTime += Time.deltaTime;
            if (countTime > 5)
            {
                Debug.Log("Ping: " + PhotonNetwork.GetPing());
                countTime = 0;

                if (isConnectedToRoom)
                {
                    textInfos.text = "Room ID: " + PhotonNetwork.CurrentRoom.Name + "\nServer: " + PhotonNetwork.CloudRegion + "\nPing: " + PhotonNetwork.GetPing();


                }
            }
        }
    }

    public override void OnConnected()
    {
        Debug.Log("Conectado ao servidor");
        isConnected = true;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado ao Master");
        Debug.Log("Servidor: " + PhotonNetwork.CloudRegion);
        Debug.Log("Ping: " + PhotonNetwork.GetPing());

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Conectado ao Lobby");
        base.OnJoinedLobby();
    }

    public void HostRoom()
    {
        string roomid = Random.Range(1000, 10000) + ":" + Random.Range(1000, 10000);
        PhotonNetwork.CreateRoom(roomid);
    }

    public void JoinRoom()
    {
        string roomid = inputFieldRoom.text;
        PhotonNetwork.JoinRoom(roomid);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Falha ao conectar-se com a Sala");
        Debug.Log(message);
        textErrors.text = "Não foi possível se conectar com essa Sala.\n" + message;
        textErrors.gameObject.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Conectado à sala: " + PhotonNetwork.CurrentRoom.Name);
        menuToHide.SetActive(false);
        menuToShow.SetActive(true);

        isConnectedToRoom = true;
    }

    

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Desconectado.");
        Debug.Log(cause);

        isConnected = false;
        isConnectedToRoom = false;
    }
}
