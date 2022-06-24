'Programmed by Jeffrey Kirchner and Your Name Here
'kirchner@chapman.edu/jkirchner@gmail.com
'Economic Science Institute, Chapman University 2008-2010 ©

Imports System.IO

Module modMain
#Region " General Variables "
    Public playerList(100) As player                  'array of players
    Public playerCount As Integer                    'number of players connected
    Public numberOfPlayers As Integer                'number of desired players
    Public sfile As String                           'location of intialization file  
    Public checkin As Integer                        'global counter 
    Public connectionCount As Integer                'total number of connections made since server start 
    Public portNumber As Integer                     'port number sockect traffic is operation on 
    Public summaryDf As StreamWriter                 'data file
    Public playerDf As StreamWriter                 'data file
    Public replayDf As StreamWriter                 'data file
    Public frmServer As New frmMain                  'main form 
    Public filename As String                        'location of data file
    Public filename2 As String                       'location of data file
    Public showInstructions As Boolean               'show client instructions  
    Public currentInstruction As Integer             'current page of instructions 
#End Region

    'global variables here
    Public numberOfPeriods As Integer     'number of periods
    Public currentPeriod As Integer       'current period 

    Public instructionX As Integer        'start up locations of windows
    Public instructionY As Integer
    Public windowX As Integer
    Public windowY As Integer

    Public testMode As String
    Public periodStart As Date

    Public noMatchEarnings As Integer

    Public nodeListInstructions(3, 3) As node
    Public nodeCountInstructions(3) As Integer

    Public instructionCount As Integer = 8

    Public nodeList(100, 100) As node  'ID/Tree
    Public nodeCount(100) As Integer

    'Public regimeList(100) As String

    Public treeCount As Integer                   'number of stored trees
    Public periods(100) As period
    Public currentSubPeriod As Integer
    Public subPeriodCount(100) As Integer


#Region " General Functions "
    Public Sub main(ByVal args() As String)
        connectionCount = 0

        AppEventLog_Init()
        appEventLog_Write("Load")

        ToggleScreenSaverActive(False)

        Application.EnableVisualStyles()
        Application.Run(frmServer)

        ToggleScreenSaverActive(True)

        appEventLog_Write("Exit")
        AppEventLog_Close()
    End Sub

    Public Sub takeIP(ByVal sinstr As String, ByVal index As Integer)
        Try
            playerList(index).ipAddress = sinstr
        Catch ex As Exception
            appEventLog_Write("error takeIP:", ex)
        End Try
    End Sub

    Public Function roundUp(ByVal value As Double) As Integer
        Try
            Dim msgtokens() As String

            If InStr(CStr(value), ".") Then
                msgtokens = CStr(value).Split(".")

                roundUp = msgtokens(0)
                roundUp += 1
            Else
                roundUp = value
            End If
        Catch ex As Exception
            Return CInt(value)
            appEventLog_Write("error roundUp:", ex)
        End Try
    End Function

    Public Function getMyColor(ByVal index As Integer) As Color
        Try
            'appEventLog_Write("get color")

            Select Case index
                Case 1
                    getMyColor = Color.Blue
                Case 2
                    getMyColor = Color.Red
                Case 3
                    getMyColor = Color.Teal
                Case 4
                    getMyColor = Color.Green
                Case 5
                    getMyColor = Color.Purple
                Case 6
                    getMyColor = Color.Orange
                Case 7
                    getMyColor = Color.Brown
                Case 8
                    getMyColor = Color.Gray
            End Select
        Catch ex As Exception
            appEventLog_Write("error getMyColor:", ex)
        End Try
    End Function

    Public Function colorToId(ByVal str As String) As Integer
        Try
            Dim i As Integer

            'appEventLog_Write("color to id :" & str)

            For i = 1 To numberOfPlayers
                If str = playerList(i).colorName Then
                    colorToId = i
                    Exit Function
                End If
            Next

            colorToId = -1
        Catch ex As Exception
            Return 0
            appEventLog_Write("error colorToId:", ex)
        End Try
    End Function
#End Region

    Public Sub takeMessage(ByVal sinstr As String)
        'when a message is received from a client it is parsed here
        'msgtokens(1) has type of message sent, having different types of messages allows you to send different formats for different actions.
        'msgtokens(2) has the semicolon delimited data that is to be parsed and acted upon.  
        'index has the client ID that sent the data.  Client ID is assigned by connection order, indexed from 1.

        Try
            With frmServer
                Dim msgtokens() As String

                msgtokens = sinstr.Split("|")

                Dim index As Integer
                index = msgtokens(0)

                Application.DoEvents()

                Select Case msgtokens(1) 'case statement to handle each of the different types of messages
                    Case "COMPUTER_NAME"
                        takeRemoteComputerName(msgtokens(2), index)
                    Case "01"
                        takeUpdateInstructionDisplay(msgtokens(2), index)
                    Case "02"
                        takeFinishedInstructions(msgtokens(2), index)
                    Case "03"
                        takeIP(msgtokens(2), index)
                    Case "04"
                        takeChoice(msgtokens(2), index)
                    Case "05"
                        takeFinishedReviewingResults(msgtokens(2), index)
                    Case "06"
                        takeTreeChoice(msgtokens(2), index)
                    Case "07"
                        takeNames(msgtokens(2), index)
                    Case "08"

                    Case "09"

                    Case "10"

                    Case "11"

                    Case "12"

                    Case "13"


                End Select

                Application.DoEvents()

            End With
            'all subs/functions should have an error trap
        Catch ex As Exception
            appEventLog_Write("error takeMessage: " & sinstr & " : ", ex)
        End Try

    End Sub

    Public Sub takeFinishedInstructions(ByVal sinstr As String, ByVal index As Integer)
        Try
            With frmServer
                Dim msgtokens() As String = sinstr.Split(";")
                checkin += 1
                .DataGridView1.Rows(index - 1).Cells(2).Value = "Waiting"

                playerList(index).instructionLength(instructionCount) += msgtokens(0)

                If checkin = getNumberOfActivePlayers() Then
                    showInstructions = False
                    checkin = 0

                    MessageBox.Show("Begin Game.", "Start", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    startNextPeriod()
                    'periodStart = Now

                    'For i As Integer = 1 To numberOfPlayers
                    '    playerList(i).finishedInstructions()
                    '    .DataGridView1.Rows(i - 1).Cells(2).Value = "Playing"
                    'Next i

                End If
            End With
        Catch ex As Exception
            appEventLog_Write("error: ", ex)
        End Try
    End Sub

    Public Function getNumberOfActivePlayers() As Integer
        Try
            Dim t As Integer = 0

            For i As Integer = 1 To numberOfPlayers
                If Not playerList(i).outOfGame Then t += 1
            Next

            Return t
        Catch ex As Exception
            appEventLog_Write("error: ", ex)
            Return 0
        End Try
    End Function


    Public Sub takeNames(ByVal sinstr As String, ByVal index As Integer)
        Try
            With frmServer
                Dim outstr As String = ""
                Dim msgtokens() As String = sinstr.Split(";")

                playerList(index).takeName(msgtokens(0))

                If currentPeriod = numberOfPeriods Then checkin += 1

                .DataGridView1.Rows(index - 1).Cells(2).Value = "Done"

                If currentPeriod <> numberOfPeriods Then Exit Sub

                For i As Integer = 1 To numberOfPlayers
                    If playerList(i).sname = "" Then Exit Sub
                Next

                playerDf.WriteLine("")

                playerDf.WriteLine("Earnings")
                outstr = "Name,Earnings,"
                playerDf.WriteLine(outstr)

                For i As Integer = 1 To numberOfPlayers

                    outstr = .DataGridView1.Rows(i - 1).Cells(1).Value & ","
                    outstr &= .DataGridView1.Rows(i - 1).Cells(3).Value & ","
                    playerDf.WriteLine(outstr)
                Next

                'playerDf.WriteLine("")
                'playerDf.WriteLine("Instruction Length")
                'outstr = "Player,"
                'For i As Integer = 1 To instructionCount
                '    outstr &= "Page " & i & ","
                'Next

                'playerDf.WriteLine(outstr)

                'For i As Integer = 1 To numberOfPlayers
                '    outstr = i & ",N\A,"

                '    For j As Integer = 2 To instructionCount
                '        outstr &= Math.Round(playerList(i).instructionLength(j), 1) & ","
                '    Next

                '    playerDf.WriteLine(outstr)
                'Next

                playerDf.Close()
                summaryDf.Close()
                'replayDf.Close()
            End With
        Catch ex As Exception
            appEventLog_Write("error: ", ex)
        End Try
    End Sub

    Public Sub loadParameters()
        Try
            'load parameters from server.ini

            numberOfPlayers = getINI(sfile, "gameSettings", "numberOfPlayers")
            numberOfPeriods = getINI(sfile, "gameSettings", "numberOfPeriods")
            showInstructions = getINI(sfile, "gameSettings", "showInstructions")
            portNumber = getINI(sfile, "gameSettings", "port")

            instructionX = getINI(sfile, "gameSettings", "instructionX")
            instructionY = getINI(sfile, "gameSettings", "instructionY")
            windowX = getINI(sfile, "gameSettings", "windowX")
            windowY = getINI(sfile, "gameSettings", "windowY")

            testMode = getINI(sfile, "gameSettings", "testMode")

            treeCount = getINI(sfile, "gameSettings", "treeCount")
            noMatchEarnings = getINI(sfile, "gameSettings", "noMatchEarnings")
        Catch ex As Exception
            appEventLog_Write("error loadParameters:", ex)
        End Try
    End Sub

    Public Sub writeSummaryData(ByVal sinstr As String, ByVal index As Integer)
        Try
            'write data to output file
            Dim outstr As String = ""

            summaryDf.WriteLine(outstr)
        Catch ex As Exception
            appEventLog_Write("error write summary data:", ex)
        End Try
    End Sub

    Public Sub takeUpdateInstructionDisplay(ByVal sinstr As String, ByVal index As Integer)
        Try
            With frmServer
                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0

                Dim tempPage As Integer = msgtokens(nextToken)
                nextToken += 1

                Dim tempLastPage As Integer = msgtokens(nextToken)
                nextToken += 1

                Dim tempTime As Double = msgtokens(nextToken)
                nextToken += 1

                .DataGridView1.Rows(index - 1).Cells(2).Value = "Page " & tempPage

                playerList(index).instructionLength(tempLastPage) += tempTime
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Function pointFromString(str As String) As Point
        Try

            Dim msgtokens() As String = str.Split({"{", "X", "=", ",", "Y", "}"}, StringSplitOptions.RemoveEmptyEntries)

            Return New Point(msgtokens(0), msgtokens(1))
        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return New Point(0, 0)
        End Try
    End Function

    Public Sub takeChoice(ByVal sinstr As String, ByVal index As Integer)
        Try
            With frmServer
                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0

                Dim tempP1 As Integer
                Dim tempP2 As Integer
                Dim tempDecisionType As String = ""
                Dim tempDecisionInfo As String = ""

                If playerList(index).myType(currentPeriod) = 1 Then
                    tempP1 = index
                    tempP2 = playerList(index).partnerList(currentPeriod)
                Else
                    tempP1 = playerList(index).partnerList(currentPeriod)
                    tempP2 = index
                End If

                Dim tempChoice As String = msgtokens(nextToken)
                nextToken += 1

                Dim tempDecisionLength As Double = msgtokens(nextToken)
                nextToken += 1

                Dim tempTreeIndex As Integer = msgtokens(nextToken)
                nextToken += 1

                'tempTreeIndex = playerList(tempP1).treeChoice(currentPeriod)

                Dim tempNode As node = playerList(tempP1).nodeList(playerList(tempP1).currentNode, currentPeriod, currentSubPeriod)

                tempNode.status = tempChoice

                If InStr(tempChoice, "pay") Then
                    checkin += 1

                    Dim tempPayoff1 As Double
                    Dim tempPayoff2 As Double

                    If tempChoice = "pay1" Then

                        tempPayoff1 = tempNode.payoff11
                        tempPayoff2 = tempNode.payoff12

                        periods(currentPeriod).nodeList(tempNode.id, tempTreeIndex, currentSubPeriod).countRight += 1
                    ElseIf tempChoice = "pay2" Then

                        tempPayoff1 = tempNode.payoff21
                        tempPayoff2 = tempNode.payoff22

                        periods(currentPeriod).nodeList(tempNode.id, tempTreeIndex, currentSubPeriod).countDown += 1
                    ElseIf tempChoice = "pay3" Then

                        tempPayoff1 = tempNode.payoff31
                        tempPayoff2 = tempNode.payoff32

                        periods(currentPeriod).nodeList(tempNode.id, tempTreeIndex, currentSubPeriod).countLeft += 1
                    End If

                    playerList(tempP1).periodEarnings = tempPayoff1
                    playerList(tempP2).periodEarnings = tempPayoff2

                    playerList(tempP1).finalNode = playerList(tempP1).currentNode
                    playerList(tempP2).finalNode = playerList(tempP1).currentNode

                    tempDecisionType = "PayOff"
                    tempDecisionInfo = tempPayoff1 & "\" & tempPayoff2

                    playerList(tempP1).currentNode = 0

                    If tempNode.subNode1Id > 0 Then
                        playerList(tempP1).killPath(playerList(tempP1).nodeList(tempNode.subNode1Id,
                                                                                currentPeriod,
                                                                                currentSubPeriod))
                    End If

                    If tempNode.subNode2Id > 0 Then
                        playerList(tempP1).killPath(playerList(tempP1).nodeList(tempNode.subNode2Id,
                                                                                currentPeriod,
                                                                                currentSubPeriod))
                    End If

                    If tempNode.subNode3Id > 0 Then
                        playerList(tempP1).killPath(playerList(tempP1).nodeList(tempNode.subNode3Id,
                                                                                currentPeriod,
                                                                                currentSubPeriod))
                    End If

                Else

                    If tempChoice = "sub1" Then
                        playerList(tempP1).currentNode = tempNode.subNode1Id

                        tempDecisionType = "Node"
                        tempDecisionInfo = tempNode.subNode1Id

                        periods(currentPeriod).nodeList(tempNode.subNode1Id, tempTreeIndex, currentSubPeriod).count += 1

                        If tempNode.subNode2Id > 0 Then
                            playerList(tempP1).killPath(playerList(tempP1).nodeList(tempNode.subNode2Id,
                                                                                    currentPeriod,
                                                                                    currentSubPeriod))
                        End If

                        If tempNode.subNode3Id > 0 Then
                            playerList(tempP1).killPath(playerList(tempP1).nodeList(tempNode.subNode3Id,
                                                                                    currentPeriod,
                                                                                    currentSubPeriod))
                        End If
                    ElseIf tempChoice = "sub2" Then
                        playerList(tempP1).currentNode = tempNode.subNode2Id

                        tempDecisionType = "Node"
                        tempDecisionInfo = tempNode.subNode2Id

                        periods(currentPeriod).nodeList(tempNode.subNode2Id, tempTreeIndex, currentSubPeriod).count += 1

                        If tempNode.subNode1Id > 0 Then
                            playerList(tempP1).killPath(playerList(tempP1).nodeList(tempNode.subNode1Id,
                                                                                    currentPeriod,
                                                                                    currentSubPeriod))
                        End If

                        If tempNode.subNode3Id > 0 Then
                            playerList(tempP1).killPath(playerList(tempP1).nodeList(tempNode.subNode3Id,
                                                                                    currentPeriod,
                                                                                    currentSubPeriod))
                        End If
                    ElseIf tempChoice = "sub3" Then
                        playerList(tempP1).currentNode = tempNode.subNode3Id

                        tempDecisionType = "Node"
                        tempDecisionInfo = tempNode.subNode3Id

                        periods(currentPeriod).nodeList(tempNode.subNode3Id, tempTreeIndex, currentSubPeriod).count += 1

                        If tempNode.subNode1Id > 0 Then
                            playerList(tempP1).killPath(playerList(tempP1).nodeList(tempNode.subNode1Id,
                                                                                    currentPeriod,
                                                                                    currentSubPeriod))
                        End If

                        If tempNode.subNode2Id > 0 Then
                            playerList(tempP1).killPath(playerList(tempP1).nodeList(tempNode.subNode2Id,
                                                                                    currentPeriod,
                                                                                    currentSubPeriod))
                        End If
                    End If
                End If


                'If tempChoice = "right" Then

                '    If tempNode.subNodeCount >= 1 Then
                '        playerList(tempP1).killPath(playerList(tempP1).nodeList(tempNode.subNode1Id, currentPeriod))
                '    End If

                '    If tempNode.payoffCount >= 1 Then
                '        checkin += 1

                '        playerList(tempP1).periodEarnings = tempNode.payoff11
                '        playerList(tempP2).periodEarnings = tempNode.payoff12

                '        playerList(tempP1).finalNode = playerList(tempP1).currentNode
                '        playerList(tempP2).finalNode = playerList(tempP1).currentNode

                '        tempDecisionType = "PayOff"
                '        tempDecisionInfo = tempNode.payoff11 & "\" & tempNode.payoff12

                '        playerList(tempP1).currentNode = 0

                '        .nodeList(tempNode.id, currentPeriod).countRight += 1
                '    Else
                '        playerList(tempP1).currentNode = tempNode.subNode2Id

                '        tempDecisionType = "Node"
                '        tempDecisionInfo = tempNode.subNode2Id

                '        .nodeList(tempNode.subNode2Id, currentPeriod).count += 1
                '    End If
                'Else
                '    If tempNode.subNodeCount = 2 Then
                '        playerList(tempP1).killPath(playerList(tempP1).nodeList(tempNode.subNode2Id, currentPeriod))
                '    End If

                '    If tempNode.payoffCount = 2 Then
                '        checkin += 1

                '        playerList(tempP1).periodEarnings = tempNode.payoff21
                '        playerList(tempP2).periodEarnings = tempNode.payoff22

                '        playerList(tempP1).finalNode = playerList(tempP1).currentNode
                '        playerList(tempP2).finalNode = playerList(tempP1).currentNode

                '        tempDecisionType = "PayOff"
                '        tempDecisionInfo = tempNode.payoff21 & "\" & tempNode.payoff22

                '        playerList(tempP1).currentNode = 0

                '        .nodeList(tempNode.id, currentPeriod).countDown += 1
                '    Else
                '        playerList(tempP1).currentNode = tempNode.subNode1Id

                '        tempDecisionType = "Node"
                '        tempDecisionInfo = tempNode.subNode1Id

                '        .nodeList(tempNode.subNode1Id, currentPeriod).count += 1
                '    End If

                '    playerList(index).downCount(currentPeriod) += 1
                'End If

                'write data, "Period,Player,Partner,PlayerType,DecisionType,DecisionLength,DecisionDirection,DecisionInfo,DecisionNode,PeriodTime"
                Dim outstr As String = ""

                outstr = currentPeriod & ","
                outstr &= currentSubPeriod & ","
                outstr &= index & ","
                outstr &= playerList(index).partnerList(currentPeriod) & ","
                outstr &= periods(currentPeriod).trees(playerList(index).treeChoice(currentPeriod)) & ","
                outstr &= playerList(index).myType(currentPeriod) & ","
                outstr &= tempDecisionType & ","
                outstr &= tempDecisionLength & ","
                outstr &= tempChoice & ","
                outstr &= tempDecisionInfo & ","
                outstr &= tempNode.id & ","

                Dim ts As TimeSpan
                ts = Now - periodStart

                outstr &= ts.TotalMilliseconds & ","

                summaryDf.WriteLine(outstr)

                'send results
                If checkin = getNumberOfActivePlayers() / 2 Then
                    checkin = 0

                    For i As Integer = 1 To numberOfPlayers
                        playerList(i).sendPeriodResults()
                    Next
                Else
                    playerList(tempP1).sendChoice()
                    playerList(tempP2).sendChoice()
                End If

            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub takeFinishedReviewingResults(ByVal sinstr As String, ByVal index As Integer)
        Try
            With frmServer
                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0

                checkin += 1

                .DataGridView1.Rows(index - 1).Cells(2).Value = "Waiting"

                If checkin = getNumberOfActivePlayers() Then
                    checkin = 0

                    If currentPeriod = numberOfPeriods Then
                        For i As Integer = 1 To numberOfPlayers
                            playerList(i).endGame()
                        Next
                    Else

                        If rand(100, 1) <= periods(currentPeriod).endingProbabilty Then
                            currentPeriod += 1
                            currentSubPeriod = 1
                        Else
                            currentSubPeriod += 1
                        End If

                        'store number of sub periods by period
                        subPeriodCount(currentPeriod) = currentSubPeriod

                        .txtPeriod.Text = "P" & currentPeriod & " - " & currentSubPeriod

                        Dim tempShowInstructions As String = getINI(sfile, "gameSettings", "showInstructions")

                        If periods(currentPeriod).instructions <> "None" And currentSubPeriod = 1 And tempShowInstructions Then
                            For i As Integer = 1 To numberOfPlayers
                                playerList(i).sendShowInstructions()
                            Next
                        Else
                            startNextPeriod()
                        End If

                    End If
                End If

            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub startNextPeriod()
        Try
            With frmServer
                If periods(currentPeriod).pairingRule = "Choice" Then
                    'start choice period
                Else 'fixed

                    'asign trees if first sub period
                    If currentSubPeriod = 1 And currentPeriod > 1 Then

                        For i As Integer = 1 To numberOfPlayers
                            playerList(i).treeChoice(currentPeriod) = playerList(i).treeChoice(currentPeriod - 1)
                        Next

                        'If periods(currentPeriod).trees(1) = "-1" Then copyPreviousTreeForward()

                        'Else
                        '    For i As Integer = 1 To numberOfPlayers
                        '        playerList(i).treeChoice(currentPeriod) = periods(currentPeriod).trees(1)
                        '    Next
                        'End If

                        'asign pairs
                        If periods(currentPeriod).pairingRule = "Fixed" Then
                            For i As Integer = 1 To numberOfPlayers
                                playerList(i).partnerList(currentPeriod) = playerList(i).partnerList(currentPeriod - 1)
                                playerList(i).myType(currentPeriod) = playerList(i).myType(currentPeriod - 1)
                            Next
                        Else 'random
                            randomizePartners()
                        End If
                    ElseIf currentSubPeriod > 1 Then
                        'copyPreviousTreeForward()

                        periods(currentPeriod).setupNodes(1)
                        periods(currentPeriod).setupNodes(2)

                    End If
                End If

                For i As Integer = 1 To numberOfPlayers
                    playerList(i).sendStartNextPeriod()
                Next

                periodStart = Now
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    'Public Sub copyPreviousTreeForward()
    '    Try
    '        'copy previous trees forward

    '    Catch ex As Exception
    '        appEventLog_Write("error :", ex)
    '    End Try
    'End Sub

    Public Function setupInstructionNodes() As Boolean
        Try
            nodeCountInstructions(1) = 1
            nodeCountInstructions(2) = 2
            nodeCountInstructions(3) = 3

            For i As Integer = 1 To 3
                For j As Integer = 1 To nodeCountInstructions(i)
                    nodeListInstructions(j, i) = New node

                    nodeListInstructions(j, i).id = j
                    nodeListInstructions(j, i).myTree = i

                    nodeListInstructions(j, i).status = "open"

                    nodeListInstructions(j, i).owner = getINI(sfile, "nodeI" & i & "-" & j, "owner")

                    nodeListInstructions(j, i).payoff11 = getINI(sfile, "nodeI" & i & "-" & j, "payoff11")
                    nodeListInstructions(j, i).payoff12 = getINI(sfile, "nodeI" & i & "-" & j, "payoff12")
                    nodeListInstructions(j, i).payoff21 = getINI(sfile, "nodeI" & i & "-" & j, "payoff21")
                    nodeListInstructions(j, i).payoff22 = getINI(sfile, "nodeI" & i & "-" & j, "payoff22")
                    nodeListInstructions(j, i).payoff31 = getINI(sfile, "nodeI" & i & "-" & j, "payoff31")
                    nodeListInstructions(j, i).payoff32 = getINI(sfile, "nodeI" & i & "-" & j, "payoff32")

                    nodeListInstructions(j, i).pt1 = pointFromString(getINI(sfile, "nodeI" & i & "-" & j, "pt1"))
                    nodeListInstructions(j, i).pt2 = pointFromString(getINI(sfile, "nodeI" & i & "-" & j, "pt2"))
                    nodeListInstructions(j, i).pt3 = pointFromString(getINI(sfile, "nodeI" & i & "-" & j, "pt3"))
                    nodeListInstructions(j, i).pt4 = pointFromString(getINI(sfile, "nodeI" & i & "-" & j, "pt4"))

                    nodeListInstructions(j, i).subNode1Id = getINI(sfile, "nodeI" & i & "-" & j, "subNode1Id")
                    nodeListInstructions(j, i).subNode2Id = getINI(sfile, "nodeI" & i & "-" & j, "subNode2Id")
                    nodeListInstructions(j, i).subNode3Id = getINI(sfile, "nodeI" & i & "-" & j, "subNode3Id")
                Next
            Next

            Return True
        Catch ex As Exception
            appEventLog_Write("error :", ex)

            Return False
        End Try
    End Function

    Public Function checkValidText(ByVal sinstr As String) As Boolean
        Try
            If InStr(sinstr, "|") > 0 Then
                MsgBox("Please do not use the ""|"" character.", MsgBoxStyle.Critical)
                sinstr = ""
                Return False
            End If

            If InStr(sinstr, "#") > 0 Then
                MsgBox("Please do not use the ""#"" character.", MsgBoxStyle.Critical)
                sinstr = ""
                Return False
            End If

            If InStr(sinstr, ";") > 0 Then
                MsgBox("Please do not use the "";"" character.", MsgBoxStyle.Critical)
                sinstr = ""
                Return False
            End If

            Return True
        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return False
        End Try
    End Function

    Public Sub takeTreeChoice(ByVal sinstr As String, ByVal index As Integer)
        Try
            With frmServer
                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0

                playerList(index).treeChoice(currentPeriod) = msgtokens(nextToken)
                nextToken += 1

                .DataGridView1.Rows(index - 1).Cells(2).Value = "Waiting"

                Dim ready As Boolean = True

                For i As Integer = 1 To numberOfPlayers
                    If Not playerList(i).outOfGame And playerList(i).treeChoice(currentPeriod) = -1 Then
                        ready = False
                        Exit For
                    End If
                Next

                If ready Then
                    startChoicePeriod()
                End If
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub startChoicePeriod()
        Try
            With frmServer

                Dim earlyOut As Boolean = False

                Dim randomOrder(numberOfPlayers) As Integer

                'randomize order of players
                For i As Integer = 1 To numberOfPlayers
                    randomOrder(i) = -1
                Next

                For i As Integer = 1 To numberOfPlayers
                    Dim go As Boolean = True

                    While go
                        Dim r As Integer = rand(numberOfPlayers, 1)
                        If randomOrder(r) = -1 Then
                            go = False
                            randomOrder(r) = i
                        End If
                    End While
                Next

                'match randomzied list
                For i As Integer = 1 To numberOfPlayers

                    Dim p As Integer = randomOrder(i)

                    If Not playerList(p).findPartner Then
                        playerList(p).sendEndGameNoMatch()
                        .DataGridView1.Rows(p - 1).Cells(2).Value = "No match, input name."
                        earlyOut = True
                    End If
                Next

                'payout players that have no pair
                If earlyOut Then

                    For i As Integer = 1 To numberOfPlayers
                        playerList(i).sendBlankScreen()
                    Next

                    .TabControl1.SelectedIndex = 0

                    frmContinue.Show()
                Else

                    For i As Integer = 1 To numberOfPlayers
                        playerList(i).sendFinishChoicePeriod()
                    Next
                End If

                periodStart = Now
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub setupNode(ByRef n As node, id As Integer, tree As Integer)
        Try
            n = New node

            n.id = id
            n.myTree = tree

            n.status = "open"

            n.owner = getINI(sfile, "node" & tree & "-" & id, "owner")
            n.payoff11 = getINI(sfile, "node" & tree & "-" & id, "payoff11")
            n.payoff12 = getINI(sfile, "node" & tree & "-" & id, "payoff12")
            n.payoff21 = getINI(sfile, "node" & tree & "-" & id, "payoff21")
            n.payoff22 = getINI(sfile, "node" & tree & "-" & id, "payoff22")
            n.payoff31 = getINI(sfile, "node" & tree & "-" & id, "payoff31")
            n.payoff32 = getINI(sfile, "node" & tree & "-" & id, "payoff32")

            n.pt1 = pointFromString(getINI(sfile, "node" & tree & "-" & id, "pt1"))
            n.pt2 = pointFromString(getINI(sfile, "node" & tree & "-" & id, "pt2"))
            n.pt3 = pointFromString(getINI(sfile, "node" & tree & "-" & id, "pt3"))
            n.pt4 = pointFromString(getINI(sfile, "node" & tree & "-" & id, "pt4"))

            n.subNode1Id = getINI(sfile, "node" & tree & "-" & id, "subNode1Id")
            n.subNode2Id = getINI(sfile, "node" & tree & "-" & id, "subNode2Id")
            n.subNode3Id = getINI(sfile, "node" & tree & "-" & id, "subNode3Id")

            n.sortValue = getINI(sfile, "node" & tree & "-" & id, "sortValue")
            n.sortValue1 = getINI(sfile, "node" & tree & "-" & id, "sortValue1")
            n.sortValue2 = getINI(sfile, "node" & tree & "-" & id, "sortValue2")
            n.sortValue3 = getINI(sfile, "node" & tree & "-" & id, "sortValue3")
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub setupNode2(ByRef localNode As node, ByVal remoteNode As node)
        Try
            localNode = New node

            localNode.id = remoteNode.id
            localNode.myTree = remoteNode.myTree

            localNode.status = "open"

            localNode.owner = remoteNode.owner
            localNode.payoff11 = remoteNode.payoff11
            localNode.payoff12 = remoteNode.payoff12
            localNode.payoff21 = remoteNode.payoff21
            localNode.payoff22 = remoteNode.payoff22
            localNode.payoff31 = remoteNode.payoff31
            localNode.payoff32 = remoteNode.payoff32

            localNode.pt1 = remoteNode.pt1
            localNode.pt2 = remoteNode.pt2
            localNode.pt3 = remoteNode.pt3
            localNode.pt4 = remoteNode.pt4

            localNode.subNode1Id = remoteNode.subNode1Id
            localNode.subNode2Id = remoteNode.subNode2Id
            localNode.subNode3Id = remoteNode.subNode3Id

            localNode.sortValue = remoteNode.sortValue
            localNode.sortValue1 = remoteNode.sortValue1
            localNode.sortValue2 = remoteNode.sortValue2
            localNode.sortValue3 = remoteNode.sortValue3


        Catch ex As Exception
            appEventLog_Write("error: ", ex)
        End Try
    End Sub

    Public Sub randomizePartners()
        Try
            For i As Integer = 1 To numberOfPlayers
                playerList(i).partnerList(currentPeriod) = -1
                playerList(i).myType(currentPeriod) = -1
            Next

            For i As Integer = 1 To numberOfPlayers
                If playerList(i).partnerList(currentPeriod) = -1 And Not playerList(i).outOfGame Then

                    Dim go As Boolean = False

                    For j As Integer = 1 To numberOfPlayers
                        If j <> i And playerList(j).partnerList(currentPeriod) = -1 And Not playerList(j).outOfGame Then
                            go = True
                            Exit For
                        End If
                    Next

                    If go Then

                        While go
                            Dim r As Integer = rand(numberOfPlayers, 1)

                            If r <> i And playerList(r).partnerList(currentPeriod) = -1 And Not playerList(r).outOfGame Then
                                playerList(i).partnerList(currentPeriod) = r
                                playerList(r).partnerList(currentPeriod) = i

                                If rand(2, 1) = 1 Then
                                    playerList(i).myType(currentPeriod) = 1
                                    playerList(r).myType(currentPeriod) = 2
                                Else
                                    playerList(i).myType(currentPeriod) = 2
                                    playerList(r).myType(currentPeriod) = 1
                                End If

                                go = False
                            End If

                        End While

                    End If
                End If
            Next
        Catch ex As Exception
            appEventLog_Write("error: ", ex)
        End Try
    End Sub

    Public Sub takeRemoteComputerName(str As String, index As Integer)
        Try
            With frmServer
                Dim msgtokens() As String = str.Split(";")
                Dim nextToken As Integer = 0

                playerList(index).computerName = msgtokens(nextToken)
                nextToken += 1

                .DataGridView1.Rows(index - 1).Cells(1).Value = playerList(index).computerName
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub
End Module
