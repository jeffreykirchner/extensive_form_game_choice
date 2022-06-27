Public Class node
    Public id As Integer
    Public myTree As Integer

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

    Public pt1 As Point             'node location
    Public pt2 As Point
    Public pt3 As Point
    Public pt4 As Point

    'display counts
    Public count As Integer
    Public countRight As Integer
    Public countDown As Integer
    Public countLeft As Integer

    Public status As String

    Dim f1 As New Font("Calibri", 40, FontStyle.Bold)
    Dim f2 As New Font("Calibri", 16, FontStyle.Bold)
    Dim f3 As New Font("Calibri", 10, FontStyle.Bold)
    Dim fmt As New StringFormat 'center alignment

    Dim p1 As New Pen(Brushes.Black, 8)
    Public Sub New()
        Try
            fmt.Alignment = StringAlignment.Center

            count = 0
            countRight = 0
            countDown = 0
            countLeft = 0

            p1.EndCap = Drawing2D.LineCap.ArrowAnchor
            p1.Alignment = Drawing2D.PenAlignment.Center
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Overrides Function toString() As String
        Try
            Dim outstr As String = ""

            outstr &= id & ";"
            outstr &= subNode1Id & ";"
            outstr &= subNode2Id & ";"
            outstr &= subNode3Id & ";"
            outstr &= payoff11 & ";"
            outstr &= payoff12 & ";"
            outstr &= payoff21 & ";"
            outstr &= payoff22 & ";"
            outstr &= payoff31 & ";"
            outstr &= payoff32 & ";"
            outstr &= owner & ";"
            outstr &= pt1.X & ";"
            outstr &= pt1.Y & ";"
            outstr &= pt2.X & ";"
            outstr &= pt2.Y & ";"
            outstr &= pt3.X & ";"
            outstr &= pt3.Y & ";"
            outstr &= pt4.X & ";"
            outstr &= pt4.Y & ";"

            Return outstr
        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return ""
        End Try
    End Function

    Public Sub fromString(ByVal sinstr As String)
        Try
            Dim msgtokens() As String = sinstr.Split(";")
            Dim nextToken As Integer = 0

            id = msgtokens(nextToken)
            nextToken += 1

            subNode1Id = msgtokens(nextToken)
            nextToken += 1

            subNode2Id = msgtokens(nextToken)
            nextToken += 1

            subNode3Id = msgtokens(nextToken)
            nextToken += 1

            payoff11 = msgtokens(nextToken)
            nextToken += 1

            payoff12 = msgtokens(nextToken)
            nextToken += 1

            payoff21 = msgtokens(nextToken)
            nextToken += 1

            payoff22 = msgtokens(nextToken)
            nextToken += 1

            payoff31 = msgtokens(nextToken)
            nextToken += 1

            payoff32 = msgtokens(nextToken)
            nextToken += 1

            owner = msgtokens(nextToken)
            nextToken += 1

            pt1.X = msgtokens(nextToken)
            nextToken += 1

            pt1.Y = msgtokens(nextToken)
            nextToken += 1

            pt2.X = msgtokens(nextToken)
            nextToken += 1

            pt2.Y = msgtokens(nextToken)
            nextToken += 1

            pt3.X = msgtokens(nextToken)
            nextToken += 1

            pt3.Y = msgtokens(nextToken)
            nextToken += 1

            pt4.X = msgtokens(nextToken)
            nextToken += 1

            pt4.Y = msgtokens(nextToken)
            nextToken += 1

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub drawNode(g As Graphics, drawCount As Boolean, Optional drawId As Boolean = True)
        Try
            With frmMain

                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                'draw sub node arrows
                If subNode1Id > 0 Then
                    If nodeCount(myTree) >= subNode1Id Then
                        drawConnection(pt1, nodeList(subNode1Id, myTree).pt1, g)
                    End If
                End If

                If subNode2Id > 0 Then
                    If nodeCount(myTree) >= subNode2Id Then
                        drawConnection(pt1, nodeList(subNode2Id, myTree).pt1, g)
                    End If
                End If

                If subNode3Id > 0 Then
                    If nodeCount(myTree) >= subNode3Id Then
                        drawConnection(pt1, nodeList(subNode3Id, myTree).pt1, g)
                    End If
                End If

                'draw payoffs
                If payoff11 >= 0 And payoff12 >= 0 Then
                    Dim tempD As Double = g.MeasureString(getPayoffParenthesis(payoff11, payoff12), f1).Width

                    g.DrawLine(p1, pt1.X, pt1.Y, CInt(pt3.X - Math.Round(tempD / 2) + 10), pt3.Y)
                    drawPayoff(pt3, g, payoff11, payoff12)
                End If

                If payoff21 >= 0 And payoff22 >= 0 Then
                    g.DrawLine(p1, pt1.X, pt1.Y, pt2.X, pt2.Y - 25)
                    drawPayoff(pt2, g, payoff21, payoff22)
                End If

                If payoff31 >= 0 And payoff32 >= 0 Then
                    Dim tempD As Double = g.MeasureString(getPayoffParenthesis(payoff31, payoff32), f1).Width

                    g.DrawLine(p1, pt1.X, pt1.Y, CInt(pt4.X + Math.Round(tempD / 2) - 10), pt4.Y)
                    drawPayoff(pt4, g, payoff31, payoff32)
                End If

                g.SmoothingMode = Drawing2D.SmoothingMode.None

                If drawCount Then
                    g.DrawString(count, f2, Brushes.DimGray, pt1.X - 30, pt1.Y - 40, fmt)

                    If payoff11 >= 0 Then
                        g.DrawString(countRight, f2, Brushes.DimGray, pt3.X - 40, pt3.Y - 40, fmt)
                    End If

                    If payoff21 >= 0 Then
                        g.DrawString(countDown, f2, Brushes.DimGray, pt2.X - 40, pt2.Y - 40, fmt)
                    End If

                    If payoff31 >= 0 Then
                        g.DrawString(countLeft, f2, Brushes.DimGray, pt4.X - 40, pt4.Y - 40, fmt)
                    End If
                ElseIf drawId Then
                    g.DrawString("(" & id & ")", f3, Brushes.DimGray, pt1.X - 40, pt1.Y - 40)
                End If

                'If subNodeCount = 3 Then

                'ElseIf subNodeCount = 2 Then
                '    'draw arrows to the sub nodes

                '    g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                '    If subNode2Id > 0 Then
                '        If pt1.Y + 100 <= pt2.Y Then
                '            g.DrawLine(p1, pt1.X, pt1.Y, pt2.X, pt2.Y - 30)
                '        Else
                '            g.DrawLine(p1, pt1.X, pt1.Y, pt2.X - 30, pt2.Y)
                '        End If

                '    End If

                '    If subNode3Id > 0 Then
                '        If pt1.Y + 100 <= pt4.Y Then
                '            g.DrawLine(p1, pt1.X, pt1.Y, pt4.X, pt4.Y - 30)
                '        Else
                '            g.DrawLine(p1, pt1.X, pt1.Y, pt4.X - 30, pt4.Y)
                '        End If
                '    End If

                '    If payoffCount = 1 Then
                '        g.DrawLine(p1, pt1.X, pt1.Y, pt3.X - 30, pt3.Y)
                '        drawPayoff(pt3, g, payoff11, payoff12)
                '    End If

                '    g.SmoothingMode = Drawing2D.SmoothingMode.None
                'ElseIf subNodeCount = 1 Then
                '    'draw arrow to sub node as needed

                '    'draw pay off right
                '    drawPayoff(pt3, g, payoff11, payoff12)

                '    If payoffCount = 2 Then
                '        drawPayoff(pt2, g, payoff21, payoff22)
                '    End If

                '    g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                '    g.DrawLine(p1, pt1.X, pt1.Y, pt3.X - 30, pt3.Y)

                '    If payoffCount = 2 Then
                '        g.DrawLine(p1, pt1.X, pt1.Y, pt2.X, pt2.Y - 25)
                '    End If

                '    If subNode3Id > 0 Then
                '        g.DrawLine(p1, pt1.X, pt1.Y, pt4.X, pt4.Y - 30)
                '    End If

                '    g.SmoothingMode = Drawing2D.SmoothingMode.None
                'Else
                '    'payoff right
                '    drawPayoff(pt3, g, payoff11, payoff12)

                '    If payoffCount >= 2 Then
                '        'payoff bottom
                '        drawPayoff(pt2, g, payoff21, payoff22)
                '    End If

                '    If payoffCount = 3 Then
                '        drawPayoff(pt4, g, payoff31, payoff32)
                '    End If

                '    g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                '    g.DrawLine(p1, pt1.X, pt1.Y, pt3.X - 30, pt3.Y)

                '    If payoffCount >= 2 Then
                '        g.DrawLine(p1, pt1.X, pt1.Y, pt2.X, pt2.Y - 25)
                '    End If

                '    If payoffCount = 3 Then
                '        g.DrawLine(p1, pt1.X, pt1.Y, pt4.X + 30, pt4.Y)
                '    End If

                '    g.SmoothingMode = Drawing2D.SmoothingMode.None
                'End If

                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                If owner = 1 Then
                    g.FillEllipse(Brushes.CornflowerBlue, New Rectangle(pt1.X - 25, pt1.Y - 25, 50, 50))
                Else
                    g.FillEllipse(Brushes.Coral, New Rectangle(pt1.X - 25, pt1.Y - 25, 50, 50))
                End If

                g.DrawEllipse(Pens.Black, New Rectangle(pt1.X - 25, pt1.Y - 25, 50, 50))

                g.SmoothingMode = Drawing2D.SmoothingMode.None

                g.DrawString(owner, f2, Brushes.Black, pt1.X - g.MeasureString(owner, f2).Width / 2, pt1.Y - 15)

            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub drawConnection(startPt As Point, endPt As Point, g As Graphics)
        Try

            Dim d As Double = Math.Sqrt((endPt.X - startPt.X) ^ 2 + (endPt.Y - startPt.Y) ^ 2)
            Dim r As Double = 28 / d

            Dim x3 As Integer = Math.Round(r * startPt.X + (1 - r) * endPt.X)
            Dim y3 As Integer = Math.Round(r * startPt.Y + (1 - r) * endPt.Y)

            g.DrawLine(p1, startPt.X, startPt.Y, x3, y3)
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub drawPayoff(startPt As Point, g As Graphics, payOffTop As String, payOffBottom As String)
        Try

            g.DrawString(getPayoffParenthesis(payOffTop, payOffBottom), f1, Brushes.Black, startPt.X, startPt.Y - g.MeasureString("(  )", f1).Height / 2, fmt)

            Dim tempSymbol As String = ""

            If frmSetup3.Visible Then
                g.DrawString(payOffTop, f2, Brushes.CornflowerBlue, startPt.X, startPt.Y - 22, fmt)
                g.DrawString(payOffBottom, f2, Brushes.Coral, startPt.X, startPt.Y - 2, fmt)
            Else
                If currentPeriod > 0 Then
                    If periods(currentPeriod).payoffMode = "Dollars" Then
                        tempSymbol = "$"

                        g.DrawString(tempSymbol & payOffTop, f2, Brushes.CornflowerBlue, startPt.X, startPt.Y - 22, fmt)
                        g.DrawString(tempSymbol & payOffBottom, f2, Brushes.Coral, startPt.X, startPt.Y - 2, fmt)
                    Else
                        tempSymbol = "¢"

                        g.DrawString(payOffTop & tempSymbol, f2, Brushes.CornflowerBlue, startPt.X, startPt.Y - 22, fmt)
                        g.DrawString(payOffBottom & tempSymbol, f2, Brushes.Coral, startPt.X, startPt.Y - 2, fmt)
                    End If
                End If
            End If





            'If periods(currentPeriod).payoffMode = "Dollars" Then

            'ElseIf periods(currentPeriod).payoffMode = "Cents" Then
            '    g.DrawString(payOffTop & "¢", f2, Brushes.CornflowerBlue, startPt.X, startPt.Y - 22, fmt)
            '    g.DrawString(payOffBottom & "¢", f2, Brushes.Coral, startPt.X, startPt.Y - 2, fmt)
            'End If

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Function isOver(x As Integer, y As Integer) As Boolean

        If x <= pt1.X + 15 And x >= pt1.X - 15 And y <= pt1.Y + 15 And y >= pt1.Y - 15 Then
            Return True
        Else
            Return False
        End If

        Return False
    End Function

    Public Sub nudge(tempX As Integer, tempY As Integer)
        Try
            pt1.X += tempX
            pt2.X += tempX
            pt3.X += tempX
            pt4.X += tempX

            pt1.Y += tempY
            pt2.Y += tempY
            pt3.Y += tempY
            pt4.Y += tempY
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

    Public Sub resetCounts()
        Try
            count = 0
            countRight = 0
            countDown = 0
            countLeft = 0
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

End Class
