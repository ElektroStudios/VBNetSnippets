' ***********************************************************************
' Author   : Elektro
' Modified : 26-October-2015
' ***********************************************************************
' <copyright file="Profilling Util.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Public Members Summary "

#Region " Types "

' ProfillingUtil.TestExecutionInfo

#End Region

#Region " Constructors "

' ProfillingUtil.TestExecutionInfo.New(TimeSpan, MethodInfo, Boolean, Exception)

#End Region

#Region " Properties "

' ProfillingUtil.Elapsed As TimeSpan
' ProfillingUtil.Method As MethodInfo
' ProfillingUtil.Success As Boolean
' ProfillingUtil.Exception As Exception

#End Region

#Region " Functions "

' ProfillingUtil.TestSuccess(Action) As Boolean
' ProfillingUtil.TestSuccessAsync(Action) As Task(Of Boolean)
' ProfillingUtil.TestTime(Action) As TestExecutionInfo
' ProfillingUtil.TestTimeAsync(Action) As Task(Of TestExecutionInfo)
' ProfillingUtil.InlineAssignHelper(Of T)(T, T) As T
' ProfillingUtil.InlineAssignHelper(Object, Object) As Object
' ProfillingUtil.FlushMemory As Boolean
' ProfillingUtil.FlushMemory(IntPtr) As Boolean

#End Region

#Region " Methods "

' ProfillingUtil.CollectGarbage

#End Region

#End Region

#Region " Usage Examples "

#Region " TestExecutionInfo "

#Region " Synchronous "

'Sub Test()
'
'    Dim successful As Boolean =
'        ProfillingUtil.TestSuccess(Sub() Convert.ToInt32("Hello World!"))
'
'    Dim teInfo As TestExecutionInfo =
'        ProfillingUtil.TestTime(Sub()
'                                    For x As Integer = 0 To 2500
'                                        Console.WriteLine(x)
'                                    Next x
'                                End Sub)
'
'    Dim sb As New StringBuilder
'    Select Case teInfo.Success
'
'        Case True
'            With sb ' Set an information message.
'                .AppendLine(String.Format("Method Name: {0}", teInfo.Method.Name))
'                .AppendLine()
'                .AppendLine(String.Format("Elapsed Time: {0}", teInfo.Elapsed.ToString("hh\:mm\:ss\:fff")))
'            End With
'            MessageBox.Show(sb.ToString, "Code Execution Measurer", MessageBoxButtons.OK, MessageBoxIcon.Information)
'
'        Case Else
'            With sb ' Set an error message.
'                .AppendLine("Exception occurred during code execution measuring.")
'                .AppendLine()
'                .AppendLine(String.Format("Method Name: {0}", teInfo.Method.Name))
'                .AppendLine()
'                .AppendLine(String.Format("Exception Type: {0}", teInfo.Exception.GetType.Name))
'                .AppendLine()
'                .AppendLine("Exception Message:")
'                .AppendLine(teInfo.Exception.Message)
'                .AppendLine()
'                .AppendLine("Exception Stack Trace:")
'                .AppendLine(teInfo.Exception.StackTrace)
'            End With
'            MessageBox.Show(sb.ToString, "Code Execution Measurer", MessageBoxButtons.OK, MessageBoxIcon.Error)
'
'    End Select
'
'End Sub

#End Region

#Region " Asynchronous "

' Sub Test()
'
'    Dim taskTestTime As Task(Of TestExecutionInfo) =
'        ProfillingUtil.TestTimeAsync(Sub()
'                                         For x As Integer = 0 To 5000
'                                             Console.WriteLine(x)
'                                         Next x
'                                     End Sub)
'
'    taskTestTime.ContinueWith(Sub() Me.ShowTestExecutionInfo(taskTestTime.Result))
'
'End Sub
'
'Private Sub ShowTestExecutionInfo(ByVal teInfo As TestExecutionInfo)
'
'    Dim sb As New StringBuilder
'    Select Case teInfo.Success
'
'        Case True
'            With sb ' Set an information message.
'                .AppendLine(String.Format("Method Name: {0}", teInfo.Method.Name))
'                .AppendLine()
'                .AppendLine(String.Format("Elapsed Time: {0}", teInfo.Elapsed.ToString("hh\:mm\:ss\:fff")))
'            End With
'            MessageBox.Show(sb.ToString, "Code Execution Measurer", MessageBoxButtons.OK, MessageBoxIcon.Information)
'
'        Case Else
'            With sb ' Set an error message.
'                .AppendLine("Exception occurred during code execution measuring.")
'                .AppendLine()
'                .AppendLine(String.Format("Method Name: {0}", teInfo.Method.Name))
'                .AppendLine()
'                .AppendLine(String.Format("Exception Type: {0}", teInfo.Exception.GetType.Name))
'                .AppendLine()
'                .AppendLine("Exception Message:")
'                .AppendLine(teInfo.Exception.Message)
'                .AppendLine()
'                .AppendLine("Exception Stack Trace:")
'                .AppendLine(teInfo.Exception.StackTrace)
'            End With
'            MessageBox.Show(sb.ToString, "Code Execution Measurer", MessageBoxButtons.OK, MessageBoxIcon.Error)
'
'    End Select
'
'End Sub

#End Region

#End Region

#End Region

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

' Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System
Imports System.Diagnostics
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Threading.Tasks

#End Region

#Region " Profilling Util "

''' ----------------------------------------------------------------------------------------------------
''' <summary>
''' Contains related profilling and unit testing utilities.
''' </summary>
''' ----------------------------------------------------------------------------------------------------
Public Module ProfillingUtil

#Region " P/Invoking "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Platform Invocation methods (P/Invoke), access unmanaged code.
    ''' This class does not suppress stack walks for unmanaged code permission.
    ''' <see cref="System.Security.SuppressUnmanagedCodeSecurityAttribute"/> must not be applied to this class.
    ''' This class is for methods that can be used anywhere because a stack walk will be performed.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' <see href="http://msdn.microsoft.com/en-us/library/ms182161.aspx"/>
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    Private NotInheritable Class NativeMethods

#Region " Functions "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sets the minimum and maximum working set sizes for the specified process.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="hProcess">
        ''' An <see cref="IntPtr"/> to the process whose working set sizes is to be set.
        ''' </param>
        ''' 
        ''' <param name="dwMinimumWorkingSetSize">
        ''' The minimum working set size for the process, in bytes.
        ''' The virtual memory manager attempts to keep at least this much memory resident in the process 
        ''' whenever the process is active.
        ''' 
        ''' This parameter must be greater than 0 but less than or equal to the maximum working set size.
        ''' The default size is 50 pages (for example, this is 204,800 bytes on systems with a 4K page size).
        ''' If the value is greater than 0 but less than 20 pages, the minimum value is set to 20 pages.
        ''' 
        ''' If both <paramref name="dwMinimumWorkingSetSize"/> and <paramref name="dwMaximumWorkingSetSize"/> have the value <c>IntPtr(-1)</c>, 
        ''' the function removes as many pages as possible from the working set of the specified process.
        ''' </param>
        ''' 
        ''' <param name="dwMaximumWorkingSetSize">
        ''' The maximum working set size for the process, in bytes. 
        ''' The virtual memory manager attempts to keep no more than this much memory resident in the process 
        ''' whenever the process is active and available memory is low.
        ''' 
        ''' This parameter must be greater than or equal to 13 pages (for example, 53,248 on systems with a 4K page size), 
        ''' and less than the system-wide maximum (number of available pages minus 512 pages). 
        ''' The default size is 345 pages (for example, this is 1,413,120 bytes on systems with a 4K page size).
        ''' 
        ''' If both <paramref name="dwMinimumWorkingSetSize"/> and <paramref name="dwMaximumWorkingSetSize"/> have the value <c>IntPtr(-1)</c>, 
        ''' the function removes as many pages as possible from the working set of the specified process.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if the function succeeds, <see langword="False"/> otherwise.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms686234%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
        Friend Shared Function SetProcessWorkingSetSize(
                               ByVal hProcess As IntPtr,
                               ByVal dwMinimumWorkingSetSize As IntPtr,
                               ByVal dwMaximumWorkingSetSize As IntPtr
        ) As Boolean
        End Function

#End Region

    End Class

#End Region

#Region " Types "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Defines the info of a code-execution test. 
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <Serializable>
    Public Structure TestExecutionInfo

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the elapsed execution time.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The elapsed execution time.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Elapsed As TimeSpan
            Get
                Return Me.elapsedB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' The elapsed time.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly elapsedB As TimeSpan

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the method metadata.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The method metadata.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Method As MethodInfo
            Get
                Return Me.methodB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' The method metadata.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly methodB As MethodInfo

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets a value indicating whether the execution of the method was successful.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' <see langword="True"/> if success; otherwise, <see langword="False"/>.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Success As Boolean
            Get
                Return Me.successB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' A value indicating whether the execution of the method was successful.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly successB As Boolean

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the <see cref="Exception"/> that occured during method execution, if any.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The <see cref="Exception"/> that occured during method execution, if any.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Exception As Exception
            Get
                Return Me.exceptionB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' The <see cref="Exception"/> that occured during method execution, if any.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly exceptionB As Exception
#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="TestExecutionInfo"/> struct.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="elapsed">
        ''' The elapsed execution time.
        ''' </param>
        ''' 
        ''' <param name="method">
        ''' The method metadata.
        ''' </param>
        ''' 
        ''' <param name="success">
        ''' A value indicating whether the execution of the method was successful.
        ''' </param>
        ''' 
        ''' <param name="exception">
        ''' The <see cref="Exception"/> that occured during method execution, if any.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New(ByVal elapsed As TimeSpan,
                       ByVal method As MethodInfo,
                       ByVal success As Boolean,
                       ByVal exception As Exception)

            Me.elapsedB = elapsed
            Me.methodB = method
            Me.successB = success
            Me.exceptionB = exception

        End Sub

#End Region

#Region " Hidden Members "

        ''' <summary>
        ''' Serves as a hash function for a particular type.
        ''' </summary>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Function GetHashCode() As Integer
            Return Nothing
        End Function

        ''' <summary>
        ''' Gets the System.Type of the current instance.
        ''' </summary>
        ''' <returns>The exact runtime type of the current instance.</returns>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Function [GetType]() As Type
            Return Nothing
        End Function

        ''' <summary>
        ''' Returns a String that represents the current object.
        ''' </summary>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Function ToString() As String
            Return Nothing
        End Function

#End Region

    End Structure

#End Region

#Region " Properties "

    ' ToDO: Add useful memory related properties (from "System.Diagnostincs.Process" Class or using the Performance counters)

#End Region

#Region " Public Methods "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Tests the execution of a <see cref="System.Action"/> then returns a value that indicates whether the execution was successful.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="action">
    ''' The <see cref="System.Action"/> to invoke.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' <see langword="True"/> if execution was successful, otherwise, <see langword="False"/>.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function TestSuccess(ByVal action As Action) As Boolean

        Try
            action.Invoke()
            Return True

        Catch
            Return False

        End Try

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Tests the execution of a <see cref="System.Action"/> then returns a value that indicates whether the execution was successful.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="action">
    ''' The <see cref="System.Action"/> to invoke.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' <see langword="True"/> if execution was successful, otherwise, <see langword="False"/>.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Async Function TestSuccessAsync(ByVal action As Action) As Task(Of Boolean)

        Dim taskTestSuccess As Task(Of Boolean) =
            Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                  Return ProfillingUtil.TestSuccess(action)
                                              End Function)

        Await taskTestSuccess
        Return taskTestSuccess.Result

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Tests the execution of a <see cref="System.Action"/> and measures the elapsed time.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Sub Test()
    ''' 
    '''     Dim successful As Boolean =
    '''         ProfillingUtil.TestSuccess(Sub() Convert.ToInt32("Hello World!"))
    ''' 
    '''     Dim teInfo As TestExecutionInfo =
    '''         ProfillingUtil.TestTime(Sub()
    '''                                     For x As Integer = 0 To 2500
    '''                                         Console.WriteLine(x)
    '''                                     Next x
    '''                                 End Sub)
    ''' 
    '''     Dim sb As New StringBuilder
    '''     Select Case teInfo.Success
    ''' 
    '''         Case True
    '''             With sb ' Set an information message.
    '''                 .AppendLine(String.Format("Method Name: {0}", teInfo.Method.Name))
    '''                 .AppendLine()
    '''                 .AppendLine(String.Format("Elapsed Time: {0}", teInfo.Elapsed.ToString("hh\:mm\:ss\:fff")))
    '''             End With
    '''             MessageBox.Show(sb.ToString, "Code Execution Measurer", MessageBoxButtons.OK, MessageBoxIcon.Information)
    ''' 
    '''         Case Else
    '''             With sb ' Set an error message.
    '''                 .AppendLine("Exception occurred during code execution measuring.")
    '''                 .AppendLine()
    '''                 .AppendLine(String.Format("Method Name: {0}", teInfo.Method.Name))
    '''                 .AppendLine()
    '''                 .AppendLine(String.Format("Exception Type: {0}", teInfo.Exception.GetType.Name))
    '''                 .AppendLine()
    '''                 .AppendLine("Exception Message:")
    '''                 .AppendLine(teInfo.Exception.Message)
    '''                 .AppendLine()
    '''                 .AppendLine("Exception Stack Trace:")
    '''                 .AppendLine(teInfo.Exception.StackTrace)
    '''             End With
    '''             MessageBox.Show(sb.ToString, "Code Execution Measurer", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''' 
    '''     End Select
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="action">
    ''' The <see cref="System.Action"/> to invoke.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' A <see cref="ProfillingUtil.TestExecutionInfo"/> instance that contains the test info.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function TestTime(ByVal action As Action) As TestExecutionInfo

        Dim success As Boolean = False
        Dim exception As Exception = Nothing
        Dim stw As New Stopwatch

        ' Start measuring time.
        stw.Start()

        Try ' Invoke the method.
            action.Invoke()
            success = True

        Catch ex As Exception
            ' Capture the exception details.
            exception = ex
            success = False

        Finally ' Ensure to stop measuring time.
            stw.Stop()

        End Try

        Return New TestExecutionInfo(elapsed:=stw.Elapsed, method:=action.Method, success:=success, exception:=exception)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Asynchronously tests the execution of a <see cref="System.Action"/> and measures the elapsed time.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Sub Test()
    ''' 
    '''     Dim taskTestTime As Task(Of TestExecutionInfo) =
    '''         ProfillingUtil.TestTimeAsync(Sub()
    '''                                          For x As Integer = 0 To 2500
    '''                                              Console.WriteLine(x)
    '''                                          Next x
    '''                                      End Sub)
    ''' 
    '''     taskTestTime.ContinueWith(Sub() Me.ShowTestExecutionInfo(taskTestTime.Result))
    ''' 
    ''' End Sub
    ''' 
    ''' Private Sub ShowTestExecutionInfo(ByVal teInfo As TestExecutionInfo)
    ''' 
    '''     Dim sb As New StringBuilder
    '''     Select Case teInfo.Success
    ''' 
    '''         Case True
    '''             With sb ' Set an information message.
    '''                 .AppendLine(String.Format("Method Name: {0}", teInfo.Method.Name))
    '''                 .AppendLine()
    '''                 .AppendLine(String.Format("Elapsed Time: {0}", teInfo.Elapsed.ToString("hh\:mm\:ss\:fff")))
    '''             End With
    '''             MessageBox.Show(sb.ToString, "Code Execution Measurer", MessageBoxButtons.OK, MessageBoxIcon.Information)
    ''' 
    '''         Case Else
    '''             With sb ' Set an error message.
    '''                 .AppendLine("Exception occurred during code execution measuring.")
    '''                 .AppendLine()
    '''                 .AppendLine(String.Format("Method Name: {0}", teInfo.Method.Name))
    '''                 .AppendLine()
    '''                 .AppendLine(String.Format("Exception Type: {0}", teInfo.Exception.GetType.Name))
    '''                 .AppendLine()
    '''                 .AppendLine("Exception Message:")
    '''                 .AppendLine(teInfo.Exception.Message)
    '''                 .AppendLine()
    '''                 .AppendLine("Exception Stack Trace:")
    '''                 .AppendLine(teInfo.Exception.StackTrace)
    '''             End With
    '''             MessageBox.Show(sb.ToString, "Code Execution Measurer", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''' 
    '''     End Select
    ''' 
    ''' End Sub
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="action">
    ''' The <see cref="System.Action"/> to invoke.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' An async <see cref="Task(Of ProfillingUtil.TestExecutionInfo)"/> object that contains the result of the test.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Async Function TestTimeAsync(ByVal action As Action) As Task(Of TestExecutionInfo)

        Dim taskTestTime As Task(Of TestExecutionInfo) =
            Task(Of TestExecutionInfo).Factory.StartNew(Function() As TestExecutionInfo
                                                            Return ProfillingUtil.TestTime(action)
                                                        End Function)

        Await taskTestTime
        Return taskTestTime.Result

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Helper function that assigns <paramref name="b"/> to <paramref name="a"/>, then returns <paramref name="b"/>.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <typeparam name="T">
    ''' </typeparam>
    ''' 
    ''' <param name="a">
    ''' The target by reference value.
    ''' </param>
    ''' 
    ''' <param name="b">
    ''' The value to return.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The value returned is <paramref name="b"/>.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function InlineAssignHelper(Of T)(ByRef a As T, ByVal b As T) As T
        a = b
        Return b
    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Helper function that assigns <paramref name="b"/> to <paramref name="a"/>, then returns <paramref name="b"/>.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="a">
    ''' The target by reference value.
    ''' </param>
    ''' 
    ''' <param name="b">
    ''' The value to return.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The value returned is <paramref name="b"/>.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function InlineAssignHelper(ByRef a As Object, ByVal b As Object) As Object
        a = b
        Return b
    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Invokes the GarbageCollector to occur immediately.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub CollectGarbage()

        GC.Collect()
        GC.WaitForPendingFinalizers()
        GC.WaitForFullGCApproach()
        GC.WaitForFullGCComplete()
        GC.Collect()

    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Flushes the memory of the current process.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' <see langword="True"/> if the function succeeds, <see langword="False"/> otherwise.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function FlushMemory() As Boolean

        Return ProfillingUtil.FlushMemory(Process.GetCurrentProcess.Handle)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Flushes the memory of the current process.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="handle">
    ''' The target process handle.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' <see langword="True"/> if the function succeeds, <see langword="False"/> otherwise.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function FlushMemory(ByVal handle As IntPtr) As Boolean

        ProfillingUtil.CollectGarbage()

        If (Environment.OSVersion.Platform = PlatformID.Win32NT) Then
            Return NativeMethods.SetProcessWorkingSetSize(handle, New IntPtr(-1), New IntPtr(-1))

        Else
            Return False

        End If

    End Function

#End Region

End Module

#End Region
