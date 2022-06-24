Imports System.IO

Module ModuleEventLog
    Public eventLog As StreamWriter

    Public Sub AppEventLog_Init()
        Try
            With frmServer
                Dim filename As String

                Dim tempTime As String = DateTime.Now.Month & "-" & DateTime.Now.Day & "-" & DateTime.Now.Year & "_" & DateTime.Now.Hour & _
                     "_" & DateTime.Now.Minute & "_" & DateTime.Now.Second

                filename = "ServerLog_" & tempTime & ".txt"
                filename = System.Windows.Forms.Application.StartupPath & "\Logs\" & filename

                eventLog = File.CreateText(filename)
            End With
        Catch ex As Exception

        End Try
    End Sub

    Public Sub appEventLog_Write(ByVal text As String)
        Try
            eventLog.WriteLine(text & " (" & Now & ")")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub AppEventLog_Close()
        Try
            If eventLog IsNot Nothing Then eventLog.Close()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub appEventLog_Write(ByVal text As String, ByVal err As Exception)
        Try
            Dim outstr As String

            eventLog.WriteLine(text & " (" & Now & ")")

            outstr = err.Message & vbCrLf
            outstr &= err.StackTrace & vbCrLf

            eventLog.WriteLine(outstr)

        Catch ex As Exception

        End Try
    End Sub
End Module
