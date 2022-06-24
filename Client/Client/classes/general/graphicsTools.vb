Imports System.Drawing.Drawing2D

Module graphicsTools
    Public Sub drawRoundedBox(ByVal g As Graphics, ByVal x1 As Integer, ByVal y1 As Integer, _
          ByVal x2 As Integer, ByVal y2 As Integer, ByVal curve As Integer, ByVal width As Integer, ByVal b As SolidBrush)

        Try
            Dim graphPath As New GraphicsPath()
            Dim rect As Rectangle

            Dim curveLine As Integer

            curveLine = curve / 2

            rect.X = x1
            rect.Y = y1
            rect.Width = curve
            rect.Height = curve
            graphPath.AddArc(rect, 180, 90)

            graphPath.AddLine(x1 + curveLine, y1, x2 - curveLine, y1)

            rect.X = x2 - curve
            rect.Y = y1
            rect.Width = curve
            rect.Height = curve
            graphPath.AddArc(rect, 270, 90)

            graphPath.AddLine(x2, y1 + curveLine, x2, y2 - curveLine)

            rect.X = x2 - curve
            rect.Y = y2 - curve
            rect.Width = curve
            rect.Height = curve
            graphPath.AddArc(rect, 0, 90)

            graphPath.AddLine(x2 - curveLine, y2, x1 + curveLine, y2)

            rect.X = x1
            rect.Y = y2 - curve
            rect.Width = curve
            rect.Height = curve
            graphPath.AddArc(rect, 90, 90)

            graphPath.AddLine(x1, y2 - curveLine, x1, y1 + curveLine)

            Dim p As New Pen(Color.Black, width)

            g.FillPath(b, graphPath)
            g.DrawPath(p, graphPath)

        Catch ex As Exception
            appEventLog_Write("error draw round box:", ex)
        End Try
    End Sub

    Public Sub drawTriangle(ByVal g As Graphics, ByVal x As Integer, ByVal y As Integer, ByVal c As Color)
        Try
            Dim pts(2) As Point

            pts(0).X = x
            pts(0).Y = y + 15 + 7
            pts(1).X = x + 15
            pts(1).Y = y + 15
            pts(2).X = x + 15
            pts(2).Y = y + 15 + 14

            Dim b As New SolidBrush(c)
            g.FillPolygon(b, pts)
            g.DrawPolygon(Pens.Black, pts)
        Catch ex As Exception
            appEventLog_Write("error drawTriangle:", ex)
        End Try
    End Sub

    Public Sub drawFilledCircle(ByVal g As Graphics, ByVal x As Integer, ByVal y As Integer, ByVal c As Color)
        Try
            Dim rect As Rectangle
            Dim b As New SolidBrush(c)

            rect = New Rectangle(x, y + 15, 15, 15)
            g.FillEllipse(b, rect)
            g.DrawEllipse(Pens.Black, rect)
        Catch ex As Exception
            appEventLog_Write("error drawFilledCircle:", ex)
        End Try
    End Sub

    Public Sub drawX(ByVal g As Graphics, ByVal c As Color, ByVal rect As Rectangle, ByVal weight As Integer)
        Try
            Dim p As New Pen(c, weight)

            g.DrawLine(p, rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height)
            g.DrawLine(p, rect.X + rect.Width, rect.Y, rect.X, rect.Y + rect.Height)
        Catch ex As Exception
            appEventLog_Write("error drawX:", ex)
        End Try

    End Sub
End Module
