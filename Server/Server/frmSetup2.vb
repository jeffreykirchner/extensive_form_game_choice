Public Class frmSetup2

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try

            For i As Integer = 1 To DataGridView2.RowCount
                Dim str As String = ""

                DataGridView2(0, i - 1).Value = i

                For j As Integer = 2 To DataGridView2.ColumnCount
                    str &= DataGridView2(j - 1, i - 1).Value & ";"
                Next

                writeINI(sfile, "periods", CStr(i), str)
            Next

            Me.Close()
        Catch ex As Exception
            appEventLog_Write("error:", ex)
        End Try
    End Sub

    Private Sub frmPeriodSetup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            DataGridView2.RowCount = numberOfPeriods

            For i As Integer = 1 To DataGridView2.RowCount

                Dim msgtokens() As String = getINI(sfile, "periods", CStr(i)).Split(";")
                Dim nextToken As Integer = 0

                DataGridView2(0, i - 1).Value = i

                If msgtokens.Length > 2 Then

                    For j As Integer = 2 To DataGridView2.ColumnCount
                        DataGridView2(j - 1, i - 1).Value = msgtokens(nextToken)
                        nextToken += 1
                    Next
                End If
            Next

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try

    End Sub

    Private Sub cmdCopyDown_Click(sender As System.Object, e As System.EventArgs) Handles cmdCopyDown.Click
        Try
            Dim tempValue As String = DataGridView2.SelectedCells(0).Value
            Dim tempIndex As Integer = DataGridView2.CurrentRow.Index

            For i As Integer = tempIndex + 1 To DataGridView2.RowCount - 1
                DataGridView2(DataGridView2.SelectedCells(0).ColumnIndex, i).Value = tempValue
            Next
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub
End Class