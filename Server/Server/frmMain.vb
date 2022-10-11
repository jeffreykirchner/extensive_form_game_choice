Imports System
Imports System.ComponentModel
Imports System.Threading
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.IO

Public Class frmMain
#Region " Winsock Code "
    Public WithEvents wsk_Col As New WinsockCollection
    Private _users As New UserCollection
    Public WithEvents wskListener As Winsock


    Private Sub wskListener_ConnectionRequest(ByVal sender As Object, ByVal e As WinsockClientReceivedEventArgs) Handles wskListener.ConnectionRequest
        Try
            'Log("Connection received from: " & e.ClientIP)
            Dim y As New clsUser
            Dim i As Integer
            Dim ID As String = connectionCount + 1

            connectionCount += 1

            _users.Add(y)
            Dim x As New Winsock(Me)
            wsk_Col.Add(x, ID)
            x.Accept(e.Client)

            If cmdBegin.Enabled = False Then
                For i = 1 To playerCount
                    If playerList(i).myIPAddress = e.ClientIP Then
                        playerList(i).socketNumber = ID 'wsk_Col.Count - 1
                        Exit For
                    End If
                Next

                Exit Sub
            End If

            playerCount += 1
            playerList(playerCount) = New player()
            playerList(playerCount).inumber = playerCount
            playerList(playerCount).socketNumber = ID 'wsk_Col.Count - 1
            playerList(playerCount).myIPAddress = e.ClientIP

            playerList(playerCount).requsetIP(playerCount)

            lblConnections.Text = CInt(lblConnections.Text) + 1

            'appEventLog_Write("connection request: " & e.ClientIP)
        Catch ex As Exception
            appEventLog_Write("error wskListener_ConnectionRequest:", ex)
        End Try
    End Sub

    Private Sub wskListener_ErrorReceived(ByVal sender As System.Object, ByVal e As WinsockErrorEventArgs) Handles wskListener.ErrorReceived
        Try
            appEventLog_Write("winsock error: " & e.Message)
        Catch ex As Exception
            appEventLog_Write("error wskListener_ErrorReceived:", ex)
        End Try
    End Sub

    Private Sub wskListener_StateChanged(ByVal sender As Object, ByVal e As WinsockStateChangingEventArgs) Handles wskListener.StateChanged
        'Log("Listener state changed from " & e.Old_State.ToString & " to " & e.New_State.ToString)
        'lblListenState.Text = "State: " & e.New_State.ToString
        'cmdListen.Enabled = False
        'cmdClose.Enabled = False
        'Select Case e.New_State
        '    Case WinsockStates.Closed
        '        cmdListen.Enabled = True
        '    Case WinsockStates.Listening
        '        cmdClose.Enabled = True
        'End Select
    End Sub

    'Private Sub Log(ByVal val As String)
    '    lstLog.SelectedIndex = lstLog.Items.Add(val)
    '    lstLog.SelectedIndex = -1
    'End Sub

    Private Sub Wsk_DataArrival(ByVal sender As Object, ByVal e As WinsockDataArrivalEventArgs) Handles wsk_Col.DataArrival
        Try
            Dim sender_key As String = wsk_Col.GetKey(sender)
            Dim buf As String = Nothing
            CType(sender, Winsock).Get(buf)

            Dim msgtokens() As String = buf.Split("#")
            Dim i As Integer

            'appEventLog_Write("data arrival: " & buf)

            For i = 1 To msgtokens.Length - 1
                takeMessage(msgtokens(i - 1))
            Next

        Catch ex As Exception
            appEventLog_Write("error Wsk_DataArrival:", ex)
        End Try
    End Sub

    Private Sub Wsk_Disconnected(ByVal sender As Object, ByVal e As System.EventArgs) Handles wsk_Col.Disconnected
        Try
            wsk_Col.Remove(sender)
            If cmdBegin.Enabled Then Exit Sub
            MsgBox("A client has been disconnected.", MsgBoxStyle.Critical)
            appEventLog_Write("client disconnected")
            'lblConNum.Text = "Connected: " & wsk_Col.Count
        Catch ex As Exception
            appEventLog_Write("error Wsk_Disconnected:", ex)
        End Try
    End Sub
    Private Sub Wsk_Connected(ByVal sender As Object, ByVal e As System.EventArgs) Handles wsk_Col.Connected
        'lblConNum.Text = "Connected: " & wsk_Col.Count
    End Sub

    Private Sub ShutDownServer()
        Try
            GC.Collect()
        Catch ex As Exception
            appEventLog_Write("error ShutDownServer:", ex)
        End Try

    End Sub
#End Region    'communication code

#Region " Extra Functions "
    Public Function convertY(ByVal p As Integer, ByVal graphMin As Integer, ByVal graphMax As Integer, _
                                 ByVal panelHeight As Integer, ByVal bottomOffset As Integer, ByVal topOffset As Integer) As Double
        Try
            Dim tempD As Double

            tempD = p - graphMin

            tempD = (tempD * (panelHeight - bottomOffset - topOffset)) / (graphMax - graphMin)
            tempD = panelHeight - (bottomOffset + topOffset) - tempD

            convertY = tempD + topOffset
        Catch ex As Exception
            Return 0
            appEventLog_Write("error convertY:", ex)
        End Try
    End Function

    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Try
            Dim i As Integer
            Dim f As New Font("Arial", 8, FontStyle.Bold)
            Dim tempN As Integer

            e.Graphics.DrawString(filename2, f, Brushes.Black, 10, 10)

            f = New Font("Arial", 15, FontStyle.Bold)

            e.Graphics.DrawString("Name", f, Brushes.Black, 10, 30)
            e.Graphics.DrawString("Earnings", f, Brushes.Black, 400, 30)

            f = New Font("Arial", 12, FontStyle.Bold)

            tempN = 55

            For i = 1 To DataGridView1.RowCount
                If i Mod 2 = 0 Then
                    e.Graphics.FillRectangle(Brushes.Aqua, 0, tempN, 500, 19)
                End If
                e.Graphics.DrawString(DataGridView1.Rows(i - 1).Cells(1).Value, f, Brushes.Black, 10, tempN)
                e.Graphics.DrawString(DataGridView1.Rows(i - 1).Cells(3).Value, f, Brushes.Black, 400, tempN)

                tempN += 20
            Next

        Catch ex As Exception
            appEventLog_Write("error PrintDocument1_PrintPage:", ex)
        End Try

    End Sub
#End Region

    Public tempTime As String 'time stamp at start of experiment
    Public mainScreen As Screen

    Public replayDfSummary() As String                         'replay data 
    Public replayDfEvents() As String

    Dim replaySpeed As Integer = 100
    Public p1_dash As New Pen(Brushes.Black, 1)

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            mainScreen = New Screen(pnlMain, New Rectangle(0, 0, pnlMain.Width, pnlMain.Height))

            sfile = System.Windows.Forms.Application.StartupPath & "\server.ini"
            loadParameters()

            Dim i As Integer           ', j As Integer

            For i = 1 To 4
                DataGridView1.Columns(i - 1).SortMode = DataGridViewColumnSortMode.NotSortable
            Next

            'setup communication on load
            wskListener = New Winsock
            wskListener.BufferSize = 8192
            wskListener.LegacySupport = False
            wskListener.LocalPort = portNumber
            wskListener.MaxPendingConnections = 1
            wskListener.Protocol = WinsockProtocols.Tcp
            wskListener.RemotePort = 8080
            wskListener.RemoteServer = "localhost"
            wskListener.SynchronizingObject = Me

            wskListener.Listen()

            playerCount = 0

            lblIP.Text = wskListener.LocalIP
            lblLocalHost.Text = SystemInformation.ComputerName

            setTriangleEndCap(p1_dash)
            p1_dash.DashStyle = DashStyle.Dash
        Catch ex As Exception
            appEventLog_Write("error frmSvr_Load:", ex)
        End Try

    End Sub

    Public Sub setTriangleEndCap(ByRef p As Pen)
        Try
            p.EndCap = LineCap.Triangle
            p.StartCap = LineCap.Triangle
            p.Alignment = PenAlignment.Center
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            drawScreen()
        Catch ex As Exception
            appEventLog_Write("error Timer1_Tick:", ex)
        End Try
    End Sub

    Public Sub drawScreen()
        Try
            With periods(currentPeriod)

                mainScreen.erase1()
                Dim g As Graphics = mainScreen.GetGraphics

                g.DrawLine(p1_dash, New Point(pnlMain.Width / 2, 0), New Point(pnlMain.Width / 2, pnlMain.Height))

                g.TranslateTransform(-143, 0)

                Dim tempTree As Integer = .trees(1)

                If tempTree <> -1 Then
                    'For i As Integer = 1 To nodeCount(tempTree)
                    '    If nodeList(i, tempTree) IsNot Nothing Then
                    '        nodeList(i, tempTree).drawNodeArrows(g, tempTree)
                    '    End If
                    'Next

                    For i As Integer = 1 To .nodeCount(1)
                        If .nodeList(i, 1, currentSubPeriod) IsNot Nothing Then
                            .nodeList(i, 1, currentSubPeriod).drawNode(g, True, False)
                        End If
                    Next
                End If

                g.ResetTransform()

                g.TranslateTransform(414, 0)

                tempTree = .trees(2)

                If tempTree <> -1 Then
                    'For i As Integer = 1 To nodeCount(tempTree)
                    '    If nodeList(i, tempTree) IsNot Nothing Then
                    '        nodeList(i, tempTree).drawNodeArrows(g, tempTree)
                    '    End If
                    'Next

                    For i As Integer = 1 To .nodeCount(2)
                        If .nodeList(i, 2, currentSubPeriod) IsNot Nothing Then
                            .nodeList(i, 2, currentSubPeriod).drawNode(g, True, False)
                        End If
                    Next
                End If

                g.ResetTransform()


                mainScreen.flip()
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub cmdBegin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBegin.Click
        Try

            'when a button is pressed it's click event is fired

            loadParameters()

            Dim nextToken As Integer = 0
            Dim str As String

            If CInt(lblConnections.Text) <> numberOfPlayers Then
                MsgBox("Incorrect number of connections.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            'define timestamp for recording data
            tempTime = DateTime.Now.Month & "-" & DateTime.Now.Day & "-" & DateTime.Now.Year & "_" & DateTime.Now.Hour &
                     "_" & DateTime.Now.Minute & "_" & DateTime.Now.Second

            'create unique file name for storing data, CSVs are excel readable, Comma Separted Value files.
            filename = "Event_Data_" & tempTime & ".csv"
            filename = System.Windows.Forms.Application.StartupPath & "\datafiles\" & filename

            summaryDf = File.CreateText(filename)
            str = "Period,SubPeriod,Player,Partner,Tree,PlayerType,DecisionType,DecisionLength,DecisionDirection,DecisionInfo,DecisionNode,PeriodTime,"
            summaryDf.WriteLine(str)

            filename = "Summary_Data_" & tempTime & ".csv"
            filename = System.Windows.Forms.Application.StartupPath & "\datafiles\" & filename

            playerDf = File.CreateText(filename)
            str = "Period,SubPeriod,Player,Partner,Tree,FinalNode,FinalDirection,MyPayoff,PartnerPayoff,MyType,MadeFinalDecision"
            playerDf.WriteLine(str)

            filename = "Recruiter_Data_" & tempTime & ".csv"
            filename = System.Windows.Forms.Application.StartupPath & "\datafiles\" & filename

            recruiterDf = File.CreateText(filename)

            filename = "Parameters_" & tempTime & ".csv"
            filename = Application.StartupPath & "\datafiles\" & filename
            writeINI(sfile, "GameSettings", "gameName", "ESI Software2")
            writeINI(sfile, "GameSettings", "gameName", "ESI Software")
            FileCopy(sfile, filename)

            DataGridView1.RowCount = numberOfPlayers

            showInstructions = getINI(sfile, "gameSettings", "showInstructions")

            'setup for display results
            'setup player type
            For i As Integer = 1 To numberOfPlayers
                DataGridView1.Rows(i - 1).Cells(0).Value = i

                playerList(i).earnings = 0
                playerList(i).roundEarnings = 0
                playerList(i).sname = ""

                DataGridView1.Rows(i - 1).Cells(0).Value = i
                DataGridView1.Rows(i - 1).Cells(1).Value = ""

                If showInstructions Then
                    DataGridView1.Rows(i - 1).Cells(2).Value = "Page 1"
                Else
                    DataGridView1.Rows(i - 1).Cells(2).Value = "Playing"
                End If

                DataGridView1.Rows(i - 1).Cells(3).Value = "0"

                For j As Integer = 1 To numberOfPeriods
                    playerList(i).treeChoice(j) = -1
                    playerList(i).partnerList(j) = -1
                Next
            Next

            currentPeriod = 1
            currentSubPeriod = 1

            txtPeriod.Text = "P1 - 1"
            txtPeriod.Enabled = False
            checkin = 0

            'disable/enable buttons needed when the experiment starts

            filename2 = filename

            showInstructions = getINI(sfile, "gameSettings", "showInstructions")

            setupInstructionNodes()
            setupNodes()
            setupPeriods()

            If periods(1).pairingRule <> "Choice" Then
                randomizePartners()

                For i As Integer = 1 To numberOfPlayers

                    playerList(i).treeChoice(1) = 1
                    playerList(i).setupNodes()

                Next
            Else

            End If

            'signal clients to begin
            checkin = 0
            For i As Integer = 1 To numberOfPlayers
                playerList(i).sendBegin()

                DataGridView1.Rows(i - 1).Cells(2).Value = "Playing"
            Next

            'nodeList(1, currentPeriod).count = numberOfPlayers / 2

            Timer1.Enabled = True

            periodStart = Now

            cmdLoad.Enabled = False
            cmdGameSetup.Enabled = False
            cmdExchange.Enabled = False
            cmdSetup2.Enabled = False
            cmdExit.Enabled = False
            cmdBegin.Enabled = False
            cmdEnd.Enabled = True
            cmdExchange.Enabled = False
            cmdTreeSetup.Enabled = False
            cmdPlayData.Enabled = False
            cmdLoadData.Enabled = False
            cmdRecoverClient.Enabled = True
            cmdMinus.Enabled = False
            cmdPlus.Enabled = False

        Catch ex As Exception
            appEventLog_Write("error cmdBegin_Click:", ex)
        End Try

    End Sub

    Public Sub setupPeriods()
        Try
            For i As Integer = 1 To numberOfPeriods
                periods(i) = New period
                periods(i).fromString(getINI(sfile, "periods", i.ToString()), i)
                subPeriodCount(i) = 1
            Next
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub cmdReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReset.Click
        Try
            'when reset is pressed bring server back to state to start another experiment

            'disable timers
            Timer1.Enabled = False
            Timer2.Enabled = False
            Timer3.Enabled = False

            'close data files
            If summaryDf IsNot Nothing Then summaryDf.Close()
            If playerDf IsNot Nothing Then playerDf.Close()
            If replayDf IsNot Nothing Then replayDf.Close()
            If recruiterDf IsNot Nothing Then recruiterDf.Close()

            'shut down clients
            Dim i As Integer
            For i = 1 To CInt(lblConnections.Text)
                playerList(i).resetClient()
            Next

            'enable/disable buttons
            cmdLoad.Enabled = True
            cmdGameSetup.Enabled = True
            cmdBegin.Enabled = True
            cmdExit.Enabled = True
            cmdEnd.Enabled = False
            cmdExchange.Enabled = True
            cmdSetup2.Enabled = True
            cmdExchange.Enabled = True
            cmdTreeSetup.Enabled = True
            cmdLoadData.Enabled = True
            cmdRecoverClient.Enabled = False

            lblConnections.Text = 0
            playerCount = 0
            currentPeriod = 0

            DataGridView1.RowCount = 0

            frmInstructions.Close()
            frmContinue.Close()

        Catch ex As Exception
            appEventLog_Write("error cmdReset_Click:", ex)
        End Try
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Try
            'exit program

            Timer1.Enabled = False
            ShutDownServer()

            Me.Close()
        Catch ex As Exception
            appEventLog_Write("error cmdExit_Click:", ex)
        End Try
    End Sub

    Private Sub cmdGameSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGameSetup.Click
        Try
            frmSetup.Show()
        Catch ex As Exception
            appEventLog_Write("error cmdGameSetup_Click:", ex)
        End Try
    End Sub

    Dim replayMS As Integer
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Try
            With periods(currentPeriod)
                resetNodeCounts()

                replayMS += replaySpeed

                Dim msgtokens() As String
                Dim tempPeriod As Integer
                Dim tempSubPeriod As Integer
                Dim tempTree As Integer
                Dim tempLength As Integer
                Dim tempNode As Integer
                Dim tempType As String
                Dim tempInfo As String
                Dim tempDirection As String
                Dim tempPayoffCount As Integer = 0
                Dim tempTreeIndex As Integer

                For i As Integer = 1 To replayDfEvents.Length - 1

                    msgtokens = replayDfEvents(i).Split(",")

                    If IsNumeric(msgtokens(0)) Then

                        tempPeriod = msgtokens(0)
                        tempSubPeriod = msgtokens(1)
                        tempTree = msgtokens(4)
                        tempType = msgtokens(5)
                        tempDirection = msgtokens(8)
                        tempInfo = msgtokens(9)
                        tempNode = msgtokens(10)
                        tempLength = msgtokens(11)

                        If tempTree = .trees(1) Then
                            tempTreeIndex = 1
                        Else
                            tempTreeIndex = 2
                        End If

                        If tempPeriod = currentPeriod And
                           tempSubPeriod = currentSubPeriod And
                           tempLength <= replayMS Then

                            If tempDirection = "pay1" Then
                                .nodeList(tempNode, tempTreeIndex, currentSubPeriod).countRight += 1
                            ElseIf tempDirection = "pay2" Then
                                .nodeList(tempNode, tempTreeIndex, currentSubPeriod).countDown += 1
                            ElseIf tempDirection = "pay3" Then
                                .nodeList(tempNode, tempTreeIndex, currentSubPeriod).countLeft += 1
                            ElseIf tempDirection = "sub1" Then
                                .nodeList(nodeList(tempNode, tempTree).subNode1Id, tempTreeIndex, currentSubPeriod).count += 1
                            ElseIf tempDirection = "sub2" Then
                                .nodeList(nodeList(tempNode, tempTree).subNode2Id, tempTreeIndex, currentSubPeriod).count += 1
                            ElseIf tempDirection = "sub3" Then
                                .nodeList(nodeList(tempNode, tempTree).subNode3Id, tempTreeIndex, currentSubPeriod).count += 1
                            End If

                        End If
                    End If
                Next

                Dim go As Boolean = False
                For i As Integer = 1 To replayDfEvents.Length - 1
                    msgtokens = replayDfEvents(i).Split(",")

                    If IsNumeric(msgtokens(0)) Then

                        tempPeriod = msgtokens(0)
                        tempSubPeriod = msgtokens(1)
                        tempTree = msgtokens(4)
                        tempType = msgtokens(5)
                        tempDirection = msgtokens(8)
                        tempInfo = msgtokens(9)
                        tempNode = msgtokens(10)
                        tempLength = msgtokens(11)

                        If tempPeriod = currentPeriod And
                           tempSubPeriod = currentSubPeriod And
                           tempLength > replayMS Then

                            go = True
                        End If
                    End If
                Next

                If Not go Then Timer2.Enabled = False

                lblReplayTime.Text = Format(replayMS / 1000, "0.0")
            End With
        Catch ex As Exception
            appEventLog_Write("error Timer2_Tick:", ex)
        End Try
    End Sub

    Private Sub cmdLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoad.Click
        Try

            Dim tempS As String
            Dim sinstr As String

            'dispaly open file dialog to select file
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Parameter Files (*.txt)|*.txt"
            OpenFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath

            OpenFileDialog1.ShowDialog()

            'if filename is not empty then continue with load
            If OpenFileDialog1.FileName = "" Then
                Exit Sub
            End If

            tempS = OpenFileDialog1.FileName

            sinstr = getINI(tempS, "gameSettings", "gameName")

            'check that this is correct type of file to load
            If sinstr <> "ESI Software" Then
                MsgBox("Invalid file", vbExclamation)
                Exit Sub
            End If

            'copy file to be loaded into server.ini
            FileCopy(OpenFileDialog1.FileName, sfile)

            'load new parameters into server
            loadParameters()
        Catch ex As Exception
            appEventLog_Write("error cmdLoad_Click:", ex)
        End Try
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            'save current parameters to a text file so they can be loaded at a later time

            SaveFileDialog1.FileName = ""
            SaveFileDialog1.Filter = "Parameter Files (*.txt)|*.txt"
            SaveFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath
            SaveFileDialog1.ShowDialog()

            If SaveFileDialog1.FileName = "" Then
                Exit Sub
            End If

            FileCopy(sfile, SaveFileDialog1.FileName)

        Catch ex As Exception
            appEventLog_Write("error cmdSave_Click:", ex)
        End Try
    End Sub



    Private Sub cmdEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEnd.Click
        Try
            'end experiment early

            Dim i As Integer
            cmdEnd.Enabled = False

            numberOfPeriods = currentPeriod

            For i = 1 To numberOfPlayers
                playerList(i).endEarly()
            Next
        Catch ex As Exception
            appEventLog_Write("error cmdEnd_Click:", ex)
        End Try
    End Sub

    Private Sub txtExchange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExchange.Click
        Try
            frmExchange.Show()
        Catch ex As Exception
            appEventLog_Write("error txtExchange:", ex)
        End Try
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Try

        Catch ex As Exception
            appEventLog_Write("error timer3 tick:", ex)
        End Try
    End Sub

    Private Sub cmdSetup2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetup2.Click
        Try
            frmSetup2.Show()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub llESI_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llESI.LinkClicked
        Try
            System.Diagnostics.Process.Start("http://www.chapman.edu/esi/")
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        Try

            If PrintDialog1.ShowDialog = DialogResult.OK Then
                PrintDocument1.Print()
            End If

        Catch ex As Exception
            appEventLog_Write("error cmdPrint_Click:", ex)
        End Try
    End Sub

    Private Sub cmdTreeSetup_Click(sender As System.Object, e As System.EventArgs) Handles cmdTreeSetup.Click
        Try
            frmSetup3.Show()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub setupNodes()
        Try
            For i As Integer = 1 To treeCount
                nodeCount(i) = getINI(sfile, "nodeCount", CStr(i))

                For j As Integer = 1 To nodeCount(i)
                    setupNode(nodeList(j, i), j, i)
                Next
            Next
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub cmdLoadData_Click(sender As System.Object, e As System.EventArgs) Handles cmdLoadData.Click
        Try
            Dim sinstr As String = ""

            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Data Files (*.csv)|*.csv"
            OpenFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath & "\Datafiles"

            OpenFileDialog1.ShowDialog()

            sinstr = OpenFileDialog1.FileName

            If OpenFileDialog1.FileName = "" Then
                Exit Sub
            End If

            Dim d(3) As String
            d(0) = "Summary_Data_"
            d(1) = "Event_Data_"
            d(2) = "Parameters_"

            Dim msgtokens2() As String = OpenFileDialog1.FileName.Split(d, StringSplitOptions.RemoveEmptyEntries)

            Dim tempFileName As String
            'Dim replayDF As String

            'Parameters
            tempFileName = msgtokens2(0) & "Parameters_" & msgtokens2(1)
            FileCopy(tempFileName, sfile)
            loadParameters()

            'read data
            tempFileName = msgtokens2(0) & "Summary_Data_" & msgtokens2(1)
            replayDfSummary = My.Computer.FileSystem.ReadAllText(tempFileName).Split(vbCrLf)

            'read data
            tempFileName = msgtokens2(0) & "Event_Data_" & msgtokens2(1)
            replayDfEvents = My.Computer.FileSystem.ReadAllText(tempFileName).Split(vbCrLf)

            'numberOfPlayers = getINI(replayDF, "gameSettings", "numberOfPlayers")
            'numberOfPeriods = getINI(replayDF, "gameSettings", "numberOfPeriods")

            txtPeriod.Text = "P1 - 1"
            'txtPeriod.Enabled = True

            currentPeriod = 1
            currentSubPeriod = 1
            replayMS = 0

            cmdPlus.Enabled = True
            cmdMinus.Enabled = True

            setupNodes()
            setupPeriods()

            'calc number of suberiods
            For i As Integer = 1 To numberOfPeriods
                subPeriodCount(i) = 1
            Next

            For i As Integer = 1 To replayDfEvents.Length - 1

                Dim msgtokens() As String = replayDfEvents(i).Split(",")

                If IsNumeric(msgtokens(0)) Then
                    Dim tempPeriod As Integer = msgtokens(0)
                    Dim tempSubPeriod As Integer = msgtokens(1)
                    Dim tempTree As Integer = msgtokens(4)

                    If subPeriodCount(tempPeriod) < tempSubPeriod Then subPeriodCount(tempPeriod) = tempSubPeriod
                End If
            Next

            replayCalcStartingNodeCount()

            'nodeList(1, currentPeriod).count = numberOfPlayers / 2

            Timer1.Enabled = True

            cmdPlayData.Enabled = True
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub cmdPlayData_Click(sender As System.Object, e As System.EventArgs) Handles cmdPlayData.Click
        Try
            replayMS = 0
            Timer2.Enabled = True
            'currentPeriod = 1

            resetNodeCounts()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub resetNodeCounts()
        Try

            For i As Integer = 1 To periods(currentPeriod).nodeCount(1)
                periods(currentPeriod).nodeList(i, 1, currentSubPeriod).resetCounts()
            Next

            For i As Integer = 1 To periods(currentPeriod).nodeCount(2)
                periods(currentPeriod).nodeList(i, 2, currentSubPeriod).resetCounts()
            Next

            'For i As Integer = 1 To nodeCount(currentPeriod)
            '    nodeList(i, currentPeriod).count = 0
            '    nodeList(i, currentPeriod).countDown = 0
            '    nodeList(i, currentPeriod).countRight = 0
            '    nodeList(i, currentPeriod).countLeft = 0
            'Next

            'nodeList(1, currentPeriod).count = numberOfPlayers / 2

            replayCalcStartingNodeCount()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    'Private Sub txtPeriod_ValueChanged(sender As System.Object, e As System.EventArgs)
    '    Try
    '        If cmdPlayData.Enabled = True Then
    '            replayMS = 0
    '            Timer2.Enabled = False

    '            currentPeriod = txtPeriod.Value
    '            lblReplayTime.Text = "-"
    '        End If
    '    Catch ex As Exception
    '        appEventLog_Write("error :", ex)
    '    End Try
    'End Sub

    Private Sub cmdRecoverClient_Click(sender As System.Object, e As System.EventArgs) Handles cmdRecoverClient.Click
        Try
            Dim tempI As Integer = DataGridView1.SelectedRows(0).Index + 1

            playerList(tempI).resendLastMessage()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub tbSpeed_Scroll(sender As Object, e As EventArgs) Handles tbSpeed.Scroll
        Try
            replaySpeed = tbSpeed.Value
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub cmdPlus_Click(sender As Object, e As EventArgs) Handles cmdPlus.Click
        Try
            If currentPeriod = numberOfPeriods And currentSubPeriod = subPeriodCount(currentPeriod) Then Exit Sub

            If currentSubPeriod = subPeriodCount(currentPeriod) Then

                currentSubPeriod = 1
                currentPeriod += 1
            Else

                currentSubPeriod += 1
                periods(currentPeriod).setupNodes(1)
                periods(currentPeriod).setupNodes(2)
            End If

            txtPeriod.Text = "P" & currentPeriod & " - " & currentSubPeriod

            replayCalcStartingNodeCount()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub cmdMinus_Click(sender As Object, e As EventArgs) Handles cmdMinus.Click
        Try
            If currentPeriod = 1 Then Exit Sub

            If currentSubPeriod = 1 Then
                currentPeriod -= 1
                currentSubPeriod = subPeriodCount(currentPeriod)
            Else
                currentSubPeriod -= 1
            End If

            txtPeriod.Text = "P" & currentPeriod & " - " & currentSubPeriod

            replayMS = 0
            Timer2.Enabled = False
            lblReplayTime.Text = "-"

            replayCalcStartingNodeCount()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub replayCalcStartingNodeCount()
        Try
            periods(currentPeriod).nodeList(1, 1, currentSubPeriod).count = 0
            periods(currentPeriod).nodeList(1, 2, currentSubPeriod).count = 0

            For i As Integer = 1 To replayDfSummary.Length - 1

                Dim msgtokens() As String = replayDfSummary(i).Split(",")

                If msgtokens(0).Trim = "" Then Exit For

                If IsNumeric(msgtokens(0)) And IsNumeric(msgtokens(9)) Then
                    Dim tempPeriod As Integer = msgtokens(0)
                    Dim tempSubPeriod As Integer = msgtokens(1)
                    Dim tempTree As Integer = msgtokens(4)
                    Dim tempType As Integer = msgtokens(9)

                    If tempPeriod = currentPeriod And tempSubPeriod = currentSubPeriod And tempType = 1 Then
                        If tempTree = periods(currentPeriod).trees(1) Then
                            periods(currentPeriod).nodeList(1, 1, currentSubPeriod).count += 1
                        Else
                            periods(currentPeriod).nodeList(1, 2, currentSubPeriod).count += 1
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub
End Class
