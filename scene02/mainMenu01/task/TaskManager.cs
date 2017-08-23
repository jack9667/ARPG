using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理宗磊
/// 在gamecontroller上
/// </summary>
public class TaskManager : MonoBehaviour {

    public TextAsset taskInfoText;
    private ArrayList taskList=new ArrayList();
    public static TaskManager instance;
    private Task curTask;
    private PlayerAutoMove playerAM;
    private PlayerAutoMove PlayerAM
    {
        get
        {
            if (playerAM == null)
            {
                playerAM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAutoMove>();
            }
            return playerAM;
        }
    }

    void Awake()
    {
        instance = this;
        InitTask();

    }
    /// <summary>
    /// 初始化任务信息
    /// </summary>
    public void InitTask()
    {
        string[] taskinfoArray = taskInfoText.ToString().Split('\n');
        foreach(string str in taskinfoArray)
        {
            string[] proArray = str.Split('|');
            Task task = new Task();
            task.Id = int.Parse(proArray[0]);
            switch (proArray[1])
            {
                case "Main":
                    task.TaskType = TaskType.Main;
                    break;
                case "Reward":
                    task.TaskType = TaskType.Reward;
                    break;
                case "Daily":
                    task.TaskType = TaskType.Daily;
                    break;
            }
            task.Name = proArray[2];
            task.Icon = proArray[3];
            task.Des = proArray[4];
            task.Coin = int.Parse(proArray[5]);
            task.Diamond = int.Parse(proArray[6]);
            task.TalkNpc = proArray[7];
            task.IdNpc = int.Parse(proArray[8]);
            task.IdTranscript = int.Parse(proArray[9]);

            taskList.Add(task);
        }
    }
    //返回下数组
    public ArrayList GetTaskList()
    {
        return taskList;
    }
    /// <summary>
    /// 执行某个任务
    /// </summary>
    public void OnExcuteTask(Task task)
    {
        curTask = task;
        if (task.Tpro == TaskProgress.NoStart)  //导航到npc
        {
            PlayerAM.SetDest(NPCManager.instance.GetnpcById().transform.position);
        }else if (task.Tpro == TaskProgress.Accept) //导航到副本入口
        {
            //layerAM.SetDest(NPCManager.instance.transpritGo.transform.position);
            playerAM.SetTranscriptDes(NPCManager.instance.transpritGo.transform.position);
        }
    }
    /// <summary>
    /// from npcuidialg.cs
    /// 寻找到接受的入口
    /// </summary>
    public void OnAcceptTask()
    {
        curTask.Tpro = TaskProgress.Accept;
        //寻路到副本入口
        playerAM.SetTranscriptDes(NPCManager.instance.transpritGo.transform.position);

    }
    /// <summary>
    /// from playerautomove
    /// </summary>
    public void GetDest()
    {
        if (curTask.Tpro == TaskProgress.NoStart)
        {
            NpcUiDialg.instance.Show(curTask.TalkNpc);
        }
    }
}
