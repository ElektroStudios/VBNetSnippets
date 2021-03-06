' ***********************************************************************
' Author   : Elektro
' Modified : 24-October-2015
' ***********************************************************************
' <copyright file="TextfileStream.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Public Members Summary "

#Region " Child Classes "

' TextfileStream.TexfileLines : Inherits List(Of String)

#End Region

#Region " Properties "

' TextfileStream.Filepath As String
' TextfileStream.Encoding As Encoding
' TextfileStream.Lines As TexfileLines
' TextfileStream.Fs As FileStream
' TextfileStream.FileHandle As Win32.SafeHandles.SafeFileHandle

' TextfileStream.TexfileLines.CountBlank() As Integer
' TextfileStream.TexfileLines.CountNonBlank() As Integer

#End Region

#Region " Functions "

' TextfileStream.ToString() As String

#End Region

#Region " Methods "

' TextfileStream.Lock()
' TextfileStream.Unlock()
' TextfileStream.Close()
' TextfileStream.Dispose()
' TextfileStream.Save(Opt: Encoding)
' TextfileStream.Save(String, Encoding)

' TextfileStream.TexfileLines.Randomize() As IEnumerable(Of String)
' TextfileStream.TexfileLines.RemoveAt(IEnumerable(Of Integer)) As IEnumerable(Of String)
' TextfileStream.TexfileLines.Trim(Opt: Char()) As IEnumerable(Of String)
' TextfileStream.TexfileLines.TrimStart(Opt: Char()) As IEnumerable(Of String)
' TextfileStream.TexfileLines.TrimEnd(Opt: Char()) As IEnumerable(Of String)

#End Region

#End Region

#Region " Usage Examples "

#Region " TextfileStream "

'Using txtFile As New TextfileStream("C:\File.txt", Encoding.Default)
'
'    txtFile.Lock()
'
'    txtFile.Lines.Add("Test")
'    txtFile.Lines(0) = "Hello World!"
'    txtFile.Save()
'
'    Dim lineIndex As Integer
'    Dim lineCount As Integer = txtFile.Lines.Count
'    Dim textFormat As String =
'        String.Join(ControlChars.NewLine,
'                    From line As String In txtFile.Lines
'                    Select String.Format("{0}: {1}",
'                    Interlocked.Increment(lineIndex).ToString(New String("0"c, lineCount.ToString.Length)), line))
'
'    Console.WriteLine(String.Format("FilePath: {0}", txtFile.Filepath))
'    Console.WriteLine(String.Format("Encoding: {0}", txtFile.Encoding.WebName))
'    Console.WriteLine(String.Format("Lines   : {0}", Environment.NewLine + textFormat))
'
'End Using

#End Region

#Region " TexfileLines "

'Using txtFile As New TextfileStream("C:\file.txt", Encoding.Default)

'    Dim txtLines As TextfileStream.TexfileLines = txtFile.Lines

'    txtLines.Add("  Hello World!  ")
'    txtLines.Trim({" "c})
'    txtLines.Randomize()

'    Dim lineIndex As Integer
'    Dim lineCount As Integer = txtFile.Lines.Count
'    Dim textFormat As String =
'        String.Join(ControlChars.NewLine,
'                    From line As String In txtFile.Lines
'                    Select String.Format("{0}: {1}",
'                           Interlocked.Increment(lineIndex).ToString(New String("0"c, lineCount.ToString.Length)), line))

'    Console.WriteLine(String.Format("Filepath.......: {0}", txtFile.Filepath))
'    Console.WriteLine(String.Format("Blank lines....: {0}", txtLines.CountBlank))
'    Console.WriteLine(String.Format("Non-blank lines: {0}", txtLines.CountNonBlank))
'    Console.WriteLine(String.Format("Lines..........: {0}", Environment.NewLine + textFormat))

'End Using

#End Region

#End Region

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports Microsoft.Win32.SafeHandles
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text

#End Region

#Region " Textfile Stream "

''' ----------------------------------------------------------------------------------------------------
''' <summary>
''' Reads and manages the contents of a textfile.
''' It encapsulates an underliying <see cref="FileStream"/> to access the file.
''' </summary>
''' ----------------------------------------------------------------------------------------------------
''' <example> This is a code example.
''' <code>
''' Using txtFile As New TextfileStream("C:\File.txt", Encoding.Default)
''' 
'''     With txtFile
'''         .Lock()
'''         .Lines.Add("Test")
'''         .Lines(0) = "Hello World!"
'''         .Save()
'''         .Unlock()
'''     End With
''' 
'''     Dim lineIndex As Integer
'''     Dim lineCount As Integer = txtFile.Lines.Count
'''     Dim textFormat As String =
'''         String.Join(ControlChars.NewLine,
'''                     From line As String In txtFile.Lines
'''                     Select String.Format("{0}: {1}",
'''                     Interlocked.Increment(lineIndex).ToString(New String("0"c, lineCount.ToString.Length)), line))
''' 
'''     Console.WriteLine(String.Format("FilePath: {0}", txtFile.Filepath))
'''     Console.WriteLine(String.Format("Encoding: {0}", txtFile.Encoding.WebName))
'''     Console.WriteLine(String.Format("Lines   : {0}", Environment.NewLine + textFormat))
''' 
''' End Using
''' </code>
''' </example>
''' ----------------------------------------------------------------------------------------------------
Public NotInheritable Class TextfileStream : Implements IDisposable

#Region " Properties "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Gets the textfile path.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <value>
    ''' The textfile path.
    ''' </value>
    ''' ----------------------------------------------------------------------------------------------------
    Public ReadOnly Property Filepath As String
        <DebuggerStepThrough>
        Get
            Return Me.filepathB
        End Get
    End Property
    ''' <summary>
    ''' (Backing field) 
    ''' The textfile path.
    ''' </summary>
    Private ReadOnly filepathB As String

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Gets the textfile <see cref="Encoding"/>.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <value>
    ''' The textfile <see cref="Encoding"/>.
    ''' </value>
    ''' ----------------------------------------------------------------------------------------------------
    Public ReadOnly Property Encoding As Encoding
        <DebuggerStepThrough>
        Get
            Return Me.encodingB
        End Get
    End Property
    ''' <summary>
    ''' (Backing field) 
    ''' The textfile <see cref="Encoding"/>.
    ''' </summary>
    Private ReadOnly encodingB As Encoding = Encoding.Default

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Gets or sets the textfile lines.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <value>
    ''' The textfile lines.
    ''' </value>
    ''' ----------------------------------------------------------------------------------------------------
    Public Property Lines As TexfileLines
        <DebuggerStepThrough>
        Get
            Return Me.linesB
        End Get
        <DebuggerStepThrough>
        Set(ByVal value As TexfileLines)
            Me.linesB = value
        End Set
    End Property
    ''' <summary>
    ''' (Backing field) 
    ''' The textfile lines.
    ''' </summary>
    Private linesB As TexfileLines

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Gets the <see cref="FileStream"/> instance that exposes a <see cref="Stream"/> around the textfile.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <value>
    ''' The <see cref="FileStream"/> instance.
    ''' </value>
    ''' ----------------------------------------------------------------------------------------------------
    Private ReadOnly Property Fs As FileStream
        <DebuggerStepThrough>
        Get
            Return Me.fsB
        End Get
    End Property
    ''' <summary>
    ''' (Backing Field) 
    ''' The <see cref="FileStream"/> instance that exposes a <see cref="Stream"/> around the textfile.
    ''' </summary>
    Private ReadOnly fsB As FileStream

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Gets a <see cref="Microsoft.Win32.SafeHandles.SafeFileHandle"/> object that represents the operating system file handle of the textfile.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <value>
    ''' A <see cref="Microsoft.Win32.SafeHandles.SafeFileHandle"/> object that represents the operating system file handle of the textfile.
    ''' </value>
    ''' ----------------------------------------------------------------------------------------------------
    Public ReadOnly Property FileHandle As SafeFileHandle
        <DebuggerStepThrough>
        Get
            Return Me.Fs.SafeFileHandle
        End Get
    End Property

#End Region

#Region " Constructors "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Prevents a default instance of the <see cref="TextfileStream"/> class from being created.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    Private Sub New()
    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Initializes a new instance of the <see cref="TextfileStream"/> class.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="filepath">
    ''' The textfile path.
    ''' If the path doesn't exists, the file will be created.
    ''' </param>
    ''' 
    ''' <param name="encoding">
    ''' The file encoding used to read the textfile.
    ''' If <paramref name="encoding"/> value is <see langword="Nothing"/>, an attempt to detect the encoding will be realized, 
    ''' if the attempt to detect the file encoding fails, then <see cref="Encoding.Default"/> will be used.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="FileNotFoundException">
    ''' File not found.
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub New(ByVal filepath As String,
                   Optional ByVal encoding As Encoding = Nothing)

        If Not File.Exists(filepath) Then
            Throw New FileNotFoundException(message:="File not found.", fileName:=filepath)

        Else
            Me.filepathB = filepath
            Me.encodingB = encoding

            If Me.encodingB Is Nothing Then
                Me.encodingB = Me.GetEncoding
            End If

            Me.linesB = New TexfileLines(File.ReadAllLines(Me.filepathB, Me.encodingB))
            Me.fsB = New FileStream(filepath, FileMode.OpenOrCreate)

        End If

    End Sub

#End Region

#Region " Public Methods "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Prevents other processes from reading or writing to the textfile.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub Lock()

        Me.fsB.Lock(position:=0, length:=Me.fsB.Length)

    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Allows access by other processes to read or write to a textfile that was previously locked.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub Unlock()

        Me.fsB.Unlock(position:=0, length:=Me.fsB.Length)

    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub Close()

        Me.fsB.Close()

    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Save the lines of the current textfile, in the current textfile.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' Note that the <see cref="TextfileStream.Save"/> method should be called to apply any realized changes in the lines of the textfile 
    ''' before disposing the current <see cref="TextfileStream"></see> instance.
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="encoding">
    ''' The file encoding used to write the textfile.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub Save(Optional ByVal encoding As Encoding = Nothing)

        If (encoding Is Nothing) Then
            encoding = Me.encodingB
        End If

        Dim bytes As Byte() = encoding.GetBytes(Me.ToString)

        With Me.fsB
            .SetLength(bytes.Length)
            .Write(bytes, 0, bytes.Length)
        End With

    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Save the lines of the current textfile, in the target textfile.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="filepath">
    ''' The target filepath where to save the text.
    ''' </param>
    ''' 
    ''' <param name="encoding">
    ''' The file encoding used to write the textfile.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Sub Save(ByVal filepath As String,
                    Optional ByVal encoding As Encoding = Nothing)

        If (encoding Is Nothing) Then
            encoding = Me.encodingB
        End If

        Using fs As New FileStream(filepath, FileMode.OpenOrCreate)

            Dim bytes As Byte() = encoding.GetBytes(Me.ToString)

            fs.SetLength(bytes.Length)
            fs.Write(bytes, 0, bytes.Length)

        End Using

    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Returns a <see cref="String"/> that represents this instance.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' A <see cref="String"/> that represents this instance.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Overrides Function ToString() As String

        Return String.Join(ControlChars.NewLine, Me.linesB)

    End Function

#End Region

#Region " Private Methods "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Determines the <see cref="Encoding"/> of the current textfile.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' If the encoding can be detected, the return value is the detected <see cref="Encoding"/>,
    ''' if the encoding can't be detected, the return value is <see cref="Encoding.Default"/>.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Private Function GetEncoding() As Encoding

        Dim encoding As Encoding = Nothing
        Dim bytes As Byte() = File.ReadAllBytes(Me.filepathB)

        For Each encodingInfo As EncodingInfo In encoding.GetEncodings()

            Dim currentEncoding As Encoding = encodingInfo.GetEncoding()
            Dim preamble As Byte() = currentEncoding.GetPreamble()
            Dim match As Boolean = True

            If (preamble.Length > 0) AndAlso (preamble.Length <= bytes.Length) Then

                For i As Integer = 0 To (preamble.Length - 1)

                    If preamble(i) <> bytes(i) Then
                        match = False
                        Exit For
                    End If

                Next i

            Else
                match = False

            End If

            If match Then
                encoding = currentEncoding
                Exit For
            End If

        Next encodingInfo

        If encoding Is Nothing Then
            Return encoding.Default

        Else
            Return encoding

        End If

    End Function

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
    ''' Releases all the resources used by this <see cref="TextfileStream"></see> instance.
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

            If (Me.fsB IsNot Nothing) Then
                Me.fsB.Close()
                Me.linesB.Clear()
            End If

        End If

        Me.isDisposed = True

    End Sub

#End Region

#Region " Child Classes "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Defines a <see cref="List(Of String)"/> that contains the text-lines of a textfile.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Using txtFile As New TextfileStream("C:\file.txt", Encoding.Default)
    ''' 
    '''     Dim txtLines As TextfileStream.TexfileLines = txtFile.Lines
    ''' 
    '''     txtLines.Add("  Hello World!  ")
    '''     txtLines.Trim({" "c})
    '''     txtLines.Randomize()
    ''' 
    '''     Dim lineIndex As Integer
    '''     Dim lineCount As Integer = txtFile.Lines.Count
    '''     Dim textFormat As String =
    '''         String.Join(ControlChars.NewLine,
    '''                     From line As String In txtFile.Lines
    '''                     Select String.Format("{0}: {1}",
    '''                            Interlocked.Increment(lineIndex).ToString(New String("0"c, lineCount.ToString.Length)), line))
    ''' 
    '''     Console.WriteLine(String.Format("Filepath.......: {0}", txtFile.Filepath))
    '''     Console.WriteLine(String.Format("Blank lines....: {0}", txtLines.CountBlank))
    '''     Console.WriteLine(String.Format("Non-blank lines: {0}", txtLines.CountNonBlank))
    '''     Console.WriteLine(String.Format("Lines..........: {0}", Environment.NewLine + textFormat))
    ''' 
    ''' End Using
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    Partial Public NotInheritable Class TexfileLines : Inherits List(Of String)

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the number of blank elements actually contained in the <see cref="List(Of T)"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The number of blank elements actually contained in the <see cref="List(Of T)"/>.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property CountBlank As Integer
            <DebuggerStepThrough>
            Get
                Return (From line As String In Me
                        Where String.IsNullOrEmpty(line) OrElse
                              String.IsNullOrWhiteSpace(line)).Count
            End Get
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the number of non-blank elements actually contained in the <see cref="List(Of T)"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The number of non-blank elements actually contained in the <see cref="List(Of T)"/>.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property CountNonBlank As Integer
            <DebuggerStepThrough>
            Get
                Return (From line As String In Me
                        Where Not String.IsNullOrEmpty(line) AndAlso
                              Not String.IsNullOrWhiteSpace(line)).Count
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="TexfileLines"/> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub New()
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="TexfileLines"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="lines">
        ''' The text-lines.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New(ByVal lines As IEnumerable(Of String))

            Me.AddRange(lines)

        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Randomizes the elements of this <see cref="TexfileLines"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub Randomize()

            Dim rand As New Random
            Dim tmpList As New List(Of String)((From line As String In Me
                                                Order By rand.Next))

            Me.Clear()
            Me.AddRange(tmpList)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Removes the elements at the specified indexes of this <see cref="TexfileLines"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="indexes">
        ''' The zero-based indexes of the elements to remove.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="IndexOutOfRangeException">
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Overloads Sub RemoveAt(ByVal indexes As IEnumerable(Of Integer))

            Dim lineCount As Integer = Me.Count

            Select Case indexes.Max

                Case Is < 0, Is > lineCount
                    Throw New IndexOutOfRangeException()

                Case Else
                    Dim tmpRef As IEnumerable(Of String) =
                        Me.Select(Function(line As String, index As Integer)
                                      Return New With
                                             {
                                                 Key .line = line,
                                                 Key .index = index + 1
                                             }
                                  End Function).
                           Where(Function(con) Not indexes.Contains(con.index)).
                           Select(Function(con) con.line)

                    Me.Clear()
                    Me.AddRange(tmpRef)
                    tmpRef = Nothing

            End Select

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Removes all leading and trailing occurrences of a set of characters from all the elements of this <see cref="TexfileLines"/>.
        ''' </summary>  
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="trimChars">
        ''' An array of Unicode characters to remove.
        ''' If <paramref name="trimChars"/> is <see langword="Nothing"/> or an empty array, 
        ''' Unicode white-space characters are removed instead.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub Trim(Optional ByVal trimChars As Char() = Nothing)

            For index As Integer = 0 To (Me.Count - 1)
                Me(index) = Me(index).Trim(trimChars)
            Next

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Removes all leading occurrences of a set of characters from all the elements of this <see cref="TexfileLines"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="trimChars">
        ''' An array of Unicode characters to remove.
        ''' If <paramref name="trimChars"/> is <see langword="Nothing"/> or an empty array, 
        ''' Unicode white-space characters are removed instead.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub TrimStart(Optional ByVal trimChars As Char() = Nothing)

            For index As Integer = 0 To (Me.Count - 1)
                Me(index) = Me(index).TrimStart(trimChars)
            Next

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Removes all trailing occurrences of a set of characters from all the elements of this <see cref="TexfileLines"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="trimChars">
        ''' An array of Unicode characters to remove.
        ''' If <paramref name="trimChars"/> is <see langword="Nothing"/> or an empty array, 
        ''' Unicode white-space characters are removed instead.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub TrimEnd(Optional ByVal trimChars As Char() = Nothing)

            For index As Integer = 0 To (Me.Count - 1)
                Me(index) = Me(index).TrimEnd(trimChars)
            Next

        End Sub

#End Region

    End Class

#End Region

End Class

#End Region
