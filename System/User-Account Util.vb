' ***********************************************************************
' Author   : Elektro
' Modified : 24-October-2015
' ***********************************************************************
' <copyright file="User-Account Util.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Required References "

' System.DirectoryServices.AccountManagement.dll

#End Region

#Region " Public Members Summary "

#Region " Properties "

' UserAccountUtil.CurrentUser As UserPrincipal
' UserAccountUtil.CurrentUserIsAdmin As Boolean

#End Region

#Region " Functions "

' UserAccountUtil.Create(String, String, String, String, Boolean, Boolean) As UserPrincipal
' UserAccountUtil.FindProfilePath(SecurityIdentifier) As String
' UserAccountUtil.FindProfilePath(String) As String
' UserAccountUtil.FindSid(String) As SecurityIdentifier
' UserAccountUtil.FindUser(SecurityIdentifier) As UserPrincipal
' UserAccountUtil.FindUser(String) As UserPrincipal
' UserAccountUtil.FindUsername(SecurityIdentifier) As String
' UserAccountUtil.GetAllUsers() As List(Of UserPrincipal)
' UserAccountUtil.IsAdmin(String) As Boolean
' UserAccountUtil.IsMemberOfGroup(String, String) As Boolean
' UserAccountUtil.IsMemberOfGroup(String, WellKnownSidType) As Boolean

#End Region

#Region " Methods "

' UserAccountUtil.Add(String, String, String, String, Boolean, Boolean, WellKnownSidType)
' UserAccountUtil.Add(UserPrincipal, WellKnownSidType)
' UserAccountUtil.Delete(String)

#End Region

#End Region

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System
Imports System.Collections.Generic
Imports System.DirectoryServices.AccountManagement
Imports System.Linq
Imports System.Security.Principal

#End Region

#Region " User-Accoun tUtil "

''' ----------------------------------------------------------------------------------------------------
''' <summary>
''' Contains related Windows user-account utilities.
''' </summary>
''' ----------------------------------------------------------------------------------------------------
Public Module UserAccountUtil

#Region " Properties "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Gets an <see cref="UserPrincipal"/> object that represents the current user.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <value>
    ''' An <see cref="UserPrincipal"/> object that represents the current user.
    ''' </value>
    ''' ----------------------------------------------------------------------------------------------------
    Public ReadOnly Property CurrentUser As UserPrincipal
        <DebuggerStepThrough>
        Get
            If UserAccountUtil.currentUserB Is Nothing Then
                UserAccountUtil.currentUserB = UserAccountUtil.FindUser(Environment.UserName)
            End If
            Return UserAccountUtil.currentUserB
        End Get
    End Property
    ''' <summary>
    ''' ( Backing field )
    ''' Gets an <see cref="UserPrincipal"/> object that represents the current user.
    ''' </summary>
    Private currentUserB As UserPrincipal

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Gets a value that indicates whether the current user has Administrator privileges.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <value>
    ''' A value that indicates whether the current user has Administrator privileges.
    ''' </value>
    ''' ----------------------------------------------------------------------------------------------------
    Public ReadOnly Property CurrentUserIsAdmin As Boolean
        <DebuggerStepThrough>
        Get
            Using group As GroupPrincipal =
                GroupPrincipal.FindByIdentity(CurrentUser.Context, IdentityType.Sid,
                                              New SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, Nothing).Value)

                Return UserAccountUtil.CurrentUser.IsMemberOf(group)
            End Using
        End Get
    End Property

#End Region

#Region " Public Methods "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Find and returns all the user accounts of the current machine context.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim users As List(Of UserPrincipal) = UserAccountUtil.GetAllUsers()
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' A <see cref="List(Of UserPrincipal)"/> collection that contains the users.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function GetAllUsers() As List(Of UserPrincipal)

        Dim context As New PrincipalContext(ContextType.Machine)

        Using user As New UserPrincipal(context)

            Using searcher As New PrincipalSearcher(user)

                Return searcher.FindAll.Cast(Of UserPrincipal).ToList

            End Using ' searcher

        End Using ' user

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Finds an user account that matches the specified name in the current machine context.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim user As UserPrincipal = UserAccountUtil.FindUser(username:="Administrator")
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="username">
    ''' The user name to find.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' An <see cref="UserPrincipal"/> object that contains the user data.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentException">
    ''' User not found.;username
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function FindUser(ByVal username As String) As UserPrincipal

        Dim context As New PrincipalContext(ContextType.Machine)

        Using user As New UserPrincipal(context)

            Using searcher As New PrincipalSearcher(user)

                Try
                    Return (From p As Principal In searcher.FindAll
                            Where p.Name.Equals(username, StringComparison.OrdinalIgnoreCase)).
                            Cast(Of UserPrincipal).
                            First

                Catch ex As InvalidOperationException
                    Throw New ArgumentException(message:="User not found.", paramName:="username", innerException:=ex)

                End Try

            End Using ' searcher

        End Using ' user

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Finds an user account that matches the specified security identifier (SID) in the current machine context.
    ''' </summary>    
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim user As UserPrincipal = UserAccountUtil.FindUser(sid:=New SecurityIdentifier("S-1-5-21-1780771175-1208154119-2269826705-500"))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sid">
    ''' A <see cref="SecurityIdentifier"/> (SID) object.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' An <see cref="UserPrincipal"/> object that contains the user data.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentException">
    ''' User not found.;username
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function FindUser(ByVal sid As SecurityIdentifier) As UserPrincipal

        Dim context As New PrincipalContext(ContextType.Machine)

        Using user As New UserPrincipal(context)

            Using searcher As New PrincipalSearcher(user)

                Try
                    Return (From p As Principal In searcher.FindAll
                            Where p.Sid.Value.Equals(sid.Value, StringComparison.OrdinalIgnoreCase)).
                            Cast(Of UserPrincipal).
                            First

                Catch ex As InvalidOperationException
                    Throw New ArgumentException(message:="User not found.", paramName:="username", innerException:=ex)

                End Try

            End Using ' searcher

        End Using ' user

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Finds the username of the specified security identifier (SID) in the current machine context.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim username As String = UserAccountUtil.FindUsername(sid:=New SecurityIdentifier("S-1-5-21-1780771175-1208154119-2269826705-500"))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sid">
    ''' A <see cref="SecurityIdentifier"/> (SID) object.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The username.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function FindUsername(ByVal sid As SecurityIdentifier) As String

        Using user As UserPrincipal = UserAccountUtil.FindUser(sid)
            Return user.Name
        End Using

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Finds the security identifier (SID) of the specified username account in the current machine context.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim sid As SecurityIdentifier = UserAccountUtil.FindSid(username:="Administrator"))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="username">
    ''' The user name.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' A <see cref="SecurityIdentifier"/> (SID) object.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function FindSid(ByVal username As String) As SecurityIdentifier

        Return UserAccountUtil.FindUser(username).Sid

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Finds the profile directory path of the specified username account in the current machine context.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim profilePath As String = UserAccountUtil.FindProfilePath(username:="Administrator"))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="username">
    ''' The user name to find.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The profile directory path.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function FindProfilePath(ByVal userName As String) As String

        Using user As UserPrincipal = UserAccountUtil.FindUser(userName)

            Return CStr(My.Computer.Registry.GetValue(String.Format("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion\ProfileList\{0}",
                                                                    user.Sid.Value),
                                                                    "ProfileImagePath", ""))

        End Using

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Finds the profile directory path of the specified username account in the current machine context.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim profilePath As String = 
    '''     UserAccountUtil.FindProfilePath(sid:=New SecurityIdentifier("S-1-5-21-1780771175-1208154119-2269826705-500"))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sid">
    ''' A <see cref="SecurityIdentifier"/> (SID) object.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The profile directory path.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function FindProfilePath(ByVal sid As SecurityIdentifier) As String

        Using user As UserPrincipal = UserAccountUtil.FindUser(sid)

            Return CStr(My.Computer.Registry.GetValue(String.Format("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion\ProfileList\{0}",
                                                                    user.Sid.Value),
                                                                    "ProfileImagePath", ""))

        End Using

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Determines whether an user-account of the current machine context is an Administrator.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim userIsAdmin As Boolean = UserAccountUtil.IsAdmin(username:="Administrator")
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="username">
    ''' The user name.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' <see langword="True"/> if the user is an Administrator, otherwise, <see langword="False"/>.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function IsAdmin(ByVal username As String) As Boolean

        Using user As UserPrincipal = UserAccountUtil.FindUser(username)

            Using group As GroupPrincipal = GroupPrincipal.FindByIdentity(user.Context, IdentityType.Sid, New SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, Nothing).Value)

                Return user.IsMemberOf(group)

            End Using ' group

        End Using ' user

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Determines whether an user-account of the current machine context is a member of the specified group.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim userIsGuest As Boolean = 
    '''     UserAccountUtil.IsMemberOfGroup(username:="Administrator", groupSid:=WellKnownSidType.BuiltinGuestsSid)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="username">
    ''' The user name.
    ''' </param>
    ''' 
    ''' <param name="groupSid">
    ''' A <see cref="WellKnownSidType"/> security identifier (SID) that determines the account group.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' <see langword="True"/> if the user is a member of the specified group, otherwise, <see langword="False"/>.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function IsMemberOfGroup(ByVal username As String,
                                    ByVal groupSid As WellKnownSidType) As Boolean

        Using user As UserPrincipal = UserAccountUtil.FindUser(username)

            Using group As GroupPrincipal = GroupPrincipal.FindByIdentity(user.Context, IdentityType.Sid, New SecurityIdentifier(groupSid, Nothing).Value)

                Return user.IsMemberOf(group)

            End Using ' group

        End Using ' user

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Determines whether an user-account of the current machine context is a member of the specified group.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim userIsGuest As Boolean = UserAccountUtil.IsMemberOfGroup(username:="Administrator", groupname:="Guests")
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="username">
    ''' The user name.
    ''' </param>
    ''' 
    ''' <param name="groupname">
    ''' The name of thehe group.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' <see langword="True"/> if the user is a member of the specified group, otherwise, <see langword="False"/>.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function IsMemberOfGroup(ByVal username As String,
                                    ByVal groupname As String) As Boolean

        Using user As UserPrincipal = UserAccountUtil.FindUser(username)

            Using group As GroupPrincipal = GroupPrincipal.FindByIdentity(user.Context, IdentityType.Name, groupname)

                Return user.IsMemberOf(group)

            End Using ' group

        End Using ' user

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Creates a new user account in the current machine context.
    ''' This function does NOT adds a new user in the current machine.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim user as UserPrincipal = UserAccountUtil.Create(username:="Elektro",
    '''                                                    password:="",
    '''                                                    displayName:="Elektro Account.",
    '''                                                    description:="This is a test user-account.",
    '''                                                    canChangePwd:=True,
    '''                                                    pwdExpires:=False,
    '''                                                    groupSid:=WellKnownSidType.BuiltinAdministratorsSid)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="username">
    ''' The user name.
    ''' </param>
    ''' 
    ''' <param name="password">
    ''' The user password.
    ''' If this value is empty, account is set to don't require any password.
    ''' </param>
    ''' 
    ''' <param name="displayName">
    ''' The display name of the user account.
    ''' </param>
    ''' 
    ''' <param name="description">
    ''' The description of the user account.
    ''' </param>
    ''' 
    ''' <param name="canChangePwd">
    ''' A value that indicates whether the user can change its password.
    ''' </param>
    ''' 
    ''' <param name="pwdExpires">
    ''' A value that indicates whether the password should expire.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' An <see cref="UserPrincipal"/> object that contains the user data.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function Create(ByVal username As String,
                           ByVal password As String,
                           ByVal displayName As String,
                           ByVal description As String,
                           ByVal canChangePwd As Boolean,
                           ByVal pwdExpires As Boolean) As UserPrincipal

        Using context As New PrincipalContext(ContextType.Machine)

            Dim user As New UserPrincipal(context)

            With user

                .Name = username

                .SetPassword(password)
                .PasswordNotRequired = String.IsNullOrEmpty(password)

                .DisplayName = displayName
                .Description = description

                .UserCannotChangePassword = canChangePwd
                .PasswordNeverExpires = pwdExpires

                .Enabled = True
                .Save()

            End With

            Return user

        End Using

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Adds a new user account in the current machine context.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' UserAccountUtil.Add(username:="Elektro",
    '''                     password:="",
    '''                     displayName:="Elektro Account.",
    '''                     description:="This is a test user-account.",
    '''                     canChangePwd:=True,
    '''                     pwdExpires:=False,
    '''                     groupSid:=WellKnownSidType.BuiltinAdministratorsSid)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="username">
    ''' The user name.
    ''' </param>
    ''' 
    ''' <param name="password">
    ''' The user password.
    ''' If this value is empty, account is set to don't require any password.
    ''' </param>
    ''' 
    ''' <param name="displayName">
    ''' The display name of the user account.
    ''' </param>
    ''' 
    ''' <param name="description">
    ''' The description of the user account.
    ''' </param>
    ''' 
    ''' <param name="canChangePwd">
    ''' A value that indicates whether the user can change its password.
    ''' </param>
    ''' 
    ''' <param name="pwdExpires">
    ''' A value that indicates whether the password should expire.
    ''' </param>
    ''' 
    ''' <param name="groupSid">
    ''' A <see cref="WellKnownSidType"/> security identifier (SID) that determines the account group where to add the user.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub Add(ByVal username As String,
                   ByVal password As String,
                   ByVal displayName As String,
                   ByVal description As String,
                   ByVal canChangePwd As Boolean,
                   ByVal pwdExpires As Boolean,
                   Optional ByVal groupSid As WellKnownSidType = WellKnownSidType.BuiltinUsersSid)

        Using context As New PrincipalContext(ContextType.Machine)

            Using user As UserPrincipal = UserAccountUtil.Create(username, password, displayName, description, canChangePwd, pwdExpires)

                Using group As GroupPrincipal = GroupPrincipal.FindByIdentity(context, IdentityType.Sid, New SecurityIdentifier(groupSid, Nothing).Value)

                    group.Members.Add(user)
                    group.Save()

                End Using ' group

            End Using ' user

        End Using ' context

    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Adds a new user account in the current machine context.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' UserAccountUtil.Add(user:=myUserPrincipal, groupSid:=WellKnownSidType.BuiltinAdministratorsSid)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="user">
    ''' An <see cref="UserPrincipal"/> object that contains the user data.
    ''' </param>
    '''
    ''' <param name="groupSid">
    ''' A <see cref="WellKnownSidType"/> security identifier (SID) that determines the account group where to add the user.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub Add(ByVal user As UserPrincipal,
                   Optional ByVal groupSid As WellKnownSidType = WellKnownSidType.BuiltinUsersSid)

        Using context As New PrincipalContext(ContextType.Machine)

            Using group As GroupPrincipal = GroupPrincipal.FindByIdentity(context, IdentityType.Sid, New SecurityIdentifier(groupSid, Nothing).Value)

                group.Members.Add(user)
                group.Save()

            End Using ' group

        End Using ' context

    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Deletes an user account in the current machine context.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' UserAccountUtil.Delete(username:="User name")
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="username">
    ''' The user name of the user-account to delete.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub Delete(ByVal username As String)

        Using curUser As UserPrincipal = UserAccountUtil.FindUser(username)
            curUser.Delete()
        End Using

    End Sub

#End Region

End Module

#End Region
