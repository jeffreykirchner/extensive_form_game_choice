Public Class frmSetup3_2

    Private Sub cmdConnect_Click(sender As System.Object, e As System.EventArgs) Handles cmdConnect.Click
        Try
            With frmSetup3
                'If cboSource.Text = "" Then
                '    MessageBox.Show("Source Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    Exit Sub
                'End If

                'If cboTarget.Text = "" Then
                '    MessageBox.Show("Target Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    Exit Sub
                'End If

                'If cboTarget.Text = cboSource.Text Then
                '    MessageBox.Show("Target Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    Exit Sub
                'End If

                'Dim tempSource As Integer = cboSource.SelectedIndex + 1
                'Dim tempTarget As Integer = cboTarget.SelectedIndex + 1

                'If rbRight.Checked And .nodeList(tempSource, .currentPeriod).subNodeCount <= 1 Then
                '    MessageBox.Show("Right Direction Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    Exit Sub
                'End If

                'If rbBottom.Checked And .nodeList(tempSource, .currentPeriod).subNodeCount = 0 Then
                '    MessageBox.Show("Right Direction Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    Exit Sub
                'End If

                'If rbLeft.Checked Then
                '    .nodeList(tempSource, .currentPeriod).subNode3Id = tempTarget
                'ElseIf rbRight.Checked Then
                '    .nodeList(tempSource, .currentPeriod).subNode1Id = tempTarget
                'Else
                '    .nodeList(tempSource, .currentPeriod).subNode2Id = tempTarget
                'End If




                Me.Close()
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub
End Class