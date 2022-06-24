Public Class frmTestMode

    Private Sub cmdTakeControl_Click(sender As System.Object, e As System.EventArgs) Handles cmd1.Click
        Try
            Try
                If cmd1.Text = "Take Control" Then
                    cmd1.Text = "Return Control"

                    frmMain.Timer3.Enabled = False
                Else
                    cmd1.Text = "Take Control"

                    frmMain.Timer3.Enabled = True
                End If
            Catch ex As Exception
                appEventLog_Write("error :", ex)
            End Try
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub
End Class