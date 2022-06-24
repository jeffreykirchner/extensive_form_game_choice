Public Class frmSetup3_1
    Public currentNode As Integer
    Public currentTree As Integer

    Private Sub cmdDone_Click(sender As System.Object, e As System.EventArgs) Handles cmdDone.Click
        Try
            With frmSetup3

                nodeList(currentNode, currentTree).payoff11 = txtTop1.Text
                nodeList(currentNode, currentTree).payoff12 = txtBottom1.Text

                nodeList(currentNode, currentTree).payoff21 = txtTop2.Text
                nodeList(currentNode, currentTree).payoff22 = txtBottom2.Text

                nodeList(currentNode, currentTree).payoff31 = txtTop3.Text
                nodeList(currentNode, currentTree).payoff32 = txtBottom3.Text

                nodeList(currentNode, currentTree).owner = nudOwner.Value
                nodeList(currentNode, currentTree).id = currentNode


                nodeList(currentNode, currentTree).pt3 =
                    New Point(nodeList(currentNode, currentTree).pt1.X + 100, nodeList(currentNode, currentTree).pt1.Y)

                nodeList(currentNode, currentTree).subNode1Id = txtSubNode1.Text


                nodeList(currentNode, currentTree).pt2 =
                    New Point(nodeList(currentNode, currentTree).pt1.X, nodeList(currentNode, currentTree).pt1.Y + 100)

                nodeList(currentNode, currentTree).subNode2Id = txtSubNode2.Text

                nodeList(currentNode, currentTree).pt4 =
                    New Point(nodeList(currentNode, currentTree).pt1.X - 100, nodeList(currentNode, currentTree).pt1.Y)

                nodeList(currentNode, currentTree).subNode3Id = txtSubNode3.Text

                nodeList(currentNode, currentTree).sortValue = txtSortValue.Text
                nodeList(currentNode, currentTree).sortValue1 = txtSortValue1.Text
                nodeList(currentNode, currentTree).sortValue2 = txtSortValue2.Text
                nodeList(currentNode, currentTree).sortValue3 = txtSortValue3.Text

            End With


            Me.Close()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub frmSetup3_1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

   
End Class