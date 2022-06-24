Module modScreenSaver
    Public Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (ByVal uAction As Integer, ByVal uParam As Integer, ByVal lpvParam As Integer, ByVal fuWinIni As Integer) As Integer

    Public Function ToggleScreenSaverActive(ByRef Active As Boolean) As Boolean
        Dim lActiveFlag As Integer
        Dim retvaL As Integer

        lActiveFlag = IIf(Active, 1, 0)
        retvaL = SystemParametersInfo(17, lActiveFlag, 0, 0)
        ToggleScreenSaverActive = retvaL > 0
    End Function
End Module
