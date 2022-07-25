<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.cmdSubmit = New System.Windows.Forms.Button()
        Me.txtMessages = New System.Windows.Forms.RichTextBox()
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.txtProfit = New System.Windows.Forms.TextBox()
        Me.lblEarnings = New System.Windows.Forms.Label()
        Me.lblPerson2 = New System.Windows.Forms.Label()
        Me.lblPerson = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblPayoff2 = New System.Windows.Forms.Label()
        Me.lblPayoff1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer1
        '
        '
        'Timer2
        '
        Me.Timer2.Interval = 500
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.White
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Location = New System.Drawing.Point(12, 12)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(798, 678)
        Me.pnlMain.TabIndex = 34
        '
        'cmdSubmit
        '
        Me.cmdSubmit.BackColor = System.Drawing.Color.LightGreen
        Me.cmdSubmit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSubmit.Location = New System.Drawing.Point(821, 444)
        Me.cmdSubmit.Name = "cmdSubmit"
        Me.cmdSubmit.Size = New System.Drawing.Size(183, 109)
        Me.cmdSubmit.TabIndex = 36
        Me.cmdSubmit.Text = "Submit"
        Me.cmdSubmit.UseVisualStyleBackColor = False
        '
        'txtMessages
        '
        Me.txtMessages.BackColor = System.Drawing.Color.White
        Me.txtMessages.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMessages.Location = New System.Drawing.Point(12, 696)
        Me.txtMessages.Name = "txtMessages"
        Me.txtMessages.ReadOnly = True
        Me.txtMessages.Size = New System.Drawing.Size(992, 30)
        Me.txtMessages.TabIndex = 37
        Me.txtMessages.TabStop = False
        Me.txtMessages.Text = ""
        '
        'Timer3
        '
        '
        'txtProfit
        '
        Me.txtProfit.BackColor = System.Drawing.Color.White
        Me.txtProfit.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProfit.Location = New System.Drawing.Point(9, 69)
        Me.txtProfit.Name = "txtProfit"
        Me.txtProfit.ReadOnly = True
        Me.txtProfit.Size = New System.Drawing.Size(164, 31)
        Me.txtProfit.TabIndex = 39
        Me.txtProfit.TabStop = False
        Me.txtProfit.Text = "0"
        Me.txtProfit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblEarnings
        '
        Me.lblEarnings.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEarnings.Location = New System.Drawing.Point(14, 35)
        Me.lblEarnings.Name = "lblEarnings"
        Me.lblEarnings.Size = New System.Drawing.Size(159, 31)
        Me.lblEarnings.TabIndex = 38
        Me.lblEarnings.Text = "Earnings"
        Me.lblEarnings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPerson2
        '
        Me.lblPerson2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPerson2.Location = New System.Drawing.Point(9, 317)
        Me.lblPerson2.Name = "lblPerson2"
        Me.lblPerson2.Size = New System.Drawing.Size(163, 24)
        Me.lblPerson2.TabIndex = 40
        Me.lblPerson2.Text = "You are"
        Me.lblPerson2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPerson
        '
        Me.lblPerson.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPerson.Location = New System.Drawing.Point(9, 341)
        Me.lblPerson.Name = "lblPerson"
        Me.lblPerson.Size = New System.Drawing.Size(163, 40)
        Me.lblPerson.TabIndex = 41
        Me.lblPerson.Text = "Person 1"
        Me.lblPerson.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.lblPayoff2)
        Me.GroupBox1.Controls.Add(Me.lblPayoff1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lblPerson)
        Me.GroupBox1.Controls.Add(Me.lblPerson2)
        Me.GroupBox1.Location = New System.Drawing.Point(816, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(188, 430)
        Me.GroupBox1.TabIndex = 42
        Me.GroupBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(30, 74)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(133, 20)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = "Example Payoff"
        '
        'lblPayoff2
        '
        Me.lblPayoff2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayoff2.ForeColor = System.Drawing.Color.Coral
        Me.lblPayoff2.Location = New System.Drawing.Point(44, 179)
        Me.lblPayoff2.Name = "lblPayoff2"
        Me.lblPayoff2.Size = New System.Drawing.Size(100, 56)
        Me.lblPayoff2.TabIndex = 44
        Me.lblPayoff2.Text = "Your Payoff"
        Me.lblPayoff2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblPayoff1
        '
        Me.lblPayoff1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayoff1.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.lblPayoff1.Location = New System.Drawing.Point(44, 109)
        Me.lblPayoff1.Name = "lblPayoff1"
        Me.lblPayoff1.Size = New System.Drawing.Size(100, 56)
        Me.lblPayoff1.TabIndex = 43
        Me.lblPayoff1.Text = "Your Payoff"
        Me.lblPayoff1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 99.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(-28, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(257, 152)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = "(   )"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtProfit)
        Me.GroupBox2.Controls.Add(Me.lblEarnings)
        Me.GroupBox2.Location = New System.Drawing.Point(820, 559)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(183, 131)
        Me.GroupBox2.TabIndex = 43
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Visible = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1016, 734)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtMessages)
        Me.Controls.Add(Me.cmdSubmit)
        Me.Controls.Add(Me.pnlMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmMain"
        Me.Text = "Client"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents cmdSubmit As System.Windows.Forms.Button
    Friend WithEvents txtMessages As System.Windows.Forms.RichTextBox
    Friend WithEvents Timer3 As System.Windows.Forms.Timer
    Friend WithEvents txtProfit As System.Windows.Forms.TextBox
    Friend WithEvents lblEarnings As System.Windows.Forms.Label
    Friend WithEvents lblPerson2 As System.Windows.Forms.Label
    Friend WithEvents lblPerson As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblPayoff2 As System.Windows.Forms.Label
    Friend WithEvents lblPayoff1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
