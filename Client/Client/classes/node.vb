Public Class node
    Public id As Integer
    Public myTree As Integer

    Public myNodeList(100, 100) As node

    Public subNode1Id As Integer
    Public subNode2Id As Integer
    Public subNode3Id As Integer

    Public payoff11 As Double
    Public payoff12 As Double
    Public payoff21 As Double
    Public payoff22 As Double
    Public payoff31 As Double
    Public payoff32 As Double

    Public owner As Integer
    Public pt1 As Point
    Public pt2 As Point
    Public pt3 As Point
    Public pt4 As Point

    Public status As String

    Dim f1 As New Font("Calibri", 40, FontStyle.Bold)
    Dim f2 As New Font("Calibri", 16, FontStyle.Bold)
    Dim f3 As New Font("Calibri", 10, FontStyle.Bold)

    Dim p1 As New Pen(Brushes.Black, 8)
    Dim p2 As New Pen(Brushes.CornflowerBlue, 8)
    Dim p3 As New Pen(Brushes.Coral, 8)
    Dim p4 As New Pen(Brushes.Black, 8)
    Dim p5 As New Pen(Brushes.LightGray, 8)

    Dim fmt As New StringFormat 'center alignment
    Public myColor As Color

    Public Sub New(ByRef myNodeList(,) As node)
        Try
            fmt.Alignment = StringAlignment.Center
            p1.EndCap = Drawing2D.LineCap.ArrowAnchor
            p1.Alignment = Drawing2D.PenAlignment.Center

            p2.EndCap = Drawing2D.LineCap.ArrowAnchor
            p2.Alignment = Drawing2D.PenAlignment.Center

            p3.EndCap = Drawing2D.LineCap.ArrowAnchor
            p3.Alignment = Drawing2D.PenAlignment.Center

            p4.Alignment = Drawing2D.PenAlignment.Center
            p4.EndCap = Drawing2D.LineCap.Triangle

            p5.Alignment = Drawing2D.PenAlignment.Center
            p5.EndCap = Drawing2D.LineCap.ArrowAnchor

            Me.myNodeList = myNodeList
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Dim tempP As Pen
    Public Sub drawNode(g As Graphics)
        Try

            Dim tempOffset As Integer = 30

            'payoffs
            If payoff11 >= 0 And payoff12 >= 0 Then

                If status = "dead" Or
                   InStr(status, "sub") Or
                   status = "pay2" Or
                   status = "pay3" Then

                    drawPayoff(pt3, g, payoff11, payoff12, Brushes.LightGray)
                Else

                    drawPayoff(pt3, g, payoff11, payoff12, Brushes.Black)
                End If
            End If

            If payoff21 >= 0 And payoff22 >= 0 Then

                If status = "dead" Or
                   InStr(status, "sub") Or
                   status = "pay1" Or
                   status = "pay3" Then

                    drawPayoff(pt2, g, payoff21, payoff22, Brushes.LightGray)
                Else

                    drawPayoff(pt2, g, payoff21, payoff22, Brushes.Black)
                End If
            End If

            If payoff31 >= 0 And payoff32 >= 0 Then

                If status = "dead" Or
                   InStr(status, "sub") Or
                   status = "pay1" Or
                   status = "pay2" Then

                    drawPayoff(pt4, g, payoff31, payoff32, Brushes.LightGray)
                Else

                    drawPayoff(pt4, g, payoff31, payoff32, Brushes.Black)
                End If
            End If

            'If status = "down" Then
            '    If subNodeCount >= 1 Then
            '        tempOffset = 0
            '    End If
            'ElseIf status = "right" Then
            '    If subNodeCount = 2 Then
            '        tempOffset = 0
            '    End If
            'End If

            'If subNodeCount = 3 Then

            'ElseIf subNodeCount = 2 Then

            '    'right
            '    If payoffCount = 1 Then
            '        drawPayoff(pt3, g, payoff11, payoff12)
            '    End If

            'ElseIf subNodeCount = 1 Then

            '    'draw pay off right
            '    drawPayoff(pt3, g, payoff11, payoff12)

            '    'down
            '    If payoffCount = 2 Then
            '        drawPayoff(pt2, g, payoff21, payoff22)
            '    End If
            'Else
            '    'payoff right
            '     drawPayoff(pt3, g, payoff11, payoff12)

            '    If payoffCount >= 2 Then
            '        'payoff bottom
            '          drawPayoff(pt2, g, payoff21, payoff22)
            '    End If

            '    If payoffCount = 3 Then
            '        drawPayoff(pt4, g, payoff31, payoff32)
            '    End If
            'End If

            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            g.FillEllipse(New SolidBrush(myColor), New Rectangle(pt1.X - 25, pt1.Y - 25, 50, 50))

            g.DrawEllipse(Pens.Black, New Rectangle(pt1.X - 25, pt1.Y - 25, 50, 50))

            g.SmoothingMode = Drawing2D.SmoothingMode.None

            g.DrawString(owner, f2, Brushes.Black, pt1.X - g.MeasureString(owner, f2).Width / 2, pt1.Y - 15)
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub drawConnection(startPt As Point, endPt As Point, g As Graphics, tempP As Pen)
        Try

            Dim d As Double = Math.Sqrt((endPt.X - startPt.X) ^ 2 + (endPt.Y - startPt.Y) ^ 2)
            Dim r As Double = 28 / d

            Dim x3 As Integer = Math.Round(r * startPt.X + (1 - r) * endPt.X)
            Dim y3 As Integer = Math.Round(r * startPt.Y + (1 - r) * endPt.Y)

            g.DrawLine(tempP, startPt.X, startPt.Y, x3, y3)
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub drawPayoff(startPt As Point, g As Graphics, payOffTop As String, payOffBottom As String, tempB As Brush)
        Try
            If showInstructions Then
                payOffTop = returnInsructionPayoff(payOffTop)
                payOffBottom = returnInsructionPayoff(payOffBottom)
            End If

            g.DrawString(getPayoffParenthesis(payOffTop, payOffBottom), f1, tempB, startPt.X, startPt.Y - g.MeasureString("(  )", f1).Height / 2, fmt)

            If periods(currentPeriod).payoffMode = "Dollars" Then
                payOffTop = "$" & payOffTop
                payOffBottom = "$" & payOffBottom
            ElseIf periods(currentPeriod).payoffMode = "Cents" Then
                payOffTop = payOffTop & "¢"
                payOffBottom = payOffBottom & "¢"
            End If

            g.DrawString(payOffTop, f2, Brushes.CornflowerBlue, startPt.X, startPt.Y - 22, fmt)
            g.DrawString(payOffBottom, f2, Brushes.Coral, startPt.X, startPt.Y - 2, fmt)
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Function getPayoffParenthesis(payOffTop As String, payOffBottom As String) As String
        Try
            Dim tempN2 As Integer = Len(payOffTop)
            Dim tempN3 As Integer = Len(payOffBottom)

            If InStr(payOffTop, ".") > 0 Then
                tempN2 -= 1
            End If

            If InStr(payOffBottom, ".") > 0 Then
                tempN3 -= 1
            End If

            Dim tempN1 As Integer = Math.Max(tempN2, tempN3)

            Dim temps As String = "("

            For i As Integer = 1 To tempN1 + 1 'for $,cents sign
                temps &= " "
            Next

            temps &= ")"

            Return temps

        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return ""
        End Try
    End Function


    Public Sub drawNodeArrows(g As Graphics, currentTree As Integer)
        Try

            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            If currentNode = id And status = "open" Then
                If myColor = Color.CornflowerBlue Then
                    tempP = p2
                Else
                    tempP = p3
                End If

                'colored choice arrow
                'ElseIf status = "down" Or status = "right" Then
                '    tempP = p4
                '    'chosen path arrow
                'ElseIf status = "dead" Then
                '    tempP = p5
                '    'gray arrow
            ElseIf status = "open" Then
                tempP = p1
                'standard black
            Else
                tempP = p5
            End If

            Dim tempNodeCount As Integer

            If showInstructions Then
                tempNodeCount = nodeCountInstructions(currentTree)
            Else
                tempNodeCount = nodeCount(currentTree)
            End If

            'draw sub node arrows
            'right
            If tickTock = 4 Or currentNode <> id Then
                If subNode1Id > 0 Then
                    If tempNodeCount >= subNode1Id Then

                        If status = "sub1" Then
                            drawConnection(pt1, myNodeList(subNode1Id, myTree).pt1, g, p4)
                        Else
                            drawConnection(pt1, myNodeList(subNode1Id, myTree).pt1, g, tempP)
                        End If

                    End If
                End If
            End If

            'down
            If tickTock = 5 Or currentNode <> id Then
                If subNode2Id > 0 Then
                    If tempNodeCount >= subNode2Id Then

                        If status = "sub2" Then
                            drawConnection(pt1, myNodeList(subNode2Id, myTree).pt1, g, p4)
                        Else
                            drawConnection(pt1, myNodeList(subNode2Id, myTree).pt1, g, tempP)
                        End If


                    End If
                End If
            End If

            'left
            If tickTock = 6 Or currentNode <> id Then
                If subNode3Id > 0 Then
                    If tempNodeCount >= subNode3Id Then

                        If status = "sub3" Then
                            drawConnection(pt1, myNodeList(subNode3Id, myTree).pt1, g, p4)
                        Else
                            drawConnection(pt1, myNodeList(subNode3Id, myTree).pt1, g, tempP)
                        End If

                    End If
                End If
            End If

            'draw payoff arrows
            'right
            If tickTock = 1 Or currentNode <> id Then
                If payoff11 >= 0 And payoff12 >= 0 Then

                    Dim tempD As Double = g.MeasureString(getPayoffParenthesis(payoff11, payoff12), f1).Width

                    If status = "pay1" Then
                        g.DrawLine(p4, pt1.X, pt1.Y, CInt(pt3.X - Math.Round(tempD / 2) + 10), pt3.Y)
                    Else

                        g.DrawLine(tempP, pt1.X, pt1.Y, CInt(pt3.X - Math.Round(tempD / 2) + 10), pt3.Y)
                    End If


                End If
            End If

            'down
            If tickTock = 2 Or currentNode <> id Then
                If payoff21 >= 0 And payoff22 >= 0 Then

                    If status = "pay2" Then
                        g.DrawLine(p4, pt1.X, pt1.Y, pt2.X, pt2.Y - 25)
                    Else
                        g.DrawLine(tempP, pt1.X, pt1.Y, pt2.X, pt2.Y - 25)
                    End If

                End If
            End If

            'left
            If tickTock = 3 Or currentNode <> id Then
                If payoff31 >= 0 And payoff32 >= 0 Then

                    Dim tempD As Double = g.MeasureString(getPayoffParenthesis(payoff31, payoff32), f1).Width

                    If status = "pay3" Then
                        g.DrawLine(p4, pt1.X, pt1.Y, CInt(pt4.X + Math.Round(tempD / 2) - 10), pt4.Y)
                    Else
                        g.DrawLine(tempP, pt1.X, pt1.Y, CInt(pt4.X + Math.Round(tempD / 2) - 10), pt4.Y)
                    End If

                End If
            End If

            g.SmoothingMode = Drawing2D.SmoothingMode.None

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Function isOverPT(x As Integer, y As Integer, pt As Point) As Boolean
        If x >= pt.X - 25 And x <= pt.X + 25 And y >= pt.Y - 25 And y <= pt.Y + 25 Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
