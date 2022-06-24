Public Class frmInstructions
    Dim tempS As Boolean
    Public startPressed As Boolean

    Public pageDone(20) As Boolean
    Public results(20) As String

    Dim page5Text As String = "Person 1 earns #payoff1#." & vbCrLf & "Person 2 earns #payoff2#."
    Dim page6Text As String = "Person #person# now makes the next decision.  He or she will choose a set of payoffs."
    Dim page7Text As String = "Person #person# now makes the next decision to choose the payoffs (#payoff11#/#payoff12#) or (#payoff21#/#payoff22#)"
    Dim continueText As String = vbCrLf & vbCrLf & "Continue to the next page of instructions."

    Public startTimeOnPage As Date
    Public lastInstruction As Integer
    Public lastTimeOnPage As TimeSpan

    Public numberOfPagesMain As Integer = 8
    Public numberOfPagesNewExperiment As Integer = 2
    Public numberOfPagesLastPeriod As Integer = 1

    Public numberOfPages As Integer
    Public instructionType As String

    Public Sub nextInstruction()
        Try
            'load the next page of instructions

            If instructionType = "Main" Then
                RichTextBox1.LoadFile(Application.StartupPath &
                 "\instructions\Main\page" & currentInstruction & ".rtf")

                variablesMain()

                If currentInstruction = 5 Or currentInstruction = 6 Or currentInstruction = 7 Then
                    cmdReset.Visible = True
                Else
                    cmdReset.Visible = False
                End If
            ElseIf instructionType = "New Experiment" Then
                RichTextBox1.LoadFile(Application.StartupPath &
                 "\instructions\NewExperiment\page" & currentInstruction & ".rtf")

                variablesNewExperiment()
            Else
                'last period
                RichTextBox1.LoadFile(Application.StartupPath &
                "\instructions\LastPeriod\page" & currentInstruction & ".rtf")

                variablesLastPeriod()
            End If

            'colorize names
            RepRTBfield2Color("Person 1", Color.CornflowerBlue)
            RepRTBfield2Color("Person 2", Color.Coral)

            RepRTBfield2Color("Blue", Color.CornflowerBlue)
            RepRTBfield2Color("Orange", Color.Coral)

            Me.Text = "Instructions " & currentInstruction & "/" & numberOfPages

            RichTextBox1.SelectionStart = 1
            RichTextBox1.ScrollToCaret()

            If Not startPressed Then wskClient.Send("01", currentInstruction & ";" & lastInstruction & ";" & lastTimeOnPage.TotalMilliseconds)

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub variablesNewExperiment()
        Try
            Dim c As Integer = 0
            Dim p As Integer
            For i As Integer = currentPeriod To numberOfPeriods
                c += 1
                If periods(i).endingProbabilty <> 100 Then
                    p = periods(i).endingProbabilty
                    Exit For
                End If
            Next

            Call RepRTBfield("numberOfPeriods", c.ToString())
            Call RepRTBfield("myType", "Person " & myType(currentPeriod - 1))
            Call RepRTBfield("endingProbability", p.ToString())

            'If periods(currentPeriod).payoffMode = "Cents" Then
            '    Call RepRTBfield("earnings", FormatCurrency(CInt(frmMain.txtProfit.Text) / 100))
            'Else
            Call RepRTBfield("earnings", FormatCurrency(frmMain.txtProfit.Text))
            'End If


            Select Case currentInstruction
                Case 1
                    pageDone(currentInstruction) = True
                Case 2
                    pageDone(currentInstruction) = True
            End Select

            'Me.Text = "Instructions " & currentInstruction & "/" & numberOfPages
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub variablesLastPeriod()
        Try
            Call RepRTBfield("myType", "Person " & myType(currentPeriod - 1))

            Select Case currentInstruction
                Case 1
                    pageDone(currentInstruction) = True
                Case 2
                    pageDone(currentInstruction) = True
            End Select

            'Me.Text = "Instructions " & currentInstruction & "/" & numberOfPages
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub variablesMain()
        Try
            'load variables into instructions

            Dim tempN As Integer = 0
            Dim outstr As String = ""
            Select Case currentInstruction
                Case 1
                    'Use the following command to insert varibles into the instructions.
                    'Call RepRTBfield("playerCount-1", numberOfPlayers - 1)

                    pageDone(currentInstruction) = True
                Case 2

                    pageDone(currentInstruction) = True
                Case 3

                    pageDone(currentInstruction) = True
                Case 4

                    pageDone(currentInstruction) = True
                Case 5

                    If Not pageDone(currentInstruction) Then
                        Call RepRTBfield("Result", " ")
                        Call RepRTBfield("Continue", " ")
                        Call RepRTBfield("done", " ")
                        frmMain.cmdSubmit.Visible = True
                        currentPeriodInstruction = 1
                        frmMain.pnlMain.Enabled = True
                        currentNode = 1
                        selection = ""
                    Else
                        Call RepRTBfield("Result", page5Text)
                        Call RepRTBfield("Continue", continueText)
                        Call RepRTBfield("done", "(done)")

                        RepRTBfieldColor("#payoff1#", Color.CornflowerBlue, 0)
                        RepRTBfieldColor("#payoff2#", Color.Coral, 0)

                        If results(currentInstruction) = "pay1" Then
                            Call RepRTBfield("payoff1", returnInsructionPayoff(nodeListInstructions(1, 1).payoff11))
                            Call RepRTBfield("payoff2", returnInsructionPayoff(nodeListInstructions(1, 1).payoff12))
                        Else
                            Call RepRTBfield("payoff1", returnInsructionPayoff(nodeListInstructions(1, 1).payoff21))
                            Call RepRTBfield("payoff2", returnInsructionPayoff(nodeListInstructions(1, 1).payoff22))
                        End If
                    End If
                Case 6
                    If myType(currentPeriod) = 1 Then
                        Call RepRTBfield("circleColor", "Orange")
                    Else
                        Call RepRTBfield("circleColor", "Blue")
                    End If

                    If Not pageDone(currentInstruction) Then
                        Call RepRTBfield("Result", " ")
                        Call RepRTBfield("Continue", " ")
                        Call RepRTBfield("done", " ")

                        frmMain.cmdSubmit.Visible = True
                        currentPeriodInstruction = 2
                        frmMain.pnlMain.Enabled = True
                        currentNode = 1
                        selection = ""
                    Else
                        Call RepRTBfield("Result", page6Text)
                        Call RepRTBfield("Continue", continueText)
                        Call RepRTBfield("done", "(done)")

                        If myType(currentPeriod) = 1 Then
                            Call RepRTBfield("person", "2")
                        Else
                            Call RepRTBfield("person", "1")
                        End If
                    End If
                Case 7
                    If Not pageDone(currentInstruction) Then
                        Call RepRTBfield("Result", " ")
                        Call RepRTBfield("Continue", " ")
                        Call RepRTBfield("done", " ")

                        frmMain.cmdSubmit.Visible = True
                        currentPeriodInstruction = 3
                        frmMain.pnlMain.Enabled = True
                        currentNode = 1
                        selection = ""
                    Else
                        Call RepRTBfield("Result", page7Text)
                        Call RepRTBfield("Continue", continueText)
                        Call RepRTBfield("done", "(done)")

                        RepRTBfieldColor("#payoff11#", Color.CornflowerBlue, 0)
                        RepRTBfieldColor("#payoff12#", Color.Coral, 0)
                        RepRTBfieldColor("#payoff21#", Color.CornflowerBlue, 0)
                        RepRTBfieldColor("#payoff22#", Color.Coral, 0)

                        If results(currentInstruction) = "sub2" Then
                            Call RepRTBfield("payoff11", returnInsructionPayoff(nodeListInstructions(2, 3).payoff11))
                            Call RepRTBfield("payoff12", returnInsructionPayoff(nodeListInstructions(2, 3).payoff12))

                            Call RepRTBfield("payoff21", returnInsructionPayoff(nodeListInstructions(2, 3).payoff21))
                            Call RepRTBfield("payoff22", returnInsructionPayoff(nodeListInstructions(2, 3).payoff22))

                            Call RepRTBfield("person", myType(currentPeriod))
                            RichTextBox1.Find("Person " & myType(currentPeriod))

                            If myType(currentPeriod) = 1 Then
                                RichTextBox1.SelectionColor = Color.CornflowerBlue
                            Else
                                RichTextBox1.SelectionColor = Color.Coral
                            End If


                        Else
                            Call RepRTBfield("payoff11", returnInsructionPayoff(nodeListInstructions(3, 3).payoff11))
                            Call RepRTBfield("payoff12", returnInsructionPayoff(nodeListInstructions(3, 3).payoff12))

                            Call RepRTBfield("payoff21", returnInsructionPayoff(nodeListInstructions(3, 3).payoff21))
                            Call RepRTBfield("payoff22", returnInsructionPayoff(nodeListInstructions(3, 3).payoff22))

                            'RichTextBox1.Find(returnInsructionPayoff(nodeListInstructions(3, 3).payoff11))
                            'RichTextBox1.SelectionColor = Color.CornflowerBlue

                            'RichTextBox1.Find(returnInsructionPayoff(nodeListInstructions(3, 3).payoff21))
                            'RichTextBox1.SelectionColor = Color.CornflowerBlue

                            'RichTextBox1.Find(returnInsructionPayoff(nodeListInstructions(3, 3).payoff12))
                            'RichTextBox1.SelectionColor = Color.Coral

                            'RichTextBox1.Find(returnInsructionPayoff(nodeListInstructions(3, 3).payoff22))
                            'RichTextBox1.SelectionColor = Color.Coral

                            If myType(currentPeriod) = 1 Then
                                Call RepRTBfield("person", "2")
                            Else
                                Call RepRTBfield("person", "1")
                            End If
                        End If
                    End If
                Case 8
                    pageDone(currentInstruction) = True
                    'Call RepRTBfield("iPage8Text", iPage8Text)
            End Select


        Catch ex As Exception
            appEventLog_Write("error variables:", ex)
        End Try
    End Sub

    Public Function RepRTBfield(ByVal sField As String, ByVal sValue As String) As Boolean
        Try
            'when the instructions are loaded into the rich text box control this function will
            'replace the variable place holders with variables.

            If RichTextBox1.Find("#" & sField & "#") = -1 Then
                RichTextBox1.DeselectAll()
                Return False
            End If

            RichTextBox1.SelectedText = sValue

            Return True
        Catch ex As Exception
            appEventLog_Write("error RepRTBfield:", ex)
            Return False
        End Try
    End Function

    Public Sub RepRTBfield2(ByVal sField As String, ByVal sValue As String)
        Try
            Do While (RepRTBfield(sField, sValue))

            Loop
        Catch ex As Exception
            appEventLog_Write("error RepRTBfield:", ex)
        End Try
    End Sub

    Public Function RepRTBfieldColor(ByVal sField As String, ByVal c As Color, start As Integer) As Integer
        Try
            'when the instructions are loaded into the rich text box control this function will
            'color the specified text the specified color

            If RichTextBox1.Find(sField, start, RichTextBoxFinds.None) = -1 Then
                RichTextBox1.DeselectAll()
                Return 0
            End If

            RichTextBox1.SelectionColor = c

            Return RichTextBox1.SelectionStart
        Catch ex As Exception
            appEventLog_Write("error RepRTBfield:", ex)
            Return False
        End Try
    End Function

    Public Sub RepRTBfield2Color(ByVal sField As String, ByVal c As Color)
        Try

            Dim start As Integer = (RepRTBfieldColor(sField, c, 1))

            Dim go As Boolean

            If start = 0 Then
                go = False
            Else
                go = True
                start += 1
            End If

            Do While go
                start = (RepRTBfieldColor(sField, c, start))

                If start = 0 Then
                    go = False
                Else
                    start += 1
                End If
            Loop
        Catch ex As Exception
            appEventLog_Write("error RepRTBfield:", ex)
        End Try
    End Sub

    Public Sub setup()
        Try

            For i As Integer = 1 To 20
                pageDone(i) = False
            Next

            If instructionType = "Main" Then
                numberOfPages = numberOfPagesMain
            ElseIf instructionType = "New Experiment" Then
                numberOfPages = numberOfPagesNewExperiment
            Else
                numberOfPages = numberOfPagesLastPeriod
            End If

            If numberOfPages = 1 Then
                cmdNext.Visible = False
                cmdStart.Visible = True
            End If

            startPressed = False
            currentInstruction = 1
            nextInstruction()
            tempS = False
        Catch ex As Exception
            appEventLog_Write("error instructinos start:", ex)
        End Try
    End Sub
    Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
        Try
            startAction()
        Catch ex As Exception
            appEventLog_Write("error instructinos start:", ex)
        End Try
    End Sub

    Public Sub startAction()
        Try
            'client done with instructions
            Dim outstr As String = ""

            Dim ts As TimeSpan = Now - startTimeOnPage

            outstr = ts.TotalMilliseconds

            wskClient.Send("02", outstr)
            cmdStart.Visible = False
            startPressed = True
        Catch ex As Exception
            appEventLog_Write("error instructinos start:", ex)
        End Try
    End Sub

    Private Sub cmdNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        Try
            cmdNextAction()
        Catch ex As Exception
            appEventLog_Write("error cmdNext_Click:", ex)
        End Try
    End Sub

    Public Sub cmdNextAction()
        Try
            'load next page of instructions


            If pageDone(currentInstruction) = False Then
                MessageBox.Show("Please take the required action before continuing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If currentInstruction = numberOfPages Then Exit Sub

            lastInstruction = currentInstruction
            lastTimeOnPage = Now - startTimeOnPage
            startTimeOnPage = Now

            currentInstruction += 1

            cmdBack.Visible = True

            If currentInstruction = numberOfPages Then cmdNext.Visible = False

            If currentInstruction = numberOfPages And Not tempS Then
                cmdStart.Visible = True
                tempS = True
            End If

            nextInstruction()
        Catch ex As Exception
            appEventLog_Write("error cmdNext_Click:", ex)
        End Try
    End Sub

    Private Sub cmdBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBack.Click
        Try
            'previous page of instructions

            cmdNext.Visible = True

            If currentInstruction = 1 Then
                Exit Sub
            End If

            lastInstruction = currentInstruction
            lastTimeOnPage = Now - startTimeOnPage
            startTimeOnPage = Now

            currentInstruction -= 1

            If currentInstruction = 1 Then cmdBack.Visible = False

            nextInstruction()
        Catch ex As Exception
            appEventLog_Write("error cmdBack_Click :", ex)
        End Try
    End Sub

    Private Sub cmdReset_Click(sender As System.Object, e As System.EventArgs) Handles cmdReset.Click
        Try
            pageDone(currentInstruction) = False
            selection = ""
            results(currentInstruction) = ""

            nodeListInstructions(1, currentInstruction - 4).status = "open"
            currentNode = 1

            'RichTextBox1.LoadFile(System.Windows.Forms.Application.StartupPath &
            '     "\instructions\page" & currentInstruction & ".rtf")

            'variables()

            nextInstruction()

            RichTextBox1.SelectionStart = 1
            RichTextBox1.ScrollToCaret()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub frmInstructions_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class