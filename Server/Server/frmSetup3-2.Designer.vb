<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetup3_2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetup3_2))
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdConnect = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboSource = New System.Windows.Forms.ComboBox()
        Me.rbBottom = New System.Windows.Forms.RadioButton()
        Me.rbRight = New System.Windows.Forms.RadioButton()
        Me.cboTarget = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rbLeft = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.Location = New System.Drawing.Point(122, 184)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(103, 32)
        Me.cmdCancel.TabIndex = 36
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdConnect
        '
        Me.cmdConnect.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdConnect.Location = New System.Drawing.Point(13, 184)
        Me.cmdConnect.Name = "cmdConnect"
        Me.cmdConnect.Size = New System.Drawing.Size(103, 32)
        Me.cmdConnect.TabIndex = 37
        Me.cmdConnect.Text = "Connect"
        Me.cmdConnect.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 20)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Source"
        '
        'cboSource
        '
        Me.cboSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSource.FormattingEnabled = True
        Me.cboSource.Location = New System.Drawing.Point(42, 32)
        Me.cboSource.Name = "cboSource"
        Me.cboSource.Size = New System.Drawing.Size(159, 24)
        Me.cboSource.TabIndex = 39
        '
        'rbBottom
        '
        Me.rbBottom.AutoSize = True
        Me.rbBottom.Checked = True
        Me.rbBottom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbBottom.Location = New System.Drawing.Point(42, 62)
        Me.rbBottom.Name = "rbBottom"
        Me.rbBottom.Size = New System.Drawing.Size(64, 20)
        Me.rbBottom.TabIndex = 40
        Me.rbBottom.Text = "Down"
        Me.rbBottom.UseVisualStyleBackColor = True
        '
        'rbRight
        '
        Me.rbRight.AutoSize = True
        Me.rbRight.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbRight.Location = New System.Drawing.Point(112, 62)
        Me.rbRight.Name = "rbRight"
        Me.rbRight.Size = New System.Drawing.Size(62, 20)
        Me.rbRight.TabIndex = 41
        Me.rbRight.Text = "Right"
        Me.rbRight.UseVisualStyleBackColor = True
        '
        'cboTarget
        '
        Me.cboTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTarget.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTarget.FormattingEnabled = True
        Me.cboTarget.Location = New System.Drawing.Point(42, 129)
        Me.cboTarget.Name = "cboTarget"
        Me.cboTarget.Size = New System.Drawing.Size(159, 24)
        Me.cboTarget.TabIndex = 43
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 106)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 20)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = "Target"
        '
        'rbLeft
        '
        Me.rbLeft.AutoSize = True
        Me.rbLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbLeft.Location = New System.Drawing.Point(180, 62)
        Me.rbLeft.Name = "rbLeft"
        Me.rbLeft.Size = New System.Drawing.Size(51, 20)
        Me.rbLeft.TabIndex = 44
        Me.rbLeft.Text = "Left"
        Me.rbLeft.UseVisualStyleBackColor = True
        '
        'frmSetup3_2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(250, 225)
        Me.ControlBox = False
        Me.Controls.Add(Me.rbLeft)
        Me.Controls.Add(Me.cboTarget)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.rbRight)
        Me.Controls.Add(Me.rbBottom)
        Me.Controls.Add(Me.cboSource)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdConnect)
        Me.Controls.Add(Me.cmdCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSetup3_2"
        Me.Text = "Connect Nodes"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdConnect As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboSource As System.Windows.Forms.ComboBox
    Friend WithEvents rbBottom As System.Windows.Forms.RadioButton
    Friend WithEvents rbRight As System.Windows.Forms.RadioButton
    Friend WithEvents cboTarget As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rbLeft As System.Windows.Forms.RadioButton
End Class
