<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChoice
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChoice))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.cmdSubmit = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.rbLeft = New System.Windows.Forms.RadioButton()
        Me.rbRight = New System.Windows.Forms.RadioButton()
        Me.lbl1 = New System.Windows.Forms.Label()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.White
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Location = New System.Drawing.Point(12, 11)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1085, 678)
        Me.pnlMain.TabIndex = 35
        '
        'cmdSubmit
        '
        Me.cmdSubmit.BackColor = System.Drawing.Color.LightGreen
        Me.cmdSubmit.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSubmit.Location = New System.Drawing.Point(463, 743)
        Me.cmdSubmit.Name = "cmdSubmit"
        Me.cmdSubmit.Size = New System.Drawing.Size(183, 40)
        Me.cmdSubmit.TabIndex = 37
        Me.cmdSubmit.Text = "Submit"
        Me.cmdSubmit.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        '
        'rbLeft
        '
        Me.rbLeft.AutoSize = True
        Me.rbLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbLeft.Location = New System.Drawing.Point(179, 749)
        Me.rbLeft.Name = "rbLeft"
        Me.rbLeft.Size = New System.Drawing.Size(127, 28)
        Me.rbLeft.TabIndex = 38
        Me.rbLeft.TabStop = True
        Me.rbLeft.Text = "My Choice"
        Me.rbLeft.UseVisualStyleBackColor = True
        '
        'rbRight
        '
        Me.rbRight.AutoSize = True
        Me.rbRight.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbRight.Location = New System.Drawing.Point(803, 749)
        Me.rbRight.Name = "rbRight"
        Me.rbRight.Size = New System.Drawing.Size(127, 28)
        Me.rbRight.TabIndex = 39
        Me.rbRight.TabStop = True
        Me.rbRight.Text = "My Choice"
        Me.rbRight.UseVisualStyleBackColor = True
        '
        'lbl1
        '
        Me.lbl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl1.Location = New System.Drawing.Point(12, 698)
        Me.lbl1.Name = "lbl1"
        Me.lbl1.Size = New System.Drawing.Size(1085, 29)
        Me.lbl1.TabIndex = 40
        Me.lbl1.Text = "Which decision problem do you choose to participate in? "
        Me.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BackColor = System.Drawing.Color.White
        Me.RichTextBox1.Location = New System.Drawing.Point(1103, 11)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ReadOnly = True
        Me.RichTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical
        Me.RichTextBox1.Size = New System.Drawing.Size(401, 766)
        Me.RichTextBox1.TabIndex = 41
        Me.RichTextBox1.TabStop = False
        Me.RichTextBox1.Text = ""
        '
        'frmChoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1510, 792)
        Me.ControlBox = False
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.lbl1)
        Me.Controls.Add(Me.rbRight)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.rbLeft)
        Me.Controls.Add(Me.cmdSubmit)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmChoice"
        Me.Text = "Client"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents cmdSubmit As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents rbRight As RadioButton
    Friend WithEvents rbLeft As RadioButton
    Friend WithEvents lbl1 As Label
    Friend WithEvents RichTextBox1 As RichTextBox
End Class
