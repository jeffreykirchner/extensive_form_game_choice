'Programmed by Jeffrey Kirchner and Your Name Here
'kirchner@chapman.edu/jkirchner@gmail.com
'Economic Science Institute, Chapman University 2008-2010 ©

Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Module modMain
    Public sfile As String

    Public inumber As Integer                  'client ID number
    Public numberOfPlayers As Integer          'number of total players in experiment
    Public currentPeriod As Integer            'current period of experiment
    Public myIPAddress As String               'IP address of client 
    Public myPortNumber As String              'port number of client      
    Public exchangeRate As Integer             'client's exchange rate
    Public currentInstruction As Integer       'current instruction
    Public numberOfPeriods As Integer          'number of periods  
    Public showInstructions As Boolean         'wether to show instructions to subject

    Public WithEvents wskClient As Winsock
    Public closing As Boolean = False

    Public instructionX As Integer            'start up locations of windows
    Public instructionY As Integer
    Public windowX As Integer
    Public windowY As Integer

    Public nodeList(100, 100) As node     'ID/Tree
    Public nodeCount(100) As Integer      'Tree   
    Public currentNode As Integer
    Public myType(100) As Integer         'period
    Public tickTock As Integer

    Public selectionPt As Point
    Public selection As String

    'Public payoffMode As String
    Public decisionStart As Date
    Public testMode As String

    Public noMatchEarnings As Integer             'earnings paid to user if a match cannot be found for them

    Public nodeListInstructions(3, 3) As node
    Public nodeCountInstructions(3) As Integer

    Public currentPeriodInstruction As Integer

    Public treeCount As Integer                   'number of stored trees

    Public periods(100) As period


#Region " Winsock Code "
    Private Sub wskClient_DataArrival(ByVal sender As Object, ByVal e As WinsockDataArrivalEventArgs) Handles wskClient.DataArrival
        Try
            Dim buf As String = Nothing
            CType(sender, Winsock).Get(buf)

            Dim msgtokens() As String = buf.Split("#")
            Dim i As Integer

            'appEventLog_Write("data arrival: " & buf)

            For i = 1 To msgtokens.Length - 1
                takeMessage(msgtokens(i - 1))
            Next

        Catch ex As Exception
            appEventLog_Write("error wskClient_DataArrival:", ex)
        End Try
    End Sub

    Private Sub wskClient_ErrorReceived(ByVal sender As System.Object, ByVal e As WinsockErrorEventArgs) Handles wskClient.ErrorReceived
        ' Log("Error: " & e.Message)
    End Sub

    Private Sub wskClient_StateChanged(ByVal sender As Object, ByVal e As WinsockStateChangingEventArgs) Handles wskClient.StateChanged
        Try
            'appEventLog_Write("state changed")

            If e.New_State = WinsockStates.Closed Then
                frmConnect.Show()
            End If
        Catch ex As Exception
            appEventLog_Write("error wskClient_StateChanged:", ex)
        End Try

    End Sub

    Public Sub connect()
        Try

            wskClient = New Winsock
            wskClient.BufferSize = 8192
            wskClient.LegacySupport = False
            wskClient.LocalPort = 8080
            wskClient.MaxPendingConnections = 1
            wskClient.Protocol = WinsockProtocols.Tcp
            wskClient.RemotePort = myPortNumber
            wskClient.RemoteServer = myIPAddress
            wskClient.SynchronizingObject = frmMain

            wskClient.Connect()
        Catch
            frmMain.Hide()
            frmConnect.Show()
        End Try
    End Sub

#End Region

#Region " General Functions "
    Public Sub main()
        AppEventLog_Init()
        appEventLog_Write("Begin")

        ToggleScreenSaverActive(False)

        Application.EnableVisualStyles()
        Application.Run(frmMain)

        ToggleScreenSaverActive(True)

        appEventLog_Write("End")
        AppEventLog_Close()
    End Sub

    Public Function timeConversion(ByVal sec As Integer) As String
        Try
            'appEventLog_Write("time conversion :" & sec)
            timeConversion = Format((sec \ 60), "00") & ":" & Format((sec Mod 60), "00")
        Catch ex As Exception
            appEventLog_Write("error timeConversion:", ex)
            timeConversion = ""
        End Try
    End Function

    Public Sub setID(ByVal sinstr As String)
        Try
            'appEventLog_Write("set id :" & sinstr)

            Dim msgtokens() As String

            msgtokens = sinstr.Split(";")

            inumber = msgtokens(0)

            appEventLog_Write("Client# = " & inumber)

        Catch ex As Exception
            appEventLog_Write("error setID:", ex)
        End Try
    End Sub


    Public Sub sendIPAddress(ByVal sinstr As String)
        Try
            'appEventLog_Write("send ip :" & sinstr)

            With frmMain
                'Dim outstr As String

                inumber = sinstr

                appEventLog_Write("Client# = " & inumber)

                'outstr = SystemInformation.ComputerName
                '.wskClient.Send("03", outstr)
            End With
        Catch ex As Exception
            appEventLog_Write("error sendIPAddress:", ex)
        End Try
    End Sub

    Public Function numberSuffix(ByVal sinstr As Integer) As String
        numberSuffix = sinstr
        Try
            Select Case sinstr
                Case 1
                    numberSuffix = sinstr & "st"
                Case 2
                    numberSuffix = sinstr & "nd"
                Case 3
                    numberSuffix = sinstr & "rd"
                Case Is >= 4
                    numberSuffix = sinstr & "th"
            End Select
        Catch ex As Exception
            appEventLog_Write("error numberSuffix:", ex)
        End Try
    End Function
#End Region

    Private Sub takeMessage(ByVal sinstr As String)
        Try
            'take message from server
            'msgtokens(0) has type of message sent, having different types of messages allows you to send different formats for different actions.
            'msgtokens(1) has the semicolon delimited data that is to be parsed and acted upon.  


            Dim msgtokens() As String
            msgtokens = sinstr.Split("|")

            Select Case msgtokens(0) 'case statement to handle each of the different types of messages
                Case "01"
                    'close client
                    closing = True

                    wskClient.Close()
                    frmMain.Close()

                Case "02"
                    takeBegin(msgtokens(1))
                Case "03"
                    setID(msgtokens(1))
                Case "04"
                    takeFinishedInstructions()
                Case "05"
                    sendIPAddress(msgtokens(1))
                Case "06"
                    takeEndGame(msgtokens(1))
                Case "07"
                    takeChoice(msgtokens(1))
                Case "08"
                    takePeriodResults(msgtokens(1))
                Case "09"
                    takeStartNextPeriod(msgtokens(1))
                Case "10"
                    takeFinishChoicePeriod(msgtokens(1))
                Case "11"
                    takeEndGame(msgtokens(1))
                Case "12"
                    takeEndEarly(msgtokens(1))
                Case "13"
                    takeBlankScreen(msgtokens(1))
                Case "14"
                    takeShowInstructions(msgtokens(1))
                Case "15"
                    takeEndGameNoMatch(msgtokens(1))
            End Select
        Catch ex As Exception
            appEventLog_Write("error takeMessage:", ex)
        End Try

    End Sub

    Public Sub takeBegin(ByVal sinstr As String)
        With frmMain
            Try
                'server has signaled client to start experiment

                currentPeriod = 1
                currentNode = 1

                'parse incoming data string
                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0

                numberOfPeriods = msgtokens(nextToken)
                nextToken += 1

                numberOfPlayers = msgtokens(nextToken)
                nextToken += 1

                showInstructions = msgtokens(nextToken)
                nextToken += 1

                instructionX = msgtokens(nextToken)
                nextToken += 1

                instructionY = msgtokens(nextToken)
                nextToken += 1

                windowX = msgtokens(nextToken)
                nextToken += 1

                windowY = msgtokens(nextToken)
                nextToken += 1

                myType(currentPeriod) = msgtokens(nextToken)
                nextToken += 1

                testMode = msgtokens(nextToken)
                nextToken += 1

                treeCount = msgtokens(nextToken)
                nextToken += 1

                noMatchEarnings = msgtokens(nextToken)
                nextToken += 1

                'periods
                For i As Integer = 1 To numberOfPeriods
                    periods(i) = New period
                    periods(i).fromString(msgtokens, nextToken)
                Next

                'experiment nodes
                For i As Integer = 1 To treeCount
                    nodeCount(i) = msgtokens(nextToken)
                    nextToken += 1

                    For j As Integer = 1 To nodeCount(i)
                        nodeList(j, i) = New node(nodeList)

                        nodeList(j, i).id = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).myTree = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).status = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).owner = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).payoff11 = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).payoff12 = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).payoff21 = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).payoff22 = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).payoff31 = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).payoff32 = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).pt1 = pointFromString(msgtokens(nextToken))
                        nextToken += 1

                        nodeList(j, i).pt2 = pointFromString(msgtokens(nextToken))
                        nextToken += 1

                        nodeList(j, i).pt3 = pointFromString(msgtokens(nextToken))
                        nextToken += 1

                        nodeList(j, i).pt4 = pointFromString(msgtokens(nextToken))
                        nextToken += 1

                        nodeList(j, i).subNode1Id = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).subNode2Id = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).subNode3Id = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).myColor = getMyColor(nodeList(j, i).owner)

                    Next
                Next

                'instruction nodes
                For i As Integer = 1 To 3
                    nodeCountInstructions(i) = msgtokens(nextToken)
                    nextToken += 1

                    For j As Integer = 1 To nodeCountInstructions(i)
                        nodeListInstructions(j, i) = New node(nodeListInstructions)

                        nodeListInstructions(j, i).id = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).myTree = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).status = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).owner = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).payoff11 = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).payoff12 = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).payoff21 = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).payoff22 = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).payoff31 = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).payoff32 = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).pt1 = pointFromString(msgtokens(nextToken))
                        nextToken += 1

                        nodeListInstructions(j, i).pt2 = pointFromString(msgtokens(nextToken))
                        nextToken += 1

                        nodeListInstructions(j, i).pt3 = pointFromString(msgtokens(nextToken))
                        nextToken += 1

                        nodeListInstructions(j, i).pt4 = pointFromString(msgtokens(nextToken))
                        nextToken += 1

                        nodeListInstructions(j, i).subNode1Id = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).subNode2Id = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).subNode3Id = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).myColor = getMyColor(nodeListInstructions(j, i).owner)
                    Next
                Next

                .Timer1.Enabled = True

                If showInstructions Then

                    If rand(2, 1) = 1 Then
                        myType(1) = 1
                    Else
                        myType(1) = 2
                    End If

                    frmInstructions.Show()

                    frmInstructions.Location = New Point(instructionX, instructionY)
                    frmInstructions.startTimeOnPage = Now
                    frmInstructions.lastInstruction = 1
                    frmInstructions.instructionType = periods(1).instructions
                    frmInstructions.setup()

                    .Location = New Point(windowX, windowY)
                    .cmdSubmit.Visible = False
                    currentPeriodInstruction = 1
                    .pnlMain.Enabled = False

                    'setup instruction nodes
                    '1
                    nodeListInstructions(1, 1).owner = myType(currentPeriod)
                    nodeListInstructions(1, 1).myColor = getMyColor(nodeListInstructions(1, 1).owner)
                    'nodeListInstructions(1, 1).payoff11 = nodeListInstructions(1, 1).payoff11.Replace("0", "x")
                    'nodeListInstructions(1, 1).payoff12 = nodeListInstructions(1, 1).payoff12.Replace("0", "y")
                    'nodeListInstructions(1, 1).payoff21 = nodeListInstructions(1, 1).payoff21.Replace("0", "a")
                    'nodeListInstructions(1, 1).payoff22 = nodeListInstructions(1, 1).payoff22.Replace("0", "b")

                    '2
                    nodeListInstructions(1, 2).owner = myType(currentPeriod)
                    nodeListInstructions(1, 2).myColor = getMyColor(nodeListInstructions(1, 2).owner)
                    'nodeListInstructions(1, 2).payoff11 = nodeListInstructions(1, 2).payoff11.Replace("0", "x")
                    'nodeListInstructions(1, 2).payoff12 = nodeListInstructions(1, 2).payoff12.Replace("0", "y")

                    If myType(currentPeriod) = 1 Then
                        nodeListInstructions(2, 2).owner = 2
                    Else
                        nodeListInstructions(2, 2).owner = 1
                    End If

                    nodeListInstructions(2, 2).myColor = getMyColor(nodeListInstructions(2, 2).owner)
                    'nodeListInstructions(2, 2).payoff11 = nodeListInstructions(2, 2).payoff11.Replace("0", "a")
                    'nodeListInstructions(2, 2).payoff12 = nodeListInstructions(2, 2).payoff12.Replace("0", "b")
                    'nodeListInstructions(2, 2).payoff21 = nodeListInstructions(2, 2).payoff21.Replace("0", "c")
                    'nodeListInstructions(2, 2).payoff22 = nodeListInstructions(2, 2).payoff22.Replace("0", "d")

                    '3
                    nodeListInstructions(1, 3).owner = myType(currentPeriod)
                    nodeListInstructions(1, 3).myColor = getMyColor(nodeListInstructions(1, 3).owner)

                    nodeListInstructions(2, 3).owner = myType(currentPeriod)
                    nodeListInstructions(2, 3).myColor = getMyColor(nodeListInstructions(2, 3).owner)
                    'nodeListInstructions(2, 3).payoff11 = nodeListInstructions(2, 3).payoff11.Replace("0", "w")
                    'nodeListInstructions(2, 3).payoff12 = nodeListInstructions(2, 3).payoff12.Replace("0", "x")
                    'nodeListInstructions(2, 3).payoff21 = nodeListInstructions(2, 3).payoff21.Replace("0", "y")
                    'nodeListInstructions(2, 3).payoff22 = nodeListInstructions(2, 3).payoff22.Replace("0", "z")

                    If myType(currentPeriod) = 1 Then
                        nodeListInstructions(3, 3).owner = 2
                    Else
                        nodeListInstructions(3, 3).owner = 1
                    End If

                    nodeListInstructions(3, 3).myColor = getMyColor(nodeListInstructions(3, 3).owner)
                    'nodeListInstructions(3, 3).payoff11 = nodeListInstructions(3, 3).payoff11.Replace("0", "a")
                    'nodeListInstructions(3, 3).payoff12 = nodeListInstructions(3, 3).payoff12.Replace("0", "b")
                    'nodeListInstructions(3, 3).payoff21 = nodeListInstructions(3, 3).payoff21.Replace("0", "c")
                    'nodeListInstructions(3, 3).payoff22 = nodeListInstructions(3, 3).payoff22.Replace("0", "d")
                End If

                tickTock = 0
                .Timer2.Enabled = True
                selection = ""

                decisionStart = Now

                .Text = "Client " & inumber

                If periods(currentPeriod).payoffMode = "Dollars" Then
                    .lblEarnings.Text = "Earnings($)"
                ElseIf periods(currentPeriod).payoffMode = "Cents" Then
                    .lblEarnings.Text = "Earnings(¢)"
                End If

                If Not showInstructions Then
                    startNextPeriod(periods(currentPeriod).pairingRule)
                Else
                    setupScreen()
                End If

                If testMode Then
                    frmTestMode.Show()
                    .Timer3.Enabled = True

                    If showInstructions Then
                        frmTestMode.Location = New Point(frmInstructions.Location.X, frmInstructions.Location.Y + frmInstructions.Height + 10)
                    End If
                End If

                wskClient.Send("COMPUTER_NAME", My.Computer.Name & ";")
            Catch ex As Exception
                appEventLog_Write("error begin:", ex)
            End Try

        End With
    End Sub

    Public Sub setupScreen()
        Try
            With frmMain
                If showInstructions Then
                    .lblPerson.Text = "Person ?"
                    .lblPerson.ForeColor = Color.Black
                Else
                    If myType(currentPeriod) = "1" Then
                        .lblPerson.Text = "Person 1"
                        .lblPerson.ForeColor = Color.CornflowerBlue

                        .lblPayoff1.Text = "Your Payoff"
                        .lblPayoff2.Text = "Person 2's Payoff"
                    Else
                        .lblPerson.Text = "Person 2"
                        .lblPerson.ForeColor = Color.Coral

                        .lblPayoff1.Text = "Person 1's Payoff"
                        .lblPayoff2.Text = "Your Payoff"
                    End If
                End If

                If showInstructions Then

                    '.lblPerson.Visible = False
                    '.lblPerson2.Visible = False

                    .lblPayoff1.Text = "Person 1's Payoff"
                    .lblPayoff2.Text = "Person 2's Payoff"
                Else
                    .lblPerson.Visible = True
                    .lblPerson2.Visible = True

                    If myType(currentPeriod) = "1" Then
                        .lblPerson.Text = "Person 1"
                        .lblPerson.ForeColor = Color.CornflowerBlue

                        .lblPayoff1.Text = "Your Payoff"
                        .lblPayoff2.Text = "Person 2's Payoff"
                    Else
                        .lblPerson.Text = "Person 2"
                        .lblPerson.ForeColor = Color.Coral

                        .lblPayoff1.Text = "Person 1's Payoff"
                        .lblPayoff2.Text = "Your Payoff"
                    End If
                End If
            End With
        Catch ex As Exception
            appEventLog_Write("error begin:", ex)
        End Try
    End Sub

    Public Sub showChoiceScreen()
        Try

        Catch ex As Exception
            appEventLog_Write("error begin:", ex)
        End Try
    End Sub

    Public Sub updateTxtMessages()
        Try
            With frmMain
                If Not frmMain.Visible Then Return
                If periods(currentPeriod).getCurrentTree() < 1 Then Return

                If nodeList(currentNode, periods(currentPeriod).getCurrentTree()).owner = myType(currentPeriod) Then
                    .txtMessages.Text = "Click on your choice then press Submit."
                    .cmdSubmit.Visible = True
                Else
                    If myType(currentPeriod) = 1 Then
                        .txtMessages.Text = "Waiting for Person 2."
                        .txtMessages.Find("Person 2")
                        .txtMessages.SelectionColor = Color.Coral
                    Else
                        .txtMessages.Text = "Waiting for Person 1."
                        .txtMessages.Find("Person 1")
                        .txtMessages.SelectionColor = Color.CornflowerBlue
                    End If

                    .cmdSubmit.Visible = False
                End If
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub takeStartNextPeriod(ByVal sinstr As String)
        Try
            With frmMain
                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0

                currentPeriod = msgtokens(nextToken)
                nextToken += 1

                periods(currentPeriod).currentTreeIndex = msgtokens(nextToken)
                nextToken += 1

                myType(currentPeriod) = msgtokens(nextToken)
                nextToken += 1

                startNextPeriod(periods(currentPeriod).pairingRule)
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub startNextPeriod(mode As String)
        Try
            With frmMain
                .pnlMain.Enabled = True
                resetTree(1)
                resetTree(2)
                currentNode = 1
                selection = ""

                showInstructions = False

                setupScreen()

                frmInstructions.Close()
                frmEndEarly.Close()
                frmChoice.Close()

                If mode = "Choice" Then
                    .Hide()
                    frmChoice.Show()
                    frmChoice.Text = "Client " & inumber
                    currentNode = 0

                    resetTree(periods(currentPeriod).trees(1))
                    resetTree(periods(currentPeriod).trees(2))
                Else
                    .Show()
                End If

                updateTxtMessages()
                decisionStart = Now
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub takeShowInstructions(ByVal sinstr As String)
        Try
            With frmMain
                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0

                currentPeriod = msgtokens(nextToken)
                nextToken += 1

                .Hide()

                showInstructions = True

                frmInstructions.Show()
                frmInstructions.Location = New Point(instructionX, instructionY)
                frmInstructions.startTimeOnPage = Now
                frmInstructions.lastInstruction = 1
                frmInstructions.instructionType = periods(currentPeriod).instructions
                frmInstructions.setup()
                frmInstructions.cmdReset.Visible = False

            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub resetTree(tempTree As Integer)
        Try
            If tempTree = -1 Then Exit Sub

            For i As Integer = 1 To nodeCount(tempTree)
                nodeList(i, tempTree).status = "open"
            Next
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub takeFinishChoicePeriod(ByVal sinstr As String)
        Try
            Dim msgtokens() As String = sinstr.Split(";")
            Dim nextToken As Integer = 0

            periods(currentPeriod).currentTreeIndex = msgtokens(nextToken)
            nextToken += 1

            myType(currentPeriod) = msgtokens(nextToken)
            nextToken += 1

            startNextPeriod("None")
            'decisionStart = Now
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    'the game is over for that user
    Public Sub takeEndGameNoMatch(ByVal sinstr As String)
        Try
            With frmMain
                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0

                Dim tempEarnings As Double = msgtokens(nextToken)
                .txtProfit.Text = Format(tempEarnings, "0.00")

                frmChoice.Hide()
                frmEndEarly.Show()
                frmNames.Show()

                frmEndEarly.Text = "Client " & inumber
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub takeBlankScreen(ByVal sinstr As String)
        Try
            Dim msgtokens() As String = sinstr.Split(";")
            Dim nextToken As Integer = 0


            frmChoice.Hide()
            frmEndEarly.Show()

            frmEndEarly.lbl1.Text = "Please wait at your seat while we pay the unmatched people." & vbCrLf & "The experiment will resume shortly. "

            frmEndEarly.Text = "Client " & inumber
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub takeEndGame(ByVal sinstr As String)
        Try
            'end the experiment
            With frmMain
                .txtMessages.Text = ""
                frmNames.Show()

            End With
        Catch ex As Exception
            appEventLog_Write("error endGame:", ex)
        End Try
    End Sub

    Public Sub takeEndEarly(ByVal sinstr As String)
        Try
            'end experiment early
            Dim msgtokens() As String
            msgtokens = sinstr.Split(";")

            numberOfPeriods = msgtokens(0)
        Catch ex As Exception
            appEventLog_Write("error endEarly:", ex)
        End Try
    End Sub

    Public Sub periodResults(ByVal sinstr As String)
        Try
            'show the results at end of period
            With frmMain

                Dim msgtokens() As String = Split(sinstr, ";")
                Dim nextToken As Integer = 0

            End With
        Catch ex As Exception
            appEventLog_Write("error periodResults:", ex)
        End Try
    End Sub

    Public Sub takeFinishedInstructions()
        Try
            With frmMain
                'close the instructions and start experiment           

                frmInstructions.Close()
                showInstructions = False

                currentPeriod = 1
                currentNode = 1

                decisionStart = Now

                .pnlMain.Enabled = True

                updateTxtMessages()

                If testMode Then .Timer3.Enabled = True
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

    Public Sub takeChoice(ByVal sinstr As String)
        Try
            With frmMain
                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0

                currentNode = msgtokens(nextToken)
                nextToken += 1

                For i As Integer = 1 To nodeCount(periods(currentPeriod).getCurrentTree())
                    nodeList(i, periods(currentPeriod).getCurrentTree()).status = msgtokens(nextToken)
                    nextToken += 1
                Next

                If currentNode = 0 Then
                    .txtMessages.Text = "Waiting for Others."
                Else
                    updateTxtMessages()

                    decisionStart = Now
                End If


            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub takePeriodResults(ByVal sinstr As String)
        Try
            With frmMain
                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0

                Dim tempPeriodEarnings As Double = msgtokens(nextToken)
                nextToken += 1

                Dim tempEarnings As Double = msgtokens(nextToken)
                .txtProfit.Text = Format(tempEarnings, "0.00")
                nextToken += 1

                currentNode = 0
                nextToken += 1

                For i As Integer = 1 To nodeCount(periods(currentPeriod).getCurrentTree())
                    nodeList(i, periods(currentPeriod).getCurrentTree()).status = msgtokens(nextToken)
                    nextToken += 1
                Next

                .txtMessages.Text = "Press the ""Ready to Go On"" Button."
                .cmdSubmit.Text = "Ready to Go On"
                .cmdSubmit.Visible = True
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub doTestMode()
        Try
            With frmMain
                If frmChoice.Visible Then
                    If frmChoice.cmdSubmit.Visible Then
                        If rand(2, 1) = 1 Then
                            frmChoice.rbLeft.Checked = True
                        Else
                            frmChoice.rbRight.Checked = True
                        End If

                        frmChoice.cmdSubmit.PerformClick()
                    End If
                Else
                    If .cmdSubmit.Visible Then
                        If .cmdSubmit.Text = "Submit" Then

                            If selection <> "" Then
                                .cmdSubmitAction()
                            Else
                                Dim t As Integer = periods(currentPeriod).getCurrentTree()

                                Select Case rand(6, 1)
                                    Case 1
                                        If nodeList(currentNode, t).payoff11 >= 0 Then
                                            .pnlMainClickAction(nodeList(currentNode, t).pt3.X, nodeList(currentNode, t).pt3.Y)
                                        End If
                                    Case 2
                                        If nodeList(currentNode, t).payoff21 >= 0 Then
                                            .pnlMainClickAction(nodeList(currentNode, t).pt2.X, nodeList(currentNode, t).pt2.Y)
                                        End If
                                    Case 3
                                        If nodeList(currentNode, t).payoff21 >= 0 Then
                                            .pnlMainClickAction(nodeList(currentNode, t).pt4.X, nodeList(currentNode, t).pt4.Y)
                                        End If
                                    Case 4
                                        If nodeList(currentNode, t).subNode1Id > 0 Then
                                            .pnlMainClickAction(nodeList(nodeList(currentNode, t).subNode1Id, t).pt1.X,
                                                                nodeList(nodeList(currentNode, t).subNode1Id, t).pt1.Y)
                                        End If
                                    Case 5
                                        If nodeList(currentNode, t).subNode2Id > 0 Then
                                            .pnlMainClickAction(nodeList(nodeList(currentNode, t).subNode2Id, t).pt1.X,
                                                                nodeList(nodeList(currentNode, t).subNode2Id, t).pt1.Y)
                                        End If
                                    Case 6
                                        If nodeList(currentNode, t).subNode3Id > 0 Then
                                            .pnlMainClickAction(nodeList(nodeList(currentNode, t).subNode3Id, t).pt1.X,
                                                                nodeList(nodeList(currentNode, t).subNode3Id, t).pt1.Y)
                                        End If
                                End Select
                            End If
                        Else
                            .cmdSubmitAction()
                        End If
                    ElseIf frmNames.Visible Then

                        Dim tempN As Integer = rand(20, 5)

                        frmNames.txtName1.Text = ""

                        For i As Integer = 1 To tempN
                            frmNames.txtName1.Text &= Chr(rand(122, 60))
                        Next

                        frmNames.cmdSubmitAction(False)
                    End If
                End If

            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub doTestModeInstructions()
        Try
            With frmMain

                If frmInstructions.cmdStart.Visible Then
                    frmInstructions.cmdStart.PerformClick()
                    Exit Sub
                End If

                If frmInstructions.startPressed Then Exit Sub

                Select Case currentInstruction
                    Case 1
                        frmInstructions.cmdNextAction()
                    Case 2
                        frmInstructions.cmdNextAction()
                    Case 3
                        frmInstructions.cmdNextAction()
                    Case 4
                        frmInstructions.cmdNextAction()
                    Case 5
                        If rand(2, 1) = 1 Then
                            .pnlMainClickAction(nodeListInstructions(1, 1).pt2.X, nodeListInstructions(1, 1).pt2.Y)
                        Else
                            .pnlMainClickAction(nodeListInstructions(1, 1).pt3.X, nodeListInstructions(1, 1).pt3.Y)
                        End If

                        .cmdSubmitActionInstruction()
                        frmInstructions.cmdNextAction()
                    Case 6
                        .pnlMainClickAction(nodeListInstructions(2, 2).pt1.X, nodeListInstructions(2, 2).pt1.Y)
                        .cmdSubmitActionInstruction()
                        frmInstructions.cmdNextAction()
                    Case 7
                        If rand(2, 1) = 1 Then
                            .pnlMainClickAction(nodeListInstructions(2, 3).pt1.X, nodeListInstructions(2, 3).pt1.Y)
                        Else
                            .pnlMainClickAction(nodeListInstructions(3, 3).pt1.X, nodeListInstructions(3, 3).pt1.Y)
                        End If

                        .cmdSubmitActionInstruction()
                        frmInstructions.cmdNextAction()
                    Case 8


                End Select

            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Function getMyColor(id As Integer) As Color
        Try
            If id = 1 Then
                Return Color.CornflowerBlue
            Else
                Return Color.Coral
            End If
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Function

    Public Function returnInsructionPayoff(value As String) As String
        Try
            Select Case value
                Case "1"
                    Return "A"
                Case "2"
                    Return "B"
                Case "3"
                    Return "C"
                Case "4"
                    Return "D"
                Case "5"
                    Return "E"
                Case "6"
                    Return "F"
                Case "7"
                    Return "G"
                Case "8"
                    Return "H"
            End Select

            Return ""
        Catch ex As Exception
            appEventLog_Write("error :", ex)

            Return ""
        End Try
    End Function
End Module
