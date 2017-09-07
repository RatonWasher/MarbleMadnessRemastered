using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections;


namespace UnityStandardAssets.Network
{
    public class LobbyManager : NetworkLobbyManager 
    {
        static short MsgKicked = MsgType.Highest + 1;

        static public LobbyManager s_Singleton;


        [Tooltip("Time in second between all players ready & match start")]
        public float prematchCountdown = 5.0f;

        [Space]
        [Header("UI Reference")]
        public LobbyTopPanel topPanel;

        public RectTransform mainMenuPanel;
        public RectTransform lobbyPanel;

        public LobbyInfoPanel infoPanel;
        public LobbyCountdownPanel countdownPanel;
        public GameObject addPlayerButton;
        public GameObject worldSelectPanel;
        public bool selectPanelIsActive = false;
        public GameObject displayedImage;

        public Sprite lvl1_1;
        public Sprite lvl1_2;
        public Sprite lvl1_3;
        public Sprite lvl2_1;
        public Sprite lvl2_2;
        public Sprite lvl2_3;
        public Sprite lvl2_4;
        public Sprite lvl2_5;
        private Image screenshot;


        protected RectTransform currentPanel;

        public Button backButton;
        public Button levelSelectButton;

        public Text statusInfo;
        public Text hostInfo;

        //Client numPlayers from NetworkManager is always 0, so we count (throught connect/destroy in LobbyPlayer) the number
        //of players, so that even client know how many player there is.
        [HideInInspector]
        public int _playerNumber = 0;

        //used to disconnect a client properly when exiting the matchmaker
        [HideInInspector]
        public bool _isMatchmaking = false;

        protected bool _disconnectServer = false;
        
        protected ulong _currentMatchID;

        void Start()
        {
            s_Singleton = this;
            currentPanel = mainMenuPanel;

            backButton.gameObject.SetActive(false);
            levelSelectButton.gameObject.SetActive(false);
            GetComponent<Canvas>().enabled = true;

            screenshot = displayedImage.GetComponent<Image>();
            lvl2_1 = Resources.Load("ScreensLevels/Level2-1", typeof(Sprite)) as Sprite;
            lvl2_2 = Resources.Load("ScreensLevels/Level2-2", typeof(Sprite)) as Sprite;
            lvl2_3 = Resources.Load("ScreensLevels/Level2-3", typeof(Sprite)) as Sprite;
            lvl2_4 = Resources.Load("ScreensLevels/Level2-4", typeof(Sprite)) as Sprite;
            lvl2_5 = Resources.Load("ScreensLevels/Level2-5", typeof(Sprite)) as Sprite;
            DontDestroyOnLoad(gameObject);

            SetServerInfo("Offline", "None");
        }

        public override void OnLobbyClientSceneChanged(NetworkConnection conn)
        {
            if (SceneManager.GetSceneAt(0).name == lobbyScene)
            {
                if (topPanel.isInGame)
                {
                    ChangeTo(lobbyPanel);
                    /*if (_isMatchmaking)
                    {
                        if (conn.playerControllers[0].unetView.isServer)
                        {
                            backDelegate = StopHostClbk;
                        }
                        else
                        {
                            backDelegate = StopClientClbk;
                        }
                    }
                    else
                    {*/
                        if (conn.playerControllers[0].unetView.isClient)
                        {
                            backDelegate = StopHostClbk;
                        }
                        else
                        {
                            backDelegate = StopClientClbk;
                        }
                    //}
                }
                else
                {
                    ChangeTo(mainMenuPanel);
                }

                topPanel.ToggleVisibility(true);
                topPanel.isInGame = false;
            }
            else
            {
                ChangeTo(null);

                Destroy(GameObject.Find("MainMenuUI(Clone)"));

                //backDelegate = StopGameClbk;
                topPanel.isInGame = true;
                topPanel.ToggleVisibility(false);
            }
        }

        public void ChangeTo(RectTransform newPanel)
        {
            if (currentPanel != null)
            {
                currentPanel.gameObject.SetActive(false);
            }

            if (newPanel != null)
            {
                newPanel.gameObject.SetActive(true);
            }

            currentPanel = newPanel;

            if (currentPanel != mainMenuPanel)
            {
                backButton.gameObject.SetActive(true);
                levelSelectButton.gameObject.SetActive(true);
            }
            else
            {
                backButton.gameObject.SetActive(false);
                levelSelectButton.gameObject.SetActive(false);
                SetServerInfo("Offline", "None");
                _isMatchmaking = false;
            }
        }

        public void DisplayIsConnecting()
        {
            var _this = this;
            infoPanel.Display("Connecting...", "Cancel", () => { _this.backDelegate(); });
        }

        public void SetServerInfo(string status, string host)
        {
            statusInfo.text = status;
            hostInfo.text = host;
        }

        public void levelArrowClick()
        {
            
            selectPanelIsActive = !selectPanelIsActive;
            worldSelectPanel.SetActive(selectPanelIsActive);
        }


        public delegate void BackButtonDelegate();
        public BackButtonDelegate backDelegate;
        public void GoBackButton()
        {
            backDelegate();
        }

        // ----------------- Server management

        public void AddLocalPlayer()
        {
            TryToAddPlayer();
        }

        public void RemovePlayer(LobbyPlayer player)
        {
            player.RemovePlayer();
        }

        public void SimpleBackClbk()
        {
            ChangeTo(mainMenuPanel);
        }
                 
        public void StopHostClbk()
        {
            if (_isMatchmaking)
            {
                this.matchMaker.DestroyMatch((NetworkID)_currentMatchID, OnMatchDestroyed);
                _disconnectServer = true;
            }
            else
            {
                StopHost();
            }

            
            ChangeTo(mainMenuPanel);
        }

        public void StopClientClbk()
        {
            StopClient();

            if (_isMatchmaking)
            {
                StopMatchMaker();
            }

            ChangeTo(mainMenuPanel);
        }

        public void StopServerClbk()
        {
            StopServer();
            ChangeTo(mainMenuPanel);
        }

        class KickMsg : MessageBase { }
        public void KickPlayer(NetworkConnection conn)
        {
            conn.Send(MsgKicked, new KickMsg());
        }

        public void KickedMessageHandler(NetworkMessage netMsg)
        {
            infoPanel.Display("Kicked by Server", "Close", null);
            netMsg.conn.Disconnect();
        }

        //===================

        public override void OnStartHost()
        {
            base.OnStartHost();
            worldSelectPanel.SetActive(false);
            ChangeTo(lobbyPanel);
            backDelegate = StopHostClbk;
            SetServerInfo("Hosting", networkAddress);
            
        }

        public override void OnMatchCreate(UnityEngine.Networking.Match.CreateMatchResponse matchInfo)
        {
            base.OnMatchCreate(matchInfo);

            _currentMatchID = (System.UInt64)matchInfo.networkId;
        }

        public void OnMatchDestroyed(BasicResponse resp)
        {
            if (_disconnectServer)
            {
                StopMatchMaker();
                StopHost();
            }
        }

        //allow to handle the (+) button to add/remove player
        public void OnPlayersNumberModified(int count)
        {
            _playerNumber += count;
            int localPlayerCount = 0;
            foreach (PlayerController p in ClientScene.localPlayers)
            {
                localPlayerCount += (p == null || p.playerControllerId == -1) ? 0 : 1;
            }
                

            addPlayerButton.SetActive(localPlayerCount < maxPlayersPerConnection && _playerNumber < maxPlayers);
        }

        // ----------------- Server callbacks ------------------

        //we want to disable the button JOIN if we don't have enough player
        //But OnLobbyClientConnect isn't called on hosting player. So we override the lobbyPlayer creation
        public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
        {
            GameObject obj = Instantiate(lobbyPlayerPrefab.gameObject) as GameObject;

            LobbyPlayer newPlayer = obj.GetComponent<LobbyPlayer>();
            newPlayer.ToggleJoinButton(numPlayers + 1 >= minPlayers);

            minPlayers = 1;
            for (int i = 0; i < lobbySlots.Length; ++i)
            {
                LobbyPlayer p = lobbySlots[i] as LobbyPlayer;
                

                if (p != null)
                {
                    if (p.playerControllerId != -1)
                    {
                        minPlayers += 1;
                    } 
                    p.RpcUpdateRemoveButton();
                    p.ToggleJoinButton(numPlayers + 1 >= minPlayers);
                }
            }

            return obj;
        }

        public override void OnLobbyServerPlayerRemoved(NetworkConnection conn, short playerControllerId)
        {
            minPlayers = 1;
            for (int i = 0; i < lobbySlots.Length; ++i)
            {
                LobbyPlayer p = lobbySlots[i] as LobbyPlayer;

                if (p != null)
                {
                    if (p.playerControllerId != -1)
                    {
                        minPlayers += 1;
                    } 
                    p.RpcUpdateRemoveButton();
                    p.ToggleJoinButton(numPlayers + 1 >= minPlayers);
                }
            }
        }

        public override void OnLobbyServerDisconnect(NetworkConnection conn)
        {
            minPlayers = 1;
            for (int i = 0; i < lobbySlots.Length; ++i)
            {
                LobbyPlayer p = lobbySlots[i] as LobbyPlayer;

                if (p != null)
                {
                    if (p.playerControllerId != -1)
                    {
                        minPlayers += 1;
                    } 
                    p.RpcUpdateRemoveButton();
                    p.ToggleJoinButton(numPlayers >= minPlayers);
                }
            }

        }

        public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
        {
            //This hook allows you to apply state data from the lobby-player to the game-player
            //just subclass "LobbyHook" and add it to the lobby object.

            return true;
        }

        // --- Countdown management

        public override void OnLobbyServerPlayersReady()
        {
            
            worldSelectPanel.SetActive(false);
            StartCoroutine(ServerCountdownCoroutine());
        }

        public IEnumerator ServerCountdownCoroutine()
        {
            float remainingTime = prematchCountdown;
            int floorTime = Mathf.FloorToInt(remainingTime);

            while (remainingTime > 0)
            {
                yield return null;

                remainingTime -= Time.deltaTime;
                int newFloorTime = Mathf.FloorToInt(remainingTime);

                if (newFloorTime != floorTime)
                {//to avoid flooding the network of message, we only send a notice to client when the number of plain seconds change.
                    floorTime = newFloorTime;

                    for (int i = 0; i < lobbySlots.Length; ++i)
                    {
                        if (lobbySlots[i] != null)
                        {//there is maxPlayer slots, so some could be == null, need to test it before accessing!
                            (lobbySlots[i] as LobbyPlayer).RpcUpdateCountdown(floorTime);
                        }
                    }
                }
            }

            for (int i = 0; i < lobbySlots.Length; ++i)
            {
                if (lobbySlots[i] != null)
                {
                    (lobbySlots[i] as LobbyPlayer).RpcUpdateCountdown(0);
                }
            }

            ServerChangeScene(playScene);
        }

        // ----------------- Client callbacks ------------------

        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);

            infoPanel.gameObject.SetActive(false);

            conn.RegisterHandler(MsgKicked, KickedMessageHandler);

            if (!NetworkServer.active)
            {//only to do on pure client (not self hosting client)
                ChangeTo(lobbyPanel);
                backDelegate = StopClientClbk;
                SetServerInfo("Client", networkAddress);
            }
        }


        public override void OnClientDisconnect(NetworkConnection conn)
        {
            minPlayers -= 1;
            base.OnClientDisconnect(conn);
            ChangeTo(mainMenuPanel);
        }

        public override void OnClientError(NetworkConnection conn, int errorCode)
        {
            ChangeTo(mainMenuPanel);
            infoPanel.Display("Cient error : " + (errorCode == 6 ? "timeout" : errorCode.ToString()), "Close", null);
        }

        public void backToMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
            Destroy(gameObject);
        }

        public void lvlClicked(int level)
        {
            
            screenshot.color = new Vector4(1, 1, 1, 1);
            /*foreach (Transform child in screenshot.transform)
            {
                child.gameObject.SetActive(true);
            }*/
            switch (level)
            {
                case (11):
                    screenshot.sprite = lvl1_1;
                    playScene = "1-1";
                    break;
                case (12):
                    screenshot.sprite = lvl1_2;
                    playScene = "1-2";
                    break;
                case (13):
                    screenshot.sprite = lvl1_3;
                    playScene = "1-3";
                    break;
                case (21):
                    screenshot.sprite = lvl2_1;
                    playScene = "2-1";
                    break;
                case (22):
                    screenshot.sprite = lvl2_2;
                    playScene = "2-2";
                    break;
                case (23):
                    screenshot.sprite = lvl2_3;
                    playScene = "2-3";
                    break;
                case (24):
                    screenshot.sprite = lvl2_4;
                    playScene = "2-4";
                    break;
                case (25):
                    screenshot.sprite = lvl2_5;
                    playScene = "2-5";
                    break;

            }
        }
       
    }
}
