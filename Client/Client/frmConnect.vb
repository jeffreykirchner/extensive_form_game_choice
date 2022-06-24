Public Class frmConnect

    Private Sub frmConnect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'load in last ip/port
            txtIP.Text = getINI(sfile, "Settings", "ip")
            txtPort.Text = getINI(sfile, "Settings", "port")
        Catch ex As Exception
            appEventLog_Write("error frmConnect_Load:", ex)
        End Try
    End Sub

    Private Sub cmdConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConnect.Click
        Try
            'try to connect to server
            Me.Hide()
            myIPAddress = Me.txtIP.Text
            myPortNumber = txtPort.Text
            connect()
            frmMain.Show()

            writeINI(sfile, "Settings", "ip", myIPAddress)
            writeINI(sfile, "Settings", "port", txtPort.Text)
        Catch ex As Exception
            appEventLog_Write("error cmdConnect_Click:", ex)
        End Try
    End Sub

    Private Sub frmConnect_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'close program on alt+k
            If e.Alt = True Then
                If CInt(e.KeyValue) = CInt(Keys.K) Then
                    If MessageBox.Show("Close Program?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.No Then Exit Sub
                    modMain.closing = True
                    frmMain.Close()
                End If
            End If
        Catch ex As Exception
            appEventLog_Write("error frmConnect_KeyDown:", ex)
        End Try
    End Sub
End Class