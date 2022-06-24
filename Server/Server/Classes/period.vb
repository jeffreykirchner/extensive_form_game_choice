Public Class period
    Public pairingRule As String
    Public endingProbabilty As Integer
    Public instructions As String
    Public setupString As String
    Public trees(2) As Integer

    Public subPeriodCount As Integer
    Public nodeList(100, 2, 1000) As node 'node,index(1 or 2),subperiod
    Public nodeCount(2) As Integer
    Public index As Integer        'period number
    Public payoffMode As String

    Public Sub fromString(str As String, index As Integer)
        Try
            Me.index = index
            setupString = str

            subPeriodCount = 1

            Dim msgtokens() As String = str.Split(";")
            Dim nextToken As Integer = 0

            pairingRule = msgtokens(nextToken)
            nextToken += 1

            If InStr(msgtokens(nextToken), ",") Then
                Dim msgtokens2() As String = msgtokens(nextToken).Split(",")

                trees(1) = msgtokens2(0)
                trees(2) = msgtokens2(1)

                nodeCount(1) = getINI(sfile, "nodeCount", CStr(trees(1)))
                nodeCount(2) = getINI(sfile, "nodeCount", CStr(trees(2)))

                setupNodes(1)
                setupNodes(2)
            ElseIf IsNumeric(msgtokens(nextToken)) Then
                trees(1) = Integer.Parse(msgtokens(nextToken))
                nodeCount(1) = getINI(sfile, "nodeCount", CStr(trees(1)))

                setupNodes(1)
            Else
                trees(1) = -1
                'trees(2) = periods(index - 1).trees(2)

                'nodeCount(1) = periods(index - 1).trees(1)
                'nodeCount(2) = periods(index - 1).trees(2)

                'setupNodes(trees(1), 1)
                'setupNodes(trees(2), 1)


            End If

            nextToken += 1

            endingProbabilty = CInt(msgtokens(nextToken))
            nextToken += 1

            instructions = msgtokens(nextToken)
            nextToken += 1

            payoffMode = msgtokens(nextToken)
            nextToken += 1

        Catch ex As Exception
            appEventLog_Write("error: ", ex)
        End Try
    End Sub

    Public Sub setupNodes(index As Integer)
        'tree is which of the pre defined trees to use
        'index in which of the two trees to setup
        Try

            For j As Integer = 1 To nodeCount(index)
                setupNode2(nodeList(j, index, currentSubPeriod),
                           modMain.nodeList(j, trees(index)))
            Next
        Catch ex As Exception
            appEventLog_Write("error: ", ex)
        End Try
    End Sub



End Class
