Public Class period
    Public pairingRule As String                    'how to pair people together
    Public trees(2) As Integer                      'tree list if there is a choice
    Public instructions As String                   'instructions files to be shown before start of period
    Public endingProbabilty As Integer              'probablity of period ending and continuing on    
    Public currentTreeIndex As Integer              'index of tree the subject will use this period

    Public payoffMode As String

    Public Sub fromString(ByRef msgTokens() As String, ByRef nextToken As Integer)
        Try
            pairingRule = msgTokens(nextToken)
            nextToken += 1

            If InStr(msgTokens(nextToken), ",") Then
                Dim msgtokens2() As String = msgTokens(nextToken).Split(",")

                trees(1) = msgtokens2(0)
                trees(2) = msgtokens2(1)
            ElseIf IsNumeric(msgTokens(nextToken)) Then
                trees(1) = Integer.Parse(msgTokens(nextToken))
            Else
                trees(1) = -1
            End If

            nextToken += 1

            endingProbabilty = CInt(msgTokens(nextToken))
            nextToken += 1

            instructions = msgTokens(nextToken)
            nextToken += 1

            payoffMode = msgTokens(nextToken)
            nextToken += 1
        Catch ex As Exception
            appEventLog_Write("error: ", ex)
        End Try
    End Sub

    Public Function getCurrentTree() As Integer
        'return the tree from the node list that is being used
        Try
            Return trees(currentTreeIndex)
        Catch ex As Exception
            appEventLog_Write("error: ", ex)
            Return 0
        End Try
    End Function
End Class
