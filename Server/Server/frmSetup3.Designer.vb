<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetup3
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetup3))
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.cmdAddNode = New System.Windows.Forms.Button()
        Me.lblCurrentNode = New System.Windows.Forms.Label()
        Me.cmdEditNode = New System.Windows.Forms.Button()
        Me.cmdConnectNode = New System.Windows.Forms.Button()
        Me.cmdDeleteNode = New System.Windows.Forms.Button()
        Me.lblPeriod = New System.Windows.Forms.Label()
        Me.cmdCopyPrevious = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdMinus = New System.Windows.Forms.Button()
        Me.cmdPlus = New System.Windows.Forms.Button()
        Me.cbGrid = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'cmdSave
        '
        Me.cmdSave.BackgroundImage = CType(resources.GetObject("cmdSave.BackgroundImage"), System.Drawing.Image)
        Me.cmdSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.cmdSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Location = New System.Drawing.Point(12, 696)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(61, 58)
        Me.cmdSave.TabIndex = 23
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save and Close.")
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.White
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Location = New System.Drawing.Point(12, 12)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(798, 678)
        Me.pnlMain.TabIndex = 33
        '
        'Timer1
        '
        '
        'cmdAddNode
        '
        Me.cmdAddNode.BackgroundImage = CType(resources.GetObject("cmdAddNode.BackgroundImage"), System.Drawing.Image)
        Me.cmdAddNode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.cmdAddNode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAddNode.Location = New System.Drawing.Point(79, 696)
        Me.cmdAddNode.Name = "cmdAddNode"
        Me.cmdAddNode.Size = New System.Drawing.Size(61, 58)
        Me.cmdAddNode.TabIndex = 34
        Me.ToolTip1.SetToolTip(Me.cmdAddNode, "Add Node.")
        Me.cmdAddNode.UseVisualStyleBackColor = True
        '
        'lblCurrentNode
        '
        Me.lblCurrentNode.AutoSize = True
        Me.lblCurrentNode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentNode.Location = New System.Drawing.Point(504, 733)
        Me.lblCurrentNode.Name = "lblCurrentNode"
        Me.lblCurrentNode.Size = New System.Drawing.Size(136, 20)
        Me.lblCurrentNode.TabIndex = 35
        Me.lblCurrentNode.Text = "Current Node: 1"
        '
        'cmdEditNode
        '
        Me.cmdEditNode.BackgroundImage = CType(resources.GetObject("cmdEditNode.BackgroundImage"), System.Drawing.Image)
        Me.cmdEditNode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.cmdEditNode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEditNode.Location = New System.Drawing.Point(146, 696)
        Me.cmdEditNode.Name = "cmdEditNode"
        Me.cmdEditNode.Size = New System.Drawing.Size(61, 58)
        Me.cmdEditNode.TabIndex = 36
        Me.ToolTip1.SetToolTip(Me.cmdEditNode, "Edit Selected Node.")
        Me.cmdEditNode.UseVisualStyleBackColor = True
        '
        'cmdConnectNode
        '
        Me.cmdConnectNode.BackgroundImage = CType(resources.GetObject("cmdConnectNode.BackgroundImage"), System.Drawing.Image)
        Me.cmdConnectNode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.cmdConnectNode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdConnectNode.Location = New System.Drawing.Point(213, 696)
        Me.cmdConnectNode.Name = "cmdConnectNode"
        Me.cmdConnectNode.Size = New System.Drawing.Size(61, 58)
        Me.cmdConnectNode.TabIndex = 37
        Me.ToolTip1.SetToolTip(Me.cmdConnectNode, "Connect Nodes.")
        Me.cmdConnectNode.UseVisualStyleBackColor = True
        Me.cmdConnectNode.Visible = False
        '
        'cmdDeleteNode
        '
        Me.cmdDeleteNode.BackgroundImage = CType(resources.GetObject("cmdDeleteNode.BackgroundImage"), System.Drawing.Image)
        Me.cmdDeleteNode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.cmdDeleteNode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDeleteNode.Location = New System.Drawing.Point(347, 696)
        Me.cmdDeleteNode.Name = "cmdDeleteNode"
        Me.cmdDeleteNode.Size = New System.Drawing.Size(61, 58)
        Me.cmdDeleteNode.TabIndex = 38
        Me.ToolTip1.SetToolTip(Me.cmdDeleteNode, "Delete Selected Node.")
        Me.cmdDeleteNode.UseVisualStyleBackColor = True
        '
        'lblPeriod
        '
        Me.lblPeriod.AutoSize = True
        Me.lblPeriod.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPeriod.Location = New System.Drawing.Point(664, 732)
        Me.lblPeriod.Name = "lblPeriod"
        Me.lblPeriod.Size = New System.Drawing.Size(65, 20)
        Me.lblPeriod.TabIndex = 40
        Me.lblPeriod.Text = "Tree: 1"
        '
        'cmdCopyPrevious
        '
        Me.cmdCopyPrevious.BackgroundImage = CType(resources.GetObject("cmdCopyPrevious.BackgroundImage"), System.Drawing.Image)
        Me.cmdCopyPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.cmdCopyPrevious.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCopyPrevious.Location = New System.Drawing.Point(280, 696)
        Me.cmdCopyPrevious.Name = "cmdCopyPrevious"
        Me.cmdCopyPrevious.Size = New System.Drawing.Size(61, 58)
        Me.cmdCopyPrevious.TabIndex = 41
        Me.ToolTip1.SetToolTip(Me.cmdCopyPrevious, "Copy Previous Period's Tree.")
        Me.cmdCopyPrevious.UseVisualStyleBackColor = True
        '
        'cmdClear
        '
        Me.cmdClear.BackgroundImage = CType(resources.GetObject("cmdClear.BackgroundImage"), System.Drawing.Image)
        Me.cmdClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.cmdClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClear.Location = New System.Drawing.Point(414, 696)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(61, 58)
        Me.cmdClear.TabIndex = 43
        Me.ToolTip1.SetToolTip(Me.cmdClear, "Delete ALL Nodes.")
        Me.cmdClear.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.DimGray
        Me.Label3.Location = New System.Drawing.Point(505, 693)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(305, 32)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "*Play will start at Node (1)." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "*Use W, A, S and D for nudging."
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdMinus
        '
        Me.cmdMinus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMinus.Location = New System.Drawing.Point(750, 730)
        Me.cmdMinus.Name = "cmdMinus"
        Me.cmdMinus.Size = New System.Drawing.Size(30, 23)
        Me.cmdMinus.TabIndex = 44
        Me.cmdMinus.Text = "-"
        Me.cmdMinus.UseVisualStyleBackColor = True
        '
        'cmdPlus
        '
        Me.cmdPlus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPlus.Location = New System.Drawing.Point(780, 730)
        Me.cmdPlus.Name = "cmdPlus"
        Me.cmdPlus.Size = New System.Drawing.Size(30, 23)
        Me.cmdPlus.TabIndex = 45
        Me.cmdPlus.Text = "+"
        Me.cmdPlus.UseVisualStyleBackColor = True
        '
        'cbGrid
        '
        Me.cbGrid.AutoSize = True
        Me.cbGrid.Checked = True
        Me.cbGrid.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbGrid.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbGrid.Location = New System.Drawing.Point(508, 696)
        Me.cbGrid.Name = "cbGrid"
        Me.cbGrid.Size = New System.Drawing.Size(56, 20)
        Me.cbGrid.TabIndex = 46
        Me.cbGrid.Text = "Grid"
        Me.cbGrid.UseVisualStyleBackColor = True
        '
        'frmSetup3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(822, 758)
        Me.ControlBox = False
        Me.Controls.Add(Me.cbGrid)
        Me.Controls.Add(Me.cmdPlus)
        Me.Controls.Add(Me.cmdMinus)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.cmdCopyPrevious)
        Me.Controls.Add(Me.lblPeriod)
        Me.Controls.Add(Me.cmdDeleteNode)
        Me.Controls.Add(Me.cmdConnectNode)
        Me.Controls.Add(Me.cmdEditNode)
        Me.Controls.Add(Me.lblCurrentNode)
        Me.Controls.Add(Me.cmdAddNode)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.Label3)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmSetup3"
        Me.Text = "Game Tree Setup"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents cmdAddNode As System.Windows.Forms.Button
    Friend WithEvents lblCurrentNode As System.Windows.Forms.Label
    Friend WithEvents cmdEditNode As System.Windows.Forms.Button
    Friend WithEvents cmdConnectNode As System.Windows.Forms.Button
    Friend WithEvents cmdDeleteNode As System.Windows.Forms.Button
    Friend WithEvents lblPeriod As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmdCopyPrevious As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdClear As System.Windows.Forms.Button
    Friend WithEvents cmdMinus As System.Windows.Forms.Button
    Friend WithEvents cmdPlus As System.Windows.Forms.Button
    Friend WithEvents cbGrid As CheckBox
End Class
