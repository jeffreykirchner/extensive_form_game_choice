Imports System.Drawing.Drawing2D

Public Class frmChoice

    Public p1_dash As New Pen(Brushes.Black, 1)
    Public fmt As New StringFormat 'center alignment
    Public fmt2 As New StringFormat 'right alignment

    Public mainScreen As screen
    Public flipScreen As Boolean = False

    Private Sub frmChoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            setTriangleEndCap(p1_dash)


            p1_dash.DashStyle = DashStyle.Dash

            mainScreen = New screen(pnlMain, New Rectangle(0, 0, pnlMain.Width, pnlMain.Height))

            Timer1.Enabled = True

            RichTextBox1.LoadFile(Application.StartupPath &
                 "\instructions\Choice\page1.rtf")

            If rand(2, 1) = 1 Then
                flipScreen = True
            Else
                flipScreen = False
            End If

            RepRTBfield2("noMatchPayment", FormatCurrency(noMatchEarnings))
        Catch ex As Exception
            appEventLog_Write("error begin:", ex)
        End Try

    End Sub

    Public Sub setTriangleEndCap(ByRef p As Pen)
        Try
            p.EndCap = LineCap.Triangle
            p.StartCap = LineCap.Triangle
            p.Alignment = PenAlignment.Center
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            drawScreen()
        Catch ex As Exception
            appEventLog_Write("error begin:", ex)
        End Try
    End Sub

    Public Sub drawScreen()
        Try
            mainScreen.erase1()
            Dim g As Graphics = mainScreen.GetGraphics

            If rbLeft.Checked Then
                g.FillRectangle(Brushes.LightYellow, New Rectangle(0, 0, pnlMain.Width / 2, pnlMain.Height))
            ElseIf rbRight.Checked Then
                g.FillRectangle(Brushes.LightYellow, New Rectangle(pnlMain.Width / 2, 0, pnlMain.Width / 2, pnlMain.Height))
            End If

            g.DrawLine(p1_dash, New Point(pnlMain.Width / 2, 0), New Point(pnlMain.Width / 2, pnlMain.Height))

            g.TranslateTransform(-143, 0)

            Dim tempTree As Integer

            If Not flipScreen Then
                tempTree = periods(currentPeriod).trees(1)
            Else
                tempTree = periods(currentPeriod).trees(2)
            End If

            If tempTree <> -1 Then
                For i As Integer = 1 To nodeCount(tempTree)
                    If nodeList(i, tempTree) IsNot Nothing Then
                        nodeList(i, tempTree).drawNodeArrows(g, tempTree)
                    End If
                Next

                For i As Integer = 1 To nodeCount(tempTree)
                    If nodeList(i, tempTree) IsNot Nothing Then
                        nodeList(i, tempTree).drawNode(g)
                    End If
                Next
            End If

            g.ResetTransform()

            g.TranslateTransform(414, 0)

            If Not flipScreen Then
                tempTree = periods(currentPeriod).trees(2)
            Else
                tempTree = periods(currentPeriod).trees(1)
            End If

            If tempTree <> -1 Then
                For i As Integer = 1 To nodeCount(tempTree)
                    If nodeList(i, tempTree) IsNot Nothing Then
                        nodeList(i, tempTree).drawNodeArrows(g, tempTree)
                    End If
                Next

                For i As Integer = 1 To nodeCount(tempTree)
                    If nodeList(i, tempTree) IsNot Nothing Then
                        nodeList(i, tempTree).drawNode(g)
                    End If
                Next
            End If

            g.ResetTransform()


            mainScreen.flip()
        Catch ex As Exception
            appEventLog_Write("error begin:", ex)
        End Try
    End Sub

    Private Sub rbLeft_CheckedChanged(sender As Object, e As EventArgs) Handles rbLeft.CheckedChanged
        Try
            drawScreen()
        Catch ex As Exception
            appEventLog_Write("error begin:", ex)
        End Try
    End Sub

    Private Sub rbRight_CheckedChanged(sender As Object, e As EventArgs) Handles rbRight.CheckedChanged
        Try
            drawScreen()
        Catch ex As Exception
            appEventLog_Write("error begin:", ex)
        End Try
    End Sub

    Private Sub cmdSubmit_Click(sender As Object, e As EventArgs) Handles cmdSubmit.Click
        Try
            If Not rbLeft.Checked And Not rbRight.Checked Then Exit Sub

            Dim str As String = ""

            If Not flipScreen Then
                If rbLeft.Checked Then
                    str &= "1;"
                Else
                    str &= "2;"
                End If
            Else
                If rbLeft.Checked Then
                    str &= "2;"
                Else
                    str &= "1;"
                End If
            End If

            rbLeft.Enabled = False
            rbRight.Enabled = False
            cmdSubmit.Visible = False

            lbl1.Text = "Waiting for others."

            wskClient.Send("06", str)
        Catch ex As Exception
            appEventLog_Write("error begin:", ex)
        End Try
    End Sub

    Private Sub pnlMain_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlMain.MouseDown
        Try
            If Not cmdSubmit.Visible Then Exit Sub

            If e.X <= pnlMain.Width / 2 Then
                rbLeft.Checked = True
            Else
                rbRight.Checked = True
            End If
        Catch ex As Exception
            appEventLog_Write("error begin:", ex)
        End Try
    End Sub

    Private Sub frmChoice_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try

            'if ALT+K are pressed kill the client
            'if ALT+Q are pressed bring up connection box
            If e.Alt = True Then
                If CInt(e.KeyValue) = CInt(Keys.K) Then
                    If MessageBox.Show("Close Program?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.No Then Exit Sub
                    modMain.closing = True
                    frmMain.Close()
                ElseIf CInt(e.KeyValue) = CInt(Keys.Q) Then
                    frmConnect.Show()
                End If
            End If
        Catch ex As Exception
            appEventLog_Write("error frmChat_KeyDown:", ex)
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
End Class