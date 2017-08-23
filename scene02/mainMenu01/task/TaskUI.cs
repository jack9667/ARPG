using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 把manager中的task初始化到scroll中
/// 
/// </summary>
public class TaskUI : MonoBehaviour {

    public static TaskUI instance;

    private UIGrid taskListGrid;
    public GameObject taskitemPrefab;
    private UIButton closeBtn;


    void Awake()
    {
        instance = this;
        taskListGrid = transform.Find("Scroll View/Grid").GetComponent<UIGrid>();
        closeBtn = transform.Find("btn-close").GetComponent<UIButton>();
    }
    void Start()
    {
        InitTaskList();
        EventDelegate ed1 = new EventDelegate(this, "Close");
        closeBtn.onClick.Add(ed1);
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public void InitTaskList()
    {
        ArrayList taskList = TaskManager.instance.GetTaskList();

        foreach(Task task in taskList)
        {
            GameObject go = NGUITools.AddChild(taskListGrid.gameObject, taskitemPrefab);//把读到的放到scroll中
            taskListGrid.AddChild(go.transform);//添加后排序
            TaskItemUI ti = go.GetComponent<TaskItemUI>();
            ti.SetTask(task);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
