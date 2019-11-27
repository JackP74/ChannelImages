<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
        Me.ListChannels = New System.Windows.Forms.ListView()
        Me.ColumnID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnStatus = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextListChannels = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ContextListChannelsDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextListChannelsDeleteSelected = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextListChannelsDeleteAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.BtnAddFromClipboard = New System.Windows.Forms.Button()
        Me.BtnStart = New System.Windows.Forms.Button()
        Me.RadioChrome = New System.Windows.Forms.RadioButton()
        Me.RadioFirefox = New System.Windows.Forms.RadioButton()
        Me.BtnAddFromURL = New System.Windows.Forms.Button()
        Me.BtnCheckURL = New System.Windows.Forms.Button()
        Me.ContextListChannels.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListChannels
        '
        Me.ListChannels.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnID, Me.ColumnType, Me.ColumnStatus})
        Me.ListChannels.ContextMenuStrip = Me.ContextListChannels
        Me.ListChannels.FullRowSelect = True
        Me.ListChannels.Location = New System.Drawing.Point(2, 2)
        Me.ListChannels.Name = "ListChannels"
        Me.ListChannels.Size = New System.Drawing.Size(415, 152)
        Me.ListChannels.TabIndex = 0
        Me.ListChannels.UseCompatibleStateImageBehavior = False
        Me.ListChannels.View = System.Windows.Forms.View.Details
        '
        'ColumnID
        '
        Me.ColumnID.Text = "ID"
        Me.ColumnID.Width = 246
        '
        'ColumnType
        '
        Me.ColumnType.Text = "Type"
        Me.ColumnType.Width = 64
        '
        'ColumnStatus
        '
        Me.ColumnStatus.Text = "Status"
        Me.ColumnStatus.Width = 82
        '
        'ContextListChannels
        '
        Me.ContextListChannels.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ContextListChannelsDelete})
        Me.ContextListChannels.Name = "ContextListChannels"
        Me.ContextListChannels.Size = New System.Drawing.Size(108, 26)
        '
        'ContextListChannelsDelete
        '
        Me.ContextListChannelsDelete.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ContextListChannelsDeleteSelected, Me.ContextListChannelsDeleteAll})
        Me.ContextListChannelsDelete.Name = "ContextListChannelsDelete"
        Me.ContextListChannelsDelete.Size = New System.Drawing.Size(107, 22)
        Me.ContextListChannelsDelete.Text = "Delete"
        '
        'ContextListChannelsDeleteSelected
        '
        Me.ContextListChannelsDeleteSelected.Name = "ContextListChannelsDeleteSelected"
        Me.ContextListChannelsDeleteSelected.Size = New System.Drawing.Size(118, 22)
        Me.ContextListChannelsDeleteSelected.Text = "Selected"
        '
        'ContextListChannelsDeleteAll
        '
        Me.ContextListChannelsDeleteAll.Name = "ContextListChannelsDeleteAll"
        Me.ContextListChannelsDeleteAll.Size = New System.Drawing.Size(118, 22)
        Me.ContextListChannelsDeleteAll.Text = "All"
        '
        'BtnAddFromClipboard
        '
        Me.BtnAddFromClipboard.Location = New System.Drawing.Point(1, 154)
        Me.BtnAddFromClipboard.Name = "BtnAddFromClipboard"
        Me.BtnAddFromClipboard.Size = New System.Drawing.Size(187, 23)
        Me.BtnAddFromClipboard.TabIndex = 1
        Me.BtnAddFromClipboard.Text = "Add From Clipboard"
        Me.BtnAddFromClipboard.UseVisualStyleBackColor = True
        '
        'BtnStart
        '
        Me.BtnStart.Location = New System.Drawing.Point(187, 154)
        Me.BtnStart.Name = "BtnStart"
        Me.BtnStart.Size = New System.Drawing.Size(231, 23)
        Me.BtnStart.TabIndex = 2
        Me.BtnStart.Text = "Start"
        Me.BtnStart.UseVisualStyleBackColor = True
        '
        'RadioChrome
        '
        Me.RadioChrome.AutoSize = True
        Me.RadioChrome.Checked = True
        Me.RadioChrome.Location = New System.Drawing.Point(4, 179)
        Me.RadioChrome.Name = "RadioChrome"
        Me.RadioChrome.Size = New System.Drawing.Size(98, 17)
        Me.RadioChrome.TabIndex = 4
        Me.RadioChrome.TabStop = True
        Me.RadioChrome.Text = "Google Chrome"
        Me.RadioChrome.UseVisualStyleBackColor = True
        '
        'RadioFirefox
        '
        Me.RadioFirefox.AutoSize = True
        Me.RadioFirefox.Location = New System.Drawing.Point(101, 179)
        Me.RadioFirefox.Name = "RadioFirefox"
        Me.RadioFirefox.Size = New System.Drawing.Size(91, 17)
        Me.RadioFirefox.TabIndex = 5
        Me.RadioFirefox.Text = "Moziila Firefox"
        Me.RadioFirefox.UseVisualStyleBackColor = True
        '
        'BtnAddFromURL
        '
        Me.BtnAddFromURL.Location = New System.Drawing.Point(190, 176)
        Me.BtnAddFromURL.Name = "BtnAddFromURL"
        Me.BtnAddFromURL.Size = New System.Drawing.Size(228, 23)
        Me.BtnAddFromURL.TabIndex = 6
        Me.BtnAddFromURL.Text = "Add from URL"
        Me.BtnAddFromURL.UseVisualStyleBackColor = True
        '
        'BtnCheckURL
        '
        Me.BtnCheckURL.Location = New System.Drawing.Point(1, 198)
        Me.BtnCheckURL.Name = "BtnCheckURL"
        Me.BtnCheckURL.Size = New System.Drawing.Size(417, 23)
        Me.BtnCheckURL.TabIndex = 7
        Me.BtnCheckURL.Text = "Check URL"
        Me.BtnCheckURL.UseVisualStyleBackColor = True
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(419, 222)
        Me.Controls.Add(Me.BtnCheckURL)
        Me.Controls.Add(Me.BtnAddFromURL)
        Me.Controls.Add(Me.RadioFirefox)
        Me.Controls.Add(Me.RadioChrome)
        Me.Controls.Add(Me.BtnStart)
        Me.Controls.Add(Me.BtnAddFromClipboard)
        Me.Controls.Add(Me.ListChannels)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Channel Images"
        Me.ContextListChannels.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListChannels As System.Windows.Forms.ListView
    Friend WithEvents ColumnID As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnType As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnStatus As System.Windows.Forms.ColumnHeader
    Friend WithEvents BtnAddFromClipboard As System.Windows.Forms.Button
    Friend WithEvents BtnStart As System.Windows.Forms.Button
    Friend WithEvents ContextListChannels As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ContextListChannelsDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextListChannelsDeleteSelected As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextListChannelsDeleteAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RadioChrome As System.Windows.Forms.RadioButton
    Friend WithEvents RadioFirefox As System.Windows.Forms.RadioButton
    Friend WithEvents BtnAddFromURL As System.Windows.Forms.Button
    Friend WithEvents BtnCheckURL As System.Windows.Forms.Button

End Class
