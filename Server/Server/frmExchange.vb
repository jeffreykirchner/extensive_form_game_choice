Public Class frmExchange
    'allows user to edit the exchange rates

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            save()

            Me.Close()
        Catch ex As Exception
            appEventLog_Write("error cmdSave_Click:", ex)
        End Try
    End Sub

    Public Sub save()
        Try
            Dim outstr As String

            'write parameters to server.ini from datagridview1
            For i As Integer = 1 To numberOfPlayers
                outstr = DataGridView1.Rows(i - 1).Cells(1).Value

                writeINI(sfile, "exchangeRate", CStr(i), outstr)
            Next
        Catch ex As Exception
            appEventLog_Write("error save exchange:", ex)
        End Try
    End Sub

    Private Sub frmbuyers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            reLoad()

        Catch ex As Exception
            appEventLog_Write("error frmbuyers_Load:", ex)
        End Try
    End Sub

    Public Sub reLoad()
        Try
            Dim i As Integer

            DataGridView1.RowCount = numberOfPlayers

            'load paremeters from server.ini
            For i = 1 To numberOfPlayers
                DataGridView1.Rows(i - 1).Cells(0).Value = CStr(i)
                DataGridView1.Rows(i - 1).Cells(1).Value = getINI(sfile, "exchangeRate", CStr(i))
            Next
        Catch ex As Exception
            appEventLog_Write("error reLoad:", ex)
        End Try
    End Sub
End Class