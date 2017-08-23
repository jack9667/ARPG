using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskType
{
    Main,//主线任务
    Reward,//赏金任务
    Daily//日常任务
}

public enum TaskProgress
{
    NoStart,    //没开始
    Accept,
    Complete,
    Reward
}

public class Task {

    //i.未开始
    //ii.接受任务
    //iii.	任务完成
    //iv.	获取奖励（结束）

    private int id;//    a)	Id
    private TaskType taskType;//b)	任务类型（Main,Reward，Daily）
    private string name;//c)	名称
    private string icon;//d)	图标
    private string des;//e)	任务描述
    private int coin; //f)	获得的金币奖励
    private int diamond; //g)	获得的钻石奖励
    private string talkNpc;//h)	跟npc交谈的话语
    private int idNpc;//i)	Npc的id
    private int idTranscript;   //副本id
    private TaskProgress taskProgress = TaskProgress.NoStart;   //k)	任务的状态

    public delegate void  OnTaskChangedEvent();
    public event OnTaskChangedEvent OnTakeChanged;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public TaskType TaskType
    {
        get { return taskType; }
        set { taskType = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public string Icon
    {
        get { return icon; }
        set { icon = value; }
    }
    public string Des
    {
        get { return des; }
        set { des = value; }
    }
    public int Coin
    {
        get { return coin; }
        set { coin = value; }
    }
    public int Diamond
    {
        get { return diamond; }
        set { diamond = value; }
    }
    public string TalkNpc
    {
        get { return talkNpc; }
        set { talkNpc = value; }
    }
    public int IdNpc
    {
        get { return idNpc; }
        set { idNpc = value; }
    }
    public int IdTranscript
    {
        get { return idTranscript; }
        set { idTranscript = value; }
    }
    public TaskProgress Tpro
    {
        get { return taskProgress; }
        set
        {
            if (taskProgress != value)
            {
                taskProgress = value;
                OnTakeChanged();
            }
                
        }
    }
}
