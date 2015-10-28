
' ***********************************************************************
' Author   : Elektro
' Modified : 28-October-2015
' ***********************************************************************
' <copyright file="Theming Util.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Public Members Summary "

#Region " Child Classes "

' ThemingUtil.Cursors
' ThemingUtil.Fonts
' ThemingUtil.ScreenSavers
' ThemingUtil.VisualThemes
' ThemingUtil.Wallpapers

#End Region

#Region " Types "

' ThemingUtil.VisualThemes.ThemeInfo

#End Region

#Region " Constructors "

' ThemingUtil.VisualThemes.ThemeInfo.New(String, String, String)

#End Region

#Region " Enumerations "

' ThemingUtil.Cursors.CursorType
' ThemingUtil.Wallpapers.WallpaperStyle

#End Region

#Region " Properties "

' ThemingUtil.Fonts.InstalledFonts As IEnumerable(Of FontFamily)
' ThemingUtil.Fonts.InstalledFontNames As IEnumerable(Of FontFamily)
' ThemingUtil.ScreenSavers.CurrentScreensaver As String
' ThemingUtil.VisualThemes.AeroEnabled() As Boolean
' ThemingUtil.VisualThemes.AeroSupported() As Boolean
' ThemingUtil.VisualThemes.CurrentTheme() As ThemingUtil.VisualThemes.ThemeInfo
' ThemingUtil.Wallpapers.CurrentWallpaper() As String
' ThemingUtil.Wallpapers.WallpaperAsJpegIsSupported() As Boolean
' ThemingUtil.Wallpapers.WallpaperStylesFitFillAreSupported() As Boolean

#End Region

#Region " Functions "

' ThemingUtil.Fonts.IsFontInstalled(String) As Boolean
' ThemingUtil.Fonts.IsFontInstalled(FontFamily) As Boolean

#End Region

#Region " Methods "

' ThemingUtil.Cursors.SetSystemCursor(String, ThemingUtil.Cursors.CursorType)
' ThemingUtil.ScreenSavers.SetScreensaver(String)
' ThemingUtil.VisualThemes.SetSystemVisualTheme(String, String, String)
' ThemingUtil.Wallpapers.RemoveDesktopWallpaper()
' ThemingUtil.Wallpapers.SetDesktopWallpaper(String, ThemingUtil.Wallpapers.WallpaperStyle)

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
Imports System.Drawing.Text
Imports System.Linq
Imports Microsoft.Win32

#End Region

#Region " Theming Util "

''' ----------------------------------------------------------------------------------------------------
''' <summary>
''' Contains related theming/personalization utilities.
''' </summary>
''' ----------------------------------------------------------------------------------------------------
Public Module ThemingUtil

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
        ''' Obtains a value that indicates whether Desktop Window Manager (DWM) composition is enabled. 
        ''' Applications on machines running Windows 7 or earlier can listen for composition state changes by handling the WM_DWMCOMPOSITIONCHANGED notification.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="enabled">
        ''' A pointer to a value that, when this function returns successfully, receives <see langword="True"/> if DWM composition is enabled; otherwise, <see langword="False"/>.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa969518%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        <DllImport("dwmapi.dll")>
        Friend Shared Function DwmIsCompositionEnabled(
                               ByRef enabled As Boolean
        ) As Integer
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Retrieves the name of the current visual style, and optionally retrieves the color scheme name and size name.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="pszThemeFileName">
        ''' Pointer to a string that receives the theme path and file name.
        ''' </param>
        ''' 
        ''' <param name="dwMaxNameChars">
        ''' The maximum number of characters allowed in the theme file name.
        ''' </param>
        ''' 
        ''' <param name="pszColorBuff">
        ''' Pointer to a string that receives the color scheme name. This parameter may be set to <see langword="Nothing"/>.
        ''' </param>
        ''' 
        ''' <param name="cchMaxColorChars">
        ''' The maximum number of characters allowed in the color scheme name.
        ''' </param>
        ''' 
        ''' <param name="pszSizeBuff">
        ''' Pointer to a string that receives the size name. This parameter may be set to NULL.</param>
        ''' 
        ''' <param name="cchMaxSizeChars">
        ''' The maximum number of characters allowed in the size name.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' Returns '0' if successful, otherwise, an error code.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/bb773365%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        <DllImport("uxtheme", CharSet:=CharSet.Auto, BestFitMapping:=False, ThrowOnUnmappableChar:=True)>
        Friend Shared Function GetCurrentThemeName(
                               ByVal pszThemeFileName As StringBuilder,
                               ByVal dwMaxNameChars As Integer,
                               ByVal pszColorBuff As StringBuilder,
                               ByVal cchMaxColorChars As Integer,
                               ByVal pszSizeBuff As StringBuilder,
                               ByVal cchMaxSizeChars As Integer
        ) As Integer
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Creates a cursor based on data contained in a file. 
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="lpFileName">
        ''' The source of the file data to be used to create the cursor. 
        ''' The data in the file must be in either .CUR or .ANI format.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' If the function succeeds, the return value is an <see cref="IntPtr"/> handle to the new cursor.
        ''' If the function fails, the return value is <see cref="IntPtr.Zero"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms648392%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        <DllImport("user32.dll", SetLastError:=True, charSet:=CharSet.Ansi, bestFitMapping:=False, throwOnUnmappableChar:=True)>
        Friend Shared Function LoadCursorFromFile(
                               ByVal lpFileName As String
        ) As IntPtr
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Enables an application to customize the system cursors. 
        ''' It replaces the contents of the system cursor specified by the <paramfer name="id"/> parameter 
        ''' with the contents of the cursor specified by the <paramfer name="hCursor"/> parameter and then destroys <paramfer name="hCursor"/>. 
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="hCursor">
        ''' A handle to the cursor. 
        ''' The function replaces the contents of the system cursor specified by <paramfer name="id"/> parameter  
        ''' with the contents of the cursor handled by <paramfer name="hCursor"/> parameter.
        ''' </param>
        ''' 
        ''' <param name="id">
        ''' The system cursor to replace with the contents of <paramfer name="hCursor"/> parameter.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' If the function succeeds, the return value is <see langword="True"/>.
        ''' If the function fails, the return value is <see langword="False"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms648395%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        <DllImport("user32.dll", SetLastError:=True)>
        Friend Shared Function SetSystemCursor(
                               ByVal hCursor As IntPtr,
                               ByVal id As UInteger
        ) As Boolean
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sets the system visual theme.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="pszThemeFileName">
        ''' The theme filepath (themme.msstyles).
        ''' </param>
        ''' 
        ''' <param name="pszColor">
        ''' The color scheme name.
        ''' </param>
        ''' 
        ''' <param name="pszSize">
        ''' The size name.</param>
        ''' 
        ''' <param name="dwReserved">
        ''' Reserved parameter by the system.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns></returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://www.pinvoke.net/default.aspx/uxtheme/SetSystemVisualStyle.html"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        <DllImport("UxTheme.DLL", BestFitMapping:=False, CallingConvention:=CallingConvention.Winapi, CharSet:=CharSet.Unicode, EntryPoint:="#65")>
        Friend Shared Function SetSystemVisualStyle(
                               ByVal pszThemeFileName As String,
                               ByVal pszColor As String,
                               ByVal pszSize As String,
                               ByVal dwReserved As Integer
        ) As Integer
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Retrieves or sets the value of one of the system-wide parameters.
        ''' This function can also update the user profile while setting a parameter.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="uiAction">
        ''' The system-wide parameter to be retrieved or set.
        ''' </param>
        ''' 
        ''' <param name="uiParam">
        ''' A parameter whose usage and format depends on the system parameter being queried or set. 
        ''' For more information about system-wide parameters, see the <paramfer name="uiAction"></paramfer> parameter. 
        ''' If not otherwise indicated, you must specify '0' for this parameter.
        ''' </param>
        ''' 
        ''' <param name="pvParam">
        ''' A parameter whose usage and format depends on the system parameter being queried or set. 
        ''' For more information about system-wide parameters, see the <paramfer name="uiAction"></paramfer> parameter. 
        ''' If not otherwise indicated, you must specify <see langword="Nothing"/> for this parameter.
        ''' For information on the PVOID datatype, see Windows Data Types.
        ''' </param>
        ''' 
        ''' <param name="fWinIni">
        ''' If a system parameter is being set, specifies whether the user profile is to be updated, 
        ''' and if so, whether the WM_SETTINGCHANGE message is to be broadcast to 
        ''' all top-level windows to notify them of the change.
        ''' 
        ''' This parameter can be '0' if you do not want to update the user profile or broadcast the WM_SETTINGCHANGE message.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' If the function succeeds, the return value is a nonzero value.
        ''' If the function fails, the return value is zero. 
        ''' To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms724947%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        <DllImport("user32.dll", EntryPoint:="SystemParametersInfo", SetLastError:=True,
                                 CharSet:=CharSet.Auto, BestFitMapping:=False, ThrowOnUnmappableChar:=True)>
        Friend Shared Function SystemParametersInfo(
                               ByVal uiAction As SystemParametersActionFlags,
                               ByVal uiParam As UInteger,
                               ByVal pvParam As String,
                               ByVal fWinIni As SystemParametersWinIniFlags
        ) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Retrieves or sets the value of one of the system-wide parameters.
        ''' This function can also update the user profile while setting a parameter.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="uiAction">
        ''' The system-wide parameter to be retrieved or set.
        ''' </param>
        ''' 
        ''' <param name="uiParam">
        ''' A parameter whose usage and format depends on the system parameter being queried or set. 
        ''' For more information about system-wide parameters, see the <paramfer name="uiAction"></paramfer> parameter. 
        ''' If not otherwise indicated, you must specify '0' for this parameter.
        ''' </param>
        ''' 
        ''' <param name="pvParam">
        ''' A parameter whose usage and format depends on the system parameter being queried or set. 
        ''' For more information about system-wide parameters, see the <paramfer name="uiAction"></paramfer> parameter. 
        ''' If not otherwise indicated, you must specify <see langword="Nothing"/> for this parameter.
        ''' For information on the PVOID datatype, see Windows Data Types.
        ''' </param>
        ''' 
        ''' <param name="fWinIni">
        ''' If a system parameter is being set, specifies whether the user profile is to be updated, 
        ''' and if so, whether the WM_SETTINGCHANGE message is to be broadcast to 
        ''' all top-level windows to notify them of the change.
        ''' 
        ''' This parameter can be '0' if you do not want to update the user profile or broadcast the WM_SETTINGCHANGE message.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' If the function succeeds, the return value is a nonzero value.
        ''' If the function fails, the return value is zero. 
        ''' To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms724947%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        <DllImport("user32.dll", EntryPoint:="SystemParametersInfo", SetLastError:=True,
                                 CharSet:=CharSet.Auto, BestFitMapping:=False, ThrowOnUnmappableChar:=True)>
        Friend Shared Function SystemParametersInfo(
                               ByVal uiAction As SystemParametersActionFlags,
                               ByVal uiParam As UInteger,
                  <[In]> <Out> ByVal pvParam As StringBuilder,
                               ByVal fWinIni As SystemParametersWinIniFlags
        ) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

#End Region

#Region " Enumerations "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Flags for <paramref name="fWinIni"/> parameter of <see cref="ThemingUtil.NativeMethods.SetSystemCursor"/> function.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms648395%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        Friend Enum SystemCursorId As UInteger

            ''' <summary>
            ''' Standard arrow and small hourglass.
            ''' </summary>
            AppStarting = 32650UI

            ''' <summary>
            ''' Standard arrow.
            ''' </summary>
            Arrow = 32512UI

            ''' <summary>
            ''' Crosshair.
            ''' </summary>
            Crosshair = 32515UI

            ''' <summary>
            ''' Hand.
            ''' </summary>
            Hand = 32649UI

            ''' <summary>
            ''' Arrow and question mark.
            ''' </summary>
            Help = 32651UI

            ''' <summary>
            ''' I-beam.
            ''' </summary>
            IBeam = 32513UI

            ''' <summary>
            ''' Slashed circle.
            ''' </summary>
            No = 32648UI

            ''' <summary>
            ''' Four-pointed arrow pointing north, south, east, and west.
            ''' </summary>
            SizeAll = 32646UI

            ''' <summary>
            ''' Double-pointed arrow pointing northeast and southwest.
            ''' </summary>
            SizeNesw = 32643UI

            ''' <summary>
            ''' Double-pointed arrow pointing north and south.
            ''' </summary>
            SizeNS = 32645UI

            ''' <summary>
            ''' Double-pointed arrow pointing northwest and southeast.
            ''' </summary>
            SizeNwse = 32642UI

            ''' <summary>
            ''' Double-pointed arrow pointing west and east.
            ''' </summary>
            SizeWE = 32644UI

            ''' <summary>
            ''' Vertical arrow.
            ''' </summary>
            Up = 32516UI

            ''' <summary>
            ''' Hourglass.
            ''' </summary>
            Wait = 32514UI

        End Enum

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Flags for <paramref name="uiAction"/> parameter of <see cref="ThemingUtil.NativeMethods.SystemParametersInfo"/> function.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms724947(v=vs.85).aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        Friend Enum SystemParametersActionFlags As UInteger

            ' *****************************************************************************
            '                            WARNING!, NEED TO KNOW...
            '
            '  THIS ENUMERATION IS PARTIALLY DEFINED JUST FOR THE PURPOSES OF THIS PROJECT
            ' *****************************************************************************

            ' ''' <summary>
            ' ''' Sets the state of the screen saver. 
            ' ''' The <paramref name="uiParam"/> parameter specifies <see langword="True"/> to activate screen saving, 
            ' ''' or <see langword="False"/> to deactivate it.
            ' ''' </summary>
            'SetScreensaveActive = &H11

            ''' <summary>
            ''' Sets the desktop wallpaper. 
            ''' The value of the <paramref name="pvParam"/> parameter determines the new wallpaper. 
            ''' To specify a wallpaper bitmap, set <paramref name="pvParam"/> to point to a 
            ''' null-terminated string containing the name of a bitmap file. 
            ''' 
            ''' Setting <paramref name="pvParam"/> to "" removes the wallpaper.
            ''' Setting <paramref name="pvParam"/> to null reverts to the default wallpaper.
            ''' </summary>
            SetDesktopWallpaper = &H14

            ''' <summary>
            ''' Retrieves the full path of the bitmap file for the desktop wallpaper.
            ''' The <paramref name="pvParam"/> parameter must point to 
            ''' a <see cref="StringBuilder"/> that receives a null-terminated path string.
            ''' 
            ''' Set the <paramref name="uiParam"/> parameter to the size, in characters, of the <paramref name="pvParam"/> buffer. 
            ''' The returned string will not exceed <see cref="StringBuilder.MaxCapacity"/> characters. 
            ''' If there is no desktop wallpaper, the returned string is empty.
            ''' </summary>
            GetDesktopWallpaper = &H73

            ' ''' <summary>
            ' ''' Reloads the system cursors. 
            ' ''' Set the <paramref name="uiParam"/> parameter to zero and the <paramref name="pvParam"/> parameter to null.
            ' ''' </summary>
            'Setcursors = &H57

            ' ''' <summary>
            ' ''' Reloads the system icons. 
            ' ''' Set the <paramref name="uiParam"/> parameter to zero and the <paramref name="pvParam"/> parameter to null.
            ' ''' </summary>
            'Seticons = &H58

        End Enum

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Flags for <paramref name="fWinIni"/> parameter of <see cref="ThemingUtil.NativeMethods.SystemParametersInfo"/> function.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms724947(v=vs.85).aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        <Flags>
        Friend Enum SystemParametersWinIniFlags As UInteger

            ''' <summary>
            ''' None.
            ''' </summary>
            None = &H0

            ''' <summary>
            ''' Writes the new system-wide parameter setting to the user profile.
            ''' </summary>
            UpdateIniFile = &H1

            ''' <summary>
            ''' Broadcasts the WM_SETTINGCHANGE message after updating the user profile.
            ''' </summary>
            SendChange = &H2

            ''' <summary>
            ''' Same as <see cref="ThemingUtil.NativeMethods.SystemParametersWinIniFlags.SendChange"/>.
            ''' </summary>
            SendWinIniChange = &H3

        End Enum

#End Region

    End Class

#End Region

#Region " Child Classes "

#Region " Cursors "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Contains related cursor utilities.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    Public NotInheritable Class Cursors

#Region " Properties "

#End Region

#Region " Enumerations "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Specifies a cursor type.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Enum CursorType As UInteger

            ''' <summary>
            ''' Standard arrow and small hourglass.
            ''' </summary>
            AppStarting = ThemingUtil.NativeMethods.SystemCursorId.AppStarting

            ''' <summary>
            ''' Standard arrow.
            ''' </summary>
            Arrow = ThemingUtil.NativeMethods.SystemCursorId.Arrow

            ''' <summary>
            ''' Crosshair.
            ''' </summary>
            Crosshair = ThemingUtil.NativeMethods.SystemCursorId.Crosshair

            ''' <summary>
            ''' Hand.
            ''' </summary>
            Hand = ThemingUtil.NativeMethods.SystemCursorId.Hand

            ''' <summary>
            ''' Arrow and question mark.
            ''' </summary>
            Help = ThemingUtil.NativeMethods.SystemCursorId.Help

            ''' <summary>
            ''' I-beam.
            ''' </summary>
            IBeam = ThemingUtil.NativeMethods.SystemCursorId.IBeam

            ''' <summary>
            ''' Slashed circle.
            ''' </summary>
            No = ThemingUtil.NativeMethods.SystemCursorId.No

            ''' <summary>
            ''' Four-pointed arrow pointing north, south, east, and west.
            ''' </summary>
            SizeAll = ThemingUtil.NativeMethods.SystemCursorId.SizeAll

            ''' <summary>
            ''' Double-pointed arrow pointing northeast and southwest.
            ''' </summary>
            SizeNesw = ThemingUtil.NativeMethods.SystemCursorId.SizeNesw

            ''' <summary>
            ''' Double-pointed arrow pointing north and south.
            ''' </summary>
            SizeNS = ThemingUtil.NativeMethods.SystemCursorId.SizeNS

            ''' <summary>
            ''' Double-pointed arrow pointing northwest and southeast.
            ''' </summary>
            SizeNwse = ThemingUtil.NativeMethods.SystemCursorId.SizeNwse

            ''' <summary>
            ''' Double-pointed arrow pointing west and east.
            ''' </summary>
            SizeWE = ThemingUtil.NativeMethods.SystemCursorId.SizeWE

            ''' <summary>
            ''' Vertical arrow.
            ''' </summary>
            Up = ThemingUtil.NativeMethods.SystemCursorId.Up

            ''' <summary>
            ''' Hourglass.
            ''' </summary>
            Wait = ThemingUtil.NativeMethods.SystemCursorId.Wait

        End Enum

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sets the system cursor.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <example> This is a code example.
        ''' <code>
        ''' ThemingUtil.SetSystemCursor("C:\Windows\Cursors\aero_pen.cur", ThemingUtil.Cursors.CursorType.Arrow)
        ''' </code>
        ''' </example>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="filePath">
        ''' The cursor file path.
        ''' </param>
        ''' 
        ''' <param name="cursorType">
        ''' The cursor type.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub SetSystemCursor(ByVal filePath As String,
                                          ByVal cursorType As ThemingUtil.Cursors.CursorType)

            If Not ThemingUtil.NativeMethods.SetSystemCursor(ThemingUtil.NativeMethods.LoadCursorFromFile(filePath), cursorType) Then
                Throw New Win32Exception([error]:=Marshal.GetLastWin32Error)
            End If

        End Sub

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="Cursors"/> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub New()
        End Sub

#End Region

#Region " Hidden Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether the specified System.Object instances are considered equal.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Function Equals(ByVal obj As Object) As Boolean
            Return MyBase.Equals(obj)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether the specified System.Object instances are the same instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Function ReferenceEquals(ByVal objA As Object, ByVal objB As Object) As Boolean
            Return Nothing
        End Function

#End Region

    End Class

#End Region

#Region " Fonts "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Contains related font utilities.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    Public NotInheritable Class Fonts

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the font families installed on the current machine.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <example> This is a code example.
        ''' <code>
        ''' For Each fontFamily As FontFamily In Fonts
        '''     Console.WriteLine(fontFamily.Name)
        ''' Next
        ''' </code>
        ''' </example>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The font families installed on the current machine.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared ReadOnly Property InstalledFonts As IEnumerable(Of FontFamily)
            <DebuggerStepThrough>
            Get
                Using fontCollection As New InstalledFontCollection
                    Return fontCollection.Families
                End Using
            End Get
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the names of the font families installed on the current machine.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <example> This is a code example.
        ''' <code>
        ''' For Each fontName As String In FontNames
        '''     Console.WriteLine(fontName)
        ''' Next
        ''' </code>
        ''' </example>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The names of the font families installed on the current machine.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared ReadOnly Iterator Property InstalledFontNames As IEnumerable(Of String)
            <DebuggerStepThrough>
            Get
                Using fontCollection As New InstalledFontCollection
                    For Each font As FontFamily In fontCollection.Families
                        Yield font.Name
                    Next font
                End Using
            End Get
        End Property

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether a text-font is installed on the current machine.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="fontName">
        ''' The family name of the font.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if font is installed, <see langword="False"/> otherwise.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Function IsFontInstalled(ByVal fontName As String) As Boolean

            Using f As New Font(fontName, emSize:=1.0F)
                Return f.Name.Equals(fontName, StringComparison.OrdinalIgnoreCase)
            End Using

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether a text-font is installed on the current machine.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="fontFamily">
        ''' The <see cref="FontFamily"/>.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if font is installed, <see langword="False"/> otherwise.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Function IsFontInstalled(ByVal fontFamily As FontFamily) As Boolean

            Using f As New Font(fontFamily, emSize:=1.0F)
                Return f.FontFamily.Equals(fontFamily)
            End Using

        End Function

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="Fonts"/> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub New()
        End Sub

#End Region

#Region " Hidden Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether the specified System.Object instances are considered equal.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Function Equals(ByVal obj As Object) As Boolean
            Return MyBase.Equals(obj)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether the specified System.Object instances are the same instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Function ReferenceEquals(ByVal objA As Object, ByVal objB As Object) As Boolean
            Return Nothing
        End Function

#End Region

    End Class

#End Region













#Region " Screensavers "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Contains related Screensaver utilities.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    Public NotInheritable Class Screensavers

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the path of the current screensaver.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The current screensaver filepath.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared ReadOnly Property CurrentScreensaver As String
            <DebuggerStepThrough>
            Get
                Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop")
                    Return CStr(regkey.GetValue("SCRNSAVE.EXE", String.Empty, RegistryValueOptions.None))
                End Using
            End Get
        End Property

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sets the system screensaver.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="filePath">
        ''' The screensaver file path.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub SetScreensaver(ByVal filePath As String)

            Using regkey As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop")
                regkey.SetValue("SCRNSAVE.EXE", value, RegistryValueKind.String)
            End Using

        End Sub

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="Screensaver"/> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub New()
        End Sub

#End Region

#Region " Hidden Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether the specified System.Object instances are considered equal.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Function Equals(ByVal obj As Object) As Boolean
            Return MyBase.Equals(obj)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether the specified System.Object instances are the same instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Function ReferenceEquals(ByVal objA As Object, ByVal objB As Object) As Boolean
            Return Nothing
        End Function

#End Region

    End Class

#End Region













#Region " VisualThemes "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Contains related visual-theme utilities.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    Public NotInheritable Class VisualThemes

#Region " Types "

#Region " ThemeInfo "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Defines the information of a Windows Visual Theme.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <Serializable>
        Public Structure ThemeInfo

#Region " Properties "

            ''' ----------------------------------------------------------------------------------------------------
            ''' <summary>
            ''' Gets the theme filename.
            ''' </summary>
            ''' ----------------------------------------------------------------------------------------------------
            ''' <value>
            ''' The theme filename.
            ''' </value>
            ''' ----------------------------------------------------------------------------------------------------
            Public ReadOnly Property FileName() As String
                <DebuggerStepThrough>
                Get
                    Return Path.GetFileName(Me.filepathB)
                End Get
            End Property

            ''' ----------------------------------------------------------------------------------------------------
            ''' <summary>
            ''' Gets the theme filepath.
            ''' </summary>
            ''' ----------------------------------------------------------------------------------------------------
            ''' <value>
            ''' The theme filepath.
            ''' </value>
            ''' ----------------------------------------------------------------------------------------------------
            Public ReadOnly Property Filepath() As String
                <DebuggerStepThrough>
                Get
                    Return Me.filepathB
                End Get
            End Property
            ''' <summary>
            ''' ( Backing Field )
            ''' The theme filepath.
            ''' </summary>
            Private ReadOnly filepathB As String

            ''' ----------------------------------------------------------------------------------------------------
            ''' <summary>
            ''' Gets the theme color scheme name.
            ''' </summary>
            ''' ----------------------------------------------------------------------------------------------------
            ''' <value>
            ''' The theme color scheme name.
            ''' </value>
            ''' ----------------------------------------------------------------------------------------------------
            Public ReadOnly Property ColorSchemeName() As String
                <DebuggerStepThrough>
                Get
                    Return Me.colorSchemeNameB
                End Get
            End Property
            ''' <summary>
            ''' ( Backing Field )
            ''' The theme color scheme name.
            ''' </summary>
            Private ReadOnly colorSchemeNameB As String

            ''' ----------------------------------------------------------------------------------------------------
            ''' <summary>
            ''' Gets the theme size name.
            ''' </summary>
            ''' ----------------------------------------------------------------------------------------------------
            ''' <value>
            ''' The theme size name.
            ''' </value>
            ''' ----------------------------------------------------------------------------------------------------
            Public ReadOnly Property SizeName() As String
                <DebuggerStepThrough>
                Get
                    Return Me.sizeNameB
                End Get
            End Property
            ''' <summary>
            ''' ( Backing Field )
            ''' The theme size name.
            ''' </summary>
            Private ReadOnly sizeNameB As String

#End Region

#Region " Constructors "

            ''' ----------------------------------------------------------------------------------------------------
            ''' <summary>
            ''' Initializes a new instance of the <see cref="ThemeInfo"/> class.
            ''' </summary>
            ''' ----------------------------------------------------------------------------------------------------
            ''' <param name="filepath">
            ''' The theme filepath.
            ''' </param>
            ''' 
            ''' <param name="colorSchemeName">
            ''' The theme color scheme name.
            ''' </param>
            ''' 
            ''' <param name="sizeName">The theme size name.
            ''' </param>
            ''' ----------------------------------------------------------------------------------------------------
            ''' <exception cref="ArgumentNullException">
            ''' filepath or colorSchemeName or sizeName
            ''' </exception>
            ''' ----------------------------------------------------------------------------------------------------
            <DebuggerStepThrough>
            Public Sub New(ByVal filepath As String,
                           ByVal colorSchemeName As String,
                           ByVal sizeName As String)

                If String.IsNullOrWhiteSpace(filepath) Then
                    Throw New ArgumentNullException(paramName:="filepath")

                ElseIf String.IsNullOrWhiteSpace(colorSchemeName) Then
                    Throw New ArgumentNullException(paramName:="colorSchemeName")

                ElseIf String.IsNullOrWhiteSpace(sizeName) Then
                    Throw New ArgumentNullException(paramName:="sizeName")

                Else
                    Me.filepathB = filepath
                    Me.colorSchemeNameB = colorSchemeName
                    Me.sizeNameB = sizeName

                End If

            End Sub

#End Region

        End Structure

#End Region

#End Region

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets a <see cref="ThemingUtil.VisualThemes.ThemeInfo"/> object that contains the info of the current windows theme.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' A <see cref="ThemingUtil.VisualThemes.ThemeInfo"/> object that contains the info of the current windows theme.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared ReadOnly Property CurrentTheme() As ThemeInfo
            <DebuggerStepThrough>
            Get
                Return ThemingUtil.VisualThemes.GetCurrentThemeInfo()
            End Get
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets a value indicating whether Aero feature is enabled on the current operating system.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' <see langword="True"/> if Aero feature is enabled on the current operating system.
        ''' <see langword="False"/> if Aero feature is not enabled or else is not supported by the 
        ''' current operating system (like Windows XP or previous versions).
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared ReadOnly Property AeroEnabled() As Boolean
            <DebuggerStepThrough>
            Get
                If Environment.OSVersion.Version.Major < 6 Then
                    Return False ' Windows version is below Windows Vista so not Aero disponible.

                Else
                    Dim isEnabled As Boolean
                    ThemingUtil.NativeMethods.DwmIsCompositionEnabled(isEnabled)
                    Return isEnabled

                End If
            End Get
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets a value indicating whether Aero feature is supported by the current operating system.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' <see langword="True"/> if Aero feature is supported by the current operating system.
        ''' <see langword="False"/> if Aero feature is not supported by the current operating system.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared ReadOnly Property AeroSupported() As Boolean
            <DebuggerStepThrough>
            Get
                Return (Environment.OSVersion.Version.Major > 5) ' Windows version is above Windows XP.
            End Get
        End Property

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sets the system visual theme.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <example> This is a code example.
        ''' <code>
        ''' ThemingUtil.SetSystemVisualTheme("C:\ThemeName.msstyles", "NormalColor", "NormalSize")
        ''' </code>
        ''' </example>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="filePath">
        ''' The theme file path.
        ''' </param>
        ''' 
        ''' <param name="colorName">
        ''' The coor scheme name.
        ''' </param>
        ''' 
        ''' <param name="sizeName">
        ''' The size name.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub SetSystemVisualTheme(ByVal filePath As String,
                                               ByVal colorName As String,
                                               ByVal sizeName As String)

            ThemingUtil.NativeMethods.SetSystemVisualStyle(filePath, colorName, sizeName, 65)

        End Sub

#End Region

#Region " Private Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets a <see cref="ThemingUtil.VisualThemes.ThemeInfo"/> object that contains the info of the current windows theme.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' A <see cref="ThemingUtil.VisualThemes.ThemeInfo"/> object that contains the info of the current windows theme.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Private Shared Function GetCurrentThemeInfo() As ThemingUtil.VisualThemes.ThemeInfo

            Dim bufferLength As Integer = 260

            Dim sbFilepath As New StringBuilder(capacity:=bufferLength)
            Dim sbColorSchemeName As New StringBuilder(capacity:=bufferLength)
            Dim sbSizeName As New StringBuilder(capacity:=bufferLength)

            ThemingUtil.NativeMethods.GetCurrentThemeName(sbFilepath, bufferLength,
                                                              sbColorSchemeName, bufferLength,
                                                              sbSizeName, bufferLength)

            Return New ThemingUtil.VisualThemes.ThemeInfo(filepath:=sbFilepath.ToString,
                                                          colorSchemeName:=sbColorSchemeName.ToString,
                                                          sizeName:=sbSizeName.ToString)

        End Function

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="VisualThemes"/> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub New()
        End Sub

#End Region

#Region " Hidden Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether the specified System.Object instances are considered equal.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Function Equals(ByVal obj As Object) As Boolean
            Return MyBase.Equals(obj)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether the specified System.Object instances are the same instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Function ReferenceEquals(ByVal objA As Object, ByVal objB As Object) As Boolean
            Return Nothing
        End Function

#End Region

    End Class

#End Region

#Region " Wallpapers "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Contains related cursor utilities.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    Public NotInheritable Class Wallpapers

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the filepath of the current desktop wallpaper.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The filepath of the current desktop wallpaper.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared ReadOnly Property CurrentWallpaper() As String
            <DebuggerStepThrough>
            Get
                Dim sb As New StringBuilder(capacity:=260)

                Dim uiAction As ThemingUtil.NativeMethods.SystemParametersActionFlags =
                    ThemingUtil.NativeMethods.SystemParametersActionFlags.GetDesktopWallpaper

                If Not ThemingUtil.NativeMethods.SystemParametersInfo(uiAction, CUInt(sb.Capacity), sb, Nothing) Then
                    Throw New Win32Exception([error]:=Marshal.GetLastWin32Error)
                Else
                    Return sb.ToString
                End If
            End Get
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets a value that determines wheter jpeg files are supported as wallpaper in the current operating system. 
        ''' The jpeg wallpapers are not supported before Windows Vista.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' <see langword="True"/> if jpeg files are supported as wallpaper in the current operating system, otherwise, <see langword="False"/>
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared ReadOnly Property WallpaperAsJpegIsSupported() As Boolean
            <DebuggerStepThrough>
            Get
                Return Environment.OSVersion.Version >= New Version(6, 0)
            End Get
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets a value that determines whether the <see cref="WallpaperStyle.Fit"/> and <see cref="WallpaperStyle.Fill"/> are 
        ''' supported in the current operating system. 
        ''' 
        ''' The <see cref="WallpaperStyle.Fit"/> and <see cref="WallpaperStyle.Fill"/> wallpaper styles are not supported before Windows 7.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' <see langword="True"/> if <see cref="WallpaperStyle.Fit"/> and <see cref="WallpaperStyle.Fill"/> are supported in the 
        ''' current operating system, otherwise, <see langword="False"/>
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared ReadOnly Property WallpaperStylesFitFillAreSupported() As Boolean
            <DebuggerStepThrough>
            Get
                Return Environment.OSVersion.Version >= New Version(6, 1)
            End Get
        End Property

#End Region

#Region " Enumerations "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Describes a wallpaper style.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Enum WallpaperStyle As Integer

            ''' <summary>
            ''' If the image is smaller than the screen, this style puts a clone of the image across the screen background.
            ''' </summary>
            Tile = 0

            ''' <summary>
            ''' Centers the image on the screen.
            ''' </summary>
            Center = 1

            ''' <summary>
            ''' Shrinks or enlarges the image to fit the monitor's height and widht. 
            ''' </summary>
            Stretch = 2

            ''' <summary>
            ''' Shrinks or enlarges the image to fit the monitor's height.
            ''' </summary>
            Fit = 3

            ''' <summary>
            ''' Shrinks or enlarges the image to fit the monitor's width.
            ''' </summary>
            Fill = 4

        End Enum

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sets the current desktop wallpaper.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="imagePath">
        ''' The wallpaper filepath.
        ''' </param>
        ''' 
        ''' <param name="style">
        ''' The wallpaper style to apply.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' imagepath
        ''' </exception>
        ''' 
        ''' <exception cref="ArgumentException">
        ''' Invalid enumeration value;style
        ''' </exception>
        ''' 
        ''' <exception cref="Exception">
        ''' The current operating system doesn't support a fitted or filled wallpaper.
        ''' </exception>
        ''' 
        ''' <exception cref="Win32Exception">
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub SetDesktopWallpaper(ByVal imagePath As String,
                                              ByVal style As ThemingUtil.Wallpapers.WallpaperStyle)

            If String.IsNullOrWhiteSpace(imagePath) Then
                Throw New ArgumentNullException(paramName:="imagepath")

            Else
                ' Set the wallpaper style and tile. 
                ' Two registry values are set in the 'HKxx\Control Panel\Desktop' key.
                '
                ' TileWallpaper:
                '  0: The wallpaper picture should not be tiled .
                '  1: The wallpaper picture should be tiled .
                ' 
                ' WallpaperStyle:
                '  0:  The image is centered if 'TileWallpaper=0' or tiled if 'TileWallpaper=1'.
                '  2:  The image is stretched to fill the screen.
                '  6:  The image is resized to fit the screen while maintaining the aspect ratio. (Windows 7 and higher)
                ' 10: The image is resized and cropped to fill the screen while maintaining the aspect ratio. (Windows 7 and higher)
                Using regKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Control Panel\Desktop", writable:=True)

                    Select Case style

                        Case ThemingUtil.Wallpapers.WallpaperStyle.Tile
                            regKey.SetValue("WallpaperStyle", "0")
                            regKey.SetValue("TileWallpaper", "1")

                        Case ThemingUtil.Wallpapers.WallpaperStyle.Center
                            regKey.SetValue("WallpaperStyle", "0")
                            regKey.SetValue("TileWallpaper", "0")

                        Case ThemingUtil.Wallpapers.WallpaperStyle.Stretch
                            regKey.SetValue("WallpaperStyle", "2")
                            regKey.SetValue("TileWallpaper", "0")

                        Case ThemingUtil.Wallpapers.WallpaperStyle.Fit ' (Windows 7 and higher)
                            regKey.SetValue("WallpaperStyle", "6")
                            regKey.SetValue("TileWallpaper", "0")

                        Case ThemingUtil.Wallpapers.WallpaperStyle.Fill ' (Windows 7 and higher)
                            regKey.SetValue("WallpaperStyle", "10")
                            regKey.SetValue("TileWallpaper", "0")

                        Case Else
                            Throw New ArgumentException(message:="Invalid enumeration value", paramName:="style")

                    End Select

                End Using

                Dim imageExt As String = Path.GetExtension(imagePath)

                If (imageExt.Equals(".jpg", StringComparison.OrdinalIgnoreCase) OrElse imageExt.Equals(".jpeg", StringComparison.OrdinalIgnoreCase)) AndAlso
                    Not (ThemingUtil.Wallpapers.WallpaperStylesFitFillAreSupported) Then

                    Throw New Exception(message:="The current operating system doesn't support a fitted or filled wallpaper.")

                Else
                    Dim uiAction As ThemingUtil.NativeMethods.SystemParametersActionFlags =
                        ThemingUtil.NativeMethods.SystemParametersActionFlags.SetDesktopWallpaper

                    If Not ThemingUtil.NativeMethods.SystemParametersInfo(uiAction, Nothing, imagePath, ThemingUtil.NativeMethods.SystemParametersWinIniFlags.None) Then
                        Throw New Win32Exception([error]:=Marshal.GetLastWin32Error)
                    End If

                End If

            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Removes the current desktop wallpaper from screen.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="Win32Exception">
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub RemoveDesktopWallpaper()

            Dim uiAction As ThemingUtil.NativeMethods.SystemParametersActionFlags =
                ThemingUtil.NativeMethods.SystemParametersActionFlags.SetDesktopWallpaper

            If Not ThemingUtil.NativeMethods.SystemParametersInfo(uiAction, Nothing, String.Empty, ThemingUtil.NativeMethods.SystemParametersWinIniFlags.None) Then
                Throw New Win32Exception([error]:=Marshal.GetLastWin32Error)
            End If

        End Sub

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="Wallpapers"/> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub New()
        End Sub

#End Region

#Region " Hidden Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether the specified System.Object instances are considered equal.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Function Equals(ByVal obj As Object) As Boolean
            Return MyBase.Equals(obj)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether the specified System.Object instances are the same instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Function ReferenceEquals(ByVal objA As Object, ByVal objB As Object) As Boolean
            Return Nothing
        End Function

#End Region

    End Class

#End Region

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
