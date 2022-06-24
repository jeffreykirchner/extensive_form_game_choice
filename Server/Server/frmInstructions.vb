Public Class frmInstructions

    Private Sub frmInstructions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            currentInstruction = 1
            nextInstruction()
        Catch ex As Exception
            appEventLog_Write("error frmInstructions_Load:", ex)
        End Try
    End Sub

    Public Sub nextInstruction()
        Try
            RichTextBox1.LoadFile(System.Windows.Forms.Application.StartupPath & _
                 "\instructions\page" & currentInstruction & ".rtf")

            variables()

            RichTextBox1.SelectionStart = 1
            RichTextBox1.ScrollToCaret()
        Catch ex As Exception
            appEventLog_Write("error nextInstruction:", ex)
        End Try
    End Sub

    Public Sub variables()
        Try
            Dim outstr As String = ""
            Select Case currentInstruction
                Case 1
                    'Call RepRTBfield("numberOfSellers", CStr(numberOfSellers))
                    'Call RepRTBfield("numberOfBuyers", CStr(numberOfBuyers))
                    'Call RepRTBfield("periodLength", CStr(periodLength))
                Case 2



                Case 3

                Case 4

            End Select
        Catch ex As Exception
            appEventLog_Write("error variables:", ex)
        End Try
    End Sub

    Public Sub RepRTBfield(ByVal sField As String, ByVal sValue As String)
        Try
            'when the instructions are loaded into the rich text box control this function will
            'replace the variable place holders with variables.

            RichTextBox1.Find("#" & sField & "#")
            RichTextBox1.SelectedText = sValue
        Catch ex As Exception
            appEventLog_Write("error RepRTBfield:", ex)
        End Try
    End Sub


    Private Sub cmdNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        Try
            If currentInstruction = 3 Then Exit Sub

            'Dim i As Integer

            currentInstruction += 1

            'For i = 1 To numberOfPlayers
            '    playerList(i).nextInstruction()
            'Next

            nextInstruction()
        Catch ex As Exception
            appEventLog_Write("error cmdNext_Click:", ex)
        End Try

    End Sub

    Private Sub cmdBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBack.Click
        Try
            If currentInstruction = 1 Then Exit Sub

            'Dim i As Integer

            currentInstruction -= 1

            'For i = 1 To numberOfPlayers
            '    playerList(i).nextInstruction()
            'Next

            nextInstruction()
        Catch ex As Exception
            appEventLog_Write("error cmdBack_Click:", ex)
        End Try
    End Sub

    Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
        Try
            showInstructions = False

            For i As Integer = 1 To numberOfPlayers
                playerList(i).sendBegin()
            Next

            Me.Close()
        Catch ex As Exception
            appEventLog_Write("error cmdStart_Click:", ex)
        End Try
    End Sub
End Class