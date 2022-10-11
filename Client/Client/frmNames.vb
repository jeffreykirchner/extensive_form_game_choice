Public Class frmNames

    Private Sub cmdSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSubmit.Click
        Try
            'submit name at end of experiment

            cmdSubmitAction(True)
        Catch ex As Exception
            appEventLog_Write("error cmdSubmit_Click:", ex)
        End Try
    End Sub

    Public Sub cmdSubmitAction(showPopups As Boolean)
        Try
            'submit name at end of experiment

            Dim outstr As String
            Dim temps As String

            If txtName1.Text = "" Then
                Exit Sub
            End If

            outstr = txtName1.Text & ";"
            outstr &= txtName2.Text & ";"

            wskClient.Send("07", outstr)

            temps = frmMain.txtProfit.Text

            temps = CDbl(temps)

            frmMain.txtMessages.Text = "Your earnings are " & FormatCurrency(temps) & "."

            'show client their total earnings
            If showPopups Then MessageBox.Show("Your earnings are " & FormatCurrency(temps) & ".", "Earnings", MessageBoxButtons.OK, MessageBoxIcon.Information)

            frmMain.Timer3.Enabled = False

            Me.Close()
        Catch ex As Exception
            appEventLog_Write("error cmdSubmit_Click:", ex)
        End Try
    End Sub
End Class