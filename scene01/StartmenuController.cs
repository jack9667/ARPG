using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartmenuController : MonoBehaviour {

    public static StartmenuController Instance;

    public TweenScale startpanneTween;
    public TweenScale loginpannelTween;
    public TweenScale registerpannelTween;
    public TweenScale serverpannelTween;
    public TweenScale characterselectTween;
    public TweenScale charactershowTween;

    //服务器初始化
    private bool haveInitServerList = false;
    public UIGrid serverlistGrid;
    public GameObject serveritemGreen;
    public GameObject serveritemRed;
    //已经选择的服务器的引用
    public GameObject serverSelected;

    //login界面的输入
    public static string username;
    public static string password;
    public UIInput usernameLoingInput;
    public UIInput passwordLoingInput;
    //start界面用户名和服务器名字
    public UILabel usernameStart;
    public UILabel servernameStart;

    //获取register界面三个输入
    public UIInput usernameInputRegister;
    public UIInput passwordInputRegister;
    public UIInput repasswordInputRegister;

    //选择界面的游戏角色数组
    public GameObject[] CharacterArray;
    //已经选择的
    public GameObject[] CharacterSelectedArray;
    //选择界面的父类位置
    public Transform CharacterParentTransform;
    //当前选择的角色
    private GameObject characterSelected;
    public UIInput characternameInput;
    public UILabel nameLabelCha_sel;
    public UILabel lvLabelCha_sel;


    private LoginController loginController;
    private RegisterController registerController;

    void Awake()
    {
        Instance = this;
        loginController = this.GetComponent<LoginController>();
        registerController = this.GetComponent<RegisterController>();
    }

    void Start()
    {
        InitServerList();
    }

    //初始化服务器链表信息
    public void InitServerList()
    {
        if (haveInitServerList)
            return;
        //1.链接服务器，取得服务器列表信息
        //TODO
        //2.根据上面的信息，添加服务器列表

        for (int i = 1; i < 21; i++)
        {
            //        public string ip = "127.0.0.1:9080";
            //public string serverName = "战神杨凯1区";
            //private int count = 100;
            string ip = "127.0.0.1:9080";
            string serverName = "战神杨凯 " + i + "区";
            int count = Random.Range(0, 100);
            GameObject go = null;
            if (count > 50)
            {
                go = NGUITools.AddChild(serverlistGrid.gameObject, serveritemRed);

            }
            else
            {
                go = NGUITools.AddChild(serverlistGrid.gameObject, serveritemGreen);
            }

            ServerProperty sp = go.GetComponent<ServerProperty>();
            sp.ip = ip;
            sp.serverName = serverName;
            //sp.GetserverNameOnLabel(serverName);
            sp.count = count;
            serverlistGrid.AddChild(go.transform);//对go进行排序
        }
        haveInitServerList = true;
    }

    //接受选择服务器给的消息，也就是一个gameobject
    public void OnServerSelect(GameObject serverGo)
    {
        serverSelected.GetComponent<UISprite>().spriteName = serverGo.GetComponent<UISprite>().spriteName;
        serverSelected.transform.Find("Label").GetComponent<UILabel>().text = serverGo.GetComponent<ServerProperty>().serverName;
        
    }

    //已选择服务器点击事件
    public void OnServerSelectCloseClick()
    {
        //关闭服务器选择界面，回到开始界面
        serverpannelTween.PlayReverse();
        StartCoroutine(HidePanel(serverpannelTween.gameObject));
        startpanneTween.gameObject.SetActive(true);
        startpanneTween.PlayReverse();
        //更新服务器名称
        servernameStart.text = serverSelected.transform.Find("Label").GetComponent<UILabel>().text;
    }

    //开始start界面点击事件
    //开始界面用户名点击事件
    public void OnStartUIUsernameClick()
    {
        //输入账号进行登陆
        //隐藏当前面板，显示登陆面板
        startpanneTween.PlayForward();
        //在startpannel播放完后将其设置为false
        StartCoroutine(HidePanel(startpanneTween.gameObject));
        loginpannelTween.gameObject.SetActive(true);
        loginpannelTween.PlayForward();
    }
    //开始界面服务器点击事件
    public void OnStartUIServerClick()
    {
        //选择服务器
        startpanneTween.PlayForward();
        StartCoroutine(HidePanel(startpanneTween.gameObject));
        serverpannelTween.gameObject.SetActive(true);
        serverpannelTween.PlayForward();
        //初始化服务器列表
        //InitServerList();

    }

    //开始界面进入游戏点击事件
    public void OnStartUIEnterGameClick()
    {
        //1.选择服务器，验证游戏账号
        //TODO
        loginController.Login(username, password);

        ////2.进入游戏选择界面
        //startpanneTween.PlayForward();
        //StartCoroutine(HidePanel(startpanneTween.gameObject));
        //characterselectTween.gameObject.SetActive(true);
        //characterselectTween.PlayForward();


    }
    //进入游戏选择界面的
    public void LoginInGameSelect()
    {
        startpanneTween.PlayForward();
        StartCoroutine(HidePanel(startpanneTween.gameObject));
        characterselectTween.gameObject.SetActive(true);
        characterselectTween.PlayForward();
    }


    //login界面的点击事件
    public void OnLoginUILoginClick()
    {
        //1.得到用户名和密码存储起来
        username = usernameLoingInput.value;
        password = passwordLoingInput.value;
       
        //2.返回start界面
        loginpannelTween.PlayReverse();
        StartCoroutine(HidePanel(loginpannelTween.gameObject));
        startpanneTween.gameObject.SetActive(true);
        startpanneTween.PlayReverse();

        usernameStart.text = username;
    }
	
    public void OnLoginUIRegisterClick()
    {
        //隐藏当前面板，显示注册面板
        loginpannelTween.PlayReverse();
        StartCoroutine(HidePanel(loginpannelTween.gameObject));
        //loginpannelTween.gameObject.SetActive(false);
        registerpannelTween.gameObject.SetActive(true);
        registerpannelTween.PlayForward();
    }

    public void OnLoginCloseClick()
    {
        //2.返回start界面
        loginpannelTween.PlayReverse();
        StartCoroutine(HidePanel(loginpannelTween.gameObject));
        startpanneTween.gameObject.SetActive(true);
        startpanneTween.PlayReverse();
    }


    //register注册见面点击事件注册
    public void OnRegisterUICloseClick()
    {
        registerpannelTween.PlayReverse();
        StartCoroutine(HidePanel(registerpannelTween.gameObject));
        startpanneTween.gameObject.SetActive(true);
        startpanneTween.PlayReverse();
    }

    //注册并登陆
    public void OnRegisterUILoginAndClick()
    {
        username = usernameInputRegister.value;
        password = passwordInputRegister.value;
        string rePassword = repasswordInputRegister.value;

        //1.本地校验，链接服务器进行校验
        if (username == "" || username.Length <= 3)
        {
            MessageManager.instance.ShowMessage("用户名输入错误(不能少于3个)", 2);
            return;
        }
        if (password == "" || password.Length <= 3)
        {
            MessageManager.instance.ShowMessage("密码输入错误(不能少于3个)", 2);
            return;
        }
        if (password != rePassword)
        {
            MessageManager.instance.ShowMessage("密码不一致", 2);
            return;
        }
        registerController.Register(username, password,this);
        //2.链接失败
        //3.链接成功，保存信息，隐藏并返回开始界面

        //usernameStart.text = username;
        //这里要把当前不用的pannel给playreverse一下要不然play的顺序会变
        //registerpannelTween.PlayReverse();
        //StartCoroutine(HidePanel(registerpannelTween.gameObject));
        //startpanneTween.gameObject.SetActive(true);
        //startpanneTween.PlayReverse();

    }

    public void HideRegisterPanel()
    {
        registerpannelTween.PlayReverse();
        StartCoroutine(HidePanel(registerpannelTween.gameObject));
    }
    public void ShowStartPanel()
    {
        startpanneTween.gameObject.SetActive(true);
        startpanneTween.PlayReverse();
    }


    public void OnRegisterUICancelClick()
    {
        //隐藏注册面板，返回登录面板
        registerpannelTween.PlayReverse();
        StartCoroutine(HidePanel(registerpannelTween.gameObject));
        //registerpannelTween.gameObject.SetActive(false);
        loginpannelTween.gameObject.SetActive(true);
        loginpannelTween.PlayForward();
       
    }

    //隐藏面板
    IEnumerator HidePanel(GameObject go)
    {
        yield return new WaitForSeconds(0.22f);
        go.SetActive(false);
    }

    IEnumerator HidePanelTrue(GameObject go)
    {
        yield return new WaitForSeconds(0.22f);
        go.SetActive(true);
    }


    //角色选择界面 点击角色事件
    public void OnCharacterClick(GameObject go)
    {
        if(go == characterSelected)
        {
            return;
        }
        iTween.ScaleTo(go, new Vector3(1f, 1f, 1f), 0.5f);

        if (characterSelected != null)
        {
            iTween.ScaleTo(characterSelected, new Vector3(0.75f, 0.75f, 0.75f), 0.5f);

        }
        characterSelected = go;
    }

    //点进更换角色
    public void OnButtonChangecharacterClick()
    {
        characterselectTween.PlayReverse();
        StartCoroutine(HidePanel(characterselectTween.gameObject));
        charactershowTween.gameObject.SetActive(true);
        charactershowTween.PlayForward();

    }
    //角色选择界面处理按钮
    public void OnCharacterShowButtonSure()
    {
        //1.判断姓名是否输入正确
        //2.判断是否选择角色

        int index = -1;
        for (int i = 0; i < CharacterArray.Length; i++)
        {
            if (characterSelected == CharacterArray[i])
            {
                index = i;
                break;
            }
        }
        if (index == -1) return;
        //清空已有角色
        GameObject.Destroy(CharacterParentTransform.GetComponentInChildren<Animation>().gameObject);
        //生成新选择角色
        GameObject go = GameObject.Instantiate(CharacterSelectedArray[index], new Vector3(662, 44, 11), Quaternion.identity) as GameObject;
        go.transform.parent = CharacterParentTransform.transform;
        go.transform.localPosition = new Vector3(322, -196, -205);
        go.transform.Rotate(new Vector3(0, -180, 0));
        go.transform.localScale = new Vector3(300, 300, 300);
        nameLabelCha_sel.text = characternameInput.value;
        lvLabelCha_sel.text = "Lv.1";
        OnCharacterShowButtonBackClick();
    }
    public void OnCharacterShowButtonBackClick()
    {
        charactershowTween.PlayReverse();
        StartCoroutine(HidePanel(charactershowTween.gameObject));
        characterselectTween.gameObject.SetActive(true);
        characterselectTween.PlayForward();
    }

    //角色选择界面进入游戏按钮事件
    public void OnCharselecetdEnterGameClick()
    {
        //异步加载
        AsyncOperation op = SceneManager.LoadSceneAsync(1);
        LoadSceneProBar.instance.Show(op);
        //SceneManager.LoadScene(1);
    }
}
