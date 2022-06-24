Public Class Screen
    Protected g As Graphics = Nothing
    Protected imageOffScreen As Image = Nothing
    Protected graphicsOffScreen As Graphics = Nothing

    Public screenX As Integer = 0
    Public screenY As Integer = 0
    Public screenWidth As Integer = 0
    Public screenHeight As Integer = 0


    Public Sub New(ByVal p As Panel, ByVal r As Rectangle)
        Try
            '
            ' TODO: Add constructor logic here
            '
            ' get a visible screen
            g = p.CreateGraphics()
            screenX = r.X
            screenY = r.Y
            screenWidth = r.Width
            screenHeight = r.Height

            ' get offscreen buffer context
            imageOffScreen = New Bitmap(screenWidth, screenHeight)
            graphicsOffScreen = Graphics.FromImage(imageOffScreen)
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub 'New
    Public Sub New(ByVal p As Graphics, ByVal r As Rectangle)
        Try
            g = p
            screenX = r.X
            screenY = r.Y
            screenWidth = r.Width
            screenHeight = r.Height

            ' get offscreen buffer context
            imageOffScreen = New Bitmap(screenWidth, screenHeight)
            graphicsOffScreen = Graphics.FromImage(imageOffScreen)
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Function GetGraphics() As Graphics
        Try
            Return graphicsOffScreen
        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return graphicsOffScreen
        End Try
    End Function 'GetGraphics

    Public Sub erase1()
        Try
            ' erase all content in back buffer by using background color
            If Not isValidGraphics() Then
                Return
            End If
            Dim blackBrush As New SolidBrush(Color.White)
            graphicsOffScreen.FillRectangle(blackBrush, 0, 0, screenWidth, screenHeight)
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub 'erase

    Public Sub flip()
        Try
            ' flip buffers for smooth animation
            g.DrawImage(imageOffScreen, screenX, screenY)
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub 'flip

    Public Function isValidGraphics() As Boolean
        Try
            If Not (g Is Nothing) And Not (graphicsOffScreen Is Nothing) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
            appEventLog_Write("error :", ex)
        End Try
    End Function 'isValidGraphics

    Public Sub New()
    End Sub 'New
End Class
