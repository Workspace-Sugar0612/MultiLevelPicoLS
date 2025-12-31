using Mirror;
using Mirror.Discovery;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorLauncher : MonoBehaviour
{
    #region Private variables
    private NetworkManager _networkManager;

    private CameraController _cameraController;

    private Action _onLauncherEvent;

    private MyNetworkDiscovery _networkDiscovery;

    #endregion

    #region U3D Methods

    private void Start()
    {
        Initialize();
    }
    #endregion

    private void Initialize()
    {
        _networkDiscovery = FindObjectOfType<MyNetworkDiscovery>();
        _networkManager = NetworkManager.singleton;
        _cameraController = CameraController.Get();

        // 规定沙盒视角为Host，其它视角为Client
        bool isNotSandtable = _cameraController.CurrentCameraTag != CameraTag.SandTable;
        _onLauncherEvent += isNotSandtable ? StartClient : StartHost;
        _onLauncherEvent?.Invoke();
    }

    public void StartClient()
    {
        StartCoroutine(_networkDiscovery.IEStartDiscovery()); //开始查找主机
    }

    public void StartHost()    
    {
        _networkManager.StartHost();
        _networkDiscovery.AdvertiseServer();
    }
}
