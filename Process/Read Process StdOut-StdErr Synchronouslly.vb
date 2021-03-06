' -------------------
' Info of interest...
' -------------------
' 
' Process.StandardOutput Property:
' http://msdn.microsoft.com/en-us/library/system.diagnostics.process.standardoutput%28v=vs.110%29.aspx
' 
' Process.StandardError Property:
' http://msdn.microsoft.com/en-us/library/system.diagnostics.process.standarderror%28v=vs.110%29.aspx


''' ----------------------------------------------------------------------------------------------------
''' <summary>
''' The process to execute.
''' </summary>
''' ----------------------------------------------------------------------------------------------------
Private WithEvents proc As New Process With
    {
        .EnableRaisingEvents = True,
        .StartInfo = New ProcessStartInfo With
                     {
                         .FileName = "CMD.exe",
                         .Arguments = "/C Dir /B ""C:\Windows\System32""",
                         .WorkingDirectory = Application.StartupPath,
                         .WindowStyle = ProcessWindowStyle.Hidden,
                         .UseShellExecute = False,
                         .CreateNoWindow = True,
                         .RedirectStandardError = True,
                         .RedirectStandardOutput = True,
                         .StandardErrorEncoding = Encoding.Default,
                         .StandardOutputEncoding = Encoding.Default
                     }
    }

Sub Test

    Dim stdOutStream As StreamReader
    Dim stdErrStream As StreamReader

    Dim stdOutLine As String
    Dim stdErrLine As String

    proc.Start()

    stdOutStream = proc.StandardOutput
    stdErrStream = proc.StandardError

    Do Until stdOutStream.EndOfStream
        stdOutLine = stdOutStream.ReadLine
        Console.WriteLine(String.Format("stdOut: {0}", stdOutLine))
    Loop

    Do Until stdErrStream.EndOfStream
        stdErrLine = stdErrStream.ReadLine
        Console.WriteLine(String.Format("stdErr: {0}", stdErrLine))
    Loop

End Sub

''' ----------------------------------------------------------------------------------------------------
''' <summary>
''' Handles the <see cref="Process.Exited"/> event of the <see cref="proc"/> instance.
''' </summary>
''' ----------------------------------------------------------------------------------------------------
''' <param name="sender">
''' The source of the event.
''' </param>
''' 
''' <param name="e">
''' The <see cref="EventArgs"/> instance containing the event data.
''' </param>
''' ----------------------------------------------------------------------------------------------------
Private Sub Process_Exited(ByVal sender As Object, ByVal e As EventArgs) _
Handles proc.Exited

    Console.WriteLine(String.Format("Process exited at {0}", Date.Now.ToShortTimeString))

End Sub
