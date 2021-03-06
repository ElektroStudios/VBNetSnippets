' ***********************************************************************
' Author   : Elektro
' Modified : 04-November-2015
' ***********************************************************************
' <copyright file="Process Util.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Public Members Summary "

#Region " Types "

' ProcessUtil.ProcessMonitor : Implements IDisposable
' ProcessUtil.ProcessMonitor.ProcessMonitorEventArgs : Inherits EventArgs

' ProcessUtil.ProcessWatcher

#End Region

#Region " Constructors "

' ProcessUtil.ProcessMonitor.New()
' ProcessUtil.ProcessMonitor.ProcessMonitorEventArgs.New(Integer, String)

' ProcessUtil.ProcessWatcher.New()

#End Region

#Region " Events "

' ProcessUtil.ProcessMonitor.ProcessStarted As EventHandler(Of ProcessUtil.ProcessMonitor.ProcessMonitorEventArgs)
' ProcessUtil.ProcessMonitor.ProcessStopped As EventHandler(Of ProcessUtil.ProcessMonitor.ProcessMonitorEventArgs)

' ProcessUtil.ProcessWatcher.ProcessStarted As EventArrivedEventHandler
' ProcessUtil.ProcessWatcher.ProcessStopped As EventArrivedEventHandler

#End Region

#Region " Properties "

' ProcessUtil.ProcessMonitor.Interval As Integer

' ProcessUtil.ProcessMonitor.ProcessMonitorEventArgs.ProcessId As Integer
' ProcessUtil.ProcessMonitor.ProcessMonitorEventArgs.ProcessName As String

#End Region

#Region " Fucntions "

' ProcessUtil.CloseProcess(String, Opt: Boolean) As Boolean
' ProcessUtil.CloseProcesses(String, Opt: Boolean) As Boolean
' ProcessUtil.GetProcessesValue(Of T)(String, Func(Of Process, T), Opt: Boolean) As IEnumerable(Of T)
' ProcessUtil.GetProcessValue(Of T)(String, Func(Of Process, T), Opt: Boolean) As T
' ProcessUtil.GetWmiProcessQuery(KeyValuePair(Of String, String)) As ManagementObject()
' ProcessUtil.IsRunning(String) As Boolean
' ProcessUtil.KillProcess(String, Opt: Boolean, Opt: Boolean) As Boolean
' ProcessUtil.KillProcesses(String, Opt: Boolean, Opt: Boolean) As Boolean
' ProcessUtil.SetProcessesValue(String, Action(Of Process), Opt: Boolean)
' ProcessUtil.SetProcessesValues(String, Action(Of Process), Opt: Boolean)

#End Region

#Region " Methods "

' ProcessUtil.WaitUntilLoaded(Process, Opt: Integer)
' ProcessUtil.WaitUntilLoadedAsync(Process, Action, Opt: Integer)

' ProcessUtil.ProcessMonitor.Start
' ProcessUtil.ProcessMonitor.Stop
' ProcessUtil.ProcessMonitor.Dispose

' ProcessUtil.ProcessWatcher.Start
' ProcessUtil.ProcessWatcher.Stop

#End Region

#End Region

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System
Imports System.Diagnostics
Imports System.Linq
Imports System.Management
Imports System.Threading.Tasks

#End Region

#Region " Process Util "

''' ----------------------------------------------------------------------------------------------------
''' <summary>
''' Contains related Process utilities.
''' </summary>
''' ----------------------------------------------------------------------------------------------------
Public Module ProcessUtil

#Region " Types "

#Region " Process Monitor "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' A simple process monitor that notifies about started and stopped processes.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' In difference to <see cref="ProcessUtil.ProcessWatcher"/>, 
    ''' this class is for scenarios on which responsivennes is a necessary requisite.
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Public Class Form1 : Inherits Form
    ''' 
    '''     Dim WithEvents procMon As New ProcessMonitor With {.Interval = 100}
    ''' 
    '''     Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) _
    '''     Handles Me.Load
    ''' 
    '''         Me.procMon.Start()
    ''' 
    '''     End Sub
    ''' 
    '''     Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
    '''     Handles Me.FormClosing
    ''' 
    '''         Me.procMon.Dispose()
    ''' 
    '''     End Sub
    ''' 
    '''     Private Sub ProcessMonitor_ProcessStarted(ByVal sender As Object, ByVal e As ProcessMonitor.ProcessMonitorEventArgs) _
    '''     Handles procMon.ProcessStarted
    ''' 
    '''         Dim p As Process = Process.GetProcessById(e.ProcessId)
    ''' 
    '''         Console.WriteLine(String.Format("Process started | Name: {0}", e.ProcessName))
    '''         Console.WriteLine(String.Format("Process started | PID : {0}", e.ProcessId))
    '''         Console.WriteLine(String.Format("Process started | Path: {0}", p.MainModule.FileName))
    ''' 
    '''     End Sub
    ''' 
    '''     Private Sub ProcessMonitor_ProcessStopped(ByVal sender As Object, ByVal e As ProcessMonitor.ProcessMonitorEventArgs) _
    '''     Handles procMon.ProcessStopped
    ''' 
    '''         Console.WriteLine(String.Format("Process started | Name: {0}", e.ProcessName))
    '''         Console.WriteLine(String.Format("Process started | PID : {0}", e.ProcessId))
    ''' 
    '''     End Sub
    ''' 
    ''' End Class
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    Public Class ProcessMonitor : Implements IDisposable

#Region " Objects "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' A <see cref="IEnumerable(Of KeyValuePair(Of Integer, String))"/> that contains the PID and names of the running processes.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private procInfoCol As IEnumerable(Of KeyValuePair(Of Integer, String))

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' A timer that collects running processes information and notifies about started and stopped processes.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private procMonTimer As Threading.Timer

#End Region

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the monitoring interval.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The monitoring interval.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Property Interval As Integer = 100

#End Region

#Region " Events "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' A list of event delegates.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly events As EventHandlerList

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Occurs when a process starts.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Custom Event ProcessStarted As EventHandler(Of ProcessMonitorEventArgs)

            <DebuggerNonUserCode>
            <DebuggerStepThrough>
            AddHandler(ByVal value As EventHandler(Of ProcessMonitorEventArgs))
                Me.events.AddHandler("ProcessStartedEvent", value)
            End AddHandler

            <DebuggerNonUserCode>
            <DebuggerStepThrough>
            RemoveHandler(ByVal value As EventHandler(Of ProcessMonitorEventArgs))
                Me.events.RemoveHandler("ProcessStartedEvent", value)
            End RemoveHandler

            <DebuggerNonUserCode>
            <DebuggerStepThrough>
            RaiseEvent(ByVal sender As Object, ByVal e As ProcessMonitorEventArgs)
                Dim handler As EventHandler(Of ProcessMonitorEventArgs) =
                    DirectCast(Me.events("ProcessStartedEvent"), EventHandler(Of ProcessMonitorEventArgs))

                If (handler IsNot Nothing) Then
                    handler.Invoke(sender, e)
                End If
            End RaiseEvent

        End Event

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Occurs when a process stops.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Custom Event ProcessStopped As EventHandler(Of ProcessMonitorEventArgs)

            <DebuggerNonUserCode>
            <DebuggerStepThrough>
            AddHandler(ByVal value As EventHandler(Of ProcessMonitorEventArgs))
                Me.events.AddHandler("ProcessStoppedEvent", value)
            End AddHandler

            <DebuggerNonUserCode>
            <DebuggerStepThrough>
            RemoveHandler(ByVal value As EventHandler(Of ProcessMonitorEventArgs))
                Me.events.RemoveHandler("ProcessStoppedEvent", value)
            End RemoveHandler

            <DebuggerNonUserCode>
            <DebuggerStepThrough>
            RaiseEvent(ByVal sender As Object, ByVal e As ProcessMonitorEventArgs)
                Dim handler As EventHandler(Of ProcessMonitorEventArgs) =
                    DirectCast(Me.events("ProcessStoppedEvent"), EventHandler(Of ProcessMonitorEventArgs))

                If (handler IsNot Nothing) Then
                    handler.Invoke(sender, e)
                End If
            End RaiseEvent

        End Event

#End Region

#Region " Events Data "

#Region " ProcessMonitorEventArgs "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Contains the event-data of <see cref="ProcessMonitor.ProcessStarted"/> and <see cref="ProcessMonitor.ProcessStopped"/> events.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Class ProcessMonitorEventArgs : Inherits EventArgs

#Region " Properties "

            ''' ----------------------------------------------------------------------------------------------------
            ''' <summary>
            ''' Gets the process id (PID) of the intercepted process.
            ''' </summary>
            ''' ----------------------------------------------------------------------------------------------------
            ''' <value>
            ''' The process id (PID) of the intercepted process.
            ''' </value>
            ''' ----------------------------------------------------------------------------------------------------
            Public Overridable ReadOnly Property ProcessId As Integer
                Get
                    Return Me.processIdB
                End Get
            End Property
            ''' ----------------------------------------------------------------------------------------------------
            ''' <summary>
            ''' ( Backing field )
            ''' The process id (PID) of the intercepted process.
            ''' </summary>
            ''' ----------------------------------------------------------------------------------------------------
            Private ReadOnly processIdB As Integer

            ''' ----------------------------------------------------------------------------------------------------
            ''' <summary>
            ''' Gets the name of the intercepted process.
            ''' </summary>
            ''' ----------------------------------------------------------------------------------------------------
            ''' <value>
            ''' The name of the intercepted process.
            ''' </value>
            ''' ----------------------------------------------------------------------------------------------------
            Public Overridable ReadOnly Property ProcessName As String
                Get
                    Return Me.processNameB
                End Get
            End Property
            ''' ----------------------------------------------------------------------------------------------------
            ''' <summary>
            ''' ( Backing field )
            ''' The name of the intercepted process.
            ''' </summary>
            ''' ----------------------------------------------------------------------------------------------------
            Private ReadOnly processNameB As String

#End Region

#Region " Constructors "

            ''' ----------------------------------------------------------------------------------------------------
            ''' <summary>
            ''' Prevents a default instance of the <see cref="ProcessMonitor.ProcessMonitorEventArgs"/> class from being created.
            ''' </summary>
            ''' ----------------------------------------------------------------------------------------------------
            <DebuggerNonUserCode>
            Private Sub New()
            End Sub

            ''' ----------------------------------------------------------------------------------------------------
            ''' <summary>
            ''' Initializes a new instance of the <see cref="ProcessMonitor.ProcessMonitorEventArgs"/> class.
            ''' </summary>
            ''' ----------------------------------------------------------------------------------------------------
            ''' <param name="processId">
            ''' The process id (PID) of the intercepted process.
            ''' </param>
            ''' 
            ''' <param name="processName">
            ''' The name of the intercepted process.
            ''' </param>
            ''' ----------------------------------------------------------------------------------------------------
            <DebuggerStepThrough>
            Public Sub New(ByVal processId As Integer, ByVal processName As String)

                Me.processIdB = processId
                Me.processNameB = processName

            End Sub

#End Region

        End Class

#End Region

#End Region

#Region " Event Invocators "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises <see cref="ProcessMonitor.ProcessStarted"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' The <see cref="ProcessMonitor.ProcessMonitorEventArgs"/> instance containing the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Protected Overridable Sub OnProcessStarted(ByVal e As ProcessMonitor.ProcessMonitorEventArgs)

            RaiseEvent ProcessStarted(Me, e)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises <see cref="ProcessMonitor.ProcessStopped"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' The <see cref="ProcessMonitor.ProcessMonitorEventArgs"/> instance containing the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Protected Overridable Sub OnProcessStopped(ByVal e As ProcessMonitor.ProcessMonitorEventArgs)

            RaiseEvent ProcessStopped(Me, e)

        End Sub

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="ProcessMonitor"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub New()

            Me.events = New EventHandlerList

        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Starts monitoring.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="Exception">
        ''' Monitor is already running.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Overridable Sub Start()

            If (Me.procMonTimer Is Nothing) Then
                Me.procInfoCol = Me.Scan.ToArray
                Me.procMonTimer = New Threading.Timer(AddressOf Callback, Nothing, Me.Interval, 0)

            Else
                Throw New Exception(message:="Monitor is already running.")

            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Stops monitoring.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="Exception">
        ''' Monitor is already stopped.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Overridable Sub [Stop]()

            If (Me.procMonTimer IsNot Nothing) Then
                Me.procMonTimer.Dispose()
                Me.procMonTimer = Nothing

            Else
                Throw New Exception(message:="Monitor is already stopped.")

            End If

        End Sub

#End Region

#Region " Private Methods "

        ''' ---------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Iterates the running process then return their PID and names.
        ''' </summary>
        ''' ---------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' A <see cref="IEnumerable(Of KeyValuePair(Of Integer, String))"/> that contains the PID and name of the running processes.
        ''' </returns>
        ''' ---------------------------------------------------------------------------------------------------
        Protected Overridable Iterator Function Scan() As IEnumerable(Of KeyValuePair(Of Integer, String))

            For Each proc As Process In Process.GetProcesses()
                Yield New KeyValuePair(Of Integer, String)(proc.Id, proc.ProcessName)
            Next proc

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The <see cref="procMonTimer"/> callback procedure.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="https://msdn.microsoft.com/en-us/library/ms149618%28v=vs.110%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="stateInfo">
        ''' Object used to turn the timer off.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overridable Sub Callback(ByVal stateInfo As Object)

            Dim active As KeyValuePair(Of Integer, String)() = Me.Scan.ToArray
            Dim started As IEnumerable(Of KeyValuePair(Of Integer, String)) = active.Except(procInfoCol)
            Dim stopped As IEnumerable(Of KeyValuePair(Of Integer, String)) = Me.procInfoCol.Except(active)

            Me.procInfoCol = active

            For Each procInfo As KeyValuePair(Of Integer, String) In started
                Me.OnProcessStarted(New ProcessMonitorEventArgs(processId:=procInfo.Key, processName:=procInfo.Value))
            Next procInfo

            For Each procInfo As KeyValuePair(Of Integer, String) In stopped
                Me.OnProcessStopped(New ProcessMonitorEventArgs(processId:=procInfo.Key, processName:=procInfo.Value))
            Next procInfo

            If (Me.procMonTimer IsNot Nothing) Then
                Me.procMonTimer.Change(Me.Interval, 0)
            End If

        End Sub

#End Region

#Region " IDisposable Implementation "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' To detect redundant calls when disposing.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private isDisposed As Boolean = False

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Releases all the resources used by this instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub Dispose() Implements IDisposable.Dispose
            Me.Dispose(isDisposing:=True)
            GC.SuppressFinalize(obj:=Me)
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ''' Releases unmanaged and - optionally - managed resources.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="isDisposing">
        ''' <see langword="True"/>  to release both managed and unmanaged resources; 
        ''' <see langword="False"/> to release only unmanaged resources.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Protected Overridable Sub Dispose(ByVal isDisposing As Boolean)

            If (Not Me.isDisposed) AndAlso (isDisposing) Then
                Me.Stop()
                Me.events.Dispose()
            End If

            Me.isDisposed = True

        End Sub

#End Region

    End Class

#End Region

#Region " Process Watcher "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' A simple process monitor that notifies about started and stopped processes.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' In difference to <see cref="ProcessUtil.ProcessMonitor"/>,
    ''' this class is for scenarios on which responsivennes is not an important requisite.
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Imports System.Management
    ''' 
    ''' Public Class Form1 : Inherits Form
    ''' 
    '''     Private WithEvents processWatcher As New ProcessWatcher
    ''' 
    '''     Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) _
    '''     Handles Me.Load
    ''' 
    '''         Me.processWatcher.Start()
    ''' 
    '''     End Sub
    '''
    '''     Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
    '''     Handles Me.FormClosing
    ''' 
    '''         Me.processWatcher.Dispose()
    ''' 
    '''     End Sub
    ''' 
    '''     Private Sub ProcessWatcher_ProcessStarted(ByVal sender As Object, ByVal e As EventArrivedEventArgs) _
    '''     Handles processWatcher.ProcessStarted
    ''' 
    '''         Dim name As String = CStr(e.NewEvent.Properties("ProcessName").Value)
    '''         Dim pid As Integer = CInt(e.NewEvent.Properties("ProcessId").Value)
    '''         Dim path As String = String.Empty
    ''' 
    '''         Dim scope As New ManagementScope("root\CIMV2")
    '''         Dim query As New SelectQuery(String.Format("SELECT * FROM Win32_Process WHERE ProcessId={0}", pid))
    '''         Dim options As New EnumerationOptions With {.ReturnImmediately = True}
    ''' 
    '''         Using wmi As New ManagementObjectSearcher(scope, query, options)
    '''             Using objCol As ManagementObjectCollection = wmi.Get
    '''                 path = CStr(DirectCast(objCol(0), ManagementObject).Properties("ExecutablePath").Value)
    '''             End Using
    '''         End Using
    ''' 
    '''         Console.WriteLine(String.Format("Process started | Name: {0}", name))
    '''         Console.WriteLine(String.Format("Process started | PID : {0}", pid))
    '''         Console.WriteLine(String.Format("Process started | Path: {0}", path))
    ''' 
    '''     End Sub
    ''' 
    '''     Private Sub ProcessWatcher_ProcessStopped(ByVal sender As Object, ByVal e As EventArrivedEventArgs) _
    '''     Handles processWatcher.ProcessStopped
    ''' 
    '''         Dim name As String = e.NewEvent.Properties("ProcessName").Value.ToString
    ''' 
    '''         Console.WriteLine(String.Format("Process stopped | Name: {0}", name))
    ''' 
    '''     End Sub
    ''' 
    ''' End Class
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    Public Class ProcessWatcher : Implements IDisposable


#Region " Objects "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' WMI object that listen and notifies about process starts discoveries.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private WithEvents processStartWatcher As ManagementEventWatcher

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' WMI object that listen and notifies about process stops discoveries.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private WithEvents processStopWatcher As ManagementEventWatcher

#End Region

#Region " Events "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Occurs when a process starts (run).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Event ProcessStarted As EventArrivedEventHandler

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Occurs when a process stops (exit).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Event ProcessStopped As EventArrivedEventHandler

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="ProcessWatcher"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub New()

            Me.processStartWatcher = New ManagementEventWatcher(New WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace WITHIN 0.1"))
            Me.processStopWatcher = New ManagementEventWatcher(New WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace WITHIN 0.1"))

        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Start monitoring for process starts and stops.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub Start()

            Me.processStartWatcher.Start()
            Me.processStopWatcher.Start()

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Stop monitoring for process starts and stops.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub [Stop]()

            Me.processStartWatcher.Stop()
            Me.processStopWatcher.Stop()

        End Sub

#End Region

#Region " Event Invocators "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises the <see cref="ProcessStarted"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' The <see cref="EventArrivedEventArgs"/> instance containing the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Protected Overridable Sub RaiseProcessStartedEvent(ByVal e As EventArrivedEventArgs)

            RaiseEvent ProcessStarted(Me, e)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises the <see cref="ProcessStopped"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' The <see cref="EventArrivedEventArgs"/> instance containing the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Protected Overridable Sub RaiseProcessStoppedEvent(ByVal e As EventArrivedEventArgs)

            RaiseEvent ProcessStopped(Me, e)

        End Sub

#End Region

#Region " Event Handlers "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="ManagementEventWatcher.EventArrived"/> event of the <see cref="processStartWatcher"/> object.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        ''' 
        ''' <param name="e">
        ''' The <see cref="EventArrivedEventArgs"/> instance containing the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Private Sub ProcessStartWatcher_EventArrived(ByVal sender As Object, ByVal e As EventArrivedEventArgs) _
        Handles processStartWatcher.EventArrived

            If (Me.ProcessStartedEvent IsNot Nothing) Then
                Me.RaiseProcessStartedEvent(e)
            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="ManagementEventWatcher.EventArrived"/> event of the <see cref="processStopWatcher"/> object.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        ''' 
        ''' <param name="e">
        ''' The <see cref="EventArrivedEventArgs"/> instance containing the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Private Sub ProcessStopWatcher_EventArrived(ByVal sender As Object, ByVal e As EventArrivedEventArgs) _
        Handles processStopWatcher.EventArrived

            If (Me.ProcessStoppedEvent IsNot Nothing) Then
                Me.RaiseProcessStoppedEvent(e)
            End If

        End Sub

#End Region

#Region " IDisposable Implementation "

#End Region

#Region "IDisposable Support"

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' To detect redundant calls when disposing.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private isDisposed As Boolean = False

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Releases all the resources used by this instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub Dispose() Implements IDisposable.Dispose
            Me.Dispose(isDisposing:=True)
            GC.SuppressFinalize(obj:=Me)
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ''' Releases unmanaged and - optionally - managed resources.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="isDisposing">
        ''' <see langword="True"/>  to release both managed and unmanaged resources; 
        ''' <see langword="False"/> to release only unmanaged resources.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Protected Overridable Sub Dispose(ByVal isDisposing As Boolean)

            If (Not Me.isDisposed) AndAlso (isDisposing) Then
                Me.Stop()
                Me.processStartWatcher.Dispose()
                Me.processStopWatcher.Dispose()
            End If

            Me.isDisposed = True

        End Sub

#End Region

    End Class

#End Region

#End Region

#Region " Public Methods "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Returns the specified value of the first ocurrence found of a running process with the specified name.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim pids As Integer =
    '''     ProcessUtil.GetProcessesValue("notepad.exe", Function(p As Process) p.Id,
    '''                                   throwOnProcessNotFound:=True)
    ''' 
    ''' MessageBox.Show(CStr(pid))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <typeparam name="T">
    ''' The <see cref="Type"/> of the value to return.
    ''' </typeparam>
    ''' 
    ''' <param name="processName">
    ''' The process name.
    ''' </param>
    ''' 
    ''' <param name="predicate">
    ''' A transform function to apply to each process found.
    ''' </param>
    ''' 
    ''' <param name="throwOnProcessNotFound">
    ''' If <see langword="True"/>, throws an <see cref="ArgumentException"/> exception if any process was found with the specified name.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' If successful, the returning value is <paramref name="T"/>; otherwise, 
    ''' <see langword="Nothing"/> if <paramref name="throwOnProcessNotFound"/> is <see langword="False"/> and 
    ''' any process was found with the specified name.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentException">
    ''' Any process found with the specified name.;processName
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function GetProcessValue(Of T)(ByVal processName As String,
                                          ByVal predicate As Func(Of Process, T),
                                          Optional throwOnProcessNotFound As Boolean = False) As T

        Dim processes As Process() = Process.GetProcessesByName(ProcessUtil.FixProcessName(processName))

        If Not (processes.Any) AndAlso (throwOnProcessNotFound) Then
            Throw New ArgumentException(message:="Any process found with the specified name.", paramName:="processName")

        Else
            Using p As Process = processes.First
                Return predicate.Invoke(p)
            End Using

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Returns the specified value of all the ocurrences found of a running process with the specified name.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim pids As IEnumerable(Of Integer) =
    '''     ProcessUtil.GetProcessesValue("notepad.exe", Function(p As Process) p.Id,
    '''                                   throwOnProcessNotFound:=True)
    ''' 
    ''' MessageBox.Show(String.Join(", ", pids))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <typeparam name="T">
    ''' The <see cref="Type"/> of the value to return.
    ''' </typeparam>
    ''' 
    ''' <param name="processName">
    ''' The process name.
    ''' </param>
    ''' 
    ''' <param name="predicate">
    ''' A transform function to apply to each process found.
    ''' </param>
    ''' 
    ''' <param name="throwOnProcessNotFound">
    ''' If <see langword="True"/>, throws an <see cref="ArgumentException"/> exception if any process was found with the specified name.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' If successful, the returning value is an <see cref="IEnumerable(Of T)"/>; otherwise, 
    ''' <see langword="Nothing"/> if <paramref name="throwOnProcessNotFound"/> is <see langword="False"/> and 
    ''' any process was found with the specified name.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentException">
    ''' Any process found with the specified name.;processName
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Iterator Function GetProcessesValue(Of T)(ByVal processName As String,
                                                     ByVal predicate As Func(Of Process, T),
                                                     Optional throwOnProcessNotFound As Boolean = False) As IEnumerable(Of T)

        Dim processes As Process() = Process.GetProcessesByName(ProcessUtil.FixProcessName(processName))

        If Not (processes.Any) AndAlso (throwOnProcessNotFound) Then
            Throw New ArgumentException(message:="Any process found with the specified name.", paramName:="processName")

        Else
            For Each p As Process In processes

                Try
                    Yield predicate.Invoke(p)

                Catch ex As Exception
                    Throw ex

                Finally
                    p.Close()

                End Try

            Next p

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Sets a value of the first ocurrence found of a running process with the specified name.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' ProcessUtil.SetProcessesValue("notepad.exe",
    '''                               Sub(p As Process) p.PriorityClass = ProcessPriorityClass.Normal,
    '''                               throwOnProcessNotFound:=True)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="processName">
    ''' The process name.
    ''' </param>
    ''' 
    ''' <param name="predicate">
    ''' A transform function to apply to each process found.
    ''' </param>
    ''' 
    ''' <param name="throwOnProcessNotFound">
    ''' If <see langword="True"/>, throws an <see cref="ArgumentException"/> exception if any process was found with the specified name.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentException">
    ''' Any process found with the specified name.;processName
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub SetProcessesValue(ByVal processName As String,
                                 ByVal predicate As Action(Of Process),
                                 Optional throwOnProcessNotFound As Boolean = False)

        Dim processes As Process() = Process.GetProcessesByName(ProcessUtil.FixProcessName(processName))

        If Not (processes.Any) AndAlso (throwOnProcessNotFound) Then
            Throw New ArgumentException(message:="Any process found with the specified name.", paramName:="processName")

        Else
            Using p As Process = processes.First
                predicate.Invoke(p)
            End Using

        End If

    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Returns the specified value of all the ocurrences found of a running process with the specified name.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' ProcessUtil.SetProcessesValues("notepad.exe",
    '''                                Sub(p As Process) p.PriorityClass = ProcessPriorityClass.Normal,
    '''                                throwOnProcessNotFound:=True)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="processName">
    ''' The process name.
    ''' </param>
    ''' 
    ''' <param name="predicate">
    ''' A transform function to apply to each process found.
    ''' </param>
    ''' 
    ''' <param name="throwOnProcessNotFound">
    ''' If <see langword="True"/>, throws an <see cref="ArgumentException"/> exception if any process was found with the specified name.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentException">
    ''' Any process found with the specified name.;processName
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub SetProcessesValues(ByVal processName As String,
                                 ByVal predicate As Action(Of Process),
                                 Optional throwOnProcessNotFound As Boolean = False)

        Dim processes As Process() = Process.GetProcessesByName(ProcessUtil.FixProcessName(processName))

        If Not (processes.Any) AndAlso (throwOnProcessNotFound) Then
            Throw New ArgumentException(message:="Any process found with the specified name.", paramName:="processName")

        Else
            For Each p As Process In processes

                Try
                    predicate.Invoke(p)

                Catch ex As Exception
                    Throw

                Finally
                    p.Close()

                End Try

            Next p

        End If

    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Determines whether exists, at least, one running process with the specified name.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="processName">
    ''' The process name.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' <see langword="True"/> if exists, at least, one running process with the specified name. <see langword="False"/> otherwise.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function IsRunning(ByVal processName As String) As Boolean

        Return Process.GetProcessesByName(ProcessUtil.FixProcessName(processName)).Any

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Performs a WMI query to Win32_Process class with the specified condition and returns the result.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' This is useful for example to get information of a 64-Bit process from a 32-Bit .Net assembly.
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim condition As New KeyValuePair(Of String, String)("Name", "notepad.exe")
    ''' 
    ''' For Each mo As ManagementObject In ProcessUtil.GetWmiProcessQuery(condition)
    ''' 
    '''     MessageBox.Show(DirectCast(mo.Properties("ExecutablePath").Value, String))
    ''' 
    ''' Next mo
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="condition">
    ''' The WHERE condition to use in the SELECT query.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The query result.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function GetWmiProcessQuery(ByVal condition As KeyValuePair(Of String, String)) As ManagementObject()

        Dim scope As New ManagementScope("root\CIMV2")
        Dim query As New SelectQuery(String.Format("SELECT * FROM Win32_Process Where {0} = '{1}'", condition.Key, condition.Value))
        Dim options As New EnumerationOptions With {.ReturnImmediately = True}

        Using wmi As New ManagementObjectSearcher(scope, query, options)

            Using objCol As ManagementObjectCollection = wmi.Get

                Return objCol.Cast(Of ManagementObject).ToArray

            End Using

        End Using

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Closes the first ocurrence found of a running process with the specified name.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="processName">
    ''' The process name.
    ''' </param>
    ''' 
    ''' <param name="throwOnProcessNotFound">
    ''' If <see langword="True"/>, throws an <see cref="ArgumentException"/> exception if any process was found with the specified name.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' <see langword="True"/> if the close message was successfully sent; 
    ''' <see langword="False"/> if the found process does not have a main window, 
    ''' or if the main window is disabled (for example if a modal dialog is being shown),
    ''' or if <paramref name="throwOnProcessNotFound"/> is <see langword="False"/> and any process was found with the specified name.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function CloseProcess(ByVal processName As String,
                                 Optional throwOnProcessNotFound As Boolean = False) As Boolean

        Return ProcessUtil.GetProcessValue(Of Boolean)(processName, Function(p As Process) p.CloseMainWindow, throwOnProcessNotFound)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Closes all the ocurrences found of running processes with the specified name.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="processName">
    ''' The process name.
    ''' </param>
    ''' 
    ''' <param name="throwOnProcessNotFound">
    ''' If <see langword="True"/>, throws an <see cref="ArgumentException"/> exception if any process was found with the specified name.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' <see langword="True"/> if the close message was successfully sent for all the process ocurrences; 
    ''' <see langword="False"/> if the at least one of the found process does not have a main window, 
    ''' or if the main window is disabled (for example if a modal dialog is being shown),
    ''' or if <paramref name="throwOnProcessNotFound"/> is <see langword="False"/> and any process was found with the specified name.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentException">
    ''' Any process found with the specified name.;processName
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function CloseProcesses(ByVal processName As String,
                                   Optional throwOnProcessNotFound As Boolean = False) As Boolean

        Dim processes As Process() = Process.GetProcessesByName(ProcessUtil.FixProcessName(processName))

        If Not (processes.Any) AndAlso (throwOnProcessNotFound) Then
            Throw New ArgumentException(message:="Any process found with the specified name.", paramName:="processName")

        Else
            Dim failCount As Integer

            processes.ToList.ForEach(Sub(p As Process)

                                         Try
                                             Dim result As Boolean = p.CloseMainWindow
                                             If Not (result) Then
                                                 failCount += 1
                                             End If

                                         Catch ' ex As Exception
                                             failCount += 1

                                         Finally
                                             p.Close()

                                         End Try

                                     End Sub)

            Return (failCount = 0)

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Kills the first ocurrence found of a running process with the specified name.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="processName">
    ''' The process name.
    ''' </param>
    ''' 
    ''' <param name="killChildProcesses">
    ''' If <see langword="True"/>, also kills any child process of the found process.
    ''' </param>
    ''' 
    ''' <param name="throwOnProcessNotFound">
    ''' If <see langword="True"/>, throws an <see cref="ArgumentException"/> exception if any process was found with the specified name.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' if successful <see langword="True"/>; if failed <see langword="True"/> or
    ''' if <paramref name="throwOnProcessNotFound"/> is <see langword="False"/> and any process was found with the specified name.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentException">
    ''' Any process found with the specified name.;processName
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function KillProcess(ByVal processName As String,
                                Optional ByVal killChildProcesses As Boolean = False,
                                Optional throwOnProcessNotFound As Boolean = False) As Boolean

        Using p As Process = Process.GetProcessesByName(ProcessUtil.FixProcessName(processName)).FirstOrDefault

            Select Case p IsNot Nothing

                Case True

                    If killChildProcesses Then ' Kill it's children processes.

                        Dim query As New SelectQuery(String.Format("Select * From Win32_Process Where ParentProcessID={0}", CStr(p.Id)))

                        Using childs As New ManagementObjectSearcher(query)

                            For Each mo As ManagementObject In childs.Get()

                                ProcessUtil.KillProcess(DirectCast(mo("Name"), String), killChildProcesses:=True)
                                Try
                                    If Not p.HasExited Then
                                        p.Kill()
                                    End If
                                Catch ex As Exception
                                    Throw
                                End Try

                                mo.Dispose()
                            Next mo

                        End Using ' Childs
                        Return True

                    Else
                        Try
                            If Not p.HasExited Then
                                p.Kill()
                            End If
                        Catch ex As Exception
                            Throw
                        End Try
                        Return True

                    End If ' KillChildProcesses

                Case Else
                    If (throwOnProcessNotFound) Then
                        Throw New ArgumentException(message:="Any process found with the specified name.", paramName:="processName")
                    End If

                    Return False

            End Select

        End Using

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Kills all the ocurrence found of running processes with the specified name.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="processName">
    ''' The process name.
    ''' </param>
    ''' 
    ''' <param name="killChildProcesses">
    ''' If <see langword="True"/>, also kills any child process of the found processes.
    ''' </param>
    ''' 
    ''' <param name="throwOnProcessNotFound">
    ''' If <see langword="True"/>, throws an <see cref="ArgumentException"/> exception if any process was found with the specified name.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' if successful <see langword="True"/>; 
    ''' <see langword="False"/> if the at least one of the found process does not have a main window, 
    ''' or if <paramref name="throwOnProcessNotFound"/> is <see langword="False"/> and any process was found with the specified name.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentException">
    ''' Any process found with the specified name.;processName
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function KillProcesses(ByVal processName As String,
                                  Optional ByVal killChildProcesses As Boolean = False,
                                  Optional throwOnProcessNotFound As Boolean = False) As Boolean

        Dim processes As Process() = Process.GetProcessesByName(ProcessUtil.FixProcessName(processName))
        Dim failCount As Integer

        processes.ToList.ForEach(Sub(p As Process)

                                     ProcessUtil.KillProcess(p.ProcessName, killChildProcesses)

                                     Try
                                         If Not p.HasExited Then
                                             p.Kill()
                                         End If

                                     Catch ' ex As Win32Exception
                                         failCount += 1

                                     End Try

                                 End Sub)

        Return (failCount = 0)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Blocks the caller thread until te specified process has been fully loaded.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim p As Process = Process.Start("C:\Program Files\Photoshop\Photoshop.exe")
    ''' ProcessUtil.WaitUntilLoaded(p, timeOut:=1000)
    ''' MsgBox("Program UI had been loaded!")
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="p">
    ''' The process.
    ''' </param>
    ''' 
    ''' <param name="timeOut">
    ''' The logic timeout, in milliseconds.
    ''' <para></para>
    ''' Recommended value is between <c>1000</c> and <c>2000</c>.
    ''' Smaller values could give unexpected results.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub WaitUntilLoaded(ByVal p As Process,
                               Optional ByVal timeOut As Integer = 1500)

        Dim cpuChanged As Boolean = True
        Dim memChanged As Boolean = True

        Dim oldCpu As Double, newCpu As Double
        Dim oldMem As Long, newMem As Long

        While ((cpuChanged OrElse memChanged) = True)

            Do Until (p.TotalProcessorTime.TotalMilliseconds <> 0.0R)
                Thread.Sleep(10)
            Loop

            If (p Is Nothing) OrElse (p.HasExited) Then
                Exit While

            Else
                newMem = p.PrivateMemorySize64
                memChanged = (newMem <> oldMem)
                oldMem = newMem

                newCpu = p.TotalProcessorTime.TotalMilliseconds
                cpuChanged = (newCpu <> oldCpu)
                oldCpu = newCpu

                Thread.Sleep(timeOut)

            End If

        End While

    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Aynchronouslly waits until te specified process has been fully loaded,
    ''' then invokes the specified callback procedure.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Private Sub Test()
    ''' 
    '''     Dim p As Process = Process.Start("C:\Program Files\Photoshop\Photoshop.exe")
    '''     ProcessUtil.WaitUntilLoadedAsync(p, AddressOf WaitUntilLoadedAsync_CallBack, timeOut:=1000)
    ''' 
    ''' End Sub
    ''' 
    ''' Private Sub WaitUntilLoadedAsync_CallBack()
    ''' 
    '''     MsgBox("Program UI had been loaded!")
    ''' 
    ''' End Sub
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="p">
    ''' The process.
    ''' </param>
    ''' 
    ''' <param name="callback">
    ''' A <see cref="System.Action"/> delegate which will be invoked inmmediately after the process is fully loaded.
    ''' <para></para>
    ''' When <see cref="ProcessUtil.WaitUntilLoadedAsync"/> is called from a UI thread, 
    ''' <paramref name="callback"/> is invoked on the same UI thread.
    ''' </param>
    ''' 
    ''' <param name="timeOut">
    ''' The logic timeout, in milliseconds.
    ''' <para></para>
    ''' Recommended value is between <c>1000</c> and <c>2000</c>.
    ''' Smaller values could give unexpected results.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub WaitUntilLoadedAsync(ByVal p As Process,
                                    ByVal callback As Action,
                                    Optional ByVal timeOut As Integer = 1500)

        Dim tScheduler As TaskScheduler

        ' If the current thread is a UI thread then run the callback synchronouslly on the UI thread...
        If Application.MessageLoop Then
            tScheduler = TaskScheduler.FromCurrentSynchronizationContext()

        Else
            tScheduler = TaskScheduler.Default

        End If

        Task.Factory.StartNew(Sub() ProcessUtil.WaitUntilLoaded(p, timeOut)).
                     ContinueWith(Sub(t) callback.Invoke(), CancellationToken.None, TaskContinuationOptions.None, tScheduler)

    End Sub

#End Region

#Region " Private Methods "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Fixes the name of a process by removing the .exe file extension.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="processName">
    ''' The process name.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The fixed process name.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' processName
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Private Function FixProcessName(ByVal processName As String) As String

        If String.IsNullOrEmpty(processName) Then
            Throw New ArgumentNullException(paramName:="processName")

        Else
            If processName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) Then
                processName = processName.Remove(processName.LastIndexOf(".exe", StringComparison.OrdinalIgnoreCase))
            End If

            Return processName

        End If

    End Function

#End Region

End Module

#End Region
