using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 任务栏中每一个任务ui的脚本，包含将读到的task存进来
/// </summary>
public class TaskItemUI : MonoBehaviour {

    private UISprite tasktypeSprite;
    private UISprite iconSprite;
    private UILabel nameLabel;
    private UILabel desLabel;
    //private UISprite reward1Sprite;//没用sprite
    private UILabel reward1Label;
    //private UISprite reward2Sprite;
    private UILabel reward2Label;
    private UIButton rewardButton;
    private UIButton combatButton;
    private UILabel combatButtonLabel;

    private Task task;

    void Awake()
    {
        tasktypeSprite = transform.Find("TasktypeSprite").GetComponent<UISprite>();
        iconSprite = transform.Find("IconBg/Sprite").GetComponent<UISprite>();
        nameLabel = transform.Find("NameLabel").GetComponent<UILabel>();
        desLabel = transform.Find("DesLabel").GetComponent<UILabel>();
        
        reward1Label = transform.Find("Reward1Label").GetComponent<UILabel>();
        reward2Label = transform.Find("Reward2Label").GetComponent<UILabel>();
        rewardButton = transform.Find("RewardButton").GetComponent<UIButton>();
        combatButton = transform.Find("CombatButton").GetComponent<UIButton>();
        combatButtonLabel = transform.Find("CombatButton/Label").GetComponent<UILabel>();

        EventDelegate ed1 = new EventDelegate(this, "OnCombatClick");
        EventDelegate ed2 = new EventDelegate(this, "OnRewardClick");

        
        combatButton.onClick.Add(ed1);
        rewardButton.onClick.Add(ed2);
    }
    /// <summary>
    /// 设置每个task的各种属性
    /// </summary>
    /// <param name="task"></param>
    public void SetTask(Task task)  //to TaskUI设置
    {
        this.task = task;
        task.OnTakeChanged += OnTaskChange;//注册事件
        UpdateTask();
        

    }
    /// <summary>
    /// 两个按钮按键
    /// </summary>
    void OnCombatClick()
    {
        TaskManager.instance.OnExcuteTask(this.task);   //传递给TaskManager
        TaskUI.instance.Close();
    }
    void OnRewardClick()
    {

    }


    /// <summary>
    /// 更新任务显示
    /// </summary>
    void UpdateTask()
    {
        switch (task.TaskType)
        {
            case TaskType.Main:
                tasktypeSprite.spriteName = "pic_主线";
                break;
            case TaskType.Reward:
                tasktypeSprite.spriteName = "pic_奖赏";
                break;
            case TaskType.Daily:
                tasktypeSprite.spriteName = "pic_日常";
                break;
        }
        iconSprite.spriteName = task.Icon;
        nameLabel.text = task.Name;
        desLabel.text = task.Des;
        reward1Label.text = "金币：" + task.Coin;
        reward2Label.text = "钻石：" + task.Diamond;

        //任务进度,不同的任务显示不同的任务提示
        switch (task.Tpro)
        {
            case TaskProgress.NoStart:
                rewardButton.gameObject.SetActive(false);
                combatButtonLabel.text = "去领取";
                break;
            case TaskProgress.Accept:
                rewardButton.gameObject.SetActive(false);
                combatButtonLabel.text = "战斗";
                break;
            case TaskProgress.Complete:
                combatButton.gameObject.SetActive(false);
                break;
        }
    }

    void OnTaskChange()
    {
        UpdateTask();
    }
}
