Imports System.Threading
Imports System.Net
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Windows.Automation
Imports NDde.Client
Imports System.Runtime.InteropServices
Imports System.Drawing.Imaging

Public Class FrmMain
    Dim ThreadStart As Thread

    <DllImport("user32.dll", SetLastError:=True)> Private Shared Function GetForegroundWindow() As IntPtr
    End Function

    Private Declare Function SetForegroundWindow Lib "user32" (ByVal hwnd As Long) As IntPtr
    Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
    End Sub

    Private Sub BtnAddFromClipboard_Click(sender As Object, e As EventArgs) Handles BtnAddFromClipboard.Click
        Try
            If Clipboard.ContainsText = False Then
                MsgBox("Couldn't add from clipboard, Error: No text in clipboard", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                Exit Sub
            End If
            Dim List As String = Clipboard.GetText
            For Each Item As String In Split(List, vbNewLine)
                Dim URL As String = Item.Trim
                If URL.Length = 0 Then
                    Continue For
                End If
                If URL.Contains("youtube.com/user") = True OrElse URL.Contains("youtube.com/channel") = True Then
                    Dim Name As String = Split(Split(URL, "youtube.com/")(1), "/")(1)
                    Dim Found As Boolean = False
                    For Each LVItem As ListViewItem In ListChannels.Items
                        If LVItem.SubItems(0).Text = Name Then Found = True
                    Next
                    If URL.Contains("youtube.com/user") = True Then
                        If Found = False Then ListChannels.Items.Add(Name).SubItems.AddRange({"user", "waiting"})
                    Else
                        If Found = False Then ListChannels.Items.Add(Name).SubItems.AddRange({"channel", "waiting"})
                    End If
                Else
                    Continue For
                End If
            Next
        Catch ex As Exception
            MsgBox("Couldn't add from clipboard, Error: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub

    Private Function LoadImageFromURL(ByVal URL As String) As Bitmap
        Try
            Dim ReadBytes As Integer = 10000
            Dim Request As WebRequest = WebRequest.Create(URL)
            Dim Response As WebResponse = Request.GetResponse()
            Dim ReceiveStream As Stream = Response.GetResponseStream()
            Dim Reader As New BinaryReader(ReceiveStream)
            Dim MemoryStream As New MemoryStream()
            Dim ByteBuffer As Byte() = New Byte(ReadBytes - 1) {}
            Dim BytesRead As Integer = Reader.Read(ByteBuffer, 0, ReadBytes)
            While BytesRead > 0
                MemoryStream.Write(ByteBuffer, 0, BytesRead)
                BytesRead = Reader.Read(ByteBuffer, 0, ReadBytes)
            End While
            Return New Bitmap(MemoryStream)
        Catch ex As Exception
            MsgBox("Couldn't get image from url, Error: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Return New Bitmap(10, 10)
        End Try
    End Function

    Public Shared Function ResizeImage(ByVal Image As Bitmap, ByVal Size As Size) As Image
        Try
            Dim NewWidth As Integer = 0
            Dim NewHeight As Integer = 0
            Dim OriginalWidth As Integer = Image.Width
            Dim OriginalHeight As Integer = Image.Height
            Dim PercentWidth As Single = CSng(Size.Width) / CSng(OriginalWidth)
            Dim PercentHeight As Single = CSng(Size.Height) / CSng(OriginalHeight)
            Dim Percent As Single = If(PercentHeight < PercentWidth, PercentHeight, PercentWidth)
            NewWidth = CInt(OriginalWidth * Percent)
            NewHeight = CInt(OriginalHeight * Percent)
            Dim NewImage As Bitmap = New Bitmap(NewWidth, NewHeight)
            Dim G As Graphics = Graphics.FromImage(NewImage)
            G.InterpolationMode = InterpolationMode.HighQualityBicubic
            G.DrawImage(Image, 0, 0, NewWidth, NewHeight)
            G.Save()
            G.Dispose()
            Return NewImage
        Catch ex As Exception
            MsgBox("Couldn't resize image, Error: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            Return New Bitmap(10, 10)
        End Try
    End Function

    Private Sub Start()
        Try
            For Each Item As ListViewItem In ListChannels.Items
                Try
                    If Item.SubItems(2).Text = "done" Then
                        Continue For
                    End If
                    Dim URL As String = "http://www.youtube.com/" & Item.SubItems(1).Text & "/" & Item.SubItems(0).Text
                    Dim ProfileURL As String = String.Empty
                    Dim CoverURL As String = String.Empty
                    Dim Name As String = String.Empty
                    Dim WebClient As New WebClient
                    WebClient.Headers("User-Agent") = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko"
                    Dim Source As String = WebClient.DownloadString(URL)
                    Source = Source.Replace(Chr(10), String.Empty)
                    Dim Split01 As String = String.Empty
                    Dim Split02 As String = String.Empty
                    Dim Split03 As String = String.Empty
                    Split01 = Split(Source, ".hd-banner-image {")(1)
                    Split02 = Split(Split01, ");")(0)
                    Split03 = Split(Split01, "channel-header-profile-image"" src=""")(1)
                    If Source.Contains("url(/yts/img/channels/c4/default_banner_hq") = True Then
                        CoverURL = "None"
                    Else
                        CoverURL = Split(Split02, "url(//")(1)
                    End If
                    ProfileURL = Replace(Split(Split03, """")(0), "s100", "s250")
                    If ProfileURL.StartsWith("//") = True Then
                        ProfileURL = "https://" & Split(ProfileURL, "//")(1)
                    End If
                    Dim Split04 As String = Split(Source, "spf-link branded-page-header-title-link yt-uix-sessionlink")(1)
                    Dim Split05 As String = Split(Split04, "data-sessionlink")(0)
                    Dim Split06 As String = Split(Split05, "title=""")(1)
                    Name = Split(Split06, """")(0)
                    Dim FinalCoverImg As Bitmap = New Bitmap(1, 1)
                    Dim CoverImg As Bitmap = New Bitmap(1, 1)
                    If Not CoverURL = "None" Then
                        CoverImg = LoadImageFromURL("https://" & CoverURL)
                        CoverImg = ResizeImage(CoverImg, New Size(2048, CoverImg.Height))
                        FinalCoverImg = New Bitmap(2048, 1152)
                        Dim G As Graphics = Graphics.FromImage(FinalCoverImg)
                        G.Clear(Color.White)
                        G.DrawImage(CoverImg, New Point(0, (1152 / 2) - (CoverImg.Size.Height / 2)))
                        G.Save()
                        G.Dispose()
                        CoverImg.Dispose()
                    End If
                    Dim ProfileImg As Bitmap = LoadImageFromURL(ProfileURL)
                    Name = Replace(Name, "/", String.Empty)
                    Name = Replace(Name, "|", String.Empty)
                    Dim Path As String = Application.StartupPath & "\" & Name & "\"
                    If My.Computer.FileSystem.DirectoryExists(Path) = False Then
                        My.Computer.FileSystem.CreateDirectory(Path)
                    Else
                        If My.Computer.FileSystem.FileExists(Path & "cover.png") = True Then
                            My.Computer.FileSystem.DeleteFile(Path & "cover.png")
                        End If
                        If My.Computer.FileSystem.FileExists(Path & "profile.png") = True Then
                            My.Computer.FileSystem.DeleteFile(Path & "profile.png")
                        End If
                    End If
                    Try
                        If Not CoverURL = "None" Then FinalCoverImg.Save(Path & "cover.png", Imaging.ImageFormat.Png)
                        ProfileImg.Save(Path & "profile.png", Imaging.ImageFormat.Png)
                    Catch ex As Exception
                        Try
                            If CoverURL = "None" Then
                                Dim MemoryStream As New MemoryStream()
                                ProfileImg.Save(MemoryStream, Imaging.ImageFormat.Png)
                                Dim File As New FileStream(Path & "profile.png", FileMode.Create, FileAccess.Write)
                                MemoryStream.WriteTo(File)
                                File.Close()
                                MemoryStream.Close()
                            Else
                                Dim MemoryStream As New MemoryStream()
                                FinalCoverImg.Save(MemoryStream, Imaging.ImageFormat.Png)
                                Dim File As New FileStream(Path & "cover.png", FileMode.Create, FileAccess.Write)
                                MemoryStream.WriteTo(File)
                                File.Close()
                                MemoryStream.Close()
                                MemoryStream = New MemoryStream
                                ProfileImg.Save(MemoryStream, Imaging.ImageFormat.Png)
                                File = New FileStream(Path & "profile.png", FileMode.Create, FileAccess.Write)
                                MemoryStream.WriteTo(File)
                                File.Close()
                                MemoryStream.Close()
                            End If

                        Catch ex01 As Exception
                            MsgBox("Couldn't save images, Error: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
                        End Try
                    End Try
                    FinalCoverImg.Dispose()
                    ProfileImg.Dispose()
                    Item.SubItems(2).Text = "done"
                Catch ex As Exception
                    MsgBox("1" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "error")
                End Try
            Next
            BtnStart.Text = "Start"
            Try
                ThreadStart.Abort()
            Catch
            End Try
        Catch ex As Exception
            If ex.Message.ToLower.Contains("thread was being aborted") = True Then
                Exit Sub
            End If
            MsgBox("Couldn't get channels images, Error: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
            BtnStart.Text = "Start"
            ThreadStart.Abort()
        End Try
    End Sub

    Private Sub BtnStart_Click(sender As Object, e As EventArgs) Handles BtnStart.Click
        Try
            If BtnStart.Text = "Start" Then
                ThreadStart = New Thread(Sub() Start()) With {.IsBackground = True}
                ThreadStart.SetApartmentState(ApartmentState.STA)
                ThreadStart.Start()
                BtnStart.Text = "Stop"
            ElseIf BtnStart.Text = "Stop" Then
                BtnStart.Text = "Start"
                Try
                    ThreadStart.Abort()
                Catch
                End Try
            Else
                Me.Close()
            End If
        Catch ex As Exception
            BtnStart.Text = "Start"
            Try
                ThreadStart.Abort()
            Catch
            End Try
        End Try
    End Sub

    Private Sub ContextListChannelsDeleteSelected_Click(sender As Object, e As EventArgs) Handles ContextListChannelsDeleteSelected.Click
        Try
            For Each Item As ListViewItem In ListChannels.SelectedItems
                ListChannels.Items.Remove(Item)
            Next
        Catch ex As Exception
            MsgBox("Couldn't delete selected channels, Error: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub

    Private Sub ContextListChannelsDeleteAll_Click(sender As Object, e As EventArgs) Handles ContextListChannelsDeleteAll.Click
        Try
            ListChannels.Items.Clear()
        Catch ex As Exception
            MsgBox("Couldn't delete all channels, Error: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub

    Private Sub BtnAddFromURL_Click(sender As Object, e As EventArgs) Handles BtnAddFromURL.Click
        Try
            If RadioChrome.Checked = True Then
                Dim ProcessChrome As Process() = Process.GetProcessesByName("chrome")
                For Each Chrome As Process In ProcessChrome
                    If Chrome.MainWindowHandle = IntPtr.Zero Then Continue For
                    Dim AutomationElement As System.Windows.Automation.AutomationElement = System.Windows.Automation.AutomationElement.FromHandle(Chrome.MainWindowHandle)
                    Dim AutomationElementURL As System.Windows.Automation.AutomationElement = AutomationElement.FindFirst(TreeScope.Descendants, New PropertyCondition(System.Windows.Automation.AutomationElement.NameProperty, "Address and search bar"))
                    If AutomationElementURL Is Nothing Then
                        Continue For
                    End If
                    Dim AutomationPattern As System.Windows.Automation.AutomationPattern() = AutomationElementURL.GetSupportedPatterns()
                    If AutomationPattern.Length = 0 Then
                        Continue For
                    End If
                    Dim ValuePattern As System.Windows.Automation.ValuePattern = DirectCast(AutomationElementURL.GetCurrentPattern(AutomationPattern(0)), System.Windows.Automation.ValuePattern)
                    If AutomationElementURL.GetCurrentPropertyValue(System.Windows.Automation.AutomationElement.HasKeyboardFocusProperty) Then
                        Continue For
                    End If
                    Dim URL As String = ValuePattern.Current.Value.Trim
                    If URL.Contains("youtube.com/user") = True OrElse URL.Contains("youtube.com/channel") = True Then
                        Dim Name As String = Split(Split(URL, "youtube.com/")(1), "/")(1)
                        Dim Found As Boolean = False
                        For Each LVItem As ListViewItem In ListChannels.Items
                            If LVItem.SubItems(0).Text = Name Then Found = True
                        Next
                        If URL.Contains("youtube.com/user") = True Then
                            If Found = False Then ListChannels.Items.Add(Name).SubItems.AddRange({"user", "waiting"})
                        Else
                            If Found = False Then ListChannels.Items.Add(Name).SubItems.AddRange({"channel", "waiting"})
                        End If
                    Else
                        Continue For
                    End If
                Next
            ElseIf RadioFirefox.Checked = True Then
                Dim DdeClient As New DdeClient("Firefox", "WWW_GetWindowInfo")
                DdeClient.Connect()
                Dim URL As String = DdeClient.Request("URL", Integer.MaxValue)
                DdeClient.Disconnect()
                URL = Split(URL, """,""")(0)
                URL = Split(URL, """")(1)
                If URL.Contains("youtube.com/user") = True OrElse URL.Contains("youtube.com/channel") = True Then
                    Dim Name As String = Split(Split(URL, "youtube.com/")(1), "/")(1)
                    Dim Found As Boolean = False
                    For Each LVItem As ListViewItem In ListChannels.Items
                        If LVItem.SubItems(0).Text = Name Then Found = True
                    Next
                    If URL.Contains("youtube.com/user") = True Then
                        If Found = False Then ListChannels.Items.Add(Name).SubItems.AddRange({"user", "waiting"})
                    Else
                        If Found = False Then ListChannels.Items.Add(Name).SubItems.AddRange({"channel", "waiting"})
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox("Couldn't add from url, Error: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub

    Private Sub BtnCheckURL_Click(sender As Object, e As EventArgs) Handles BtnCheckURL.Click
        Try
            Dim CurrentURL As String = String.Empty
            If RadioChrome.Checked = True Then
                Dim ProcessChrome As Process() = Process.GetProcessesByName("chrome")
                For Each Chrome As Process In ProcessChrome
                    If Chrome.MainWindowHandle = IntPtr.Zero Then Continue For
                    Dim AutomationElement As System.Windows.Automation.AutomationElement = System.Windows.Automation.AutomationElement.FromHandle(Chrome.MainWindowHandle)
                    Dim AutomationElementURL As System.Windows.Automation.AutomationElement = AutomationElement.FindFirst(TreeScope.Descendants, New PropertyCondition(System.Windows.Automation.AutomationElement.NameProperty, "Address and search bar"))
                    If AutomationElementURL Is Nothing Then
                        Continue For
                    End If
                    Dim AutomationPattern As System.Windows.Automation.AutomationPattern() = AutomationElementURL.GetSupportedPatterns()
                    If AutomationPattern.Length = 0 Then
                        Continue For
                    End If
                    Dim ValuePattern As System.Windows.Automation.ValuePattern = DirectCast(AutomationElementURL.GetCurrentPattern(AutomationPattern(0)), System.Windows.Automation.ValuePattern)
                    If AutomationElementURL.GetCurrentPropertyValue(System.Windows.Automation.AutomationElement.HasKeyboardFocusProperty) Then
                        Continue For
                    End If
                    Dim URL As String = ValuePattern.Current.Value.Trim
                    If URL.Contains("youtube.com/user") = True OrElse URL.Contains("youtube.com/channel") = True Then
                        CurrentURL = "https://" & URL
                    Else
                        Continue For
                    End If
                Next
            ElseIf RadioFirefox.Checked = True Then
                Dim DdeClient As New DdeClient("Firefox", "WWW_GetWindowInfo")
                DdeClient.Connect()
                Dim URL As String = DdeClient.Request("URL", Integer.MaxValue)
                DdeClient.Disconnect()
                URL = Split(URL, """,""")(0)
                URL = Split(URL, """")(1)
                If URL.Contains("youtube.com/user") = True OrElse URL.Contains("youtube.com/channel") = True Then
                    CurrentURL = "https://" & URL
                End If
            End If
            Dim Name As String = String.Empty
            Dim WebClient As New WebClient
            WebClient.Headers("User-Agent") = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko"
            Dim Source As String = WebClient.DownloadString(CurrentURL)
            Clipboard.SetText(Source)
            Source = Source.Replace(Chr(10), String.Empty)
            Dim Split01 As String = Split(Source, "spf-link branded-page-header-title-link yt-uix-sessionlink")(1)
            Dim Split02 As String = Split(Split01, "data-sessionlink")(0)
            Dim Split03 As String = Split(Split02, "title=""")(1)
            Name = Split(Split03, """")(0)
            Name = Replace(Name, "/", String.Empty)
            Name = Replace(Name, "|", String.Empty)
            Dim Path As String = Application.StartupPath & "\" & Name & "\"
            If My.Computer.FileSystem.DirectoryExists(Path) = False Then
                MsgBox("Channel Not Found", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Warning")
            Else
                If My.Computer.FileSystem.FileExists(Path & "cover.png") = True AndAlso My.Computer.FileSystem.FileExists(Path & "profile.png") = True Then
                    MsgBox("Channel Found", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Info")
                Else
                    If My.Computer.FileSystem.FileExists(Path & "cover.png") = True AndAlso My.Computer.FileSystem.FileExists(Path & "profile.png") = False Then
                        MsgBox("Channel Found But Profile Image is Missing", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Warning")
                    ElseIf My.Computer.FileSystem.FileExists(Path & "cover.png") = False AndAlso My.Computer.FileSystem.FileExists(Path & "profile.png") = True Then
                        MsgBox("Channel Found But Cover Image is Missing", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Warning")
                    ElseIf My.Computer.FileSystem.FileExists(Path & "cover.png") = False AndAlso My.Computer.FileSystem.FileExists(Path & "profile.png") = False Then
                        MsgBox("Channel Found But Both Images are Missing", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Warning")
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox("Couldn't Check URL, Error: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub
End Class
