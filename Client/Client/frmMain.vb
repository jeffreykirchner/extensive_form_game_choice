Public Class frmMain
    'Public WithEvents mobjSocketClient As TCPConnection
    Delegate Sub SetTextCallback(ByVal [text] As String)
    Delegate Sub SetTextCallback2()

    Public mainScreen As screen

    Private Sub frmMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try

            'if ALT+K are pressed kill the client
            'if ALT+Q are pressed bring up connection box
            If e.Alt = True Then
                If CInt(e.KeyValue) = CInt(Keys.K) Then
                    If MessageBox.Show("Close Program?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.No Then Exit Sub
                    modMain.closing = True
                    Me.Close()
                ElseIf CInt(e.KeyValue) = CInt(Keys.Q) Then
                    frmConnect.Show()
                End If
            End If
        Catch ex As Exception
            appEventLog_Write("error frmChat_KeyDown:", ex)
        End Try
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            sfile = System.Windows.Forms.Application.StartupPath & "\client.ini"

            'take IP from command line
            Dim commandLine As String = Command()

            If commandLine <> "" Then
                writeINI(sfile, "Settings", "ip", commandLine)
            End If

            'connect
            myIPAddress = getINI(sfile, "Settings", "ip")
            myPortNumber = getINI(sfile, "Settings", "port")
            connect()

            mainScreen = New screen(pnlMain, New Rectangle(0, 0, pnlMain.Width, pnlMain.Height))

        Catch ex As Exception
            appEventLog_Write("errorfrmChat_Load :", ex)
        End Try

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If frmInstructions.Visible Then
                drawScreenInstructions()
            Else
                drawScreen()
            End If

        Catch ex As Exception
            appEventLog_Write("error Timer1_Tick:", ex)
        End Try
    End Sub

    Public Sub drawScreenInstructions()
        Try
            mainScreen.erase1()
            Dim g As Graphics = mainScreen.GetGraphics

            If selection <> "" Then
                g.FillRectangle(Brushes.Yellow,
                              New Rectangle(selectionPt.X - 38,
                                            selectionPt.Y - 38,
                                            76,
                                            76))
            End If

            'For i As Integer = 1 To nodeCountInstructions(currentPeriodInstruction)
            '    If nodeListInstructions(i, currentPeriodInstruction).subNode1Id > 0 Then
            '        nodeListInstructions(i, currentPeriodInstruction).pt2 = nodeListInstructions(nodeListInstructions(i, currentPeriodInstruction).subNode1Id, currentPeriodInstruction).pt1
            '    End If

            '    If nodeListInstructions(i, currentPeriodInstruction).subNode2Id > 0 Then
            '        nodeListInstructions(i, currentPeriodInstruction).pt3 = nodeListInstructions(nodeListInstructions(i, currentPeriodInstruction).subNode2Id, currentPeriodInstruction).pt1
            '    End If
            'Next

            For i As Integer = 1 To nodeCountInstructions(currentPeriodInstruction)
                If nodeListInstructions(i, currentPeriodInstruction) IsNot Nothing Then
                    nodeListInstructions(i, currentPeriodInstruction).drawNodeArrows(g,currentPeriodInstruction)
                End If
            Next

            For i As Integer = 1 To nodeCountInstructions(currentPeriodInstruction)
                If nodeListInstructions(i, currentPeriodInstruction) IsNot Nothing Then
                    nodeListInstructions(i, currentPeriodInstruction).drawNode(g)
                End If
            Next

            mainScreen.flip()
        Catch ex As Exception
            appEventLog_Write("error Timer1_Tick:", ex)
        End Try
    End Sub


    Public Sub drawScreen()
        Try
            mainScreen.erase1()
            Dim g As Graphics = mainScreen.GetGraphics

            If selection <> "" Then
                g.FillRectangle(Brushes.Yellow,
                              New Rectangle(selectionPt.X - 38,
                                            selectionPt.Y - 38,
                                            76,
                                            76))
            End If


            'For i As Integer = 1 To nodeCount(currentPeriod)
            '    If nodeList(i, currentPeriod).subNode1Id > 0 Then
            '        nodeList(i, currentPeriod).pt3 = nodeList(nodeList(i, currentPeriod).subNode1Id, currentPeriod).pt1
            '    End If

            '    If nodeList(i, currentPeriod).subNode2Id > 0 Then
            '        nodeList(i, currentPeriod).pt2 = nodeList(nodeList(i, currentPeriod).subNode2Id, currentPeriod).pt1
            '    End If

            '    If nodeList(i, currentPeriod).subNode3Id > 0 Then
            '        nodeList(i, currentPeriod).pt4 = nodeList(nodeList(i, currentPeriod).subNode3Id, currentPeriod).pt1
            '    End If
            'Next
            If periods(currentPeriod).currentTreeIndex = -1 Then Exit Sub

            Dim tempTree As Integer = periods(currentPeriod).getCurrentTree()

            If tempTree <> -1 Then
                For i As Integer = 1 To nodeCount(tempTree)
                    If nodeList(i, tempTree) IsNot Nothing Then
                        nodeList(i, tempTree).drawNodeArrows(g, tempTree)
                    End If
                Next

                For i As Integer = 1 To nodeCount(tempTree)
                    If nodeList(i, tempTree) IsNot Nothing Then
                        nodeList(i, tempTree).drawNode(g)
                    End If
                Next
            End If

            mainScreen.flip()
        Catch ex As Exception
            appEventLog_Write("error Timer1_Tick:", ex)
        End Try
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Try
            If cmdSubmit.Visible Then
                If cmdSubmit.BackColor = Color.Gainsboro Then
                    cmdSubmit.BackColor = Color.LightGreen
                Else
                    cmdSubmit.BackColor = Color.Gainsboro
                End If
            End If

            Dim go As Boolean = True

            Dim tempNode As node

            If currentNode = 0 Then Exit Sub

            If showInstructions Then
                If currentNode > nodeCountInstructions(currentPeriodInstruction) Then Exit Sub

                tempNode = nodeListInstructions(currentNode, currentPeriodInstruction)
            Else
                If currentNode > nodeCount(periods(currentPeriod).getCurrentTree()) Then Exit Sub

                tempNode = nodeList(currentNode, periods(currentPeriod).getCurrentTree())
            End If

            Do While go

                tickTock += 1

                If tickTock > 6 Then tickTock = 1

                Select Case tickTock
                    Case 1
                        If tempNode.payoff11 >= 0 Then go = False
                    Case 2
                        If tempNode.payoff21 >= 0 Then go = False
                    Case 3
                        If tempNode.payoff31 >= 0 Then go = False
                    Case 4
                        If tempNode.subNode1Id > 0 Then go = False
                    Case 5
                        If tempNode.subNode2Id > 0 Then go = False
                    Case 6
                        If tempNode.subNode3Id > 0 Then go = False
                End Select

            Loop

        Catch ex As Exception
            appEventLog_Write("error Timer2_Tick client:", ex)
        End Try
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If Not modMain.closing Then e.Cancel = True
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub pnlMain_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles pnlMain.MouseClick
        Try
            '  If frmInstructions.Visible Then
            '     pnlMainClickActionInstructions(e.X, e.Y)
            '  Else
            pnlMainClickAction(e.X, e.Y)
            '  End If

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub pnlMainClickActionInstructions(x As Integer, y As Integer)
        Try
            If cmdSubmit.Visible = False Then Exit Sub

            If nodeListInstructions(currentNode, currentPeriodInstruction).isOverPT(x, y, nodeListInstructions(currentNode, currentPeriodInstruction).pt2) Then
                selectionPt = nodeListInstructions(currentNode, currentPeriodInstruction).pt2
                selection = "down"
            ElseIf nodeListInstructions(currentNode, currentPeriodInstruction).isOverPT(x, y, nodeListInstructions(currentNode, currentPeriodInstruction).pt3) Then
                selectionPt = nodeListInstructions(currentNode, currentPeriodInstruction).pt3
                selection = "right"
            End If

            drawScreenInstructions()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub pnlMainClickAction(x As Integer, y As Integer)
        Try
            If cmdSubmit.Visible = False Then Exit Sub
            If cmdSubmit.Text <> "Submit" Then Exit Sub

            Dim tempNode As node
            Dim tempNodeList(100, 100) As node
            Dim tempTree As Integer

            If showInstructions Then
                tempNode = nodeListInstructions(currentNode, currentPeriodInstruction)
                tempNodeList = nodeListInstructions
                tempTree = currentPeriodInstruction
            Else
                tempNode = nodeList(currentNode, periods(currentPeriod).getCurrentTree())
                tempNodeList = nodeList
                tempTree = periods(currentPeriod).getCurrentTree()
            End If

            'check over payoffs
            If tempNode.payoff11 >= 0 Then
                If tempNode.isOverPT(x, y, tempNode.pt3) Then
                    selectionPt = tempNode.pt3
                    selection = "pay1"
                End If
            End If

            If tempNode.payoff21 >= 0 Then
                If tempNode.isOverPT(x, y, tempNode.pt2) Then
                    selectionPt = tempNode.pt2
                    selection = "pay2"
                End If
            End If

            If tempNode.payoff31 >= 0 Then
                If tempNode.isOverPT(x, y, tempNode.pt4) Then
                    selectionPt = tempNode.pt4
                    selection = "pay3"
                End If
            End If

            'check over sub nodes
            If tempNode.subNode1Id > 0 Then
                If tempNode.isOverPT(x, y, tempNodeList(tempNode.subNode1Id, tempTree).pt1) Then
                    selectionPt = tempNodeList(tempNode.subNode1Id, tempTree).pt1
                    selection = "sub1"
                End If
            End If

            If tempNode.subNode2Id > 0 Then
                If tempNode.isOverPT(x, y, tempNodeList(tempNode.subNode2Id, tempTree).pt1) Then
                    selectionPt = tempNodeList(tempNode.subNode2Id, tempTree).pt1
                    selection = "sub2"
                End If
            End If

            If tempNode.subNode3Id > 0 Then
                If tempNode.isOverPT(x, y, tempNodeList(tempNode.subNode3Id, tempTree).pt1) Then
                    selectionPt = tempNodeList(tempNode.subNode3Id, tempTree).pt1
                    selection = "sub3"
                End If
            End If

            'update screen
            If showInstructions Then
                drawScreenInstructions()
            Else
                drawScreen()
            End If

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub


    Private Sub cmdSubmit_Click(sender As System.Object, e As System.EventArgs) Handles cmdSubmit.Click
        Try
            If frmInstructions.Visible = True Then
                cmdSubmitActionInstruction()
            Else
                cmdSubmitAction()
            End If

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub cmdSubmitActionInstruction()
        Try
            If selection = "" Then Exit Sub

            If frmInstructions.pageDone(currentInstruction) Then Exit Sub

            If currentInstruction = 5 Then
                nodeListInstructions(1, 1).status = selection
                currentNode = 0
            ElseIf currentInstruction = 6 Then
                If selection <> "sub1" Then Exit Sub
                nodeListInstructions(1, 2).status = selection
                currentNode = 2
            ElseIf currentInstruction = 7 Then

                If selection = "sub1" Then
                    currentNode = 3
                Else
                    currentNode = 2
                End If

                nodeListInstructions(1, 3).status = selection
            End If

            cmdSubmit.Visible = False
            frmInstructions.pageDone(currentInstruction) = True
            frmInstructions.results(currentInstruction) = selection
            frmInstructions.nextInstruction()

            pnlMain.Enabled = False
            selection = ""

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub


    Public Sub cmdSubmitAction()
        Try
            If cmdSubmit.Text = "Submit" Then
                If selection = "" Then Exit Sub
            End If

            cmdSubmit.Visible = False

            Dim outstr As String = ""

            txtMessages.Text = "Waiting for others."

            If cmdSubmit.Text = "Submit" Then
                Dim ts As TimeSpan
                ts = Now - decisionStart
                outstr = selection & ";"
                outstr &= ts.TotalMilliseconds & ";"
                outstr &= periods(currentPeriod).currentTreeIndex & ";"

                selection = ""

                wskClient.Send("04", outstr)
            Else
                cmdSubmit.Text = "Submit"
                wskClient.Send("05", outstr)
            End If

            Dim a As Integer = 1
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub Timer3_Tick(sender As System.Object, e As System.EventArgs) Handles Timer3.Tick
        Try
            Timer3.Interval = rand(1500, 500)

            If frmInstructions.Visible Then
                doTestModeInstructions()
            Else
                doTestMode()
            End If


        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub
End Class
