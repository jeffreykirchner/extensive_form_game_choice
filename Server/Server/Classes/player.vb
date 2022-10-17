Imports System.Drawing.Drawing2D

Public Class player
    Public inumber As Integer            'ID number
    Public sname As String               'name of person
    Public sid As String                 'student id number of person
    Public socketNumber As String        'winsock ID number
    Public relativeNumber As Integer     'either buyer or seller number
    Public earnings As Double            'experimental earnings
    Public ipAddress As String           'IP address of player's machine 
    Public myIPAddress As String         'IP address of player's machine 
    Public computerName As String        'computer name of player's machine 
    Public roundEarnings As Integer      'earnings for a induvidual round/period
    Public exchangeRate As Integer       'conversion rate from experimental dollars to $
    Public colorName As String           'colorName of player

    Public nodeList(100, 100, 1000) As node    'ID/Period/sub period
    Public nodeCount(100) As Integer

    Public partnerList(100) As Integer
    Public myType(100) As Integer        'period  
    Public currentNode As Integer
    Public finalNode As Integer

    Public periodEarnings As Double
    Public instructionLength(100) As Double

    Public lastIDSent As String
    Public lastMessageSent As String

    Public treeChoice(100) As Integer       'choice the player makes between two trees (1 or 2)
    Public outOfGame As Boolean


    Public Sub player()

    End Sub

    Public Sub sendBegin()
        Try
            With frmServer

                outOfGame = False

                If periods(1).pairingRule = "Choice" Then

                    For i As Integer = 1 To numberOfPeriods
                        treeChoice(i) = -1
                        partnerList(i) = -1
                    Next

                    .DataGridView1.Rows(inumber - 1).Cells(4).Value = "-"
                    .DataGridView1.Rows(inumber - 1).Cells(5).Value = "-"
                Else

                    .DataGridView1.Rows(inumber - 1).Cells(4).Value = partnerList(1)
                    .DataGridView1.Rows(inumber - 1).Cells(5).Value = periods(currentPeriod).trees(treeChoice(1))
                End If

                'If inumber <= numberOfPlayers / 2 Then
                '    myType(currentPeriod) = 1
                'Else
                '    myType(currentPeriod) = 2
                'End If

                For i As Integer = 1 To instructionCount
                    instructionLength(i) = 0
                Next

                'singal to clients to start the experiment

                currentNode = 1

                'winsock can send character strings to the clients
                Dim outstr As String = ""

                'create parseable string to send to clients by putting ";" between each value
                outstr = numberOfPeriods & ";"
                outstr &= numberOfPlayers & ";"
                outstr &= showInstructions & ";"

                outstr &= instructionX & ";"
                outstr &= instructionY & ";"
                outstr &= windowX & ";"
                outstr &= windowY & ";"

                outstr &= myType(currentPeriod) & ";"
                outstr &= testMode & ";"

                outstr &= treeCount & ";"
                outstr &= noMatchEarnings & ";"

                For i As Integer = 1 To numberOfPeriods
                    outstr &= periods(i).setupString
                Next

                For i As Integer = 1 To treeCount
                    outstr &= modMain.nodeCount(i) & ";"

                    For j As Integer = 1 To modMain.nodeCount(i)

                        outstr &= modMain.nodeList(j, i).id & ";"
                        outstr &= modMain.nodeList(j, i).myTree & ";"

                        outstr &= modMain.nodeList(j, i).status & ";"

                        outstr &= modMain.nodeList(j, i).owner & ";"
                        outstr &= modMain.nodeList(j, i).payoff11 & ";"
                        outstr &= modMain.nodeList(j, i).payoff12 & ";"
                        outstr &= modMain.nodeList(j, i).payoff21 & ";"
                        outstr &= modMain.nodeList(j, i).payoff22 & ";"
                        outstr &= modMain.nodeList(j, i).payoff31 & ";"
                        outstr &= modMain.nodeList(j, i).payoff32 & ";"

                        outstr &= modMain.nodeList(j, i).pt1.ToString & ";"
                        outstr &= modMain.nodeList(j, i).pt2.ToString & ";"
                        outstr &= modMain.nodeList(j, i).pt3.ToString & ";"
                        outstr &= modMain.nodeList(j, i).pt4.ToString & ";"

                        outstr &= modMain.nodeList(j, i).subNode1Id & ";"
                        outstr &= modMain.nodeList(j, i).subNode2Id & ";"
                        outstr &= modMain.nodeList(j, i).subNode3Id & ";"
                    Next
                Next

                For i As Integer = 1 To 3
                    outstr &= nodeCountInstructions(i) & ";"

                    For j As Integer = 1 To nodeCountInstructions(i)

                        outstr &= nodeListInstructions(j, i).id & ";"
                        outstr &= nodeListInstructions(j, i).myTree & ";"

                        outstr &= nodeListInstructions(j, i).status & ";"

                        outstr &= nodeListInstructions(j, i).owner & ";"
                        outstr &= nodeListInstructions(j, i).payoff11 & ";"
                        outstr &= nodeListInstructions(j, i).payoff12 & ";"
                        outstr &= nodeListInstructions(j, i).payoff21 & ";"
                        outstr &= nodeListInstructions(j, i).payoff22 & ";"
                        outstr &= nodeListInstructions(j, i).payoff31 & ";"
                        outstr &= nodeListInstructions(j, i).payoff32 & ";"

                        outstr &= nodeListInstructions(j, i).pt1.ToString & ";"
                        outstr &= nodeListInstructions(j, i).pt2.ToString & ";"
                        outstr &= nodeListInstructions(j, i).pt3.ToString & ";"
                        outstr &= nodeListInstructions(j, i).pt4.ToString & ";"

                        outstr &= nodeListInstructions(j, i).subNode1Id & ";"
                        outstr &= nodeListInstructions(j, i).subNode2Id & ";"
                        outstr &= nodeListInstructions(j, i).subNode3Id & ";"
                    Next
                Next

                'call the send command (message ID found in takeMessage function,winsock ID,data) 
                sendMessageToClient("02", outstr)
            End With

        Catch ex As Exception
            appEventLog_Write("error player begin:", ex)
        End Try
    End Sub

    Public Sub sendMessageToClient(messageId As String, messageString As String)
        With frmServer
            lastMessageSent = messageString
            lastIDSent = messageId

            .wsk_Col.Send(messageId, socketNumber, messageString)
        End With
    End Sub

    Public Sub resendLastMessage()
        Try
            With frmServer
                sendMessageToClient(lastIDSent, lastMessageSent)
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub setupNodes()
        Try

            Dim tempTree As Integer = periods(currentPeriod).trees(treeChoice(currentPeriod))

            nodeCount(currentPeriod) = modMain.nodeCount(tempTree)

            For j As Integer = 1 To nodeCount(currentPeriod)
                setupNode2(nodeList(j, currentPeriod, currentSubPeriod), modMain.nodeList(j, tempTree))
            Next

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub


    Public Sub resetClient()
        Try
            'kill client
            With frmServer
                '.wsk_Col.Send("01", socketNumber, "")
                sendMessageToClient("01", "")
            End With
        Catch ex As Exception
            appEventLog_Write("error resetClient:", ex)
        End Try
    End Sub

    Public Sub requsetIP(ByVal count As Integer)
        Try
            'request the client send it's IP address
            With frmServer
                '.wsk_Col.Send("05", socketNumber, CStr(count))
                sendMessageToClient("05", CStr(count))
            End With
        Catch ex As Exception
            appEventLog_Write("error requsetIP:", ex)
        End Try
    End Sub

    Public Sub endGame()
        Try
            If outOfGame Then Exit Sub

            'tell clients to end the game
            With frmServer
                Dim outstr As String = ""

                ' .wsk_Col.Send("06", socketNumber, outstr)
                sendMessageToClient("06", outstr)
            End With
        Catch ex As Exception
            appEventLog_Write("error endGame:", ex)
        End Try
    End Sub

    Public Sub takeName(ByVal sname As String, ByVal sid As String)
        Try
            'get the subject's name

            With frmServer
                Me.sname = sname
                Me.sid = sid
                .DataGridView1.Rows(inumber - 1).Cells(1).Value = sname
            End With
        Catch ex As Exception
            appEventLog_Write("error takeName:", ex)
        End Try
    End Sub

    Public Sub endEarly()
        Try
            'end experiment early

            With frmServer
                Dim outstr As String

                outstr = numberOfPeriods & ";"
                '.wsk_Col.Send("12", socketNumber, outstr)

                sendMessageToClient("12", outstr)
            End With
        Catch ex As Exception
            appEventLog_Write("error endEarly:", ex)
        End Try
    End Sub

    Public Sub finishedInstructions()
        Try
            With frmServer

                ' .wsk_Col.Send("04", socketNumber, "")
                sendMessageToClient("04", "")
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub sendChoice()
        Try
            With frmServer
                If outOfGame Then Exit Sub

                Dim outstr As String = ""

                If myType(currentPeriod) = 1 Then

                    outstr = currentNode & ";"
                    outstr &= returnTreeStatus()

                    If currentNode = 0 Then
                        .DataGridView1.Rows(inumber - 1).Cells(2).Value = "Waiting"
                    Else
                        If nodeList(currentNode, currentPeriod, currentSubPeriod).owner = myType(currentPeriod) Then
                            .DataGridView1.Rows(inumber - 1).Cells(2).Value = "Playing"
                        Else
                            .DataGridView1.Rows(inumber - 1).Cells(2).Value = "Waiting"
                        End If
                    End If
                Else

                    outstr = playerList(partnerList(currentPeriod)).currentNode & ";"
                    outstr &= playerList(partnerList(currentPeriod)).returnTreeStatus()

                    If playerList(partnerList(currentPeriod)).currentNode = 0 Then
                        .DataGridView1.Rows(inumber - 1).Cells(2).Value = "Waiting"
                    Else
                        If playerList(partnerList(currentPeriod)).nodeList(playerList(partnerList(currentPeriod)).currentNode, currentPeriod, currentSubPeriod).owner = myType(currentPeriod) Then
                            .DataGridView1.Rows(inumber - 1).Cells(2).Value = "Playing"
                        Else
                            .DataGridView1.Rows(inumber - 1).Cells(2).Value = "Waiting"
                        End If
                    End If
                End If

                '.wsk_Col.Send("07", socketNumber, outstr)
                sendMessageToClient("07", outstr)
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub sendPeriodResults()
        Try
            With frmServer
                Dim outstr As String = ""

                If Not outOfGame Then
                    .DataGridView1.Rows(inumber - 1).Cells(2).Value = "Reviewing Results"

                    'update this earnings type by period

                    If periods(currentPeriod).payoffMode = "Dollars" Then
                        earnings += periodEarnings
                    Else
                        earnings += periodEarnings / 100
                    End If

                    .DataGridView1.Rows(inumber - 1).Cells(3).Value = FormatCurrency(earnings)

                    outstr = periodEarnings & ";"
                    outstr &= earnings & ";"


                    If myType(currentPeriod) = 1 Then
                        outstr &= currentNode & ";"
                        outstr &= returnTreeStatus()
                    Else
                        outstr &= playerList(partnerList(currentPeriod)).currentNode & ";"
                        outstr &= playerList(partnerList(currentPeriod)).returnTreeStatus()
                    End If

                    '.wsk_Col.Send("08", socketNumber, outstr)

                    sendMessageToClient("08", outstr)
                End If

                'write summary data
                '"Period,Player,Partner,FinalNode,FinalDirection,MyPayoff,PartnerPayoff,MyType,MadeFinalDecision,"

                If Not outOfGame Then
                    outstr = currentPeriod & ","
                    outstr &= currentSubPeriod & ","
                    outstr &= inumber & ","
                    outstr &= partnerList(currentPeriod) & ","
                    outstr &= periods(currentPeriod).trees(treeChoice(currentPeriod)) & ","

                    If myType(currentPeriod) = 1 Then
                        outstr &= finalNode & ","
                        outstr &= nodeList(finalNode, currentPeriod, currentSubPeriod).status & ","
                    Else
                        outstr &= finalNode & ","
                        outstr &= playerList(partnerList(currentPeriod)).nodeList(finalNode, currentPeriod, currentSubPeriod).status & ","
                    End If

                    outstr &= periodEarnings & ","
                    outstr &= playerList(partnerList(currentPeriod)).periodEarnings & ","
                    outstr &= myType(currentPeriod) & ","

                    If myType(currentPeriod) = 1 Then
                        If nodeList(finalNode, currentPeriod, currentSubPeriod).owner = myType(currentPeriod) Then
                            outstr &= "True,"
                        Else
                            outstr &= "False,"
                        End If
                    Else
                        If playerList(partnerList(currentPeriod)).nodeList(finalNode, currentPeriod, currentSubPeriod).owner = myType(currentPeriod) Then
                            outstr &= "True,"
                        Else
                            outstr &= "False,"
                        End If
                    End If

                    playerDf.WriteLine(outstr)
                ElseIf periods(currentPeriod).pairingRule = "Choice" Then
                    outstr = currentPeriod & ","
                    outstr &= currentSubPeriod & ","
                    outstr &= inumber & ","
                    outstr &= "Out" & ","
                    outstr &= periods(currentPeriod).trees(treeChoice(currentPeriod)) & ","
                    outstr &= ","
                    outstr &= ","

                    outstr &= periodEarnings & ","
                    outstr &= ","
                    outstr &= ","
                    outstr &= ","

                    playerDf.WriteLine(outstr)
                End If
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub sendStartNextPeriod()
        Try
            With frmServer

                If outOfGame Then
                    .DataGridView1.Rows(inumber - 1).Cells(4).Value = "Out"
                    .DataGridView1.Rows(inumber - 1).Cells(5).Value = "Out"
                    Exit Sub
                End If

                'display count
                If myType(currentPeriod) = 1 Then
                    periods(currentPeriod).nodeList(1, treeChoice(currentPeriod), currentSubPeriod).count += 1
                End If

                .DataGridView1.Rows(inumber - 1).Cells(2).Value = "Playing"

                If periods(currentPeriod).pairingRule = "Choice" Then
                    .DataGridView1.Rows(inumber - 1).Cells(4).Value = "-"
                    .DataGridView1.Rows(inumber - 1).Cells(5).Value = "-"
                Else
                    setupNodes()

                    .DataGridView1.Rows(inumber - 1).Cells(4).Value = partnerList(currentPeriod)
                    .DataGridView1.Rows(inumber - 1).Cells(5).Value = periods(currentPeriod).trees(treeChoice(currentPeriod))
                End If


                currentNode = 1

                Dim outstr As String = ""

                outstr &= currentPeriod & ";"
                outstr &= treeChoice(currentPeriod) & ";"
                outstr &= myType(currentPeriod) & ";"

                '.wsk_Col.Send("09", socketNumber, outstr)
                sendMessageToClient("09", outstr)
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub sendFinishChoicePeriod()
        Try
            With frmServer

                If outOfGame Then Exit Sub

                Dim outstr As String = ""

                'setup nodes for tree
                setupNodes()

                'display count
                If myType(currentPeriod) = 1 Then
                    periods(currentPeriod).nodeList(1, treeChoice(currentPeriod), 1).count += 1
                End If

                outstr = treeChoice(currentPeriod) & ";"
                outstr += myType(currentPeriod) & ";"

                .DataGridView1.Rows(inumber - 1).Cells(2).Value = "Playing"
                .DataGridView1.Rows(inumber - 1).Cells(4).Value = partnerList(currentPeriod)
                .DataGridView1.Rows(inumber - 1).Cells(5).Value = periods(currentPeriod).trees(treeChoice(currentPeriod))

                sendMessageToClient("10", outstr)
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub sendShowInstructions()
        Try
            With frmServer

                If outOfGame Then Exit Sub

                Dim outstr As String = ""

                outstr &= currentPeriod & ";"

                sendMessageToClient("14", outstr)

                .DataGridView1.Rows(inumber - 1).Cells(2).Value = "Page 1"
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub sendEndGame()
        Try
            With frmServer

                Dim outstr As String = ""

                .DataGridView1.Rows(inumber - 1).Cells(2).Value = "Out"
                sendMessageToClient("11", outstr)
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub sendBlankScreen()
        Try
            With frmServer

                If outOfGame Then Exit Sub

                Dim outstr As String = ""

                sendMessageToClient("13", outstr)
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub killPath(ByRef tempN As node)
        Try
            tempN.status = "dead"

            If tempN.subNode1Id >= 1 Then
                killPath(nodeList(tempN.subNode1Id, currentPeriod, currentSubPeriod))
            End If

            If tempN.subNode2Id >= 1 Then
                killPath(nodeList(tempN.subNode2Id, currentPeriod, currentSubPeriod))
            End If

            If tempN.subNode3Id >= 1 Then
                killPath(nodeList(tempN.subNode3Id, currentPeriod, currentSubPeriod))
            End If
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Function returnTreeStatus() As String
        Try
            Dim outstr As String = ""

            For i As Integer = 1 To nodeCount(currentPeriod)
                outstr &= nodeList(i, currentPeriod, currentSubPeriod).status & ";"
            Next

            Return outstr
        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return ""
        End Try
    End Function

    Public Sub sendEndGameNoMatch()
        Try
            With frmServer

                earnings += noMatchEarnings
                periodEarnings = noMatchEarnings

                .DataGridView1.Rows(inumber - 1).Cells(3).Value = FormatCurrency(earnings)

                Dim outstr As String = ""

                outstr = earnings & ";"

                sendMessageToClient("15", outstr)
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Function findPartner() As Boolean
        Try

            'partner already made
            If partnerList(currentPeriod) <> -1 Then Return True

            'make sure partner can be found
            Dim partnerExists As Boolean = False

            For i As Integer = 1 To numberOfPlayers

                If playerList(i).checkValidPartner(inumber) Then
                    partnerExists = True
                    Exit For
                End If

            Next

            If Not partnerExists Then
                outOfGame = True
                Return False
            End If

            'randomly assign partner
            Dim go As Boolean = True
            Dim p As Integer = rand(numberOfPlayers, 1)

            Do
                If playerList(p).checkValidPartner(inumber) Then
                    go = False
                Else
                    p = rand(numberOfPlayers, 1)
                End If
            Loop While go = True

            playerList(p).partnerList(currentPeriod) = inumber
            partnerList(currentPeriod) = p

            If rand(2, 1) = 1 Then
                playerList(p).myType(currentPeriod) = 1
                myType(currentPeriod) = 2
            Else
                playerList(p).myType(currentPeriod) = 2
                myType(currentPeriod) = 1
            End If

            Return True
        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return False
        End Try
    End Function

    Public Function checkValidPartner(index As Integer) As Boolean
        Try
            If index = inumber Then Return False

            If Not outOfGame And
               partnerList(currentPeriod) = -1 And
               playerList(index).treeChoice(currentPeriod) = treeChoice(currentPeriod) Then

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return False
        End Try
    End Function
End Class
