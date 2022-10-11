Public Class frmContinue
    Private Sub cmdContinue_Click(sender As Object, e As EventArgs) Handles cmdContinue.Click
        Try

            If getNumberOfActivePlayers() + checkin < numberOfPlayers Then
                Me.Height = 225
                lblError.Visible = True
                Exit Sub
            End If

            checkin = 0

            For i As Integer = 1 To numberOfPlayers
                playerList(i).sendFinishChoicePeriod()
            Next

            periodStart = Now
            Me.Close()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

End Class