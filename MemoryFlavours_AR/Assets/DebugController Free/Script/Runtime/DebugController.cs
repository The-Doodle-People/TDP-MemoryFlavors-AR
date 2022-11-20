using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Profiling;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HT.DEBUG
{
    [AddComponentMenu("Debug Controller")]
    public sealed class DebugController : MonoBehaviour
    {
        #region Static Property
        private static DebugController _instance;
        public static DebugController Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        #region Public Field
        /// <summary>
        /// 是否开启调试器
        /// </summary>
        public bool AllowDebugging = true;
        #endregion

        #region Private Field
        private Localization _localization;
        private DebugType _debugType = DebugType.Console;
        //FPS
        private int _fps = 0;
        private Color _fpsColor = Color.white;
        private float _lastShowFPSTime = 0f;
        private bool _expansion = false;
        private Rect _windowRect = new Rect(0, 0, 100, 60);
        //Console
        private List<LogData> _logInformations = new List<LogData>();
        private int _currentLogIndex = -1;
        private int _infoLogCount = 0;
        private int _warningLogCount = 0;
        private int _errorLogCount = 0;
        private int _fatalLogCount = 0;
        private bool _showInfoLog = true;
        private bool _showWarningLog = true;
        private bool _showErrorLog = true;
        private bool _showFatalLog = true;
        private Vector2 _scrollLogView = Vector2.zero;
        private Vector2 _scrollCurrentLogView = Vector2.zero;
        //Scene
        private List<Transform> _sceneTransforms = new List<Transform>();
        private int _currentTransformIndex = -1;
        private List<Component> _transformComponents = new List<Component>();
        private int _currentComponentIndex = -1;
        private Dictionary<Type, Type> _debugComponents = new Dictionary<Type, Type>();
        private string _transformFiltrate = "";
        private List<Type> _addComponents = new List<Type>();
        private string _componentFiltrate = "";
        private bool _isAddComponent = false;
        private bool _isShowDebugCamera = false;
        private Camera _debugCamera;
        private Transform _debugCameraTarget;
        private float _debugCameraAngelX = 90.0f;
        private float _debugCameraAngelY = 30.0f;
        private float _debugCameraDistance = 5;
        private Vector2 _scrollSceneView = Vector2.zero;
        private Vector2 _scrollInspectorView = Vector2.zero;
        //DrawCall
        private Vector2 _scrollDrawCallView = Vector2.zero;
        //System
        private Vector2 _scrollSystemView = Vector2.zero;
        #endregion

        #region Lifecycle Function
        private void Awake()
        {
            _instance = this;
        }
        private void Start()
        {
            Application.logMessageReceived += LogCallback;

            //获取本地化配置文件
            _localization = Resources.Load<Localization>("LocalizationData");
            if (_localization == null)
            {
                AllowDebugging = false;
                Debug.LogError("丢失本地化配置文件！");
            }

            //创建调试摄像机
            GameObject camera = new GameObject("DebugCamera");
            camera.transform.SetParent(transform);
            camera.hideFlags = HideFlags.HideInHierarchy;
            _debugCamera = camera.AddComponent<Camera>();
            _debugCamera.nearClipPlane = 0.01f;
            _debugCamera.enabled = _isShowDebugCamera;
        }
        private void Update()
        {
            if (AllowDebugging)
            {
                FPSUpdate();
                DebugCameraUpdate();
            }
        }
        private void OnGUI()
        {
            if (AllowDebugging)
            {
                if (_expansion)
                {
                    _windowRect = GUI.Window(0, _windowRect, ExpansionGUIWindow, _localization.LanguageValue(0));
                }
                else
                {
                    _windowRect = GUI.Window(0, _windowRect, ShrinkGUIWindow, _localization.LanguageValue(0));
                }
            }
        }
        private void OnDestory()
        {
            Application.logMessageReceived -= LogCallback;
        }
        #endregion

        #region Additional Function
        /// <summary>
        /// 调试摄像机刷新
        /// </summary>
        private void DebugCameraUpdate()
        {
            _debugCamera.enabled = _isShowDebugCamera;
            if (_isShowDebugCamera)
            {
                if (_debugCameraTarget)
                {
                    if (Input.GetMouseButton(1) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved))
                    {
                        _debugCameraAngelX += Input.GetAxis("Mouse X") * 3;
                        _debugCameraAngelY -= Input.GetAxis("Mouse Y") * 3;
                        if (_debugCameraAngelY < -360)
                            _debugCameraAngelY += 360;
                        else if (_debugCameraAngelY > 360)
                            _debugCameraAngelY -= 360;
                        _debugCameraAngelY = Mathf.Clamp(_debugCameraAngelY, -85, 85);
                    }

                    if (Input.GetAxis("Mouse ScrollWheel") != 0)
                    {
                        _debugCameraDistance -= Input.GetAxis("Mouse ScrollWheel");
                    }

                    Quaternion rotation = Quaternion.Euler(_debugCameraAngelY, _debugCameraAngelX, 0.0f);
                    Vector3 disVector = new Vector3(0.0f, 0.0f, -_debugCameraDistance);
                    Vector3 position = _debugCameraTarget.position + rotation * disVector;

                    _debugCamera.transform.rotation = Quaternion.Lerp(_debugCamera.transform.rotation, rotation, Time.deltaTime * 5);
                    _debugCamera.transform.position = Vector3.Lerp(_debugCamera.transform.position, position, Time.deltaTime * 5);

                    _debugCamera.transform.LookAt(_debugCameraTarget);
                }
            }
        }
        /// <summary>
        /// 调试摄像机深度置顶
        /// </summary>
        private void DebugCameraDepthStick()
        {
            Camera[] cameras = FindObjectsOfType<Camera>();
            foreach (Camera camera in cameras)
            {
                if (camera != _debugCamera)
                {
                    if (_debugCamera.depth <= camera.depth)
                    {
                        _debugCamera.depth = camera.depth + 1;
                    }
                }
            }
        }
        /// <summary>
        /// 刷新FPS
        /// </summary>
        private void FPSUpdate()
        {
            float time = Time.realtimeSinceStartup - _lastShowFPSTime;
            if (time >= 1)
            {
                _fps = (int)(1.0f / Time.deltaTime);
                _lastShowFPSTime = Time.realtimeSinceStartup;
            }
        }
        /// <summary>
        /// 日志回调
        /// </summary>
        private void LogCallback(string condition, string stackTrace, LogType type)
        {
            LogData log = new LogData();
            log.time = DateTime.Now.ToString("HH:mm:ss");
            log.message = condition;
            log.stackTrace = stackTrace;
            if (type == LogType.Assert)
            {
                log.type = "Fatal";
                _fatalLogCount += 1;
            }
            else if (type == LogType.Exception || type == LogType.Error)
            {
                log.type = "Error";
                _errorLogCount += 1;
            }
            else if (type == LogType.Warning)
            {
                log.type = "Warning";
                _warningLogCount += 1;
            }
            else if (type == LogType.Log)
            {
                log.type = "Info";
                _infoLogCount += 1;
            }
            log.showName = "[" + log.type + "] [" + log.time + "] " + log.message;
            _logInformations.Add(log);

            if (_warningLogCount > 0)
            {
                _fpsColor = Color.yellow;
            }
            if (_errorLogCount > 0)
            {
                _fpsColor = Color.red;
            }
        }
        /// <summary>
        /// 屏幕截图
        /// </summary>
        private IEnumerator ScreenShot()
        {
            string path = "";
#if UNITY_ANDROID
            path = "/mnt/sdcard/DCIM/ScreenShot/";
#endif

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            path = Application.dataPath + "/ScreenShot/";
#endif

            if (path != "")
            {
                AllowDebugging = false;
                yield return new WaitForEndOfFrame();

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
                texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
                texture.Apply();
                string name = "ScreenShotImage_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".png";
                byte[] bytes = texture.EncodeToPNG();
                File.WriteAllBytes(path + name, bytes);
                AllowDebugging = true;
            }
            else
            {
                Debug.LogWarning("当前平台不支持截屏！");
                yield return 0;
            }
        }
        /// <summary>
        /// 刷新场景中所有物体
        /// </summary>
        private void RefreshSceneTransforms()
        {
            _sceneTransforms.Clear();
            _sceneTransforms = FindObjectsOfType<Transform>().ToList();
            _currentTransformIndex = -1;
            _debugCameraTarget = null;
        }
        /// <summary>
        /// 刷新当前选择物体的所有组件
        /// </summary>
        private void RefreshTransformComponents()
        {
            _transformComponents.Clear();
            if (_currentTransformIndex != -1 && _currentTransformIndex < _sceneTransforms.Count)
            {
                _transformComponents = _sceneTransforms[_currentTransformIndex].GetComponents<Component>().ToList();
            }
            _currentComponentIndex = -1;
            _isAddComponent = false;
        }
        #endregion

        #region GUI Function
        /// <summary>
        /// 展开的GUI窗口
        /// </summary>
        private void ExpansionGUIWindow(int windowId)
        {
            GUI.DragWindow(new Rect(0, 0, 10000, 20));

            ExpansionTitleGUI();

            switch (_debugType)
            {
                case DebugType.Console:
                    ExpansionConsoleGUI();
                    break;
                case DebugType.Memory:
                    ExpansionMemoryGUI();
                    break;
                case DebugType.DrawCall:
                    ExpansionDrawCallGUI();
                    break;
                case DebugType.System:
                    ExpansionSystemGUI();
                    break;
                case DebugType.Screen:
                    ExpansionScreenGUI();
                    break;
                case DebugType.Quality:
                    ExpansionQualityGUI();
                    break;
                case DebugType.Time:
                    ExpansionTimeGUI();
                    break;
                case DebugType.Environment:
                    ExpansionEnvironmentGUI();
                    break;
                default:
                    break;
            }
        }
        private void ExpansionTitleGUI()
        {
            GUILayout.BeginHorizontal();
            GUI.contentColor = _fpsColor;
            if (GUILayout.Button(_localization.LanguageValue(1) + ": " + _fps, GUILayout.Height(30)))
            {
                _expansion = false;
                _windowRect.width = 100;
                _windowRect.height = 60;
            }
            GUI.contentColor = (_debugType == DebugType.Console ? Color.white : Color.gray);
            if (GUILayout.Button(_localization.LanguageValue(2), GUILayout.Height(30)))
            {
                _debugType = DebugType.Console;
            }
            GUI.contentColor = (_debugType == DebugType.Memory ? Color.white : Color.gray);
            if (GUILayout.Button(_localization.LanguageValue(4), GUILayout.Height(30)))
            {
                _debugType = DebugType.Memory;
            }
            GUI.contentColor = (_debugType == DebugType.DrawCall ? Color.white : Color.gray);
            if (GUILayout.Button(_localization.LanguageValue(5), GUILayout.Height(30)))
            {
                _debugType = DebugType.DrawCall;
            }
            GUI.contentColor = (_debugType == DebugType.System ? Color.white : Color.gray);
            if (GUILayout.Button(_localization.LanguageValue(6), GUILayout.Height(30)))
            {
                _debugType = DebugType.System;
            }
            GUI.contentColor = (_debugType == DebugType.Screen ? Color.white : Color.gray);
            if (GUILayout.Button(_localization.LanguageValue(7), GUILayout.Height(30)))
            {
                _debugType = DebugType.Screen;
            }
            GUI.contentColor = (_debugType == DebugType.Quality ? Color.white : Color.gray);
            if (GUILayout.Button(_localization.LanguageValue(8), GUILayout.Height(30)))
            {
                _debugType = DebugType.Quality;
            }
            GUI.contentColor = (_debugType == DebugType.Time ? Color.white : Color.gray);
            if (GUILayout.Button(_localization.LanguageValue(9), GUILayout.Height(30)))
            {
                _debugType = DebugType.Time;
            }
            GUI.contentColor = (_debugType == DebugType.Environment ? Color.white : Color.gray);
            if (GUILayout.Button(_localization.LanguageValue(10), GUILayout.Height(30)))
            {
                _debugType = DebugType.Environment;
            }
            GUI.contentColor = Color.white;
            GUILayout.EndHorizontal();
        }
        private void ExpansionConsoleGUI()
        {
            GUILayout.BeginHorizontal();
            GUI.contentColor = Color.white;
            if (GUILayout.Button(_localization.LanguageValue(11), GUILayout.Width(80)))
            {
                _logInformations.Clear();
                _fatalLogCount = 0;
                _warningLogCount = 0;
                _errorLogCount = 0;
                _infoLogCount = 0;
                _currentLogIndex = -1;
                _fpsColor = Color.white;
            }
            GUI.contentColor = (_showInfoLog ? Color.white : Color.gray);
            _showInfoLog = GUILayout.Toggle(_showInfoLog, _localization.LanguageValue(12) + " [" + _infoLogCount + "]");
            GUI.contentColor = (_showWarningLog ? Color.white : Color.gray);
            _showWarningLog = GUILayout.Toggle(_showWarningLog, _localization.LanguageValue(13) + " [" + _warningLogCount + "]");
            GUI.contentColor = (_showErrorLog ? Color.white : Color.gray);
            _showErrorLog = GUILayout.Toggle(_showErrorLog, _localization.LanguageValue(14) + " [" + _errorLogCount + "]");
            GUI.contentColor = (_showFatalLog ? Color.white : Color.gray);
            _showFatalLog = GUILayout.Toggle(_showFatalLog, _localization.LanguageValue(15) + " [" + _fatalLogCount + "]");
            GUI.contentColor = Color.white;
            GUILayout.EndHorizontal();

            _scrollLogView = GUILayout.BeginScrollView(_scrollLogView, "Box", GUILayout.Height(165));
            for (int i = 0; i < _logInformations.Count; i++)
            {
                bool show = false;
                Color color = Color.white;
                switch (_logInformations[i].type)
                {
                    case "Fatal":
                        show = _showFatalLog;
                        color = Color.red;
                        break;
                    case "Error":
                        show = _showErrorLog;
                        color = Color.red;
                        break;
                    case "Info":
                        show = _showInfoLog;
                        color = Color.white;
                        break;
                    case "Warning":
                        show = _showWarningLog;
                        color = Color.yellow;
                        break;
                    default:
                        break;
                }

                if (show)
                {
                    GUILayout.BeginHorizontal();
                    GUI.contentColor = color;
                    if (GUILayout.Toggle(_currentLogIndex == i, _logInformations[i].showName))
                    {
                        _currentLogIndex = i;
                    }
                    GUILayout.FlexibleSpace();
                    GUI.contentColor = Color.white;
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndScrollView();

            _scrollCurrentLogView = GUILayout.BeginScrollView(_scrollCurrentLogView, "Box", GUILayout.Height(100));
            if (_currentLogIndex != -1)
            {
                GUILayout.Label(_logInformations[_currentLogIndex].message + "\r\n\r\n" + _logInformations[_currentLogIndex].stackTrace);
            }
            GUILayout.EndScrollView();
        }
        private void ExpansionMemoryGUI()
        {
            GUILayout.BeginHorizontal();
            GUI.contentColor = Color.white;
            GUILayout.Label(_localization.LanguageValue(23));
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical("Box", GUILayout.Height(250));
            GUILayout.Label(_localization.LanguageValue(24) + ": " + Profiler.GetTotalReservedMemoryLong() / 1000000 + "MB");
            GUILayout.Label(_localization.LanguageValue(25) + ": " + Profiler.GetTotalAllocatedMemoryLong() / 1000000 + "MB");
            GUILayout.Label(_localization.LanguageValue(26) + ": " + Profiler.GetTotalUnusedReservedMemoryLong() / 1000000 + "MB");
            GUILayout.Label(_localization.LanguageValue(27) + ": " + Profiler.GetMonoHeapSizeLong() / 1000000 + "MB");
            GUILayout.Label(_localization.LanguageValue(28) + ": " + Profiler.GetMonoUsedSizeLong() / 1000000 + "MB");
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(_localization.LanguageValue(29)))
            {
                Resources.UnloadUnusedAssets();
            }
            if (GUILayout.Button(_localization.LanguageValue(30)))
            {
                GC.Collect();
            }
            GUILayout.EndHorizontal();
        }
        private void ExpansionDrawCallGUI()
        {
            GUILayout.BeginHorizontal();
            GUI.contentColor = Color.white;
            GUILayout.Label(_localization.LanguageValue(31));
            GUILayout.EndHorizontal();

            _scrollDrawCallView = GUILayout.BeginScrollView(_scrollDrawCallView, "Box");
#if UNITY_EDITOR
            GUILayout.Label(_localization.LanguageValue(32) + ": " + UnityEditor.UnityStats.drawCalls);
            GUILayout.Label(_localization.LanguageValue(33) + ": " + UnityEditor.UnityStats.batches);
            GUILayout.Label(_localization.LanguageValue(34) + ": " + UnityEditor.UnityStats.staticBatchedDrawCalls);
            GUILayout.Label(_localization.LanguageValue(35) + ": " + UnityEditor.UnityStats.staticBatches);
            GUILayout.Label(_localization.LanguageValue(36) + ": " + UnityEditor.UnityStats.dynamicBatchedDrawCalls);
            GUILayout.Label(_localization.LanguageValue(37) + ": " + UnityEditor.UnityStats.dynamicBatches);
            if (UnityEditor.UnityStats.triangles > 10000)
            {
                GUILayout.Label(_localization.LanguageValue(38) + ": " + UnityEditor.UnityStats.triangles / 10000 + "W");
            }
            else
            {
                GUILayout.Label(_localization.LanguageValue(38) + ": " + UnityEditor.UnityStats.triangles);
            }
            if (UnityEditor.UnityStats.vertices > 10000)
            {
                GUILayout.Label(_localization.LanguageValue(39) + ": " + UnityEditor.UnityStats.vertices / 10000 + "W");
            }
            else
            {
                GUILayout.Label(_localization.LanguageValue(39) + ": " + UnityEditor.UnityStats.vertices);
            }
#else
        GUILayout.Label("只有在编辑器模式下才能显示DrawCall！");
#endif
            GUILayout.EndScrollView();
        }
        private void ExpansionSystemGUI()
        {
            GUILayout.BeginHorizontal();
            GUI.contentColor = Color.white;
            GUILayout.Label(_localization.LanguageValue(40));
            GUILayout.EndHorizontal();

            _scrollSystemView = GUILayout.BeginScrollView(_scrollSystemView, "Box");
            GUILayout.Label(_localization.LanguageValue(41) + ": " + SystemInfo.operatingSystem);
            GUILayout.Label(_localization.LanguageValue(42) + ": " + SystemInfo.systemMemorySize + "MB");
            GUILayout.Label(_localization.LanguageValue(43) + ": " + SystemInfo.processorType);
            GUILayout.Label(_localization.LanguageValue(44) + ": " + SystemInfo.processorCount);
            GUILayout.Label(_localization.LanguageValue(45) + ": " + SystemInfo.graphicsDeviceName);
            GUILayout.Label(_localization.LanguageValue(46) + ": " + SystemInfo.graphicsDeviceType);
            GUILayout.Label(_localization.LanguageValue(47) + ": " + SystemInfo.graphicsMemorySize + "MB");
            GUILayout.Label(_localization.LanguageValue(48) + ": " + SystemInfo.graphicsDeviceID);
            GUILayout.Label(_localization.LanguageValue(49) + ": " + SystemInfo.graphicsDeviceVendor);
            GUILayout.Label(_localization.LanguageValue(50) + ": " + SystemInfo.graphicsDeviceVendorID);
            GUILayout.Label(_localization.LanguageValue(51) + ": " + SystemInfo.deviceModel);
            GUILayout.Label(_localization.LanguageValue(52) + ": " + SystemInfo.deviceName);
            GUILayout.Label(_localization.LanguageValue(53) + ": " + SystemInfo.deviceType);
            GUILayout.Label(_localization.LanguageValue(54) + ": " + SystemInfo.deviceUniqueIdentifier);
            GUILayout.EndScrollView();
        }
        private void ExpansionScreenGUI()
        {
            GUILayout.BeginHorizontal();
            GUI.contentColor = Color.white;
            GUILayout.Label(_localization.LanguageValue(55));
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical("Box", GUILayout.Height(250));
            GUILayout.Label(_localization.LanguageValue(56) + ": " + Screen.dpi);
            GUILayout.Label(_localization.LanguageValue(57) + ": " + Screen.width + " x " + Screen.height);
            GUILayout.Label(_localization.LanguageValue(58) + ": " + Screen.currentResolution.ToString());
            GUILayout.Label(_localization.LanguageValue(59) + ": " + (Screen.sleepTimeout == SleepTimeout.NeverSleep ? _localization.LanguageValue(60) : _localization.LanguageValue(61)));
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(_localization.LanguageValue(59)))
            {
                if (Screen.sleepTimeout == SleepTimeout.NeverSleep)
                {
                    Screen.sleepTimeout = SleepTimeout.SystemSetting;
                }
                else
                {
                    Screen.sleepTimeout = SleepTimeout.NeverSleep;
                }
            }
            if (GUILayout.Button(_localization.LanguageValue(62)))
            {
                StartCoroutine(ScreenShot());
            }
            if (GUILayout.Button(_localization.LanguageValue(63)))
            {
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, !Screen.fullScreen);
            }
            GUILayout.EndHorizontal();
        }
        private void ExpansionQualityGUI()
        {
            GUILayout.BeginHorizontal();
            GUI.contentColor = Color.white;
            GUILayout.Label(_localization.LanguageValue(64));
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical("Box", GUILayout.Height(250));
            string value = "";
            if (QualitySettings.GetQualityLevel() == 0)
            {
                value = " [最低]";
            }
            else if (QualitySettings.GetQualityLevel() == QualitySettings.names.Length - 1)
            {
                value = " [最高]";
            }

            GUILayout.Label(_localization.LanguageValue(65) + ": " + QualitySettings.names[QualitySettings.GetQualityLevel()] + value);
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(_localization.LanguageValue(66)))
            {
                QualitySettings.DecreaseLevel();
            }
            if (GUILayout.Button(_localization.LanguageValue(67)))
            {
                QualitySettings.IncreaseLevel();
            }
            GUILayout.EndHorizontal();
        }
        private void ExpansionTimeGUI()
        {
            GUILayout.BeginHorizontal();
            GUI.contentColor = Color.white;
            GUILayout.Label(_localization.LanguageValue(68));
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical("Box", GUILayout.Height(250));
            GUILayout.Label(_localization.LanguageValue(69) + ": " + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
            GUILayout.Label(_localization.LanguageValue(70) + ": " + (int)Time.realtimeSinceStartup);
            GUILayout.Label(_localization.LanguageValue(71) + ": " + Time.timeScale);
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("0.1 " + _localization.LanguageValue(72)))
            {
                Time.timeScale = 0.1f;
            }
            if (GUILayout.Button("0.2 " + _localization.LanguageValue(72)))
            {
                Time.timeScale = 0.2f;
            }
            if (GUILayout.Button("0.5 " + _localization.LanguageValue(72)))
            {
                Time.timeScale = 0.5f;
            }
            if (GUILayout.Button("1 " + _localization.LanguageValue(72)))
            {
                Time.timeScale = 1;
            }
            if (GUILayout.Button("2 " + _localization.LanguageValue(72)))
            {
                Time.timeScale = 2;
            }
            if (GUILayout.Button("5 " + _localization.LanguageValue(72)))
            {
                Time.timeScale = 5;
            }
            if (GUILayout.Button("10 " + _localization.LanguageValue(72)))
            {
                Time.timeScale = 10;
            }
            GUILayout.EndHorizontal();
        }
        private void ExpansionEnvironmentGUI()
        {
            GUILayout.BeginHorizontal();
            GUI.contentColor = Color.white;
            GUILayout.Label(_localization.LanguageValue(73));
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical("Box", GUILayout.Height(250));
            GUILayout.Label(_localization.LanguageValue(74) + ": " + Application.productName);
            GUILayout.Label(_localization.LanguageValue(75) + ": " + Application.identifier);
            GUILayout.Label(_localization.LanguageValue(76) + ": " + Application.version);
            GUILayout.Label(_localization.LanguageValue(77) + ": " + Application.dataPath);
            GUILayout.Label(_localization.LanguageValue(78) + ": " + Application.companyName);
            GUILayout.Label(_localization.LanguageValue(79) + ": " + Application.unityVersion);
            GUILayout.Label(_localization.LanguageValue(80) + ": " + Application.HasProLicense());
            string internetState = _localization.LanguageValue(82);
            if (Application.internetReachability == NetworkReachability.NotReachable)
                internetState = _localization.LanguageValue(82);
            else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
                internetState = _localization.LanguageValue(83);
            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
                internetState = _localization.LanguageValue(84);
            GUILayout.Label(_localization.LanguageValue(81) + ": " + internetState);
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(_localization.LanguageValue(85)))
            {
                Application.Quit();
            }
            GUILayout.EndHorizontal();
        }
        /// <summary>
        /// 收缩的GUI窗口
        /// </summary>
        private void ShrinkGUIWindow(int windowId)
        {
            GUI.DragWindow(new Rect(0, 0, 10000, 20));

            GUI.contentColor = _fpsColor;
            if (GUILayout.Button(_localization.LanguageValue(1) + ": " + _fps, GUILayout.Width(80), GUILayout.Height(30)))
            {
                _expansion = true;
                _windowRect.width = 700;
                _windowRect.height = 360;
            }
            GUI.contentColor = Color.white;
        }
        #endregion
    }
    #region Additional Type
    public struct LogData
    {
        public string time;
        public string type;
        public string message;
        public string stackTrace;
        public string showName;
    }
    public enum DebugType
    {
        Console,
        Memory,
        DrawCall,
        System,
        Screen,
        Quality,
        Time,
        Environment
    }
    #endregion
}