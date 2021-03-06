' ***********************************************************************
' Author   : Elektro
' Modified : 28-October-2015
' ***********************************************************************
' <copyright file="String Extensions.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Public Members Summary "

#Region " Types "

' StringExtensions.GroupingInfo <Serializable>
' StringExtensions.GroupingCharsInfo <Serializable>

#End Region

#Region " Constructors "

' StringExtensions.GroupingCharsInfo.New(Char, Char)
' StringExtensions.GroupingInfo.New(String, List(Of GroupingCharsInfo), Boolean, Boolean, Integer, Integer, Dictionary(Of Integer, Integer), List(Of Integer))

#End Region

#Region " Enumerations "

' StringExtensions.StringCase As Integer
' StringExtensions.StringDirection As Integer

#End Region

#Region " Properties "

' StringExtensions.GroupingCharsInfo.Aperture As Char
' StringExtensions.GroupingCharsInfo.Closure As Char

' StringExtensions.GroupingInfo.Source As String
' StringExtensions.GroupingInfo.GroupingChars As List(Of GroupingCharsInfo)
' StringExtensions.GroupingInfo.HasClosedGroups As Boolean
' StringExtensions.GroupingInfo.HasOpenGroups As Boolean
' StringExtensions.GroupingInfo.GroupsClosedCount As Integer
' StringExtensions.GroupingInfo.GroupsOpenCount As Integer
' StringExtensions.GroupingInfo.GroupsClosedPositions As Dictionary(Of Integer, Integer)
' StringExtensions.GroupingInfo.GroupsOpenPositions As List(Of Integer)

#End Region

#Region " Functions "

' String.Measure(Font) As Size
' String.SizeInMemory() As Integer
' String.SizeInFile(Encoding) As Integer
' String.SizeInFile(Encoding, Boolean) As Integer

' String.ExpandBlankSpace(Char, Opt: Integer) As String
' String.ExpandBlankSpace(Opt: Integer) As String
' String.ExpandChars(Char, Opt: Integer) As String
' String.ExpandChars(Opt: Integer) As String
' String.ExpandVariables() As String
' String.NormalizeDiacritics(NormalizationForm) As String
' String.Randomize() As String
' String.Randomize(Integer) As String
' String.Rename(StringExtensions.StringCase) As String
' String.Reverse() As String
' String.Rotate(StringExtensions.StringDirection, Integer) As String
' String.SplitByLength(Integer) As IEnumerable(Of String)
' String.SplitByPosition(Integer) As IEnumerable(Of String)

' String.Replace(String, String, StringComparison) As String
' String.ReplaceRegEx(String, String, RegExOptions) As String

' String.HasDiacritics() As Boolean
' String.IsAlphabetic() As Boolean
' String.IsCase(StringExtensions.StringCase) As Boolean
' String.IsNumeric() As Boolean
' String.IsNumericOf(Of T) As Boolean
' String.IsValueOf(Of T) As Boolean

' String.ToByteArray(Opt: Encoding) As Byte()
' String.ToHtmlDocument() As Htmldocument

' String.BinaryToAscII() As String
' String.HexToCSharpHex() As String
' String.HexToInt32() As Integer
' String.HexToInt64() As Long
' String.HexToString() As String
' String.HexToUInt32() As UInteger
' String.HexToUInt64() As ULong
' String.HexToVbHex() As String
' String.ToBase64() As String
' String.ToBinary(Opt: String, Opt: Encoding) As String
' String.ToHex(Opt: String, Opt: Encoding) As String
' String.ToMD5() As String

' String.IndexOfAnyRegEx(IEnumerable(Of String), Integer, Integer, RegexOptions) As Integer
' String.IndexOfAnyRegEx(IEnumerable(Of String), Integer, RegexOptions) As Integer
' String.IndexOfAnyRegEx(IEnumerable(Of String), RegexOptions) As Integer

' String.IndexOfRegEx(String, Integer, Integer, RegexOptions) As Integer
' String.IndexOfRegEx(String, Integer, RegexOptions) As Integer
' String.IndexOfRegEx(String, RegexOptions) As Integer

' String.LastIndexOfAnyRegEx(IEnumerable(Of String), Integer, Integer, RegexOptions) As Integer
' String.LastIndexOfAnyRegEx(IEnumerable(Of String), Integer, RegexOptions) As Integer
' String.LastIndexOfAnyRegEx(IEnumerable(Of String), RegexOptions) As Integer

' String.LastIndexOfRegEx(String, Integer, Integer, RegexOptions) As Integer
' String.LastIndexOfRegEx(String, Integer, RegexOptions) As Integer
' String.LastIndexOfRegEx(String, RegexOptions) As Integer

' String.Delimit(String, Integer, Opt: RegexOptions) As String
' String.Delimit(String, Opt: RegexOptions) As String
' String.Delimit(String, String, Integer, Opt: RegexOptions) As String
' String.Delimit(String, String, Opt: RegexOptions) As String

' String.CountCharacter(Char) As Integer
' String.CountCharacters(Char, StringComparison) As Integer
' String.GetGroupingInfo(Opt: List(Of StringExtensions.GroupingCharsInfo)) As StringExtensions.GroupingInfo

#End Region

#End Region

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Diagnostics
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions

#End Region

''' ----------------------------------------------------------------------------------------------------
''' <summary>
''' Contains custom extension methods to use with a <see cref="String"/>.
''' </summary>
''' ----------------------------------------------------------------------------------------------------
Public Module StringExtensions

#Region " Types "

#Region " GroupingCharsInfo "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Defines the characters used for an aperture and enclosue of an string.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <Serializable>
    Public Structure GroupingCharsInfo

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the aperture char.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The aperture char.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Aperture As Char
            Get
                Return Me.apertureB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' The aperture char.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly apertureB As Char

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the closure char.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The closure char.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Closure As Char
            Get
                Return Me.closureB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' The closure char.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly closureB As Char

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="GroupingCharsInfo"/> class.
        ''' </summary>
        ''' <param name="apertureChar">
        ''' The aperture character.
        ''' </param>
        ''' 
        ''' <param name="closureChar">
        ''' The closure character.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New(ByVal apertureChar As Char,
                       ByVal closureChar As Char)

            Me.apertureB = apertureChar
            Me.closureB = closureChar

        End Sub

#End Region

    End Structure

#End Region

#Region " GroupingInfo "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Defines the info about enclosed and opened groups of chars in a String.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <Serializable>
    Public Structure GroupingInfo

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the source string.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The source string.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Source As String
            Get
                Return Me.sourceB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' The source string.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly sourceB As String

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the grouping characters.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The grouping characters.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property GroupingChars As List(Of GroupingCharsInfo)
            Get
                Return Me.groupingCharsB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' The grouping characters.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly groupingCharsB As List(Of GroupingCharsInfo)

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets a value that determines whether the source string contains enclosed groups.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' A value that determines whether the source string contains enclosed groups.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property HasClosedGroups As Boolean
            Get
                Return Me.hasClosedGroupsB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' A value that determines whether the source string contains enclosed groups.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly hasClosedGroupsB As Boolean

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets a value that determines whether the source string contains open groups.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' A value that determines whether the source string contains open groups.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property HasOpenGroups As Boolean
            Get
                Return Me.hasOpenGroupsB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' A value that determines whether the source string contains open groups.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly hasOpenGroupsB As Boolean

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the amount of enclosed groups.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The amount of enclosed groups.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property GroupsClosedCount As Integer
            Get
                Return Me.groupsClosedCountB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' The amount of enclosed groups.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly groupsClosedCountB As Integer

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the amount of open groups.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The amount of open groups.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property GroupsOpenCount As Integer
            Get
                Return Me.groupsOpenCountB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' The amount of open groups.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly groupsOpenCountB As Integer

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the positions in the string of the enclosed groups.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The positions in the string of the enclosed groups.
        ''' </value>
        Public ReadOnly Property GroupsClosedPositions As Dictionary(Of Integer, Integer)
            Get
                Return Me.groupsClosedPositionsB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' The positions in the string of the enclosed groups.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly groupsClosedPositionsB As Dictionary(Of Integer, Integer)

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the positions in the string of the open groups.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The positions in the string of the open groups.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property GroupsOpenPositions As List(Of Integer)
            Get
                Return Me.groupsOpenPositionsB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' The positions in the string of the open groups.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly groupsOpenPositionsB As List(Of Integer)

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="GroupingInfo"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="source">
        ''' The source string.
        ''' </param>
        ''' 
        ''' <param name="groupingChars">
        ''' The grouping characters.
        ''' </param>
        ''' 
        ''' <param name="hasClosedGroups">
        ''' A value that determines whether the source string contains enclosed groups.
        ''' </param>
        ''' 
        ''' <param name="hasOpenGroups">
        ''' A value that determines whether the source string contains open groups.
        ''' </param>
        ''' 
        ''' <param name="groupsClosedCount">
        ''' The amount of enclosed groups.
        ''' </param>
        ''' 
        ''' <param name="groupsOpenCount">
        ''' The amount of open groups.
        ''' </param>
        ''' 
        ''' <param name="groupsClosedPositions">
        ''' The positions in the string of the enclosed groups.
        ''' </param>
        ''' 
        ''' <param name="groupsOpenPositions">
        ''' The positions in the string of the open groups.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New(ByVal source As String,
                       ByVal groupingChars As List(Of GroupingCharsInfo),
                       ByVal hasClosedGroups As Boolean,
                       ByVal hasOpenGroups As Boolean,
                       ByVal groupsClosedCount As Integer,
                       ByVal groupsOpenCount As Integer,
                       ByVal groupsClosedPositions As Dictionary(Of Integer, Integer),
                       ByVal groupsOpenPositions As List(Of Integer))

            Me.groupsOpenPositionsB = groupsOpenPositions
            Me.groupsClosedPositionsB = groupsClosedPositions
            Me.groupsOpenCountB = groupsOpenCount
            Me.groupsClosedCountB = groupsClosedCount
            Me.hasOpenGroupsB = hasOpenGroups
            Me.hasClosedGroupsB = hasClosedGroups
            Me.groupingCharsB = groupingChars
            Me.sourceB = source

        End Sub

#End Region

    End Structure

#End Region

#End Region

#Region " Enumerations "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Specifies a string case.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    Public Enum StringCase As Integer

        ''' <summary>
        ''' LowerCase
        ''' 
        ''' [Example]
        ''' Input : ABCDEF
        ''' Output: abcdef
        ''' </summary>
        LowerCase = &H0

        ''' <summary>
        ''' UpperCase.
        ''' 
        ''' [Example]
        ''' Input : abcdef
        ''' Output: ABCDEF
        ''' </summary>
        UpperCase = &H1

        ''' <summary>
        ''' TitleCase.
        ''' 
        ''' [Example]
        ''' Input : abcdef
        ''' Output: Abcdef
        ''' </summary>
        TitleCase = &H2

        ''' <summary>
        ''' WordCase.
        ''' 
        ''' [Example]
        ''' Input : abc def
        ''' Output: Abc Def
        ''' </summary>
        WordCase = &H3

        ''' <summary>
        ''' CamelCase (With first letter to LowerCase).
        ''' 
        ''' [Example]
        ''' Input : ABC DEF
        ''' Output: abcDef
        ''' </summary>
        CamelCaseLower = &H4

        ''' <summary>
        ''' CamelCase (With first letter to UpperCase).
        ''' 
        ''' [Example]
        ''' Input : ABC DEF
        ''' Output: AbcDef
        ''' </summary>
        CamelCaseUpper = &H5

        ''' <summary>
        ''' MixedCase (With first letter to LowerCase).
        ''' 
        ''' [Example]
        ''' Input : ab cd ef
        ''' Output: aB Cd eF
        ''' </summary>
        MixedTitleCaseLower = &H6

        ''' <summary>
        ''' MixedCase (With first letter to UpperCase).
        ''' 
        ''' [Example]
        ''' Input : ab cd ef
        ''' Output: Ab cD Ef
        ''' </summary>
        MixedTitleCaseUpper = &H7

        ''' <summary>
        ''' MixedCase (With first letter of each word to LowerCase).
        ''' 
        ''' [Example]
        ''' Input : ab cd ef
        ''' Output: aB cD eF
        ''' </summary>
        MixedWordCaseLower = &H8

        ''' <summary>
        ''' MixedCase (With first letter of each word to UpperCase).
        ''' 
        ''' [Example]
        ''' Input : ab cd ef
        ''' Output: Ab Cd Ef
        ''' </summary>
        MixedWordCaseUpper = &H9

        ''' <summary>
        ''' ToggleCase.
        ''' 
        ''' [Example]
        ''' Input : abc def ghi
        ''' Output: aBC dEF gHI
        ''' </summary>
        ToggleCase = &H10

        ''' <summary>
        ''' Duplicates the characters.
        ''' 
        ''' [Example]
        ''' Input : Hello World!
        ''' Output: HHeelllloo  WWoorrlldd!!
        ''' </summary>
        DuplicateChars = &H11

        ''' <summary>
        ''' Alternates the characters.
        ''' 
        ''' [Example]
        ''' Input : Hello World!
        ''' Output: hELLO wORLD!
        ''' </summary>
        AlternateChars = &H12

    End Enum

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Specifies the direction to read a String.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    Public Enum StringDirection As Integer

        ''' <summary>
        ''' Reads a string from the left.
        ''' </summary>
        Left = 0

        ''' <summary>
        ''' Reads a string from the right.
        ''' </summary>
        Right = 1

    End Enum

#End Region

#Region " Generic String manipulation "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Randomizes a String.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source String.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The randomized String.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function Randomize(ByVal sender As String) As String

        Dim rand As New Random
        Return String.Join("", From c As Char In sender Order By rand.Next)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Randomizes a String.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source String.
    ''' </param>
    ''' 
    ''' <param name="length">
    ''' The length of the randomized String.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The randomized String.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Value bigger than 0 is required.;length
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function Randomize(ByVal sender As String,
                              ByVal length As Integer) As String

        If (length <= 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="length", message:="Value bigger than 0 is required.")

        Else
            Select Case sender.Length

                Case 0
                    Return sender

                Case 1
                    Return New String(sender.First, Math.Abs(length))

                Case Else
                    Dim rand As New Random
                    Dim charsetLength As Integer = sender.Length
                    Dim sb As New StringBuilder

                    Do Until (sb.Length = length)
                        sb.Append(sender(rand.Next(0, charsetLength)))
                    Loop

                    Return sb.ToString

            End Select

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Rotates the position of the characters in a String.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source String.
    ''' </param>
    ''' 
    ''' <param name="direction">
    ''' A <see cref="StringExtensions.StringDirection"/> that determines the rotation direction.
    ''' </param>
    ''' 
    ''' <param name="positions">
    ''' The amount of character positions to rotate.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The rotated String.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Value bigger than 0 is required.;positions
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Value smaller than the source string length is required.;positions
    ''' </exception>
    ''' 
    ''' <exception cref="InvalidEnumArgumentException">
    ''' direction
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function Rotate(ByVal sender As String,
                           ByVal direction As StringDirection,
                           ByVal positions As Integer) As String

        If (positions <= 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="positions",
                                                  message:="Value bigger than 0 is required.")

        ElseIf (positions >= sender.Length) Then
            Throw New ArgumentOutOfRangeException(paramName:="positions",
                                                  message:="Value smaller than the source string length is required.")

        Else
            Select Case direction

                Case StringExtensions.StringDirection.Left
                    Return String.Format("{0}{1}", sender.Substring(positions), sender.Substring(0, positions))

                Case StringExtensions.StringDirection.Right
                    Return String.Format("{0}{1}", sender.Substring(sender.Length - positions), sender.Remove(sender.Length - positions))

                Case Else
                    Throw New InvalidEnumArgumentException(argumentName:="direction",
                                                           invalidValue:=direction,
                                                           enumClass:=GetType(StringExtensions.StringDirection))

            End Select ' direction

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Inverts the order of the characters in a String.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("Test".Reverse)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The reversed String.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function Reverse(ByVal sender As String) As String

        Return String.Join("", Enumerable.Reverse(sender))

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Renames a string to the specified StringCase.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim str As String = "Hello World!".Rename(StringCase.UpperCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="stringCase">
    ''' The <see cref="StringExtensions.StringCase"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The renamed string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function Rename(ByVal sender As String,
                           ByVal stringCase As StringExtensions.StringCase) As String

        Select Case stringCase

            Case StringExtensions.StringCase.LowerCase
                Return sender.ToLower

            Case StringExtensions.StringCase.UpperCase
                Return sender.ToUpper

            Case StringExtensions.StringCase.TitleCase
                Return (Char.ToUpper(sender.First) & sender.Remove(0, 1).ToLower)

            Case StringExtensions.StringCase.WordCase
                Return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(sender.ToLower)

            Case StringExtensions.StringCase.CamelCaseLower
                Return Char.ToLower(sender.First) &
                       CultureInfo.InvariantCulture.TextInfo.ToTitleCase(sender.ToLower).
                       Replace(" "c, String.Empty).
                       Remove(0, 1)

            Case StringExtensions.StringCase.CamelCaseUpper
                Return Char.ToUpper(sender.First) &
                       CultureInfo.InvariantCulture.TextInfo.ToTitleCase(sender.ToLower).
                       Replace(" "c, String.Empty).
                       Remove(0, 1)

            Case StringExtensions.StringCase.MixedTitleCaseLower
                Dim sb As New StringBuilder
                For i As Integer = 0 To (sender.Length - 1) Step 2
                    If Not (i + 1) >= sender.Length Then
                        sb.Append(Char.ToLower(sender(i)) & Char.ToUpper(sender(i + 1)))
                    Else
                        sb.Append(Char.ToLower(sender(i)))
                    End If
                Next i
                Return sb.ToString

            Case StringExtensions.StringCase.MixedTitleCaseUpper
                Dim sb As New StringBuilder
                For i As Integer = 0 To (sender.Length - 1) Step 2
                    If Not (i + 1) >= sender.Length Then
                        sb.Append(Char.ToUpper(sender(i)) & Char.ToLower(sender(i + 1)))
                    Else
                        sb.Append(Char.ToUpper(sender(i)))
                    End If
                Next i
                Return sb.ToString

            Case StringExtensions.StringCase.MixedWordCaseLower
                Dim sb As New StringBuilder
                For Each word As String In sender.Split
                    sb.Append(StringExtensions.Rename(word, StringExtensions.StringCase.MixedTitleCaseLower) & " ")
                Next word
                Return sb.ToString

            Case StringExtensions.StringCase.MixedWordCaseUpper
                Dim sb As New StringBuilder
                For Each word As String In sender.Split
                    sb.Append(StringExtensions.Rename(word, StringExtensions.StringCase.MixedTitleCaseUpper) & " ")
                Next word
                Return sb.ToString

            Case StringExtensions.StringCase.ToggleCase
                Dim sb As New StringBuilder
                For Each word As String In sender.Split
                    sb.Append(Char.ToLower(word.First) & word.Remove(0, 1).ToUpper & " ")
                Next word
                Return sb.ToString

            Case StringExtensions.StringCase.DuplicateChars
                Dim sb As New StringBuilder
                For Each c As Char In sender
                    sb.Append(New String(c, 2))
                Next c
                Return sb.ToString

            Case StringExtensions.StringCase.AlternateChars
                Dim sb As New StringBuilder
                For Each c As Char In sender
                    Select Case Char.IsLower(c)
                        Case True
                            sb.Append(Char.ToUpper(c))
                        Case Else
                            sb.Append(Char.ToLower(c))
                    End Select
                Next c
                Return sb.ToString

            Case Else
                Return sender

        End Select

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Split a String into parts of the specified length.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source String.
    ''' </param>
    ''' 
    ''' <param name="length">
    ''' The length.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' An <see cref="IEnumerable(Of String)"/> that contains the parts of the splitted striing.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Value bigger than 0 is required.;length
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Value smaller than the source string length is required.;length
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Iterator Function SplitByLength(ByVal sender As String,
                                           ByVal length As Integer) As IEnumerable(Of String)

        If (length <= 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="length",
                                                  message:="Value bigger than 0 is required.")

        ElseIf (length >= sender.Length) Then
            Throw New ArgumentOutOfRangeException(paramName:="length",
                                                  message:="Value smaller than the source string length is required.")

        Else
            Do Until (sender.Length <= length)
                Yield sender.Substring(0, length)
                sender = sender.Remove(0, length)
            Loop

            ' Add the last missing part (if any).
            If Not String.IsNullOrEmpty(sender) Then
                Yield sender
            End If

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Split a String into two parts by the specified character position.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source String.
    ''' </param>
    ''' 
    ''' <param name="position">
    ''' The starting character position where to split.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' An <see cref="IEnumerable(Of String)"/> that contains the parts of the splitted striing.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Value bigger than 0 is required.;position
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Value smaller than the source string length is required.;position
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function SplitByPosition(ByVal sender As String,
                                    ByVal position As Integer) As IEnumerable(Of String)

        If (position <= 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="position",
                                                  message:="Value bigger than 0 is required.")

        ElseIf (position >= sender.Length) Then
            Throw New ArgumentOutOfRangeException(paramName:="position",
                                                  message:="Value smaller than the source string length is required.")

        Else
            Return New String() {
                                    sender.Substring(0, position),
                                    sender.Remove(0, position)
                                }

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Expands the white-spaces and tabulations of a string by adding more whitespaces.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("Hello World".ExpandBlankSpace(10))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' 
    ''' <param name="count">
    ''' The amount of times to repeat the white-space character.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The expanded string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Value bigger than 0 is required.;count
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function ExpandBlankSpace(ByVal sender As String,
                                     Optional ByVal count As Integer = 1) As String

        If (count <= 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="count",
                                                  message:="Value bigger than 0 is required.")
        Else
            Return New Regex("(\t+|\s+)", RegexOptions.None).Replace(sender, "$1" & New String(" "c, count))

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Expands the white-spaces and tabulations of a string by adding the specified separator char.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("Hello World".ExpandBlankSpace(" "c, 10))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' 
    ''' <param name="separator">
    ''' The character used to expand the white-spaces and tabulations.
    ''' </param>
    ''' 
    ''' <param name="count">
    ''' The amount of times to repeat the separator character.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The expanded string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Value bigger than 0 is required.;count
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function ExpandBlankSpace(ByVal sender As String,
                                     ByVal separator As Char,
                                     Optional ByVal count As Integer = 1) As String

        If (count <= 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="count",
                                                  message:="Value bigger than 0 is required.")
        Else
            Return New Regex("(\t+|\s+)", RegexOptions.None).Replace(sender, "$1" & New String(separator, count))

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Expands the characters of a String by adding more white-spaces.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("Hello World".ExpandChars(2))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' 
    ''' <param name="count">
    ''' The amount of times to repeat the white-space character.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The expanded string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Value bigger than 0 is required.;count
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function ExpandChars(ByVal sender As String,
                                Optional ByVal count As Integer = 1) As String

        If (count <= 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="count",
                                                  message:="Value bigger than 0 is required.")
        Else
            Dim sb As New StringBuilder

            For Each c As Char In sender
                sb.Append(c & New String(" "c, count))
            Next c

            Return sb.ToString

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Expands the characters of a String by adding the specified separator char.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("Hello World".ExpandChars(" "c, 2))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' 
    ''' <param name="separator">
    ''' The character used to expand the characters.
    ''' </param>
    ''' 
    ''' <param name="count">
    ''' The amount of times to repeat the separator character.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The expanded string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Value bigger than 0 is required.;count
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function ExpandChars(ByVal sender As String,
                                ByVal separator As Char,
                                Optional ByVal count As Integer = 1) As String

        If (count <= 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="count",
                                                  message:="Value bigger than 0 is required.")
        Else
            Dim sb As New StringBuilder

            For Each c As Char In sender
                sb.Append(c & New String(separator, count))
            Next c

            Return sb.ToString

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Replaces the name of each environment variable embedded in the specified string with 
    ''' the string equivalent of the value of the variable.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("%HomeDrive%\Users\%UserName%\%Fake-Var%\".ExpandVariables)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The expanded string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function ExpandVariables(ByVal sender As String) As String

        Return Environment.ExpandEnvironmentVariables(sender)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Converts the diacritic characters in a String to an equivalent normalized character of English alphabet.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("áéíóú àèìòù äëïöü ñÑ çÇ".ConvertDiacritics(normalization:=NormalizationForm.FormKD))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' 
    ''' <param name="normalization">
    ''' The type of Unicode character normalization to perform.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The formated string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function NormalizeDiacritics(ByVal sender As String,
                                        Optional ByVal normalization As NormalizationForm = NormalizationForm.FormKD) As String

        Dim sb As New StringBuilder

        For Each c As Char In sender.Normalize(normalization)

            Select Case CharUnicodeInfo.GetUnicodeCategory(c)

                Case UnicodeCategory.NonSpacingMark,
                     UnicodeCategory.SpacingCombiningMark,
                     UnicodeCategory.EnclosingMark

                    ' Do nothing.
                    Exit Select

                Case Else
                    sb.Append(c)

            End Select

        Next c

        Return sb.ToString

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Determines wheter a String is written in the specified string case.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("HELLO WORLD".IsCase(StringCase.UpperCase))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' <see langword="True"/> if String is written in the specified string case, <see langword="False"/> otherwise.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' sender
    ''' </exception>
    ''' 
    ''' <exception cref="NotImplementedException">
    ''' This function does not support the specified string case.
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function IsCase(ByVal sender As String, ByVal stringCase As StringCase) As Boolean

        If String.IsNullOrEmpty(sender) Then
            Throw New ArgumentNullException(paramName:="sender")

        Else
            Select Case stringCase

                Case StringExtensions.StringCase.LowerCase
                    For Each c As Char In sender
                        If (CharUnicodeInfo.GetUnicodeCategory(c) = UnicodeCategory.UppercaseLetter) Then
                            Return False
                        End If
                    Next c

                Case StringExtensions.StringCase.UpperCase
                    For Each c As Char In sender
                        If (CharUnicodeInfo.GetUnicodeCategory(c) = UnicodeCategory.LowercaseLetter) Then
                            Return False
                        End If
                    Next c

                Case StringExtensions.StringCase.TitleCase
                    If Char.IsLower(sender.First) Then
                        Return False
                    End If

                    For Each c As Char In sender.Remove(0, 1)
                        If (CharUnicodeInfo.GetUnicodeCategory(c) = UnicodeCategory.UppercaseLetter) Then
                            Return False
                        End If
                    Next c

                Case StringExtensions.StringCase.WordCase
                    For Each word As String In sender.Split

                        If Char.IsLower(word.First) Then
                            Return False
                        End If

                        For Each c As Char In word.Remove(0, 1)
                            If (CharUnicodeInfo.GetUnicodeCategory(c) = UnicodeCategory.UppercaseLetter) Then
                                Return False
                            End If
                        Next c

                    Next word

                Case Else
                    Throw New NotImplementedException(message:="This function does not support the specified string case.")

            End Select

            Return True

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Gets info about the grooupings of a String.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Private Sub Test()
    ''' 
    '''     Dim source As String() =
    '''         {
    '''             "This is (good)",
    '''             "This (is ({good}))",
    '''             "This is good",
    '''             "This is (bad))",
    '''             "This is (bad",
    '''             "This is bad)",
    '''             "This is bad)("
    '''         }
    ''' 
    '''     Dim groupingCharList As New List(Of GroupingCharsInfo) From
    '''         {
    '''             New GroupingCharsInfo(apertureChar:="("c, closureChar:=")"c),
    '''             New GroupingCharsInfo(apertureChar:="{"c, closureChar:="}"c),
    '''             New GroupingCharsInfo(apertureChar:="["c, closureChar:="]"c)
    '''         }
    ''' 
    '''     For Each str As String In source
    ''' 
    '''         Dim info As StringExtensions.GroupingInfo = str.GetGroupingInfo(groupingCharList)
    ''' 
    '''         Dim sb As New System.Text.StringBuilder
    '''         With sb
    ''' 
    '''             .AppendLine(String.Format("Input String: {0}", info.Source))
    '''             .AppendLine()
    ''' 
    '''             .Append("Grouping Characters: ")
    '''             For Each charInfo As GroupingCharsInfo In groupingCharList
    '''                 .Append(String.Format("{0}{1} ", charInfo.Aperture, charInfo.Closure))
    '''             Next charInfo
    '''             .AppendLine()
    ''' 
    '''             .AppendLine(String.Format("String has closed agrupations?: {0}", info.HasClosedGroups))
    '''             .AppendLine(String.Format("String has opened agrupations?: {0}", info.HasOpenGroups))
    '''             .AppendLine()
    ''' 
    '''             .AppendLine(String.Format("Closed Agrupations Count: {0}", info.GroupsClosedCount))
    '''             .AppendLine(String.Format("Opened Agrupations Count: {0}", info.GroupsOpenCount))
    '''             .AppendLine()
    ''' 
    '''             .AppendLine("Closed Agrupations Indexes:")
    '''             For Each item As KeyValuePair(Of Integer, Integer) In info.GroupsClosedPositions
    '''                 .AppendLine(String.Format("Start: {0}, End: {1}", CStr(item.Key), CStr(item.Value)))
    '''             Next item
    '''             .AppendLine()
    ''' 
    '''             .AppendLine(String.Format("Opened Agrupations Indexes: {0}",
    '''                                       String.Join(", ", info.GroupsOpenPositions)))
    ''' 
    '''         End With
    ''' 
    '''         MessageBox.Show(sb.ToString, "Agrupations Information",
    '''                         MessageBoxButtons.OK, MessageBoxIcon.Information)
    ''' 
    '''     Next str
    ''' 
    ''' End Sub
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source String.
    ''' </param>
    ''' 
    ''' <param name="groupingChars">
    ''' The grouping chars.
    ''' 
    ''' If this value is <see langword="Nothing"/>, the default grouping characters are used instead. 
    ''' 
    ''' The default grouping characters are: "()", "{}" and "[]".
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' A <see cref="StringExtensions.GroupingInfo"/> object that contains the grouping info.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' groupingChars
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentException">
    ''' An aperture character cannot be the same as a closure character.;groupingChars
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function GetGroupingInfo(ByVal sender As String,
                                    Optional ByVal groupingChars As List(Of GroupingCharsInfo) = Nothing) As StringExtensions.GroupingInfo

        If (groupingChars IsNot Nothing) AndAlso (Not groupingChars.Any) Then
            Throw New ArgumentNullException(paramName:="groupingChars")

        ElseIf (From groupingCharInfo As GroupingCharsInfo In groupingChars
                Where groupingCharInfo.Aperture.Equals(groupingCharInfo.Closure)).Any Then

            Throw New ArgumentException(paramName:="groupingChars",
                                        message:="An aperture character cannot be the same as a closure character.")

        ElseIf String.IsNullOrEmpty(sender) Then
            Return Nothing

        Else
            If (groupingChars Is Nothing) Then
                groupingChars = New List(Of GroupingCharsInfo) From
                {
                    New GroupingCharsInfo(apertureChar:="("c, closureChar:=")"c),
                    New GroupingCharsInfo(apertureChar:="{"c, closureChar:="}"c),
                    New GroupingCharsInfo(apertureChar:="["c, closureChar:="]"c)
                }
            End If

            Dim charStack As New Stack(Of Char)
            Dim charPosStack As New Stack(Of Integer)

            Dim groupsOpenCount As Integer
            Dim groupsclosedCount As Integer

            Dim aperturePositions As New List(Of Integer)
            Dim closurePositions As New Dictionary(Of Integer, Integer)

            For i As Integer = 0 To (sender.Length - 1)

                For Each groupingCharInfo As GroupingCharsInfo In groupingChars

                    Select Case sender(i)

                        Case groupingCharInfo.Aperture
                            charStack.Push(groupingCharInfo.Aperture)
                            charPosStack.Push(i)
                            aperturePositions.Add(i)
                            groupsOpenCount += 1

                        Case groupingCharInfo.Closure
                            If (charStack.Count <> 0) AndAlso charStack.Pop.Equals(groupingCharInfo.Aperture) Then
                                closurePositions.Add(charPosStack.Pop, i)
                                aperturePositions.RemoveAt(aperturePositions.Count - 1)
                                groupsclosedCount += 1
                                groupsOpenCount -= 1

                            Else
                                groupsOpenCount += 1
                                aperturePositions.Add(i)

                            End If

                    End Select

                Next groupingCharInfo

            Next i

            ' Sort the closure positions by the aperture position.
            closurePositions = (From pair As KeyValuePair(Of Integer, Integer) In closurePositions
                                Order By pair.Key).
                               ToDictionary(Function(pair) pair.Key,
                                            Function(pair) pair.Value)

            Return New StringExtensions.GroupingInfo(sender, groupingChars,
                                                     groupsclosedCount <> 0, groupsOpenCount <> 0,
                                                     groupsclosedCount, groupsOpenCount,
                                                     closurePositions, aperturePositions)

        End If

    End Function

#End Region

#Region " Find And Replace "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Counts the occurences of the specified character in an String.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MsgBox("Hello world!".CountCharacter("o"c))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="character">
    ''' The character to find.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The amount of occurences of the specified character.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function CountCharacter(ByVal sender As String,
                                   ByVal character As Char) As Integer

        Return StringExtensions.CountCharacter(sender, character, StringComparison.Ordinal)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Counts the occurences of the specified character in an String.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MsgBox("Hello world!".CountCharacter("o"c, StringComparison.Ordinal))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="character">
    ''' The character to find.
    ''' </param>
    ''' 
    ''' <param name="stringComparison">
    ''' The string-case rules to compare.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The amount of occurences of the specified character.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function CountCharacter(ByVal sender As String,
                                   ByVal character As Char,
                                   ByVal stringComparison As StringComparison) As Integer

        Return (From c As Char In sender
                Where CStr(c).Equals(CStr(character), stringComparison)
               ).Count

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Delimits once a string by the given delimiter.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("You are welcome to my party tomorrow!".Delimit("to"))
    ''' MessageBox.Show("You are welcome to my party tomorrow!".Delimit("to", RegexOptions.RightToLeft))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="delimiter">
    ''' The delimiter string.
    ''' </param>
    ''' 
    ''' <param name="options">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The delimited string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function Delimit(ByVal sender As String,
                            ByVal delimiter As String,
                            Optional ByVal options As RegexOptions = RegexOptions.None) As String

        Return StringExtensions.InternalDelimit(sender, delimiter, String.Empty, 0, options)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Delimits once a string by the given delimiter.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("You are welcome to my party tomorrow!".Delimit("to"))
    ''' MessageBox.Show("You are welcome to my party tomorrow!".Delimit("a", 1, RegexOptions.RightToLeft))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="delimiter">
    ''' The delimiter string.
    ''' </param>
    ''' 
    ''' <param name="count">
    ''' The delimiter occurrence count.
    ''' </param>
    ''' 
    ''' <param name="options">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The delimited string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function Delimit(ByVal sender As String,
                            ByVal delimiter As String,
                            ByVal count As Integer,
                            Optional ByVal options As RegexOptions = RegexOptions.None) As String

        Return StringExtensions.InternalDelimit(sender, delimiter, String.Empty, count, options)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Delimits once a string by the given two delimiters.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("You are welcome to my party tomorrow!".Delimit("to", "party"))
    ''' MessageBox.Show("You are welcome to my party tomorrow!".Delimit("tomorrow", "welcome", RegexOptions.RightToLeft))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="delimiterA">
    ''' The first delimiter string.
    ''' </param>
    ''' 
    ''' <param name="delimiterB">
    ''' The last delimiter string.
    ''' </param>
    ''' 
    ''' <param name="options">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The delimited string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function Delimit(ByVal sender As String,
                            ByVal delimiterA As String,
                            ByVal delimiterB As String,
                            Optional ByVal options As RegexOptions = RegexOptions.None) As String

        Return StringExtensions.InternalDelimit(sender, delimiterA, delimiterB, 0, options)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Delimits once a string by the given two delimiters.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("You are welcome to my party tomorrow!".Delimit("o", "tomorrow", 1))
    ''' MessageBox.Show("You are welcome to my party tomorrow!".Delimit("o", "Y", 1, RegexOptions.RightToLeft))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="delimiterA">
    ''' The first delimiter string.
    ''' </param>
    ''' 
    ''' <param name="delimiterB">
    ''' The last delimiter string.
    ''' </param>
    ''' 
    ''' <param name="count">
    ''' The <paramref name="delimiterA"/> occurrence count.
    ''' </param>
    ''' 
    ''' <param name="options">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The delimited string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function Delimit(ByVal sender As String,
                            ByVal delimiterA As String,
                            ByVal delimiterB As String,
                            ByVal count As Integer,
                            Optional ByVal options As RegexOptions = RegexOptions.None) As String

        Return StringExtensions.InternalDelimit(sender, delimiterA, delimiterB, count, options)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Delimits once a string by the given two delimiters.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("You are welcome to my party tomorrow!".Delimit("o", "tomorrow", 1))
    ''' MessageBox.Show("You are welcome to my party tomorrow!".Delimit("o", "Y", 1, RegexOptions.RightToLeft))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="delimiterA">
    ''' The first delimiter string.
    ''' </param>
    ''' 
    ''' <param name="delimiterB">
    ''' The last delimiter string.
    ''' </param>
    ''' 
    ''' <param name="count">
    ''' The <paramref name="delimiterA"/> occurrence count.
    ''' </param>
    ''' 
    ''' <param name="options">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The delimited string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerHidden>
    <DebuggerStepThrough>
    Private Function InternalDelimit(ByVal sender As String,
                                     ByVal delimiterA As String,
                                     ByVal delimiterB As String,
                                     ByVal count As Integer,
                                     ByVal options As RegexOptions) As String

        Dim m1 As IEnumerable(Of Match)
        Dim m2 As IEnumerable(Of Match)

        m1 = New Regex(delimiterA, options).Matches(sender).Cast(Of Match)()
        If (Not m1.Any) OrElse (m1.Count < count) Then
            Return String.Empty

        Else
            Select Case options.HasFlag(RegexOptions.RightToLeft)

                Case False ' Left To Right
                    sender = sender.Substring(m1(count).Index + m1(count).Length)

                Case True ' Right To Left
                    sender = sender.Substring(0, m1(count).Index)

            End Select

        End If

        m2 = New Regex(delimiterB, options).Matches(sender).Cast(Of Match)()
        If (Not String.IsNullOrEmpty(delimiterB)) AndAlso (Not (m2.Any)) Then
            Return String.Empty

        ElseIf String.IsNullOrEmpty(delimiterB) Then
            Return sender

        Else
            Select Case options.HasFlag(RegexOptions.RightToLeft)

                Case False ' Left To Right
                    sender = sender.Substring(0, m2(0).Index)

                Case True ' Right To Left

                    sender = sender.Substring(m2(0).Index + m2(0).Length)
            End Select

        End If

        Return sender

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Reports the zero-based index of the first occurrence of the specified string in this instance, 
    ''' using a regular expression.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim indexOf As Integer = 
    '''     "Hello World!".IndexOfRegEx("\sWorld", regexOptions:=RegexOptions.IgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findExpression">
    ''' The <see cref="Regex"/> find expression.
    ''' </param>
    ''' 
    ''' <param name="regexOptions">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' findExpression
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The zero-based index position of value if that string is found, or -1 if it is not. 
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function IndexOfRegEx(ByVal sender As String,
                                 ByVal findExpression As String,
                                 Optional ByVal regexOptions As RegexOptions =
                                                RegularExpressions.RegexOptions.None) As Integer

        If String.IsNullOrEmpty(findExpression) Then
            Throw New ArgumentNullException(paramName:="findExpression")

        Else
            Return StringExtensions.InternalIndexOfRegEx(sender, findExpression, 0, -1, regexOptions)

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Reports the zero-based index of the first occurrence of the specified string in this instance, 
    ''' using a regular expression.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim indexOf As Integer = 
    '''     "Hello World!".IndexOfRegEx("\sWorld", startIndex:=0, regexOptions:=RegexOptions.IgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findExpression">
    ''' The <see cref="Regex"/> find expression.
    ''' </param>
    ''' 
    ''' <param name="startIndex">
    ''' The search starting position.
    ''' </param>
    ''' 
    ''' <param name="regexOptions">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' findExpression
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' startIndex
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The zero-based index position of value if that string is found, or -1 if it is not. 
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function IndexOfRegEx(ByVal sender As String,
                                 ByVal findExpression As String,
                                 ByVal startIndex As Integer,
                                 Optional ByVal regexOptions As RegexOptions =
                                                RegularExpressions.RegexOptions.None) As Integer

        If String.IsNullOrEmpty(findExpression) Then
            Throw New ArgumentNullException(paramName:="findExpression")

        ElseIf (startIndex < 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="startIndex")

        Else
            Return StringExtensions.InternalIndexOfRegEx(sender, findExpression, startIndex, -1, regexOptions)

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Reports the zero-based index of the first occurrence of the specified string in this instance, 
    ''' using a regular expression.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim indexOf As Integer = 
    '''     "Hello World!".IndexOfRegEx("^H$", startIndex:=0, count:=1, regexOptions:=RegexOptions.IgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findExpression">
    ''' The <see cref="Regex"/> find expression.
    ''' </param>
    ''' 
    ''' <param name="startIndex">
    ''' The search starting position.
    ''' </param>
    ''' 
    ''' <param name="count">
    ''' The number of character positions to examine.
    ''' </param>
    ''' 
    ''' <param name="regexOptions">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' findExpression
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' startIndex or count
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The zero-based index position of value if that string is found, or -1 if it is not. 
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function IndexOfRegEx(ByVal sender As String,
                                 ByVal findExpression As String,
                                 ByVal startIndex As Integer,
                                 ByVal count As Integer,
                                 Optional ByVal regexOptions As RegexOptions =
                                                RegularExpressions.RegexOptions.None) As Integer

        If String.IsNullOrEmpty(findExpression) Then
            Throw New ArgumentNullException(paramName:="findExpression")

        ElseIf (startIndex < 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="startIndex")

        ElseIf (count <= 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="count")

        Else
            Return StringExtensions.InternalIndexOfRegEx(sender, findExpression, startIndex, count, regexOptions)

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Reports the zero-based index of the first occurrence of the specified string in this instance, 
    ''' using a regular expression.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim indexOf As Integer = 
    '''     "Hello World!".IndexOfRegEx("^H$", startIndex:=0, count:=1, regexOptions:=RegexOptions.IgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findExpression">
    ''' The <see cref="Regex"/> find expression.
    ''' </param>
    ''' 
    ''' <param name="startIndex">
    ''' The search starting position.
    ''' </param>
    ''' 
    ''' <param name="count">
    ''' The number of character positions to examine.
    ''' </param>
    ''' 
    ''' <param name="regexOptions">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The zero-based index position of value if that string is found, or -1 if it is not. 
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerHidden>
    <DebuggerStepThrough>
    Private Function InternalIndexOfRegEx(ByVal sender As String,
                                          ByVal findExpression As String,
                                          ByVal startIndex As Integer,
                                          ByVal count As Integer,
                                          ByVal regexOptions As RegexOptions) As Integer

        Dim rgx As New Regex(findExpression, regexOptions)

        sender = sender.Substring(startIndex)
        If (count <> -1) Then
            sender = sender.Substring(0, count)
        End If

        If rgx.IsMatch(sender) Then
            Return (rgx.Match(sender).Index + startIndex)

        Else
            Return -1

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Reports the zero-based index of the first occurrence in this instance of 
    ''' any <see cref="Regex"/> expression in a specified array. 
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim Dim indexOfAny As Integer = 
    '''     "Hello World!".IndexOfAnyRegEx({"\s", "[\!]"}, regexOptions:=RegexOptions.IgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findExpressions">
    ''' An <see cref="IEnumerable(Of String)"/> containing one or more <see cref="Regex"/> expression to find.
    ''' </param>
    ''' 
    ''' <param name="regexOptions">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' findExpressions
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The zero-based index position of the first occurrence in 
    ''' this instance where any regular expression in <paramref name="findExpressions"/> was found; 
    ''' or -1 if no expression in <paramref name="findExpressions"/> was found.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function IndexOfAnyRegEx(ByVal sender As String,
                                    ByVal findExpressions As IEnumerable(Of String),
                                    Optional ByVal regexOptions As RegexOptions =
                                                   RegularExpressions.RegexOptions.None) As Integer

        If Not findExpressions.Any Then
            Throw New ArgumentNullException(paramName:="findExpressions")

        Else
            Return StringExtensions.InternalIndexOfAnyRegEx(sender, findExpressions, 0, -1, regexOptions)

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Reports the zero-based index of the first occurrence in this instance of 
    ''' any <see cref="Regex"/> expression in a specified array. 
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim Dim indexOfAny As Integer = 
    '''     "Hello World!".IndexOfAnyRegEx({"\s", "[\!]"}, startIndex:=0, regexOptions:=RegexOptions.IgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findExpressions">
    ''' An <see cref="IEnumerable(Of String)"/> containing one or more <see cref="Regex"/> expression to find.
    ''' </param>
    ''' 
    ''' <param name="startIndex">
    ''' The search starting position.
    ''' </param>
    ''' 
    ''' <param name="regexOptions">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' findExpressions
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' startIndex
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The zero-based index position of the first occurrence in 
    ''' this instance where any regular expression in <paramref name="findExpressions"/> was found; 
    ''' or -1 if no expression in <paramref name="findExpressions"/> was found.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function IndexOfAnyRegEx(ByVal sender As String,
                                    ByVal findExpressions As IEnumerable(Of String),
                                    ByVal startIndex As Integer,
                                    Optional ByVal regexOptions As RegexOptions =
                                                   RegularExpressions.RegexOptions.None) As Integer

        If Not findExpressions.Any Then
            Throw New ArgumentNullException(paramName:="findExpressions")

        ElseIf (startIndex < 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="startIndex")

        Else
            Return StringExtensions.InternalIndexOfAnyRegEx(sender, findExpressions, startIndex, -1, regexOptions)

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Reports the zero-based index of the first occurrence in this instance of 
    ''' any <see cref="Regex"/> expression in a specified array. 
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim Dim indexOfAny As Integer = 
    '''     "Hello World!".IndexOfAnyRegEx({"\s", "[\!]"}, startIndex:=0, count:=6, regexOptions:=RegexOptions.IgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findExpressions">
    ''' An <see cref="IEnumerable(Of String)"/> containing one or more <see cref="Regex"/> expression to find.
    ''' </param>
    ''' 
    ''' <param name="startIndex">
    ''' The search starting position.
    ''' </param>
    ''' 
    ''' <param name="count">
    ''' The number of character positions to examine.
    ''' </param>
    ''' 
    ''' <param name="regexOptions">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' findExpressions
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' startIndex or count
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The zero-based index position of the first occurrence in 
    ''' this instance where any regular expression in <paramref name="findExpressions"/> was found; 
    ''' or -1 if no expression in <paramref name="findExpressions"/> was found.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function IndexOfAnyRegEx(ByVal sender As String,
                                    ByVal findExpressions As IEnumerable(Of String),
                                    ByVal startIndex As Integer,
                                    ByVal count As Integer,
                                    Optional ByVal regexOptions As RegexOptions =
                                                   RegularExpressions.RegexOptions.None) As Integer

        If Not findExpressions.Any Then
            Throw New ArgumentNullException(paramName:="findExpressions")

        ElseIf (startIndex < 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="startIndex")

        ElseIf (count <= 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="count")

        Else
            Return StringExtensions.InternalIndexOfAnyRegEx(sender, findExpressions, startIndex, count, regexOptions)

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Reports the zero-based index of the first occurrence in this instance of 
    ''' any <see cref="Regex"/> expression in a specified array. 
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim Dim indexOfAny As Integer = 
    '''     "Hello World!".IndexOfAnyRegEx({"\s", "[\!]"}, startIndex:=0, count:=6, regexOptions:=RegexOptions.IgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findExpressions">
    ''' An <see cref="IEnumerable(Of String)"/> containing one or more <see cref="Regex"/> expression to find.
    ''' </param>
    ''' 
    ''' <param name="startIndex">
    ''' The search starting position.
    ''' </param>
    ''' 
    ''' <param name="count">
    ''' The number of character positions to examine.
    ''' </param>
    ''' 
    ''' <param name="regexOptions">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The zero-based index position of the first occurrence in 
    ''' this instance where any regular expression in <paramref name="findExpressions"/> was found; 
    ''' or -1 if no expression in <paramref name="findExpressions"/> was found.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerHidden>
    <DebuggerStepThrough>
    Private Function InternalIndexOfAnyRegEx(ByVal sender As String,
                                             ByVal findExpressions As IEnumerable(Of String),
                                             ByVal startIndex As Integer,
                                             ByVal count As Integer,
                                             ByVal regexOptions As RegexOptions) As Integer

        Dim rgx As New Regex(String.Join("|", findExpressions), regexOptions)

        sender = sender.Substring(startIndex)
        If (count <> -1) Then
            sender = sender.Substring(0, count)
        End If

        If rgx.IsMatch(sender) Then
            Return (rgx.Match(sender).Index + startIndex)

        Else
            Return -1

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Reports the zero-based index position of the last occurrence of a specified string within this instance,
    ''' using a regular expression.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim lastIndexOf As Integer = 
    '''     "Hello World!".LastIndexOfRegEx("\s", regexOptions:=RegexOptions.IgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findExpression">
    ''' The <see cref="Regex"/> find expression.
    ''' </param>
    ''' 
    ''' <param name="regexOptions">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' findExpression
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The zero-based index position of value if that string is found, or -1 if it is not found.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function LastIndexOfRegEx(ByVal sender As String,
                                     ByVal findExpression As String,
                                     Optional ByVal regexOptions As RegexOptions =
                                                    RegularExpressions.RegexOptions.None) As Integer

        If String.IsNullOrEmpty(findExpression) Then
            Throw New ArgumentNullException(paramName:="findExpression")

        Else
            Return StringExtensions.InternalLastIndexOfRegEx(sender, findExpression, 0, -1, regexOptions)

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Reports the zero-based index position of the last occurrence of a specified string within this instance,
    ''' using a regular expression.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim lastIndexOf As Integer = 
    '''     "Hello World!".LastIndexOfRegEx("\s", startIndex:=0, regexOptions:=RegexOptions.IgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findExpression">
    ''' The <see cref="Regex"/> find expression.
    ''' </param>
    ''' 
    ''' <param name="startIndex">
    ''' The search starting position.
    ''' </param>
    ''' 
    ''' <param name="regexOptions">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' findExpression
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' startIndex
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The zero-based index position of value if that string is found, or -1 if it is not found.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function LastIndexOfRegEx(ByVal sender As String,
                                     ByVal findExpression As String,
                                     ByVal startIndex As Integer,
                                     Optional ByVal regexOptions As RegexOptions =
                                                    RegularExpressions.RegexOptions.None) As Integer

        If String.IsNullOrEmpty(findExpression) Then
            Throw New ArgumentNullException(paramName:="findExpression")

        ElseIf (startIndex < 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="startIndex")

        Else
            Return StringExtensions.InternalLastIndexOfRegEx(sender, findExpression, startIndex, -1, regexOptions)

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Reports the zero-based index position of the last occurrence of a specified string within this instance,
    ''' using a regular expression.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim lastIndexOf As Integer = 
    '''     "Hello World!".LastIndexOfRegEx("\s", startIndex:=0, count:=6, regexOptions:=RegexOptions.IgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findExpression">
    ''' The <see cref="Regex"/> find expression.
    ''' </param>
    ''' 
    ''' <param name="startIndex">
    ''' The search starting position.
    ''' </param>
    ''' 
    ''' <param name="count">
    ''' The number of character positions to examine.
    ''' </param>
    ''' 
    ''' <param name="regexOptions">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' findExpression
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' startIndex or count
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The zero-based index position of value if that string is found, or -1 if it is not found.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function LastIndexOfRegEx(ByVal sender As String,
                                     ByVal findExpression As String,
                                     ByVal startIndex As Integer,
                                     ByVal count As Integer,
                                     Optional ByVal regexOptions As RegexOptions =
                                                     RegularExpressions.RegexOptions.None) As Integer

        If String.IsNullOrEmpty(findExpression) Then
            Throw New ArgumentNullException(paramName:="findExpression")

        ElseIf (startIndex < 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="startIndex")

        ElseIf (count <= 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="count")

        Else
            Return StringExtensions.InternalLastIndexOfRegEx(sender, findExpression, startIndex, count, regexOptions)

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Reports the zero-based index position of the last occurrence of a specified string within this instance,
    ''' using a regular expression.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim lastIndexOf As Integer = 
    '''     "Hello World!".LastIndexOfRegEx("\s", startIndex:=0, count:=6, regexOptions:=RegexOptions.IgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findExpression">
    ''' The <see cref="Regex"/> find expression.
    ''' </param>
    ''' 
    ''' <param name="startIndex">
    ''' The search starting position.
    ''' </param>
    ''' 
    ''' <param name="count">
    ''' The number of character positions to examine.
    ''' </param>
    ''' 
    ''' <param name="regexOptions">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The zero-based index position of value if that string is found, or -1 if it is not found.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerHidden>
    <DebuggerStepThrough>
    Private Function InternalLastIndexOfRegEx(ByVal sender As String,
                                              ByVal findExpression As String,
                                              ByVal startIndex As Integer,
                                              ByVal count As Integer,
                                              ByVal regexOptions As RegexOptions) As Integer

        Dim rgx As New Regex(findExpression, regexOptions)

        sender = sender.Substring(startIndex)
        If (count <> -1) Then
            sender = sender.Substring(0, count)
        End If

        If rgx.IsMatch(sender) Then
            Return (From m As Match In rgx.Matches(sender).Cast(Of Match)()
                    Select m.Index).Max + startIndex

        Else
            Return -1

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Reports the zero-based index of the last occurrence in this instance of 
    ''' any <see cref="Regex"/> expression in a specified array. 
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim Dim lastIndexOfAny As Integer = 
    '''     "Hello World!".LastIndexOfAnyRegEx({"\s", "o"c}, regexOptions:=RegexOptions.IgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findExpressions">
    ''' An <see cref="IEnumerable(Of String)"/> containing one or more <see cref="Regex"/> expression to find.
    ''' </param>
    ''' 
    ''' <param name="regexOptions">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' findExpressions
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The zero-based index position of the last occurrence in 
    ''' this instance where any regular expression in <paramref name="findExpressions"/> was found; 
    ''' or -1 if no expression in <paramref name="findExpressions"/> was found.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function LastIndexOfAnyRegEx(ByVal sender As String,
                                        ByVal findExpressions As IEnumerable(Of String),
                                        Optional ByVal regexOptions As RegexOptions =
                                                       RegularExpressions.RegexOptions.None) As Integer

        If (Not findExpressions.Any) Then
            Throw New ArgumentNullException(paramName:="findExpressions")

        Else
            Return StringExtensions.InternalLastIndexOfAnyRegEx(sender, findExpressions, 0, -1, regexOptions)

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Reports the zero-based index of the last occurrence in this instance of 
    ''' any <see cref="Regex"/> expression in a specified array. 
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim Dim lastIndexOfAny As Integer = 
    '''     "Hello World!".LastIndexOfAnyRegEx({"\s", "o"c}, startIndex:=0, regexOptions:=RegexOptions.IgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findExpressions">
    ''' An <see cref="IEnumerable(Of String)"/> containing one or more <see cref="Regex"/> expression to find.
    ''' </param>
    ''' 
    ''' <param name="startIndex">
    ''' The search starting position.
    ''' </param>
    ''' 
    ''' <param name="regexOptions">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' findExpressions
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' startIndex
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The zero-based index position of the last occurrence in 
    ''' this instance where any regular expression in <paramref name="findExpressions"/> was found; 
    ''' or -1 if no expression in <paramref name="findExpressions"/> was found.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function LastIndexOfAnyRegEx(ByVal sender As String,
                                        ByVal findExpressions As IEnumerable(Of String),
                                        ByVal startIndex As Integer,
                                        Optional ByVal regexOptions As RegexOptions =
                                                       RegularExpressions.RegexOptions.None) As Integer

        If (Not findExpressions.Any) Then
            Throw New ArgumentNullException(paramName:="findExpressions")

        ElseIf (startIndex < 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="startIndex")

        Else
            Return StringExtensions.InternalLastIndexOfAnyRegEx(sender, findExpressions, startIndex, -1, regexOptions)

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Reports the zero-based index of the last occurrence in this instance of 
    ''' any <see cref="Regex"/> expression in a specified array. 
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim Dim lastIndexOfAny As Integer = 
    '''     "Hello World!".LastIndexOfAnyRegEx({"\s", "o"c}, startIndex:=0, count:=6, regexOptions:=RegexOptions.IgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findExpressions">
    ''' An <see cref="IEnumerable(Of String)"/> containing one or more <see cref="Regex"/> expression to find.
    ''' </param>
    ''' 
    ''' <param name="startIndex">
    ''' The search starting position.
    ''' </param>
    ''' 
    ''' <param name="count">
    ''' The number of character positions to examine.
    ''' </param>
    ''' 
    ''' <param name="regexOptions">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' findExpressions
    ''' </exception>
    ''' 
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' startIndex or count
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The zero-based index position of the last occurrence in 
    ''' this instance where any regular expression in <paramref name="findExpressions"/> was found; 
    ''' or -1 if no expression in <paramref name="findExpressions"/> was found.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function LastIndexOfAnyRegEx(ByVal sender As String,
                                        ByVal findExpressions As IEnumerable(Of String),
                                        ByVal startIndex As Integer,
                                        ByVal count As Integer,
                                        Optional ByVal regexOptions As RegexOptions =
                                                       RegularExpressions.RegexOptions.None) As Integer

        If (Not findExpressions.Any) Then
            Throw New ArgumentNullException(paramName:="findExpressions")

        ElseIf (startIndex < 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="startIndex")

        ElseIf (count <= 0) Then
            Throw New ArgumentOutOfRangeException(paramName:="count")

        Else
            Return StringExtensions.InternalLastIndexOfAnyRegEx(sender, findExpressions, startIndex, count, regexOptions)

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Reports the zero-based index of the last occurrence in this instance of 
    ''' any <see cref="Regex"/> expression in a specified array. 
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim Dim lastIndexOfAny As Integer = 
    '''     "Hello World!".LastIndexOfAnyRegEx({"\s", "o"c}, startIndex:=0, count:=6, regexOptions:=RegexOptions.IgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findExpressions">
    ''' An <see cref="IEnumerable(Of String)"/> containing one or more <see cref="Regex"/> expression to find.
    ''' </param>
    ''' 
    ''' <param name="startIndex">
    ''' The search starting position.
    ''' </param>
    ''' 
    ''' <param name="count">
    ''' The number of character positions to examine.
    ''' </param>
    ''' 
    ''' <param name="regexOptions">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The zero-based index position of the last occurrence in 
    ''' this instance where any regular expression in <paramref name="findExpressions"/> was found; 
    ''' or -1 if no expression in <paramref name="findExpressions"/> was found.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerHidden>
    <DebuggerStepThrough>
    Private Function InternalLastIndexOfAnyRegEx(ByVal sender As String,
                                                 ByVal findExpressions As IEnumerable(Of String),
                                                 ByVal startIndex As Integer,
                                                 ByVal count As Integer,
                                                 ByVal regexOptions As RegexOptions) As Integer

        Dim rgx As New Regex(String.Join("|", findExpressions), regexOptions)

        sender = sender.Substring(startIndex)
        If (count <> -1) Then
            sender = sender.Substring(0, count)
        End If

        If rgx.IsMatch(sender) Then
            Return (From m As Match In rgx.Matches(sender).Cast(Of Match)()
                    Select m.Index).Max + startIndex

        Else
            Return -1

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Replaces text using a regular expression.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim str As String = "Hello World!".ReplaceRegEx("world", "kitty", RegexOptions.IgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findWhat">
    ''' The <see cref="Regex"/> find expression.
    ''' </param>
    ''' 
    ''' <param name="regexOptions">
    ''' The <see cref="RegexOptions"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' findWhat
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The replaced string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function ReplaceRegEx(ByVal sender As String,
                                 ByVal findWhat As String,
                                 ByVal replaceWith As String,
                                 Optional ByVal regexOptions As RegexOptions =
                                                RegularExpressions.RegexOptions.None) As String

        If String.IsNullOrEmpty(sender) Then
            Return sender

        ElseIf String.IsNullOrEmpty(findWhat) Then
            Throw New ArgumentNullException(paramName:="findWhat")

        Else
            Return Regex.Replace(sender, findWhat, replaceWith, regexOptions)

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Replaces text using the specified string comparison type.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim str As String = "Hello World!".Replace("world", "kitty", StringComparison.OrdinalIgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findWhat">
    ''' The <see cref="Regex"/> find expression.
    ''' </param>
    ''' 
    ''' <param name="comparisonType">
    ''' The string comparison type.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentNullException">
    ''' findWhat
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The replaced string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function Replace(ByVal sender As String,
                            ByVal findWhat As String,
                            ByVal replaceWith As String,
                            ByVal comparisonType As StringComparison) As String

        If String.IsNullOrEmpty(sender) Then
            Return sender

        ElseIf String.IsNullOrEmpty(findWhat) Then
            Throw New ArgumentNullException(paramName:="findWhat")

        Else
            Return StringExtensions.InternalReplace(sender, findWhat, replaceWith, comparisonType, stringBuilderCapacity:=-1)

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Replaces text using the specified string comparison type.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' Original source: 
    ''' <see href="http://www.codeproject.com/Articles/10890/Fastest-C-Case-Insenstive-String-Replace?msg=1835929#xx1835929xx"/>
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim str As String = "Hello World!".Replace("world", "kitty", StringComparison.OrdinalIgnoreCase)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="findWhat">
    ''' The <see cref="Regex"/> find expression.
    ''' </param>
    ''' 
    ''' <param name="comparisonType">
    ''' The string comparison type.
    ''' </param>
    ''' 
    ''' <param name="stringBuilderCapacity">
    ''' The initial buffer size of the <see cref="Stringbuilder"/>.
    ''' This parameter is reserved for testing purposes.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The replaced string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerHidden>
    <DebuggerStepThrough>
    Private Function InternalReplace(ByVal sender As String,
                                     ByVal findWhat As String,
                                     ByVal replaceWith As String,
                                     ByVal comparisonType As StringComparison,
                                     ByVal stringBuilderCapacity As Integer) As String

        Dim posCurrent As Integer = 0
        Dim lenPattern As Integer = findWhat.Length
        Dim idxNext As Integer = sender.IndexOf(findWhat, comparisonType)
        Dim result As New StringBuilder(capacity:=If(stringBuilderCapacity <= 0, Math.Min(4096, sender.Length), stringBuilderCapacity))

        While (idxNext >= 0)
            result.Append(sender, posCurrent, (idxNext - posCurrent))
            result.Append(replaceWith)

            posCurrent = (idxNext + lenPattern)
            idxNext = sender.IndexOf(findWhat, posCurrent, comparisonType)
        End While

        result.Append(sender, posCurrent, (sender.Length - posCurrent))

        Return result.ToString

    End Function

#End Region

#Region " Validation "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Determines whether a string is numeric.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' <see langword="True"/> if string is numeric, <see langword="False"/> otherwise.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function IsNumeric(ByVal sender As String) As Boolean

        If String.IsNullOrEmpty(sender) Then
            Return False

        Else
            Return Double.TryParse(sender, New Double)

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Determines whether a string is numeric of specified <see cref="Type"/>.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <typeparam name="T">
    ''' The numeric <see cref="Type"/>
    ''' </typeparam>
    ''' 
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' <see langword="True"/> if string is numeric of the specified <see cref="type"/>, <see langword="False"/> otherwise.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <exception cref="ArgumentException">
    ''' The specified DataType is not numeric;T
    ''' </exception>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function IsNumericOf(Of T)(ByVal sender As String) As Boolean

        If String.IsNullOrEmpty(sender) Then
            Return False

        Else
            Select Case True

                Case GetType(T).Equals(GetType(Byte))
                    Return Byte.TryParse(sender, New Byte)

                Case GetType(T).Equals(GetType(SByte))
                    Return SByte.TryParse(sender, New SByte)

                Case GetType(T).Equals(GetType(Short))
                    Return Short.TryParse(sender, New Short)

                Case GetType(T).Equals(GetType(UShort))
                    Return UShort.TryParse(sender, New UShort)

                Case GetType(T).Equals(GetType(Integer))
                    Return Integer.TryParse(sender, New Integer)

                Case GetType(T).Equals(GetType(UInteger))
                    Return UInteger.TryParse(sender, New UInteger)

                Case GetType(T).Equals(GetType(Long))
                    Return Long.TryParse(sender, New Long)

                Case GetType(T).Equals(GetType(ULong))
                    Return ULong.TryParse(sender, New ULong)

                Case GetType(T).Equals(GetType(Decimal))
                    Return Decimal.TryParse(sender, New Decimal)

                Case GetType(T).Equals(GetType(Single))
                    Return Single.TryParse(sender, New Single)

                Case GetType(T).Equals(GetType(Double))
                    Return Double.TryParse(sender, New Double)

                Case Else
                    Throw New ArgumentException(message:="The specified DataType is not numeric.", paramName:="T")

            End Select

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Determines whether a string is alphabetic (english letters).
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' <see langword="True"/> if string is alphabetic, <see langword="False"/> otherwise.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function IsAlphabetic(ByVal sender As String) As Boolean

        If String.IsNullOrEmpty(sender) Then
            Return False

        Else
            Return Not (From c As Char In sender Where Not "abcdefghijklmnopqrstuvwxyz".Contains(c)).Any

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Determines whether a string is a valid value of the specified <see cref="Type"/>.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MsgBox("White".IsValueOf(Of Color))
    ''' MsgBox("A".IsValueOf(Of Keys))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <typeparam name="T">
    ''' The <see cref="Type"/>
    ''' </typeparam>
    ''' 
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' <see langword="True"/> if string is a valid value of specified <see cref="Type"/>; otherwise, <see langword="False"/>.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function IsValueOf(Of T)(ByVal sender As String) As Boolean

        Dim converter As TypeConverter = TypeDescriptor.GetConverter(GetType(T))

        If (converter IsNot Nothing) Then
            Return converter.IsValid(sender)

        Else
            Return False

        End If

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Determines whether a string contains diacritic characters.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MsgBox("Pingüino".HasDiacritics)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' <see langword="True"/> if strin contains diacritic characters; otherwise, <see langword="False"/>.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function HasDiacritics(ByVal sender As String) As Boolean

        If String.IsNullOrEmpty(sender) Then
            Return False

        Else
            For Each c As Char In sender
                Dim descomposed As Char() = c.ToString.Normalize(NormalizationForm.FormKD).ToCharArray

                If (descomposed.Count <> 1 OrElse String.IsNullOrWhiteSpace(descomposed)) Then
                    Return True
                End If

            Next c

            Return False

        End If

    End Function

#End Region

#Region " Conversions "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Converts a string to a Binary representation.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("Test".ToBinary(delimiter:=" "))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' 
    ''' <param name="delimiter">
    ''' The string used to delimite letter sequences.
    ''' </param>
    ''' 
    ''' <param name="encoding">
    ''' The <see cref="Encoding"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The Binary string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function ToBinary(ByVal sender As String,
                             Optional ByVal delimiter As String = "",
                             Optional ByVal encoding As Encoding = Nothing) As String

        If (encoding Is Nothing) Then
            encoding = System.Text.Encoding.ASCII
        End If

        Dim sb As New StringBuilder
        For Each c As Byte In encoding.GetBytes(sender)
            sb.Append(Convert.ToString(c, 2).PadLeft(8, "0"c) & delimiter)
        Next c
        Return sb.ToString

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Converts a string to a Hexadecimal representation.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("Test".ToHex(delimiter:=" "))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' 
    ''' <param name="delimiter">
    ''' The string used to delimite letter sequences.
    ''' </param>
    ''' 
    ''' <param name="encoding">
    ''' The <see cref="Encoding"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The Hexadecimal string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function ToHex(ByVal sender As String,
                          Optional ByVal delimiter As String = "",
                          Optional ByVal encoding As Encoding = Nothing) As String

        If (encoding Is Nothing) Then
            encoding = System.Text.Encoding.ASCII
        End If

        Dim sb As New StringBuilder
        For Each c As Byte In encoding.GetBytes(sender)
            sb.Append(Convert.ToString(c, 16) & delimiter)
        Next c
        Return sb.ToString

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Converts a Binary string to ASCII string.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("01001000 01100101 01101100 01101100 01101111 00100000 01010111 01101111 01110010 01101100 01100100 00100001".BinaryToASCII)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The converted string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function BinaryToAscII(ByVal sender As String) As String

        Dim sb As New StringBuilder

        For Each value As String In StringExtensions.SplitByLength(sender.Replace(" "c, ""), 8)
            sb.Append(Convert.ToChar(Convert.ToByte(value, 2)))
        Next value

        Return sb.ToString

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Converts a Hexadecimal string to string.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("48 65 6C 6C 6F 20 57 6F 72 6C 64 21".HexToString)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The converted string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function HexToString(ByVal sender As String) As String

        Dim sb As New StringBuilder

        For Each value As String In StringExtensions.SplitByLength(sender.Replace(" "c, ""), 2)
            sb.Append(Char.ConvertFromUtf32(Convert.ToInt32(value, 16)))
        Next value

        Return sb.ToString

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Converts an Hexadecimal String to <see cref="Short"/> value.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MsgBox("0x0200".HexToInt16)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The Hexadecimal value.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The <see cref="Short"/> representation of the Hexadecimal value.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function HexToInt16(ByVal sender As String) As Short

        Return Convert.ToInt16(sender.Replace(" ", ""), fromBase:=16)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Converts an Hexadecimal String to <see cref="UShort"/> value.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MsgBox("0x0200".HexToUInt16)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The Hexadecimal value.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The <see cref="UShort"/> representation of the Hexadecimal value.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function HexToUInt16(ByVal sender As String) As UShort

        Return Convert.ToUInt16(sender.Replace(" ", ""), fromBase:=16)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Converts an Hexadecimal String to <see cref="Integer"/> value.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MsgBox("0x020032".HexToInt32)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The Hexadecimal value.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The <see cref="Integer"/> representation of the Hexadecimal value.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function HexToInt32(ByVal sender As String) As Integer

        Return Convert.ToInt32(sender.Replace(" ", ""), fromBase:=16)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Converts an Hexadecimal String to <see cref="UInteger"/> value.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MsgBox("0x020032".HexToUInt32)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The Hexadecimal value.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The <see cref="UInteger"/> representation of the Hexadecimal value.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function HexToUInt32(ByVal sender As String) As UInteger

        Return Convert.ToUInt32(sender.Replace(" ", ""), fromBase:=16)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Converts an Hexadecimal String to <see cref="Long"/> value.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MsgBox("0x020032".HexToInt64)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The Hexadecimal value.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The <see cref="Long"/> representation of the Hexadecimal value.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function HexToInt64(ByVal sender As String) As Long

        Return Convert.ToInt64(sender.Replace(" ", ""), fromBase:=16)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Converts an Hexadecimal String to <see cref="ULong"/> value.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MsgBox("0x020032".HexToUInt64)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The Hexadecimal value.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The <see cref="ULong"/> representation of the Hexadecimal value.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function HexToUInt64(ByVal sender As String) As ULong

        Return Convert.ToUInt64(sender.Replace(" ", ""), fromBase:=16)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Converts an Hexadecimal value to its Visual Basic literal Hexadecimal representation.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MsgBox("48 65 6C 6C 6F 20 57".HexToVbHex)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source value.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The Hexadecimal value.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function HexToVbHex(ByVal sender As String) As String

        Return String.Format("&H{0}", Convert.ToString(StringExtensions.HexToInt64(sender), toBase:=16).ToUpper)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Converts an Hexadecimal value to its C# literal Hexadecimal representation.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MsgBox("48 65 6C 6C 6F 20 57".HexToCSharpHex)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source value.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The Hexadecimal value.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function HexToCSharpHex(ByVal sender As String) As String

        If sender.StartsWith("&H", StringComparison.OrdinalIgnoreCase) Then
            sender = sender.Substring(2)
        End If

        Return String.Format("0x{0}", Convert.ToString(StringExtensions.HexToInt64(sender), toBase:=16).ToUpper)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Converts a string to an encoded Base64 string.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("Test".ToBase64)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' 
    ''' <param name="encoding">
    ''' The <see cref="Encoding"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The encoded string.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function ToBase64(ByVal sender As String,
                             Optional ByVal encoding As Encoding = Nothing) As String

        If (encoding Is Nothing) Then
            encoding = System.Text.Encoding.ASCII
        End If

        Return Convert.ToBase64String(encoding.GetBytes(sender))

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Converts a string to a MD5 hash.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show("Test".ToMD5)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' 
    ''' <param name="encoding">
    ''' The <see cref="Encoding"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The hash.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function ToMD5(ByVal sender As String,
                          Optional ByVal encoding As Encoding = Nothing) As String

        If (encoding Is Nothing) Then
            encoding = System.Text.Encoding.ASCII
        End If

        Using md5Crypto As New MD5CryptoServiceProvider

            Dim byteData As Byte() = encoding.GetBytes(sender)
            Dim hash As Byte() = md5Crypto.ComputeHash(byteData)

            Dim sb As New StringBuilder

            For Each b As Byte In hash
                sb.Append(b.ToString("x").PadLeft(2, "0"c))
            Next

            Return sb.ToString

        End Using

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Converts a string to a Byte array.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MessageBox.Show(String.Join(", ", "Test".ToByteArray(Encoding.ASCII)))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' 
    ''' <param name="encoding">
    ''' The <see cref="Encoding"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The bytes.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function ToByteArray(ByVal sender As String,
                                Optional ByVal encoding As Encoding = Nothing) As Byte()

        If (encoding Is Nothing) Then
            encoding = System.Text.Encoding.ASCII
        End If

        Return encoding.GetBytes(sender)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Converts a string to a <see cref="HtmlDocument"/>.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim doc As HtmlDocument = File.ReadAllText("C:\Source.html", Encoding.Default).ToHtmlDocument
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source string.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The <see cref="HtmlDocument"/>.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function ToHtmlDocument(ByVal sender As String) As HtmlDocument

        Using wb As New WebBrowser

            With wb
                .ScriptErrorsSuppressed = True
                .DocumentText = ""
                .Document.OpenNew(replaceInHistory:=True)
                .Document.Write(sender)
            End With

            Return wb.Document

        End Using

    End Function

#End Region

#Region " Miscellaneous "

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Provides the <see cref="Size"/>, in pixels, of the specified text when drawn with the specified font.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Dim pxSize As Size = "Hello World!".Measure(New Font("Lucida Console", 12))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The <see cref="Size"/>, in pixels, of the specified text when drawn with the specified font.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function Measure(ByVal sender As String,
                            ByVal font As Font) As Size

        Return TextRenderer.MeasureText(sender, font)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Gets the size, in bytes, that occupies a string in memory.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' sgBox("Test".SizeInMemory)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The size, in bytes.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function SizeInMemory(ByVal sender As String) As Integer

        Return (sender.Length * Len(New Char))

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Gets the size, in bytes, of how much a string will occupy when written to a file using the specified <see cref="Encoding"/>.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MsgBox("Test".SizeInFile(Encoding.Default))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="encoding">
    ''' The <see cref="Encoding"/>.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The size, in bytes.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function SizeInFile(ByVal sender As String,
                               ByVal encoding As Encoding) As Integer

        Return StringExtensions.InternalSizeInFile(sender, encoding, sumByteOrderMark:=False)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Gets the size, in bytes, of how much a string will occupy when written to a file using the specified <see cref="Encoding"/>.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MsgBox("Test".SizeInFile(Encoding.UTF8, sumByteOrderMark:=True))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="encoding">
    ''' The <see cref="Encoding"/>.
    ''' </param>
    ''' 
    ''' <param name="sumByteOrderMark">
    ''' If <see langword="True"/>, takes into account the BOM (Byte Order Mark) in the resulting value.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The size, in bytes.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Extension>
    Public Function SizeInFile(ByVal sender As String,
                               ByVal encoding As Encoding,
                               ByVal sumByteOrderMark As Boolean) As Integer

        Return StringExtensions.InternalSizeInFile(sender, encoding, sumByteOrderMark)

    End Function

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Gets the size, in bytes, of how much a string will occupy when written to a file.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' MsgBox("Test".SizeInFile(Encoding.UTF8, sumByteOrderMark:=True))
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="sender">
    ''' The source <see cref="String"/>.
    ''' </param>
    ''' 
    ''' <param name="encoding">
    ''' The <see cref="Encoding"/>.
    ''' </param>
    ''' 
    ''' <param name="sumByteOrderMark">
    ''' If <see langword="True"/>, takes into account the BOM (Byte Order Mark) in the resulting value.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <returns>
    ''' The size, in bytes.
    ''' </returns>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerHidden>
    <DebuggerStepThrough>
    Private Function InternalSizeInFile(ByVal sender As String,
                                        ByVal encoding As Encoding,
                                        ByVal sumByteOrderMark As Boolean) As Integer

        If (encoding Is Nothing) Then
            encoding = System.Text.Encoding.Default
        End If

        If sumByteOrderMark Then
            Return (encoding.GetByteCount(sender) + encoding.GetPreamble.Count)

        Else
            Return encoding.GetByteCount(sender)

        End If

    End Function

#End Region

End Module
