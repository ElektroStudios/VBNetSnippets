' ***********************************************************************
' Author   : Elektro
' Modified : 26-October-2015
' ***********************************************************************
' <copyright file="String Util.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Public Members Summary "

#Region " Types "

' StringUtil.FixedLengthString <Serializable>

#End Region

#Region " Properties "

' StringUtil.FixedLengthString.FixedLength As Integer
' StringUtil.FixedLengthString.PaddingChar As Char
' StringUtil.FixedLengthString.ValueUnfixed As String
' StringUtil.FixedLengthString.ValueFixed As String

#End Region

#Region " Functions "

' StringUtil.GetWhiteSpacedString(Integer) As String

' StringUtil.FixedLengthString.ToString() As String

#End Region

#Region " Methods "

' StringUtil.FixedLengthString.New(String, Integer, Opt: Char)

#End Region

#End Region

#Region " Usage Examples "

#Region " FixedLengthString "

'Dim fixedStr As New FixedLengthString("", 10)
'MessageBox.Show(String.Format("""{0}""", fixedStr.ValueFixed)) ' Result: "          "
'
'fixedStr.ValueUnfixed = "12345"
'MessageBox.Show(String.Format("""{0}""", fixedStr.ValueFixed)) ' Result: "1245      "
'
'fixedStr.ValueUnfixed = "1234567890abc"
'MessageBox.Show(String.Format("""{0}""", fixedStr.ValueFixed)) ' Result: "1234567890"

#End Region

#End Region

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System
Imports System.Linq

#End Region

''' ----------------------------------------------------------------------------------------------------
''' <summary>
''' Contains custom extension methods to use with a <see cref="String"/>.
''' </summary>
''' ----------------------------------------------------------------------------------------------------
Public Module StringUtil

#Region " Types "

#Region " FixedLength String "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Defines a <see cref="String"/> with a fixed length.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim fixedStr As New FixedLengthString("", 10)
    ''' MessageBox.Show(String.Format("""{0}""", fixedStr.ValueFixed)) ' Result: "          "
    ''' 
    ''' fixedStr.ValueUnfixed = "12345"
    ''' MessageBox.Show(String.Format("""{0}""", fixedStr.ValueFixed)) ' Result: "1245      "
    ''' 
    ''' fixedStr.ValueUnfixed = "1234567890abc"
    ''' MessageBox.Show(String.Format("""{0}""", fixedStr.ValueFixed)) ' Result: "1234567890"
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    <Serializable>
    Public NotInheritable Class FixedLengthString

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the fixed string length.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The fixed string length.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Property FixedLength As Integer
            Get
                Return Me.fixedLengthB
            End Get
            <DebuggerStepThrough>
            Set(ByVal value As Integer)
                Me.fixedLengthB = value
            End Set
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The fixed string length.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private fixedLengthB As Integer

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the padding character thath fills the string.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The padding character thath fills the string.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Property PaddingChar As Char
            Get
                Return Me.paddingCharB
            End Get
            <DebuggerStepThrough>
            Set(ByVal value As Char)
                Me.paddingCharB = value
            End Set
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The padding character thath fills the string.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private paddingCharB As Char

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the unmodified string.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The unmodified string.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Property ValueUnfixed As String
            Get
                Return Me.valueUnfixedB
            End Get
            <DebuggerStepThrough>
            Set(ByVal value As String)
                Me.valueUnfixedB = value
            End Set
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The unmodified string.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private valueUnfixedB As String

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the fixed string.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The fixed string.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property ValueFixed As String
            Get
                Return Me.FixLength(Me.valueUnfixedB, Me.fixedLengthB, Me.paddingCharB)
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="FixedLengthString" /> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub New()
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="FixedLengthString" /> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="value">
        ''' The string value.
        ''' </param>
        ''' 
        ''' <param name="fixedLength">
        ''' The fixed string length.
        ''' </param>
        ''' 
        ''' <param name="paddingChar">
        ''' The padding character thath fills the string.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub New(ByVal value As String,
                       ByVal fixedLength As Integer,
                       Optional ByVal paddingChar As Char = " "c)

            Me.valueUnfixedB = value
            Me.fixedLengthB = fixedLength
            Me.paddingCharB = paddingChar

        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Returns a <see cref="System.String"/> that represents this instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' A <see cref="System.String"/> that represents this instance.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overrides Function ToString() As String
            Return Me.ValueFixed
        End Function

#End Region

#Region " Private Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Fixes the length of the specified string.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="value">
        ''' The string value.
        ''' </param>
        ''' 
        ''' <param name="fixedLength">
        ''' The fixed string length.
        ''' </param>
        ''' 
        ''' <param name="paddingChar">
        ''' The padding character thath fills the string.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The fixed-length string.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Private Function FixLength(ByVal value As String,
                                   ByVal fixedLength As Integer,
                                   ByVal paddingChar As Char) As String

            If (value.Length > fixedLength) Then
                Return value.Substring(0, fixedLength)
            Else
                Return value.PadRight(fixedLength, paddingChar)
            End If

        End Function

#End Region

    End Class

#End Region

#End Region

#Region " Public Methods "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Returns a white-spaced string with the specified length.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim s As String = StringUtil.GetWhiteSpacedString(10)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="Length">
    ''' The white-space length.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The white-spaced string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Public Function GetWhiteSpacedString(ByVal length As Integer) As String

        Return New String(" "c, length)

    End Function

#End Region

End Module
