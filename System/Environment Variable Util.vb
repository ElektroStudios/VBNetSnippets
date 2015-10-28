
' ***********************************************************************
' Author   : Elektro
' Modified : 28-October-2015
' ***********************************************************************
' <copyright file="Environment Variable Util.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Public Members Summary "

#Region " Types "

' EnvironmentVariableUtil.VariableInfo <Serializable>

#End Region

#Region " Constructors "

' EnvironmentVariableUtil.VariableInfo.New(EnvironmentVariableUtil.VariableScope, String, String)

#End Region

#Region " Enumerations "

' EnvironmentVariableUtil.VariableScope As Integer

#End Region

#Region " Properties "

' EnvironmentVariableUtil.CurrentVariables(EnvironmentVariableUtil.VariableScope) As ReadOnlyCollection(Of EnvironmentVariableUtil.VariableInfo)
' EnvironmentVariableUtil.VariableInfo.Name As String
' EnvironmentVariableUtil.VariableInfo.Value As String
' EnvironmentVariableUtil.VariableInfo.Scope As EnvironmentVariableUtil.VariableScope

#End Region

#Region " Functions "

' EnvironmentVariableUtil.GetValue(EnvironmentVariableUtil.VariableScope, String, Boolean) As Opt: Boolean
' EnvironmentVariableUtil.GetVariableInfo(EnvironmentVariableUtil.VariableScope, String, Opt: Boolean) As EnvironmentVariableUtil.VariableInfo

#End Region

#Region " Methods "

' EnvironmentVariableUtil.RegisterVariable(EnvironmentVariableUtil.VariableInfo, Opt: Boolean)
' EnvironmentVariableUtil.RegisterVariable(EnvironmentVariableUtil.VariableScope, String, String, Opt: Boolean)
' EnvironmentVariableUtil.UnregisterVariable(EnvironmentVariableUtil.VariableScope, String, Opt: Boolean)
' EnvironmentVariableUtil.UnregisterVariable(EnvironmentVariableUtil.VariableInfo, Opt: Boolean)

#End Region

#End Region

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports System.Runtime.InteropServices
Imports Microsoft.Win32

#End Region

#Region " Environment Variable Util "

''' ----------------------------------------------------------------------------------------------------
''' <summary>
''' Contains related Environment Variable utilities.
''' </summary>
''' ----------------------------------------------------------------------------------------------------
Public Module EnvironmentVariableUtil

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
        ''' Sends the specified message to one or more windows.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="hWnd">
        ''' A handle to the window whose window procedure will receive the message.
        ''' 
        ''' If this parameter is <see cref="EnvironmentVariableUtil.NativeMethods.WindowsMessages.HWNDBROADCAST"/>, 
        ''' the message is sent to all top-level windows in the system, including disabled or invisible unowned windows.
        ''' 
        ''' The function does not return until each window has timed out. 
        ''' Therefore, the total wait time can be up to the value of uTimeout multiplied by the number of top-level windows.
        ''' </param>
        ''' 
        ''' <param name="Msg">
        ''' The message to be sent.
        ''' For lists of the system-provided messages, see System-Defined Messages:
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms644927%28v=vs.85%29.aspx#system_defined"/>
        ''' </param>
        ''' 
        ''' <param name="wParam">
        ''' Any additional message-specific information.
        ''' </param>
        ''' 
        ''' <param name="lParam">
        ''' Any additional message-specific information.
        ''' </param>
        ''' 
        ''' <param name="fuFlags">
        ''' The behavior of this function.
        ''' </param>
        ''' 
        ''' <param name="uTimeout">
        ''' The duration of the time-out period, in milliseconds. 
        ''' If the message is a broadcast message, each window can use the full time-out period. 
        ''' For example, if you specify a five second time-out period and there are three top-level windows that fail to process the message, 
        ''' you could have up to a 15 second delay.
        ''' </param>
        ''' 
        ''' <param name="lpdwResult">
        ''' The result of the message processing. 
        ''' The value of this parameter depends on the message that is specified.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' If the function succeeds, the return value is nonzero. 
        ''' 
        ''' If the function fails or times out, the return value is 0.
        ''' 
        ''' <see cref="EnvironmentVariableUtil.NativeMethods.SendMessageTimeout"/> does not provide information about 
        ''' individual windows timing out if <see cref="EnvironmentVariableUtil.NativeMethods.WindowsMessages.HWNDBROADCAST"/> is used.
        ''' 
        ''' To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        ''' If <see cref="Marshal.GetLastWin32Error"/> returns ERROR_TIMEOUT, then the function timed out.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms644952%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto, BestFitMapping:=False, ThrowOnUnmappableChar:=True)>
        Friend Shared Function SendMessageTimeout(
                               ByVal hwnd As IntPtr,
                               ByVal msg As Integer,
                               ByVal wParam As IntPtr,
                               ByVal lParam As String,
                               ByVal fuFlags As SendMessageTimeoutFlags,
                               ByVal uTimeout As Integer,
                         <Out> ByRef lpdwResult As IntPtr
        ) As IntPtr
        End Function

#End Region

#Region " Enumerations "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Flags for <paramref name="fuFlags"/> parameter of <see cref="EnvironmentVariableUtil.NativeMethods.SendMessageTimeout"/> function.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms644952%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        <Flags()>
        Friend Enum SendMessageTimeoutFlags As Integer

            ''' <summary>
            ''' The calling thread is not prevented from processing other requests while waiting for the function to return.
            ''' </summary>
            Normal = &H0

            ''' <summary>
            ''' Prevents the calling thread from processing any other requests until the function returns.
            ''' </summary>
            Block = &H1

            ''' <summary>
            ''' The function returns without waiting for the time-out period to elapse if the receiving thread appears to not respond or "hangs."
            ''' </summary>
            AbortIfHung = &H2

            ''' <summary>
            ''' The function does not enforce the time-out period  as long as the receiving thread is processing messages.
            ''' </summary>
            NoTimeoutIfNotHung = &H8

            ''' <summary>
            ''' The function should return 0 if the receiving window is destroyed or its owning thread dies while the message is being processed.
            ''' </summary>
            ErrorOnExit = &H20

        End Enum

        ''' <summary>
        ''' The system sends or posts a system-defined message when it communicates with an application. 
        ''' It uses these messages to control the operations of applications and to provide input and other information for applications to process. 
        ''' An application can also send or post system-defined messages.
        ''' Applications generally use these messages to control the operation of control windows created by using preregistered window classes.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms644927%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        Friend Enum WindowsMessages As Integer

            ''' ----------------------------------------------------------------------------------------------------
            ''' <summary>
            ''' The message is sent to all top-level windows in the system, including disabled or invisible unowned windows. 
            ''' The function does not return until each window has timed out. 
            ''' Therefore, the total wait time can be up to the value of uTimeout multiplied by the number of top-level windows.
            ''' </summary>
            ''' ----------------------------------------------------------------------------------------------------
            ''' <remarks>
            ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms644952%28v=vs.85%29.aspx"/>
            ''' </remarks>
            ''' ----------------------------------------------------------------------------------------------------
            HwndBroadcast = &HFFFF&

            ''' ----------------------------------------------------------------------------------------------------
            ''' <summary>
            ''' A message that is sent to all top-level windows when 
            ''' the SystemParametersInfo function changes a system-wide setting or when policy settings have changed.
            ''' 
            ''' Applications should send <see cref="EnvironmentVariableUtil.NativeMethods.WindowsMessages.WMSETTINGCHANGE"/> to all top-level windows when 
            ''' they make changes to system parameters
            ''' (This message cannot be sent directly to a single window.)
            ''' 
            ''' To send the <see cref="EnvironmentVariableUtil.NativeMethods.WindowsMessages.WMSETTINGCHANGE"/> message to all top-level windows, 
            ''' use the <see cref="EnvironmentVariableUtil.NativeMethods.SendMessageTimeout"/> function with the <paramref name="hwnd"/> parameter set to 
            ''' <see cref="EnvironmentVariableUtil.NativeMethods.WindowsMessages.HWNDBROADCAST"/>.
            ''' </summary>
            ''' ----------------------------------------------------------------------------------------------------
            ''' <remarks>
            ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms725497%28v=vs.85%29.aspx"/>
            ''' </remarks>
            ''' ----------------------------------------------------------------------------------------------------
            WMSettingchange = &H1A

        End Enum

#End Region

    End Class

#End Region

#Region " Types "

#Region " VariableInfo "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Defines the info of a Windows environment Variable.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <Serializable>
    Public Structure VariableInfo

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the variable scope.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The variable scope.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Scope As EnvironmentVariableUtil.VariableScope
            Get
                Return Me.scopeB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' The variable scope.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly scopeB As EnvironmentVariableUtil.VariableScope

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the variable name.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The variable name.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Name As String
            Get
                Return Me.nameB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' The variable name.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly nameB As String

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the variable value.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The variable value.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Value As String
            Get
                Return Me.valueB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' The variable value.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly valueB As String

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="VariableInfo"/> structure.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="scope">
        ''' The variable scope.
        ''' </param>
        ''' 
        ''' <param name="name">
        ''' The variable name.
        ''' </param>
        ''' 
        ''' <param name="value">
        ''' The variable value.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New(ByVal scope As EnvironmentVariableUtil.VariableScope, ByVal name As String, ByVal value As String)

            Me.scopeB = scope
            Me.nameB = name
            Me.valueB = value

        End Sub

#End Region

    End Structure

#End Region

#End Region

#Region " Enumerations "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Specified an environment scope (registry root key).
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    Public Enum VariableScope As Integer

        ''' <summary>
        ''' This reffers to the commonly known "HKLM" or "HKEY_LOCAL_MACHINE" registry root key.
        ''' Changes made on this registry root key will affect all users.
        ''' </summary>
        Machine = 0

        ''' <summary>
        ''' Current User, this reffers to the commonly known "HKCU" or "HKEY_CURRENT_USER" registry root key.
        ''' Changes made on this registry root key will affect the current user.
        ''' </summary>
        CurrentUser = 1

    End Enum

#End Region

#Region " Properties "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Gets a <see cref="IEnumerable(Of EnvironmentVariableUtil.VariableInfo)"/> collection with the Environment Variable of the specified environment user.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' For Each envVar As EnvironmentVariableUtil.VariableInfo In EnvironmentVariableUtil.CurrentVariables(EnvironmentVariableUtil.VariableScope.CurrentUser)
    ''' 
    '''     Console.WriteLine(String.Format("Name:{0}; Value:{1}", envVar.Name, envVar.Value))
    ''' 
    ''' Next envVar
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <value>
    ''' A <see cref="IEnumerable(Of EnvironmentVariableUtil.VariableInfo)"/> collection with the Environment Variable.
    ''' </value>
    ''' ----------------------------------------------------------------------------------------------------
    Public ReadOnly Property CurrentVariables(ByVal scope As EnvironmentVariableUtil.VariableScope) As ReadOnlyCollection(Of EnvironmentVariableUtil.VariableInfo)
        <DebuggerStepThrough>
        Get
            Return New ReadOnlyCollection(Of EnvironmentVariableUtil.VariableInfo)(EnvironmentVariableUtil.GetEnvironmentVariables(scope).ToList)
        End Get
    End Property

#End Region

#Region " Public Methods "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Registers a Windows environment variable.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' EnvironmentVariableUtil.RegisterVariable(EnvironmentVariableUtil.VariableScope.CurrentUser, 
    '''                                           "VariableName", "Elektro is the best!", 
    '''                                           throwOnExistingVariable:=True)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="scope">
    ''' The environment scope that will owns the variable.
    ''' </param>
    ''' 
    ''' <param name="name">
    ''' The variable name.
    ''' </param>
    ''' 
    ''' <param name="value">
    ''' The variable value.
    ''' </param>
    ''' 
    ''' <param name="throwOnExistingVariable">
    ''' If <see langword="True"/>, raises an exception if the variable already exists.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' name
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentException">
    ''' Invalid enumeration value;scope
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentException">
    ''' The specified variable already exists.;name
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub RegisterVariable(ByVal scope As EnvironmentVariableUtil.VariableScope,
                                ByVal name As String,
                                ByVal value As String,
                                Optional ByVal throwOnExistingVariable As Boolean = False)

        If String.IsNullOrWhiteSpace(name) Then
            Throw New ArgumentNullException(paramName:="name")

        Else
            Dim regKey As RegistryKey
            Dim regPath As String

            Select Case scope

                Case EnvironmentVariableUtil.VariableScope.Machine
                    regKey = Registry.LocalMachine
                    regPath = "SYSTEM\CurrentControlSet\Control\Session Manager\Environment\"

                Case EnvironmentVariableUtil.VariableScope.CurrentUser
                    regKey = Registry.CurrentUser
                    regPath = "Environment\"

                Case Else
                    Throw New ArgumentException(message:="Invalid enumeration value.", paramName:="scope")

            End Select

            Using regKey

                If (throwOnExistingVariable) AndAlso
                   (regKey.OpenSubKey(regPath, writable:=False).GetValueNames.
                               Any(Function(varName As String) varName.ToLower.Equals(name, StringComparison.OrdinalIgnoreCase))) Then

                    Throw New ArgumentException(message:="The specified variable already exists.", paramName:="name")

                Else
                    regKey.OpenSubKey(regPath, writable:=True).SetValue(name, value, RegistryValueKind.String)
                    EnvironmentVariableUtil.NotifyRegistryChange("Environment")

                End If

            End Using

        End If

    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Registers a Windows environment variable.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' EnvironmentVariableUtil.RegisterVariable(
    '''     New EnvironmentVariableUtil.VariableInfo(EnvironmentVariableUtil.VariableScope.CurrentUser, 
    '''                                                          "VariableName", "Elektro is the best!"),
    '''                                                          throwOnExistingVariable:=True)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="variableInfo">
    ''' A <see cref="EnvironmentVariableUtil.VariableInfo"/> object that contains the variable data.
    ''' </param>
    ''' 
    ''' <param name="throwOnExistingVariable">
    ''' If <see langword="True"/>, raises an exception if the variable already exists.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub RegisterVariable(ByVal variableInfo As EnvironmentVariableUtil.VariableInfo,
                                Optional ByVal throwOnExistingVariable As Boolean = False)

        EnvironmentVariableUtil.RegisterVariable(variableInfo.Scope, variableInfo.Name, variableInfo.Value, throwOnExistingVariable)

    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Unregisters a Windows environment variable.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' EnvironmentVariableUtil.UnregisterVariable(EnvironmentVariableUtil.VariableScope.CurrentUser, 
    '''                                             "VariableName", throwOnMissingVariable:=True)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="scope">
    ''' The environment scope that owns the variable.
    ''' </param>
    ''' 
    ''' <param name="name">
    ''' The variable name.
    ''' </param>
    ''' 
    ''' <param name="throwOnMissingVariable">
    ''' If <see langword="True"/>, raises an exception if the variable doesn't exists.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' name
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentException">
    ''' Invalid enumeration value;scope
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentException">
    ''' The specified variable doesn't exists.;name
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub UnregisterVariable(ByVal scope As EnvironmentVariableUtil.VariableScope,
                                  ByVal name As String,
                                  Optional throwOnMissingVariable As Boolean = False)

        If String.IsNullOrWhiteSpace(name) Then
            Throw New ArgumentNullException(paramName:="name")

        Else
            Dim regKey As RegistryKey
            Dim regPath As String

            Select Case scope

                Case EnvironmentVariableUtil.VariableScope.Machine
                    regKey = Registry.LocalMachine
                    regPath = "SYSTEM\CurrentControlSet\Control\Session Manager\Environment\"

                Case EnvironmentVariableUtil.VariableScope.CurrentUser
                    regKey = Registry.CurrentUser
                    regPath = "Environment\"

                Case Else
                    Throw New ArgumentException(message:="Invalid enumeration value.", paramName:="scope")

            End Select

            Using regKey

                If (throwOnMissingVariable) AndAlso
                   Not (regKey.OpenSubKey(regPath, writable:=False).GetValueNames.
                               Any(Function(varName As String) varName.ToLower.Equals(name, StringComparison.OrdinalIgnoreCase))) Then

                    Throw New ArgumentException(message:="The specified variable doesn't exists.", paramName:="name")

                Else
                    regKey.OpenSubKey(regPath, writable:=True).DeleteValue(name, throwOnMissingVariable)
                    EnvironmentVariableUtil.NotifyRegistryChange("Environment")

                End If

            End Using

        End If

    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Unregisters a Windows environment variable.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' EnvironmentVariableUtil.UnregisterVariable(EnvironmentVariableUtil.VariableScope.CurrentUser, 
    '''                                             "VariableName", throwOnMissingVariable:=True)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="variableInfo">
    ''' A <see cref="EnvironmentVariableUtil.VariableInfo"/> object that contains the variable data.
    ''' </param>
    ''' 
    ''' <param name="throwOnMissingVariable">
    ''' If <see langword="True"/>, raises an exception if the variable doesn't exists.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' name
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentException">
    ''' Invalid enumeration value;scope
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentException">
    ''' The specified variable doesn't exists.;name
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub UnregisterVariable(ByVal variableInfo As EnvironmentVariableUtil.VariableInfo,
                                  Optional throwOnMissingVariable As Boolean = False)

        EnvironmentVariableUtil.UnregisterVariable(variableInfo.Scope, variableInfo.Name, throwOnMissingVariable)

    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Finds an environment variable and returns a <see cref="EnvironmentVariableUtil.VariableInfo"/> object that contains the variable data.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exanple>
    ''' Dim envVarInfo As EnvironmentVariableUtil.VariableInfo =
    '''     EnvironmentVariableUtil.GetVariableInfo(EnvironmentVariableUtil.VariableScope.CurrentUser, 
    '''                                              "System32", throwOnMissingVariable:=False)
    ''' </exanple>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="scope">
    ''' The environment scope that owns the variable.
    ''' </param>
    ''' 
    ''' <param name="name">
    ''' The variable name.
    ''' </param>
    ''' 
    ''' <param name="throwOnMissingVariable">
    ''' If <see langword="True"/>, raises an exception if the variable doesn't exists.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' name
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentException">
    ''' Invalid enumeration value;scope
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentException">
    ''' The specified variable doesn't exists.;name
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' A <see cref="EnvironmentVariableUtil.VariableInfo"/> object that contains the variable data.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function GetVariableInfo(ByVal scope As EnvironmentVariableUtil.VariableScope,
                                    ByVal name As String,
                                    Optional throwOnMissingVariable As Boolean = False) As EnvironmentVariableUtil.VariableInfo

        If String.IsNullOrWhiteSpace(name) Then
            Throw New ArgumentNullException(paramName:="name")

        Else
            Dim regKey As RegistryKey
            Dim regPath As String

            Select Case scope

                Case EnvironmentVariableUtil.VariableScope.Machine
                    regKey = Registry.LocalMachine
                    regPath = "SYSTEM\CurrentControlSet\Control\Session Manager\Environment\"

                Case EnvironmentVariableUtil.VariableScope.CurrentUser
                    regKey = Registry.CurrentUser
                    regPath = "Environment\"

                Case Else
                    Throw New ArgumentException(message:="Invalid enumeration value.", paramName:="scope")

            End Select

            Using regKey

                If (throwOnMissingVariable) AndAlso
                   Not (regKey.OpenSubKey(regPath, writable:=False).GetValueNames.
                               Any(Function(varName As String) varName.ToLower.Equals(name, StringComparison.OrdinalIgnoreCase))) Then

                    Throw New ArgumentException(message:="The specified variable doesn't exists.", paramName:="name")

                Else
                    regKey = regKey.OpenSubKey(regPath, writable:=False)

                    Return (From valueName As String In regKey.GetValueNames
                            Where valueName.Equals(name, StringComparison.OrdinalIgnoreCase)
                            Select New EnvironmentVariableUtil.VariableInfo(
                                       scope, name:=valueName, value:=CStr(regKey.GetValue(valueName, "", RegistryValueOptions.None)))
                            ).FirstOrDefault

                End If

            End Using

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Returns the value of the specified environment variable.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' EnvironmentVariableUtil.GetValue(EnvironmentVariableUtil.VariableScope.CurrentUser, "System32", throwOnMissingVariable:=True)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="scope">
    ''' The environment scope that owns the variable.
    ''' </param>
    ''' 
    ''' <param name="name">
    ''' The variable name.
    ''' </param>
    ''' 
    ''' <param name="throwOnMissingVariable">
    ''' If <see langword="True"/>, raises an exception if the variable doesn't exists.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' name
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentException">
    ''' Invalid enumeration value;scope
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentException">
    ''' The specified variable doesn't exists.;name
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The value of the specified environment variable.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function GetValue(ByVal scope As EnvironmentVariableUtil.VariableScope,
                             ByVal name As String,
                             Optional throwOnMissingVariable As Boolean = False) As String

        If String.IsNullOrWhiteSpace(name) Then
            Throw New ArgumentNullException(paramName:="name")

        Else
            Dim regKey As RegistryKey
            Dim regPath As String

            Select Case scope

                Case EnvironmentVariableUtil.VariableScope.Machine
                    regKey = Registry.LocalMachine
                    regPath = "SYSTEM\CurrentControlSet\Control\Session Manager\Environment\"

                Case EnvironmentVariableUtil.VariableScope.CurrentUser
                    regKey = Registry.CurrentUser
                    regPath = "Environment\"

                Case Else
                    Throw New ArgumentException(message:="Invalid enumeration value.", paramName:="scope")

            End Select

            Using regKey

                If (throwOnMissingVariable) AndAlso
                   Not (regKey.OpenSubKey(regPath, writable:=False).GetValueNames.
                               Any(Function(varName As String) varName.ToLower.Equals(name, StringComparison.OrdinalIgnoreCase))) Then

                    Throw New ArgumentException(message:="The specified variable doesn't exists.", paramName:="name")

                Else
                    Return CStr(regKey.OpenSubKey(regPath, writable:=False).GetValue(name, "", RegistryValueOptions.None))

                End If

            End Using

        End If

    End Function

#End Region

#Region " Private Methods "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Gets a <see cref="IEnumerable(Of EnvironmentVariableUtil.VariableInfo)"/> collection with the 
    ''' Environment Variable of the specified environment user.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="scope">
    ''' The environment scope that owns the variable.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentException">
    ''' Invalid enumeration value;scope
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' A <see cref="IEnumerable(Of EnvironmentVariableUtil.VariableInfo)"/> collection with the Environment Variable.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Private Function GetEnvironmentVariables(ByVal scope As EnvironmentVariableUtil.VariableScope) As IEnumerable(Of EnvironmentVariableUtil.VariableInfo)

        Dim regKey As RegistryKey
        Dim regPath As String

        Select Case scope

            Case EnvironmentVariableUtil.VariableScope.Machine
                regKey = Registry.LocalMachine
                regPath = "SYSTEM\CurrentControlSet\Control\Session Manager\Environment\"

            Case EnvironmentVariableUtil.VariableScope.CurrentUser
                regKey = Registry.CurrentUser
                regPath = "Environment\"

            Case Else
                Throw New ArgumentException(message:="Invalid enumeration value.", paramName:="scope")

        End Select

        Using regKey

            regKey = regKey.OpenSubKey(regPath, writable:=False)

            Return From valueName As String In regKey.GetValueNames
                   Select New EnvironmentVariableUtil.VariableInfo(
                              scope, name:=valueName, value:=CStr(regKey.GetValue(valueName, "", RegistryValueOptions.None)))

        End Using

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Notifies the system to update after a registry change.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="keyName">
    ''' A string that indicates the area containing the system parameter that was changed.
    ''' 
    ''' This string can be the name of a registry key or the name of a section in the Win.ini file. 
    ''' 
    ''' When the string is a registry name, it typically indicates only the leaf node in the registry, not the full path.
    ''' 
    ''' To effect a change in the policy settings, this parameter points to the string "Policy".
    ''' To effect a change in the locale settings, this parameter points to the string "intl".
    ''' To effect a change in the Environment Variable for the system or the user, this parameter points to the string "Environment".
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms725497%28v=vs.85%29.aspx"/>
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Private Sub NotifyRegistryChange(ByVal keyName As String)

        EnvironmentVariableUtil.NativeMethods.SendMessageTimeout(
            New IntPtr(EnvironmentVariableUtil.NativeMethods.WindowsMessages.HwndBroadcast),
            CInt(EnvironmentVariableUtil.NativeMethods.WindowsMessages.WMSettingchange),
            New IntPtr(0),
            keyName,
            EnvironmentVariableUtil.NativeMethods.SendMessageTimeoutFlags.AbortIfHung,
            1,
            IntPtr.Zero)

    End Sub

#End Region

#Region " Hidden Methods "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Determines whether the specified System.Object instances are considered equal.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <EditorBrowsable(EditorBrowsableState.Never)>
    Public Function Equals(ByVal obj As Object) As Boolean
        Return Nothing
    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Determines whether the specified System.Object instances are the same instance.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <EditorBrowsable(EditorBrowsableState.Never)>
    Public Function ReferenceEquals(ByVal objA As Object, ByVal objB As Object) As Boolean
        Return Nothing
    End Function

#End Region

End Module

#End Region
