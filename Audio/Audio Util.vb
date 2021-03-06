' ***********************************************************************
' Author   : Elektro
' Modified : 29-October-2015
' ***********************************************************************
' <copyright file="Audio Util.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Public Members Summary "

#Region " Types "

' AudioUtil.AudioPlayer : IDisposable
' AudioUtil.StereoVolume <Serializable>
' AudioUtil.WaveRecorder : IDisposable

#End Region

#Region " Cosntructors "

' AudioUtil.AudioPlayer.New()
' AudioUtil.AudioPlayer.New(Form)

' AudioUtil.StereoVolume(Integer, Integer)

' AudioUtil.WaveRecorder.New()

#End Region

#Region " Properties "

' AudioUtil.AudioPlayer.Filepath As String
' AudioUtil.AudioPlayer.Status As PlayerState
' AudioUtil.AudioPlayer.PlaybackMode As AudioPlayMode
' AudioUtil.AudioPlayer.Channels As Integer
' AudioUtil.AudioPlayer.Length As Integer
' AudioUtil.AudioPlayer.Position As TimeSpan
' AudioUtil.AudioPlayer.IsFileLoaded As Boolean

' AudioUtil.StereoVolume.LeftChannel As Integer
' AudioUtil.StereoVolume.RightChannel As Integer

' AudioUtil.WaveRecorder.Status As AudioUtil.WaveRecorder.RecorderStatus

#End Region

#Region " Enumerations "

' AudioUtil.ChannelMode As Integer

' AudioUtil.AudioPlayer.PlayerState As Integer

' AudioUtil.WaveRecorder.RecorderStatus As Integer

#End Region

#Region " Functions "

' AudioUtil.GetAppVolume() As AudioUtil.StereoVolume

#End Region

#Region " Methods "

' AudioUtil.MuteSystemVolume() 

' AudioUtil.SetAppVolume(Integer) 
' AudioUtil.SetAppVolume(Integer, Integer) 
' AudioUtil.SetAppVolume(AudioUtil.StereoVolume) 

' AudioUtil.AudioPlayer.LoadFile(String)
' AudioUtil.AudioPlayer.UnloadFile
' AudioUtil.AudioPlayer.Play(Opt: AudioPlayMode)
' AudioUtil.AudioPlayer.Seek(Long)
' AudioUtil.AudioPlayer.Seek(TimeSpan)
' AudioUtil.AudioPlayer.Pause
' AudioUtil.AudioPlayer.Resume
' AudioUtil.AudioPlayer.Stop
' AudioUtil.AudioPlayer.Dispose

' AudioUtil.WaveRecorder.Record
' AudioUtil.WaveRecorder.Stop
' AudioUtil.WaveRecorder.Play
' AudioUtil.WaveRecorder.Delete
' AudioUtil.WaveRecorder.Save(String, Opt: Boolean)
' AudioUtil.WaveRecorder.Dispose

#End Region

#End Region

#Region " Usage Examples "

#Region " WaveRecorder "

'Dim recorder As New WaveRecorder
'
'Sub Button_Record_Click() Handles Button_Record.Click
'
'    If Not (recorder.Status = WaveRecorder.RecorderStatus.Recording) Then
'        recorder.Record()
'    End If
'
'End Sub
'
'Sub Button_Stop_Click() Handles Button_Stop.Click
'
'    If (recorder.Status = WaveRecorder.RecorderStatus.Recording) Then
'        recorder.Stop()
'    End If
'
'End Sub
'
'Sub Button_Play_Click() Handles Button_Play.Click
'
'    If (recorder.Status = WaveRecorder.RecorderStatus.Stopped) Then
'        recorder.Play()
'    End If
'
'End Sub
'
'Sub Button_Delete_Click() Handles Button_Delete.Click
'
'    If Not (recorder.Status = WaveRecorder.RecorderStatus.Empty) Then
'        recorder.Delete()
'    End If
'
'End Sub
'
'Sub Button_Save_Click() Handles Button_Save.Click
'
'    If Not (recorder.Status = WaveRecorder.RecorderStatus.Empty) Then
'        recorder.Save("C:\File.wav", overWrite:=True)
'    End If
'
'End Sub

#End Region

#Region " AudioPlayer "

'Dim player As New AudioPlayer
'
'Sub Button_LoadFile_Click() Handles Button_LoadFile.Click
'
'    If Not player.IsFileLoaded Then
'        player.LoadFile("C:\File.wav")
'    End If
'
'End Sub
'
'Sub Button_Play_Click() Handles Button_Play.Click
'
'    If Not (player.Status = AudioPlayer.PlayerState.Playing) Then
'        player.Play(AudioPlayMode.Background)
'    End If
'
'End Sub
'
'Sub Button_Stop_Click() Handles Button_Stop.Click
'
'    If Not (player.Status = AudioPlayer.PlayerState.Stopped) Then
'        player.Stop()
'    End If
'
'End Sub
'
'Sub Button_PauseResume_Click() Handles Button_PauseResume.Click
'
'    If (player.Status = AudioPlayer.PlayerState.Playing) Then
'        player.Pause()
'
'    ElseIf (player.Status = AudioPlayer.PlayerState.Paused) Then
'        player.Resume()
'
'    End If
'
'End Sub
'
'Private Sub Button_SeekBackward_Click(sender As Object, e As EventArgs) Handles Button_SeekBackward.Click
'
'    Dim currentPosition As Long = CLng(player.Position.TotalMilliseconds)
'
'    If ((currentPosition - 5000) <= 0) Then
'        player.Seek(0)
'
'    Else
'        player.Seek(currentPosition - 5000)
'
'    End If
'
'End Sub
'
'Private Sub Button_SeekForward_Click(sender As Object, e As EventArgs) Handles Button_SeekForward.Click
'
'    Dim currentPosition As Long = CLng(player.Position.TotalMilliseconds)
'
'    If Not ((currentPosition + 5000) >= player.Length) Then
'        player.Seek(currentPosition + 5000)
'    End If
'
'End Sub
'
'Sub Button_UnloadFile_Click() Handles Button_UnloadFile.Click
'
'    If player.IsFileLoaded Then
'        player.UnLoadFile()
'    End If
'
'End Sub

#End Region

#End Region

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System
Imports System.ComponentModel
Imports System.Linq
Imports System.Runtime.InteropServices

#End Region

#Region " Audio Util "

''' ----------------------------------------------------------------------------------------------------
''' <summary>
''' Contains related audio utilities.
''' </summary>
''' ----------------------------------------------------------------------------------------------------
Public Module AudioUtil

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
        ''' Sends a command string to an MCI device. 
        ''' The device that the command is sent to is specified in the command string.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="command">
        ''' Pointer to a null-terminated string that specifies an MCI command string. 
        ''' For a list, see <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/dd743572(v=vs.85).aspx"/>.
        ''' </param>
        ''' 
        ''' <param name="buffer">
        ''' <see cref="StringBuilder"/> that receives return information. 
        ''' If no return information is needed, this parameter can be <see langword="Nothing"/>.
        ''' </param>
        ''' 
        ''' <param name="bufferSize">
        ''' Size, in characters, of the return buffer specified by the <paramref name="buffer"/> parameter.
        ''' </param>
        ''' 
        ''' <param name="hwndCallback">
        ''' A <see cref="IntPtr"/> to a callback window if the "notify" flag was specified in the command string.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' Returns zero if successful or an error otherwise. 
        ''' The low-order word of the returned DWORD value contains the error return value. 
        ''' If the error is device-specific, the high-order word of the return value is the driver identifier; 
        ''' otherwise, the high-order word is zero. For a list of possible error values, see MCIERR Return Values.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/dd757161%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        <DllImport("winmm.dll", EntryPoint:="mciSendString", SetLastError:=True, CharSet:=CharSet.Ansi, BestFitMapping:=False, ThrowOnUnmappableChar:=True)>
        Friend Shared Function MciSendString(
                         ByVal command As String,
                         ByVal buffer As StringBuilder,
                         ByVal bufferSize As Integer,
                         ByVal hwndCallback As IntPtr
        ) As Integer
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sends the specified message to a window or windows.
        ''' The SendMessage function calls the window procedure for the specified window
        ''' and does not return until the window procedure has processed the message.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="hWnd">
        ''' A handle to the window whose window procedure will receive the message.
        ''' </param>
        ''' 
        ''' <param name="msg">
        ''' The message to be sent.
        ''' </param>
        ''' 
        ''' <param name="wParam">
        ''' Additional message-specific information.
        ''' </param>
        ''' 
        ''' <param name="lParam">
        ''' Additional message-specific information.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The return value specifies the result of the message processing; it depends on the message sent.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms644950%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto, BestFitMapping:=False, ThrowOnUnmappableChar:=True)>
        Friend Shared Function SendMessage(
                         ByVal hWnd As IntPtr,
                         ByVal msg As WindowsMessages,
                         ByVal wParam As IntPtr,
                         ByVal lParam As IntPtr
        ) As IntPtr
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the volume level of the specified waveform-audio output device.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="https://msdn.microsoft.com/en-us/library/aa909806.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="hwo">
        ''' A <see cref="IntPtr"/> to an open waveform-audio output device. 
        ''' This parameter can also be a device identifier.
        ''' </param>
        ''' 
        ''' <param name="dwVolume">T
        ''' Pointer to a variable to be filled with the current volume setting. 
        ''' The low-order word of this location contains the left-channel volume setting, 
        ''' and the high-order word contains the right-channel setting. 
        ''' 
        ''' A value of <c>0xFFFF</c> represents full volume, and a value of <c>0x0000</c> is silence.
        ''' 
        ''' If a device does not support both left and right volume control, 
        ''' the low-order word of the specified location contains the mono volume level. 
        ''' 
        ''' The full 16-bit setting(s) set with the <see cref="NativeMethods.WaveOutSetVolume"/> function is returned, 
        ''' regardless of whether the device supports the full 16 bits of volume-level control. 
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' Returns <see cref="AudioUtil.NativeMethods.WinMmResult.NoError"/> if successful. 
        ''' 
        ''' Possible error values include the following:
        ''' <see cref="AudioUtil.NativeMethods.WinMmResult.BadDeviceId"/>, 
        ''' <see cref="AudioUtil.NativeMethods.WinMmResult.InvalidHandle"/>
        ''' <see cref="AudioUtil.NativeMethods.WinMmResult.NoDriver"/>, 
        ''' <see cref="AudioUtil.NativeMethods.WinMmResult.NoMem"/>,
        ''' <see cref="AudioUtil.NativeMethods.WinMmResult.NotSupported"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DllImport("winmm.dll", EntryPoint:="waveOutGetVolume", SetLastError:=True)>
        Friend Shared Function WaveOutGetVolume(
                         ByVal hwo As IntPtr,
                         ByRef dwVolume As UInteger
        ) As Integer
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sets the volume level of the specified waveform-audio output device.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/dd743874%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="hwo">
        ''' A <see cref="IntPtr"/> to an open waveform-audio output device. 
        ''' This parameter can also be a device identifier.
        ''' </param>
        ''' 
        ''' <param name="dwVolume">T
        ''' New volume setting. 
        ''' The low-order word contains the left-channel volume setting, and the high-order word contains the right-channel setting. 
        ''' A value of <c>0xFFFF</c> represents full volume, and a value of <c>0x0000</c> is silence.
        ''' 
        ''' If a device does not support both left and right volume control, 
        ''' the low-order word of <paramref name="dwVolume"/> specifies the volume level, and the high-order word is ignored.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' Returns <see cref="AudioUtil.NativeMethods.WinMmResult.NoError"/> if successful. 
        ''' 
        ''' Possible error values include the following:
        ''' <see cref="AudioUtil.NativeMethods.WinMmResult.BadDeviceId"/>, 
        ''' <see cref="AudioUtil.NativeMethods.WinMmResult.InvalidHandle"/>
        ''' <see cref="AudioUtil.NativeMethods.WinMmResult.NoDriver"/>, 
        ''' <see cref="AudioUtil.NativeMethods.WinMmResult.NoMem"/>,
        ''' <see cref="AudioUtil.NativeMethods.WinMmResult.NotSupported"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DllImport("winmm.dll", EntryPoint:="waveOutSetVolume", SetLastError:=True)>
        Friend Shared Function WaveOutSetVolume(
                         ByVal hwo As IntPtr,
                         ByVal dwVolume As UInteger
        ) As Integer
        End Function

#End Region

#Region " Enumerations "

        ''' ----------------------------------------------------------------------------------------------------
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
            ''' Notifies an application that an MCI device has completed an operation. 
            ''' MCI devices send this message only when the MCI_NOTIFY flag is used.
            ''' </summary>
            ''' ----------------------------------------------------------------------------------------------------
            ''' <remarks>
            ''' <see href="https://msdn.microsoft.com/en-us/library/windows/desktop/dd757358%28v=vs.85%29.aspx"/>
            ''' </remarks>
            ''' ----------------------------------------------------------------------------------------------------
            MmMciNotify = 953

            ''' ----------------------------------------------------------------------------------------------------
            ''' <summary>
            ''' Notifies a window that the user generated an application command event, for example, 
            ''' by clicking an application command button using the mouse or typing an application command key on the keyboard.
            ''' </summary>
            ''' ----------------------------------------------------------------------------------------------------
            ''' <remarks>
            ''' <see href="http://msdn.microsoft.com/es-es/library/windows/desktop/ms646275%28v=vs.85%29.aspx"/>
            ''' </remarks>
            ''' ----------------------------------------------------------------------------------------------------
            WMAppcommand = &H319

        End Enum

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Specifies additional message-specific information for a System-Defined Message.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms644927%28v=vs.85%29.aspx#system_defined"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        Friend Enum LParams As UInteger

            ''' <summary>
            ''' A Null LParam.
            ''' </summary>
            None = 0UI

            ''' ----------------------------------------------------------------------------------------------------
            ''' <summary>
            ''' Mute the volume.
            ''' </summary>
            ''' ----------------------------------------------------------------------------------------------------
            ''' <remarks>
            ''' <see href="http://msdn.microsoft.com/es-es/library/windows/desktop/ms646275%28v=vs.85%29.aspx"/>
            ''' </remarks>
            ''' ----------------------------------------------------------------------------------------------------
            AppCommandVolumeMute = &H80000

        End Enum

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Defines an error value for <see cref="AudioUtil.NativeMethods.WaveOutGetVolume"/> and 
        ''' <see cref="AudioUtil.NativeMethods.WaveOutSetVolume"/> functions.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://www.pinvoke.net/default.aspx/winmm/MMRESULT.html"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        Friend Enum WinMmResult As Integer

            ' *****************************************************************************
            '                            WARNING!, NEED TO KNOW...
            '
            '  THIS ENUMERATION IS PARTIALLY DEFINED TO MEET THE PURPOSES OF THIS PROJECT
            ' *****************************************************************************

            ''' <summary>
            ''' Successful, no error.
            ''' </summary>
            NoError = &H0

            ''' <summary>
            ''' Specified device identification number is out of range.
            ''' </summary>
            BadDeviceId = &H2

            ''' <summary>
            ''' Specified device handle is invalid.
            ''' </summary>
            InvalidHandle = &H5

            ''' <summary>
            ''' No device driver is present.
            ''' </summary>
            NoDriver = &H6

            ''' <summary>
            ''' Unable to allocate or lock memory.
            ''' </summary>
            NoMem = &H7

            ''' <summary>
            ''' Function is not supported.
            ''' </summary>
            NotSupported = &H8

        End Enum

#End Region

    End Class

#End Region

#Region " Types "

#Region " StereoVolume "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Defines a volume level from range 0 to 100 for left and right stereo channels.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <Serializable>
    Public Structure StereoVolume

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the left channel valume, from range 0 to 100.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The left channel valume, from range 0 to 100.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property LeftChannel As Integer
            Get
                Return Me.leftChannelB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' The left channel valume.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly leftChannelB As Integer

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the right channel valume, from range 0 to 100.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The right channel valume, from range 0 to 100.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property RightChannel As Integer
            Get
                Return Me.rightChannelB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' The right channel valume.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly rightChannelB As Integer

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="StereoVolume"/> structure.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="leftChannelVolume">
        ''' The left channel valume, from range 0 to 100.
        ''' </param>
        ''' 
        ''' <param name="rightChannelVolume">
        ''' The right channel valume, from range 0 to 100.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentOutOfRangeException">
        ''' leftChannelVolume;A value between 0 and 100 is required.
        ''' or
        ''' rightChannelVolume;A value between 0 and 100 is required.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub New(leftChannelVolume As Integer, rightChannelVolume As Integer)

            If (leftChannelVolume < 0) OrElse (leftChannelVolume > 100) Then
                Throw New ArgumentOutOfRangeException(paramName:="leftChannelVolume", message:="A value between 0 and 100 is required.")

            ElseIf (rightChannelVolume < 0) OrElse (rightChannelVolume > 100) Then
                Throw New ArgumentOutOfRangeException(paramName:="rightChannelVolume", message:="A value between 0 and 100 is required.")

            Else
                Me.leftChannelB = leftChannelVolume
                Me.rightChannelB = rightChannelVolume

            End If

        End Sub

#End Region

    End Structure

#End Region

#Region " WaveRecorder "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Defines a Wave audio recorder.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' Default recording quality specifications: 
    ''' Microsoft Wave, two channels (Stereo), 16-bit depth, 44.100 khz, 192.000 kbps.
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim recorder As New WaveRecorder
    ''' 
    ''' Sub Button_Record_Click() Handles Button_Record.Click
    ''' 
    '''     If Not (recorder.Status = WaveRecorder.RecorderStatus.Recording) Then
    '''         recorder.Record()
    '''     End If
    ''' 
    ''' End Sub
    ''' 
    ''' Sub Button_Stop_Click() Handles Button_Stop.Click
    ''' 
    '''     If (recorder.Status = WaveRecorder.RecorderStatus.Recording) Then
    '''         recorder.Stop()
    '''     End If
    ''' 
    ''' End Sub
    ''' 
    ''' Sub Button_Play_Click() Handles Button_Play.Click
    ''' 
    '''     If (recorder.Status = WaveRecorder.RecorderStatus.Stopped) Then
    '''         recorder.Play()
    '''     End If
    ''' 
    ''' End Sub
    ''' 
    ''' Sub Button_Delete_Click() Handles Button_Delete.Click
    ''' 
    '''     If Not (recorder.Status = WaveRecorder.RecorderStatus.Empty) Then
    '''         recorder.Delete()
    '''     End If
    ''' 
    ''' End Sub
    ''' 
    ''' Sub Button_Save_Click() Handles Button_Save.Click
    ''' 
    '''     If Not (recorder.Status = WaveRecorder.RecorderStatus.Empty) Then
    '''         recorder.Save("C:\File.wav", overWrite:=True)
    '''     End If
    ''' 
    ''' End Sub
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    Public NotInheritable Class WaveRecorder : Implements IDisposable

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the current recording status.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The current recording status.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Status As RecorderStatus
            Get
                Return Me.statusB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' The current recording status.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private statusB As RecorderStatus = RecorderStatus.Empty

#End Region

#Region " Constants / ReadOnly "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The alias identifier for <see cref="AudioUtil.NativeMethods.MciSendString"/> function.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly aliasId As String

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="WaveRecorder"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub New()

            Dim rand As New Random
            Me.aliasId = String.Format("WaveRecorder_Id_{0}", rand.Next(minValue:=1, maxValue:=Integer.MaxValue))

        End Sub

#End Region

#Region " Enumerations "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Specifies the state of a <see cref="AudioUtil.WaveRecorder"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Enum RecorderStatus As Integer

            ''' <summary>
            ''' The recorder is stopped and there is no audio recorded.
            ''' </summary>
            Empty = 0

            ''' <summary>
            ''' The recorder is stopped.
            ''' </summary>
            Stopped = 1

            ''' <summary>
            ''' The recorder is recording audio.
            ''' </summary>
            Recording = 2

        End Enum

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Starts or resumes the audio recording.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="Exception">
        ''' The recorder is already recording.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub Record()

            If (Me.statusB = RecorderStatus.Recording) Then
                Throw New Exception(message:="The recorder is already recording.")

            Else
                Dim openCommand As String =
                    String.Format("open new type waveaudio alias {0} wait", Me.aliasId)

                Dim captureCommand As String =
                    String.Format("set {0} time format ms bitspersample {1} channels {2} samplespersec {3} bytespersec {4} alignment {5}",
                                  Me.aliasId, 16, 2, 44100, 192000, 4)

                AudioUtil.NativeMethods.MciSendString(openCommand, Nothing, 0, IntPtr.Zero)
                AudioUtil.NativeMethods.MciSendString(captureCommand, Nothing, 0, IntPtr.Zero)
                AudioUtil.NativeMethods.MciSendString(String.Format("record {0}", Me.aliasId), Nothing, 0, IntPtr.Zero)
                Me.statusB = RecorderStatus.Recording

            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Stops recording.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="Exception">
        ''' The recorder is already stopped.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub [Stop]()

            If (Me.statusB <> RecorderStatus.Recording) Then
                Throw New Exception(message:="The recorder is already stopped.")

            Else
                AudioUtil.NativeMethods.MciSendString(String.Format("stop {0}", Me.aliasId), Nothing, 0, IntPtr.Zero)
                Me.statusB = RecorderStatus.Stopped

            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Plays the recording.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="Exception">
        ''' The recorder is not stopped.
        ''' or
        ''' Any audio is recorded
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub Play()

            If (Me.statusB = RecorderStatus.Recording) Then
                Throw New Exception(message:="The recorder is not stopped.")

            ElseIf (Me.statusB = RecorderStatus.Empty) Then
                Throw New Exception(message:="Any audio is recorded.")

            Else
                AudioUtil.NativeMethods.MciSendString(String.Format("play {0} from 0", Me.aliasId), Nothing, 0, IntPtr.Zero)

            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Deletes any audio recorded.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="Exception">
        ''' The recording is already empty.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub Delete()

            If (Me.statusB = RecorderStatus.Empty) Then
                Throw New Exception(message:="The recording is already empty.")

            Else
                AudioUtil.NativeMethods.MciSendString(String.Format("stop {0}", Me.aliasId), Nothing, 0, IntPtr.Zero)
                AudioUtil.NativeMethods.MciSendString(String.Format("delete {0}", Me.aliasId), Nothing, 0, IntPtr.Zero)
                Me.statusB = RecorderStatus.Empty

            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Stops recording and saves the recorded audio to disk.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="filepath">
        ''' The filepath.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="Exception">
        ''' Cannot save because any audio is recorded.
        ''' or
        ''' The destination file already exists.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub Save(ByVal filepath As String, Optional overWrite As Boolean = False)

            If (Me.statusB = RecorderStatus.Empty) Then
                Throw New Exception(message:="Cannot save because any audio is recorded.")

            ElseIf Not (overWrite) AndAlso (File.Exists(filepath)) Then
                Throw New IOException(message:="The destination file already exists.")

            Else
                AudioUtil.NativeMethods.MciSendString(String.Format("stop {0}", Me.aliasId), Nothing, 0, IntPtr.Zero)
                AudioUtil.NativeMethods.MciSendString(String.Format("save {0} ""{1}""", Me.aliasId, filepath), Nothing, 0, IntPtr.Zero)
                Me.statusB = RecorderStatus.Stopped

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
        Protected Sub Dispose(ByVal isDisposing As Boolean)

            If (Not Me.isDisposed) AndAlso (isDisposing) Then
                AudioUtil.NativeMethods.MciSendString(String.Format("stop {0}", Me.aliasId), Nothing, 0, IntPtr.Zero)
                AudioUtil.NativeMethods.MciSendString(String.Format("delete {0}", Me.aliasId), Nothing, 0, IntPtr.Zero)
                AudioUtil.NativeMethods.MciSendString(String.Format("close {0}", Me.aliasId), Nothing, 0, IntPtr.Zero)
                Me.statusB = RecorderStatus.Empty
            End If

            Me.isDisposed = True

        End Sub

#End Region

    End Class

#End Region

#Region " AudioPlayer "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Plays a Wave, Mp3 or Mid file.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim player As New AudioPlayer
    ''' 
    ''' Sub Button_LoadFile_Click() Handles Button_LoadFile.Click
    ''' 
    '''     If Not player.IsFileLoaded Then
    '''         player.LoadFile("C:\File.wav")
    '''     End If
    ''' 
    ''' End Sub
    ''' 
    ''' Sub Button_Play_Click() Handles Button_Play.Click
    ''' 
    '''     If Not (player.Status = AudioPlayer.PlayerState.Playing) Then
    '''         player.Play(AudioPlayMode.Background)
    '''     End If
    ''' 
    ''' End Sub
    ''' 
    ''' Sub Button_Stop_Click() Handles Button_Stop.Click
    ''' 
    '''     If Not (player.Status = AudioPlayer.PlayerState.Stopped) Then
    '''         player.Stop()
    '''     End If
    ''' 
    ''' End Sub
    ''' 
    ''' Sub Button_PauseResume_Click() Handles Button_PauseResume.Click
    ''' 
    '''     If (player.Status = AudioPlayer.PlayerState.Playing) Then
    '''         player.Pause()
    ''' 
    '''     ElseIf (player.Status = AudioPlayer.PlayerState.Paused) Then
    '''         player.Resume()
    ''' 
    '''     End If
    ''' 
    ''' End Sub
    ''' 
    ''' Private Sub Button_SeekBackward_Click(sender As Object, e As EventArgs) Handles Button_SeekBackward.Click
    ''' 
    '''     Dim currentPosition As Long = CLng(player.Position.TotalMilliseconds)
    ''' 
    '''     If ((currentPosition - 5000) &lt;= 0) Then
    '''         player.Seek(0)
    ''' 
    '''     Else
    '''         player.Seek(currentPosition - 5000)
    ''' 
    '''     End If
    ''' 
    ''' End Sub
    ''' 
    ''' Private Sub Button_SeekForward_Click(sender As Object, e As EventArgs) Handles Button_SeekForward.Click
    ''' 
    '''     Dim currentPosition As Long = CLng(player.Position.TotalMilliseconds)
    ''' 
    '''     If Not ((currentPosition + 5000) &gt;= player.Length) Then
    '''         player.Seek(currentPosition + 5000)
    '''     End If
    ''' 
    ''' End Sub
    ''' 
    ''' Sub Button_UnloadFile_Click() Handles Button_UnloadFile.Click
    ''' 
    '''     If player.IsFileLoaded Then
    '''         player.UnLoadFile()
    '''     End If
    ''' 
    ''' End Sub
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    Public NotInheritable Class AudioPlayer : Inherits NativeWindow : Implements IDisposable

#Region " Variables "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The window to associate a handle.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private WithEvents window As Form

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The alias identifier for <see cref="AudioUtil.NativeMethods.MciSendString"/> function.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private aliasId As String

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Flag to cancel the loop of a <see cref="AudioPlayMode.BackgroundLoop"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private cancelLoop As Boolean

#End Region

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the filepath of the audio file that will be played.
        ''' </summary>
        ''' <value>
        ''' The filepath of the audio file that will be played.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="Exception">
        ''' Any file loaded.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Filepath As String
            <DebuggerStepThrough>
            Get
                If String.IsNullOrEmpty(Me.aliasId) Then
                    Throw New Exception(Message:="Any file loaded.")

                Else
                    Return Me.filepathB

                End If
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( backing field )
        ''' The filepath of the audio file that will be played.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private filepathB As String = String.Empty

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the current player State.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The current player State.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Status As PlayerState
            <DebuggerStepThrough>
            Get
                Dim buffer As New StringBuilder With {.Capacity = 255}
                AudioUtil.NativeMethods.MciSendString(String.Format("status {0} mode", Me.aliasId), buffer, buffer.Capacity, IntPtr.Zero)

                Dim result As AudioUtil.AudioPlayer.PlayerState
                If [Enum].TryParse(buffer.ToString, True, result) Then
                    Return result
                Else
                    Return PlayerState.Stopped
                End If
            End Get
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the playback mode for the current audio file.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The playback mode for the current audio file.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Property PlaybackMode As AudioPlayMode

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the channels of the current audio file.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The channels of the current audio file.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="Exception">
        ''' Any file loaded.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Channels As Integer
            <DebuggerStepThrough>
            Get
                If String.IsNullOrEmpty(Me.aliasId) Then
                    Throw New Exception(Message:="Any file loaded.")

                Else
                    Dim buffer As New StringBuilder With {.Capacity = 255}
                    AudioUtil.NativeMethods.MciSendString(String.Format("status {0} channels", Me.aliasId), buffer, buffer.Capacity, IntPtr.Zero)

                    Dim result As Integer
                    If Integer.TryParse(buffer.ToString, result) Then
                        Return result

                    Else
                        Return -1

                    End If

                End If
            End Get
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the audio length of the current file, in milleseconds.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The audio length of the current file, in milleseconds.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="Exception">
        ''' Any file loaded.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Length As Integer
            <DebuggerStepThrough>
            Get
                If String.IsNullOrEmpty(Me.aliasId) Then
                    Throw New Exception(Message:="Any file loaded.")

                Else
                    Dim buffer As New StringBuilder With {.Capacity = 255}
                    AudioUtil.NativeMethods.MciSendString(String.Format("set {0} time format milliseconds", Me.aliasId), Nothing, 0, IntPtr.Zero)
                    AudioUtil.NativeMethods.MciSendString(String.Format("status {0} length", Me.aliasId), buffer, buffer.Capacity, IntPtr.Zero)

                    Dim result As Integer
                    If Integer.TryParse(buffer.ToString, result) Then
                        Return result

                    Else
                        Return -1

                    End If

                End If
            End Get
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the audio position of the current playback, in milleseconds.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The audio position of the current playback, in milleseconds.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="Exception">
        ''' Any file loaded.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Position As TimeSpan
            <DebuggerStepThrough>
            Get
                If String.IsNullOrEmpty(Me.aliasId) Then
                    Throw New Exception(Message:="Any file loaded.")

                Else
                    Dim buffer As New StringBuilder With {.Capacity = 255}
                    AudioUtil.NativeMethods.MciSendString(String.Format("set {0} time format milliseconds", Me.aliasId), Nothing, 0, IntPtr.Zero)
                    AudioUtil.NativeMethods.MciSendString(String.Format("status {0} position", Me.aliasId), buffer, buffer.Capacity, IntPtr.Zero)

                    Dim result As Long
                    If Long.TryParse(buffer.ToString, result) Then
                        Return TimeSpan.FromMilliseconds(result)

                    Else
                        Return Nothing

                    End If

                End If
            End Get
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets a value that indicates whether the player has loaded a file.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' A value that indicates whether the player has loaded a file.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property IsFileLoaded As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me.aliasId)
            End Get
        End Property

#End Region

#Region " Enumerations "

        ''' <summary>
        ''' Specifies the state of a <see cref="AudioUtil.AudioPlayer"/>.
        ''' </summary>
        Public Enum PlayerState As Integer

            ''' <summary>
            ''' The player is playing a file.
            ''' </summary>
            Playing = 0

            ''' <summary>
            ''' The player is paused.
            ''' </summary>
            Paused = 1

            ''' <summary>
            ''' The player is stopped.
            ''' </summary>
            Stopped = 2

        End Enum

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="AudioPlayer"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New()
            Me.New(Form.ActiveForm)
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="AudioUtil.AudioPlayer"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="window">
        ''' A window to associate its handle to this <see cref="AudioPlayer"/> instance.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New(ByVal window As Form)

            Me.window = window

            ' Assign the window handle.
            Me.SetFormHandle()

        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Loads an audio file on the player.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="filepath">
        ''' The audio filepath.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="FileNotFoundException">
        ''' File not found.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub LoadFile(ByVal filepath As String)

            If Not File.Exists(filepath) Then
                Throw New FileNotFoundException(message:="File not found.", fileName:=filepath)

            Else
                Me.UnLoadFile()
                Me.filepathB = filepath

                Dim rand As New Random
                Me.aliasId = String.Format("Player_Id_{0}", rand.Next(minValue:=1, maxValue:=Integer.MaxValue))

                Select Case Path.GetExtension(Me.filepathB).ToLower

                    Case ".mp3"
                        Dim buffer As New StringBuilder With {.Capacity = 255}

                        AudioUtil.NativeMethods.MciSendString(String.Format("open ""{0}"" type mpegvideo alias {1}", Me.filepathB, Me.aliasId), buffer, 0, IntPtr.Zero)
                        MsgBox(buffer.ToString)
                    Case ".wav"
                        AudioUtil.NativeMethods.MciSendString(String.Format("open ""{0}"" type waveaudio alias {1}", Me.filepathB, Me.aliasId), Nothing, 0, IntPtr.Zero)

                    Case ".mid", ".midi"
                        ' AudioUtil.NativeMethods.MciSendString("stop midi", Nothing, 0, IntPtr.Zero)
                        ' AudioUtil.NativeMethods.MciSendString("close midi", Nothing, 0, IntPtr.Zero)
                        AudioUtil.NativeMethods.MciSendString(String.Format("open sequencer! ""{0}"" alias {1}", Me.filepathB, Me.aliasId), Nothing, 0, IntPtr.Zero)

                    Case Else
                        Throw New NotImplementedException("Audio format not supported.")
                        Exit Sub

                End Select

            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Unloads the current audio file from playback.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub UnLoadFile()

            AudioUtil.NativeMethods.MciSendString(String.Format("stop {0}", Me.aliasId), Nothing, 0, IntPtr.Zero)
            AudioUtil.NativeMethods.MciSendString(String.Format("close {0}", Me.aliasId), Nothing, 0, IntPtr.Zero)
            Me.filepathB = String.Empty
            Me.aliasId = String.Empty

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Plays the file that is specified as the filename.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="playbackMode">
        ''' The playback mode.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="NotImplementedException">
        ''' Audio format not supported.
        ''' </exception>
        ''' 
        ''' <exception cref="InvalidEnumArgumentException">
        ''' playbackMode
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub Play(Optional ByVal playbackMode As AudioPlayMode = AudioPlayMode.Background)

            Dim playCommand As String = String.Empty
            Select Case playbackMode

                Case AudioPlayMode.Background
                    playCommand = String.Format("play {0} from 0", Me.aliasId)
                    Me.PlaybackMode = AudioPlayMode.Background

                Case AudioPlayMode.BackgroundLoop
                    playCommand = String.Format("play {0} from 0 notify", Me.aliasId)
                    Me.PlaybackMode = AudioPlayMode.BackgroundLoop

                Case AudioPlayMode.WaitToComplete
                    playCommand = String.Format("play {0} from 0 wait", Me.aliasId)
                    Me.PlaybackMode = AudioPlayMode.WaitToComplete

                Case Else
                    Throw New InvalidEnumArgumentException("playbackMode", playbackMode, GetType(AudioPlayMode))
                    Exit Sub

            End Select

            AudioUtil.NativeMethods.MciSendString(playCommand, Nothing, 0, If(playbackMode = AudioPlayMode.BackgroundLoop, Me.Handle, IntPtr.Zero))

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sets the playback position.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="Exception">
        ''' Cannot pause playback because the player is not playing any audio.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub Seek(ByVal milliseconds As Long)

            If Not (Me.IsFileLoaded) Then
                Throw New Exception("Any file loaded.")

            ElseIf (Me.Status = PlayerState.Playing) Then
                    AudioUtil.NativeMethods.MciSendString(String.Format("seek {0} to {1}", Me.aliasId, milliseconds), Nothing, 0, IntPtr.Zero)
                    AudioUtil.NativeMethods.MciSendString(String.Format("play {0}", Me.aliasId), Nothing, 0, IntPtr.Zero)

            Else
                AudioUtil.NativeMethods.MciSendString(String.Format("seek {0} to {1}", Me.aliasId, milliseconds), Nothing, 0, IntPtr.Zero)

            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Pauses the current playback.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="Exception">
        ''' Cannot pause playback because the player is not playing any audio.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub Seek(ByVal position As TimeSpan)

            Me.Seek(CLng(position.TotalMilliseconds))

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Pauses the current playback.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="Exception">
        ''' Cannot pause playback because the player is not playing any audio.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub Pause()

            If (Me.Status <> PlayerState.Playing) Then
                Throw New Exception("Cannot pause playback because the player is not playing any audio.")

            Else
                Me.cancelLoop = True
                AudioUtil.NativeMethods.MciSendString(String.Format("pause {0}", Me.aliasId), Nothing, 0, IntPtr.Zero)

            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Resumes a previously paused playback.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="Exception">
        ''' Cannot resume playback because the player is not paused.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub [Resume]()

            If (Me.Status <> PlayerState.Paused) Then
                Throw New Exception("Cannot resume playback because the player is not paused.")

            Else
                Me.cancelLoop = False
                AudioUtil.NativeMethods.MciSendString(String.Format("resume {0}", Me.aliasId), Nothing, 0, IntPtr.Zero)

            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Stops the current playback.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="Exception">
        ''' Cannot stop playback because the player is not playing any audio.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub [Stop]()

            If (Me.Status = PlayerState.Stopped) Then
                Throw New Exception("Cannot stop playback because the player is not playing any audio.")

            Else
                Me.cancelLoop = True
                AudioUtil.NativeMethods.MciSendString(String.Format("stop {0}", Me.aliasId), Nothing, 0, IntPtr.Zero)

            End If

        End Sub

#End Region

#Region " Event Handlers "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Assigns a handle to this <see cref="NativeWindow"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Private Sub SetFormHandle() Handles window.HandleCreated,
                                            window.Load,
                                            window.Shown

            Try
                If Not Me.Handle.Equals(Me.window.Handle) Then
                    Me.AssignHandle(Me.window.Handle)
                End If

            Catch ex As InvalidOperationException
                ' Do Nothing.

            End Try

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Releases the handle associated with this window.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Private Sub OnHandleDestroyed() Handles window.HandleDestroyed

            Me.ReleaseHandle()

        End Sub

#End Region

#Region " Window Procedure "

        ''' <summary>
        ''' Invokes the default window procedure associated with this window to process messages.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="m">
        ''' A <see cref="T:System.Windows.Forms.Message"/> that is associated with the current Windows message.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Protected Overrides Sub WndProc(ByRef m As Message)

            MyBase.WndProc(m)

            If (m.Msg = AudioUtil.NativeMethods.WindowsMessages.MmMciNotify) Then

                If Not (cancelLoop) Then
                    Me.Play(AudioPlayMode.BackgroundLoop)

                Else
                    Me.cancelLoop = False

                End If

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
        Protected Sub Dispose(ByVal isDisposing As Boolean)

            If (Not Me.isDisposed) AndAlso (isDisposing) Then
                Me.UnLoadFile()
                Me.window = Nothing
                Me.ReleaseHandle()
                Me.DestroyHandle()
            End If

            Me.isDisposed = True

        End Sub

#End Region

#Region " Hidden Methods "

        ''' <summary>
        ''' Assigns a handle to this window.
        ''' </summary>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Sub AssignHandle(handle As IntPtr)
            MyBase.AssignHandle(handle)
        End Sub

        ''' <summary>
        ''' Creates a window and its handle with the specified creation parameters.
        ''' </summary>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Sub CreateHandle(cp As CreateParams)
            MyBase.CreateHandle(cp)
        End Sub

        ''' <summary>
        ''' Destroys the window and its handle.
        ''' </summary>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Sub DestroyHandle()
            MyBase.DestroyHandle()
        End Sub

        ''' <summary>
        ''' Releases the handle associated with this window.
        ''' </summary>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Sub ReleaseHandle()
            MyBase.ReleaseHandle()
        End Sub

        ''' <summary>
        ''' Retrieves the current lifetime service object that controls the lifetime policy for this instance.
        ''' </summary>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Function GetLifeTimeService() As Object
            Return MyBase.GetLifetimeService
        End Function

        ''' <summary>
        ''' Obtains a lifetime service object to control the lifetime policy for this instance.
        ''' </summary>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Function InitializeLifeTimeService() As Object
            Return MyBase.InitializeLifetimeService
        End Function

        ''' <summary>
        ''' Creates an object that contains all the relevant information required to generate a proxy used to communicate with a remote object.
        ''' </summary>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Function CreateObjRef(requestedType As Type) As System.Runtime.Remoting.ObjRef
            Return MyBase.CreateObjRef(requestedType)
        End Function

        ''' <summary>
        ''' Invokes the default window procedure associated with this window.
        ''' </summary>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Shadows Sub DefWndProc(ByRef m As Message)
            MyBase.DefWndProc(m)
        End Sub

#End Region

    End Class

#End Region

#End Region

#Region " Enumerations "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Specifies an ausio device channel mode.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    Public Enum ChannelMode As Integer

        ''' <summary>
        ''' One channel.
        ''' </summary>
        Mono = 1

        ''' <summary>
        ''' Two channels.
        ''' </summary>
        Stereo = 2

    End Enum

#End Region

#Region " Public Methods "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Gets the volume of the current application.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' A <see cref="AudioUtil.StereoVolume"/> structure that contains the left and right channels volume, from range 0 to 100.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="Win32Exception">
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function GetAppVolume() As AudioUtil.StereoVolume

        Dim varVolume As UInteger = 0UI
        Dim result As Integer = NativeMethods.WaveOutGetVolume(IntPtr.Zero, varVolume)
        Dim win32Err As Integer = Marshal.GetLastWin32Error

        If (result <> NativeMethods.WinMmResult.NoError) Then
            Throw New Win32Exception([error]:=win32Err)

        Else
            Dim leftChannelVolume As UShort =
                CUShort(BitConverter.ToUInt16(BitConverter.GetBytes(varVolume), 0) / (UShort.MaxValue / 100))

            Dim rightChannelVolume As UShort =
                CUShort(BitConverter.ToUInt16(BitConverter.GetBytes(varVolume), 2) / (UShort.MaxValue / 100))

            Return New AudioUtil.StereoVolume(leftChannelVolume, rightChannelVolume)

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Sets the volume of the current application.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="volume">
    ''' The volume, from range 0 to 100.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' volume,A value between 0 and 100 is required.
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub SetAppVolume(ByVal volume As Integer)

        If (volume < 0) OrElse (volume > 100) Then
            Throw New ArgumentOutOfRangeException(paramName:="volume", message:="A value between 0 and 100 is required.")

        Else
            AudioUtil.SetAppVolume(channelLeft:=volume, channelRight:=volume)

        End If

    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Sets the volume of the current application.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="volume">
    ''' A <see cref="AudioUtil.StereoVolume"/> structure that contains the left and right channels volume, from range 0 to 100.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub SetAppVolume(ByVal volume As AudioUtil.StereoVolume)

        AudioUtil.SetAppVolume(volume.LeftChannel, volume.RightChannel)

    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Sets the volume of the current application.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="channelLeft">
    ''' The left channel volume, from range 0 to 100.
    ''' </param>
    ''' 
    ''' <param name="channelRight">
    ''' The right channel volume, from range 0 to 100.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' channelLeft;A value between 0 and 100 is required.
    ''' or
    ''' channelRight;A value between 0 and 100 is required.
    ''' </exception>
    ''' 
    ''' <exception cref="Win32Exception">
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub SetAppVolume(ByVal channelLeft As Integer, ByVal channelRight As Integer)

        If (channelLeft < 0) OrElse (channelLeft > 100) Then
            Throw New ArgumentOutOfRangeException(paramName:="channelLeft", message:="A value between 0 and 100 is required.")

        ElseIf (channelRight < 0) OrElse (channelRight > 100) Then
            Throw New ArgumentOutOfRangeException(paramName:="channelRight", message:="A value between 0 and 100 is required.")

        Else
            Dim loBytes As Byte() = BitConverter.GetBytes(CUShort(channelLeft / (100US / UShort.MaxValue)))
            Dim hiBytes As Byte() = BitConverter.GetBytes(CUShort(channelRight / (100US / UShort.MaxValue)))
            Dim dWord As UInteger = BitConverter.ToUInt32(loBytes.Concat(hiBytes).ToArray, 0)

            Dim result As Integer = NativeMethods.WaveOutSetVolume(IntPtr.Zero, dWord)
            Dim win32Err As Integer = Marshal.GetLastWin32Error

            If (result <> AudioUtil.NativeMethods.WinMmResult.NoError) Then
                Throw New Win32Exception([error]:=win32Err)
            End If

        End If

    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Mutes or unmutes the system volume.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    Public Sub MuteSystemVolume()

        AudioUtil.NativeMethods.SendMessage(
            Process.GetCurrentProcess.MainWindowHandle,
            AudioUtil.NativeMethods.WindowsMessages.WMAppcommand,
            Process.GetCurrentProcess.MainWindowHandle,
            New IntPtr(AudioUtil.NativeMethods.LParams.AppCommandVolumeMute))

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
