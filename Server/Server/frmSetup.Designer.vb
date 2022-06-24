<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetup))
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtPeriods = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkShowInstructions = New System.Windows.Forms.CheckBox()
        Me.txtPlayers = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtWindowX = New System.Windows.Forms.TextBox()
        Me.txtWindowY = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtInstructionX = New System.Windows.Forms.TextBox()
        Me.txtInstructionY = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.chkTestMode = New System.Windows.Forms.CheckBox()
        Me.txtTreeCount = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtNoMatchEarnings = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtPort
        '
        Me.txtPort.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPort.Location = New System.Drawing.Point(308, 76)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(162, 26)
        Me.txtPort.TabIndex = 50
        Me.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(8, 79)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(204, 20)
        Me.Label10.TabIndex = 49
        Me.Label10.Text = "Port # (Requires restart)"
        '
        'txtPeriods
        '
        Me.txtPeriods.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPeriods.Location = New System.Drawing.Point(308, 44)
        Me.txtPeriods.Name = "txtPeriods"
        Me.txtPeriods.Size = New System.Drawing.Size(162, 26)
        Me.txtPeriods.TabIndex = 48
        Me.txtPeriods.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 50)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(157, 20)
        Me.Label7.TabIndex = 47
        Me.Label7.Text = "Number of Periods"
        '
        'chkShowInstructions
        '
        Me.chkShowInstructions.AutoSize = True
        Me.chkShowInstructions.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowInstructions.Location = New System.Drawing.Point(300, 268)
        Me.chkShowInstructions.Name = "chkShowInstructions"
        Me.chkShowInstructions.Size = New System.Drawing.Size(172, 24)
        Me.chkShowInstructions.TabIndex = 46
        Me.chkShowInstructions.Text = "Show Instructions"
        Me.chkShowInstructions.UseVisualStyleBackColor = True
        '
        'txtPlayers
        '
        Me.txtPlayers.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlayers.Location = New System.Drawing.Point(308, 12)
        Me.txtPlayers.Name = "txtPlayers"
        Me.txtPlayers.Size = New System.Drawing.Size(162, 26)
        Me.txtPlayers.TabIndex = 45
        Me.txtPlayers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(155, 20)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Number of Players"
        '
        'cmdSave
        '
        Me.cmdSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Location = New System.Drawing.Point(14, 308)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(457, 27)
        Me.cmdSave.TabIndex = 43
        Me.cmdSave.Text = "Save and Close"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(406, 143)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(21, 20)
        Me.Label23.TabIndex = 101
        Me.Label23.Text = "Y"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(333, 143)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(21, 20)
        Me.Label24.TabIndex = 100
        Me.Label24.Text = "X"
        '
        'txtWindowX
        '
        Me.txtWindowX.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWindowX.Location = New System.Drawing.Point(357, 140)
        Me.txtWindowX.Name = "txtWindowX"
        Me.txtWindowX.Size = New System.Drawing.Size(39, 26)
        Me.txtWindowX.TabIndex = 99
        Me.txtWindowX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtWindowY
        '
        Me.txtWindowY.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWindowY.Location = New System.Drawing.Point(430, 140)
        Me.txtWindowY.Name = "txtWindowY"
        Me.txtWindowY.Size = New System.Drawing.Size(39, 26)
        Me.txtWindowY.TabIndex = 98
        Me.txtWindowY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(8, 143)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(228, 20)
        Me.Label25.TabIndex = 97
        Me.Label25.Text = "Main Window Start Position"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(406, 111)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(21, 20)
        Me.Label22.TabIndex = 96
        Me.Label22.Text = "Y"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(333, 111)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(21, 20)
        Me.Label21.TabIndex = 95
        Me.Label21.Text = "X"
        '
        'txtInstructionX
        '
        Me.txtInstructionX.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstructionX.Location = New System.Drawing.Point(357, 108)
        Me.txtInstructionX.Name = "txtInstructionX"
        Me.txtInstructionX.Size = New System.Drawing.Size(39, 26)
        Me.txtInstructionX.TabIndex = 94
        Me.txtInstructionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtInstructionY
        '
        Me.txtInstructionY.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstructionY.Location = New System.Drawing.Point(430, 108)
        Me.txtInstructionY.Name = "txtInstructionY"
        Me.txtInstructionY.Size = New System.Drawing.Size(39, 26)
        Me.txtInstructionY.TabIndex = 93
        Me.txtInstructionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(8, 111)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(209, 20)
        Me.Label20.TabIndex = 92
        Me.Label20.Text = "Instruction Start Position"
        '
        'chkTestMode
        '
        Me.chkTestMode.AutoSize = True
        Me.chkTestMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTestMode.Location = New System.Drawing.Point(78, 268)
        Me.chkTestMode.Name = "chkTestMode"
        Me.chkTestMode.Size = New System.Drawing.Size(112, 24)
        Me.chkTestMode.TabIndex = 106
        Me.chkTestMode.Text = "Test Mode"
        Me.chkTestMode.UseVisualStyleBackColor = True
        '
        'txtTreeCount
        '
        Me.txtTreeCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTreeCount.Location = New System.Drawing.Point(308, 172)
        Me.txtTreeCount.Name = "txtTreeCount"
        Me.txtTreeCount.Size = New System.Drawing.Size(162, 26)
        Me.txtTreeCount.TabIndex = 113
        Me.txtTreeCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 175)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 20)
        Me.Label6.TabIndex = 112
        Me.Label6.Text = "Tree Count"
        '
        'txtNoMatchEarnings
        '
        Me.txtNoMatchEarnings.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoMatchEarnings.Location = New System.Drawing.Point(309, 203)
        Me.txtNoMatchEarnings.Name = "txtNoMatchEarnings"
        Me.txtNoMatchEarnings.Size = New System.Drawing.Size(162, 26)
        Me.txtNoMatchEarnings.TabIndex = 115
        Me.txtNoMatchEarnings.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(9, 206)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(186, 20)
        Me.Label8.TabIndex = 114
        Me.Label8.Text = "No Match Payment ($)"
        '
        'frmSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(483, 350)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtNoMatchEarnings)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtTreeCount)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.chkTestMode)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.txtWindowX)
        Me.Controls.Add(Me.txtWindowY)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.txtInstructionX)
        Me.Controls.Add(Me.txtInstructionY)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.txtPort)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtPeriods)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.chkShowInstructions)
        Me.Controls.Add(Me.txtPlayers)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSetup"
        Me.Text = "Setup"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtPeriods As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkShowInstructions As System.Windows.Forms.CheckBox
    Friend WithEvents txtPlayers As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtWindowX As System.Windows.Forms.TextBox
    Friend WithEvents txtWindowY As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtInstructionX As System.Windows.Forms.TextBox
    Friend WithEvents txtInstructionY As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents chkTestMode As System.Windows.Forms.CheckBox
    Friend WithEvents txtTreeCount As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtNoMatchEarnings As TextBox
    Friend WithEvents Label8 As Label
End Class
