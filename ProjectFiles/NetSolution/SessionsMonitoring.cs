#region Using directives
using System;
using System.Linq;
using FTOptix.Core;
using FTOptix.CoreBase;
using FTOptix.HMIProject;
using FTOptix.NativeUI;
using FTOptix.NetLogic;
using FTOptix.Retentivity;
using FTOptix.UI;
using FTOptix.WebUI;
using UAManagedCore;
using static System.Collections.Specialized.BitVector32;
using OpcUa = UAManagedCore.OpcUa;
#endregion

public class SessionsMonitoring : BaseNetLogic
{
    public override void Start()
    {
        periodicTask = new PeriodicTask(MonitorSessions, 500, LogicObject);
        periodicTask.Start();
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        periodicTask?.Dispose();
    }

    public void MonitorSessions()
    {
        // Insert code to be executed when the user-defined logic is started
        var wpe = Project.Current.Get<WebUIPresentationEngine>("UI/WebPresentationEngine");
        var activeSessions = wpe.Sessions.Where(s => s.BrowseName.StartsWith("Session-"));

        var mainSessions = 0;
        var subSessions = 0;

        // Count the number of active sessions with Main and Sub panels
        foreach (var session in activeSessions)
        {
            var panelLoader = session.Get<PanelLoader>("UIRoot/MainPanelLoader");
            var loadedPanel = InformationModel.Get(panelLoader.Panel);
            if (Log.Node(loadedPanel).Contains("/Main/"))
            {
                mainSessions++;
            }
            else if (Log.Node(loadedPanel).Contains("/Sub/"))
            {
                subSessions++;
            }
        }

        // Check if any counter is bigger than the set limit
        if (mainSessions >= LogicObject.GetVariable("MaxNumberOfMainSessions").Value)
        {
            Owner.Get<Button>("Main").Enabled = false;
            Log.Info($"Main sessions limit exceeded: {mainSessions} active sessions, limit is {LogicObject.GetVariable("MaxNumberOfMainSessions").Value}");
        }
        else
        {
            Owner.Get<Button>("Main").Enabled = true;
        }

        if (subSessions >= LogicObject.GetVariable("MaxNumberOfSubSessions").Value)
        {
            Owner.Get<Button>("Sub").Enabled = false;
            Log.Info($"Sub sessions limit exceeded: {subSessions} active sessions, limit is {LogicObject.GetVariable("MaxNumberOfSubSessions").Value}");
        }
        else
        {
            Owner.Get<Button>("Sub").Enabled = true;
        }
    }

    private PeriodicTask periodicTask;
}
