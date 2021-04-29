Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Globalization
Imports System.Environment
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Net.IPAddress
Imports System.Net.WebException



Public Class Form1

    Private psi As ProcessStartInfo 'msdos'
    Private cmd As Process 'msdos'
    Private Delegate Sub InvokeWithString(ByVal text As String) 'msdos'
    Public Sub runCMD() 'msdos'
        Try
            cmd.Kill()
        Catch ex As Exception
        End Try
        rtbResult.Clear()
        If txtCmd.Text.Contains(" ") Then
            psi = New ProcessStartInfo(txtCmd.Text.Split(" ")(0), txtCmd.Text.Split(" ")(1))
        Else
            psi = New ProcessStartInfo(txtCmd.Text$)
        End If
        Dim systemencoding As System.Text.Encoding
        System.Text.Encoding.GetEncoding(Globalization.CultureInfo.CurrentUICulture.TextInfo.OEMCodePage)
        With psi
            .UseShellExecute = False
            .RedirectStandardError = True
            .RedirectStandardOutput = True
            .RedirectStandardInput = True
            .CreateNoWindow = True
            .StandardOutputEncoding = systemencoding
            .StandardErrorEncoding = systemencoding
        End With
        cmd = New Process With {.StartInfo = psi, .EnableRaisingEvents = True}
        AddHandler cmd.ErrorDataReceived, AddressOf Async_Data_Received
        AddHandler cmd.OutputDataReceived, AddressOf Async_Data_Received
        If txtCmd.Text = "" Then
            MsgBox("Vui lòng nhập đầy đủ thông tin", vbOKOnly & vbExclamation, "Command Prompt")
        Else
            cmd.Start()
            cmd.BeginOutputReadLine()
            cmd.BeginErrorReadLine()


        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnRun.Click
        'runCMD() 'msdos'
    End Sub
    Private Sub Async_Data_Received(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
        Me.Invoke(New InvokeWithString(AddressOf Sync_Output), e.Data) 'msdos'
    End Sub
    Private Sub Sync_Output(ByVal text As String)
        rtbResult.AppendText(text & Environment.NewLine) 'msdos'
        rtbResult.ScrollToCaret()
    End Sub
    Private Sub txtCmd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCmd.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then 'msdos'
            SendKeys.Send("{TAB}")
            e.Handled = True
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Process.Start("https://newsystemvietnam.com") ' link web'
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)
        Process.Start("https://newsystemvietnam.com") ' link web anh'
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        RichTextBox1.Clear() 'Code for get IP'
        Dim IPS As IPAddress()
        Try
            IPS = Dns.GetHostAddresses(TextBox1.Text)
            For I = 0 To IPS.Length - 1

                RichTextBox1.Text = RichTextBox1.Text & IPS(I).ToString & vbCrLf
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then 'msdos'
            SendKeys.Send("{TAB}")
            e.Handled = True
        End If
    End Sub

    Private Sub Button3_Click_2(sender As Object, e As EventArgs) Handles Button3.Click
        RichTextBox1.Text = "" 'Code for get dns'
        Dim NOMBRE As IPHostEntry
        Try
            NOMBRE = Dns.GetHostEntry(TextBox3.Text)
            RichTextBox1.Text = NOMBRE.HostName
        Catch ex As Exception
            MsgBox(ex.Message,, "Command Prompt")
        End Try
    End Sub
    Private Function getcom() As String
        Return Dns.GetHostName 'Code for Local IP'
    End Function

    Private Sub Form1_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.AddRange(Dns.GetHostAddresses(getcom)) 'Code for Local IP'

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim client As New WebClient 'codde for IP public
        '// Add a user agent header in case the requested URI contains a query.
        client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR1.0.3705;)")
        Dim baseurl As String = "http://checkip.dyndns.org/"
        ' with proxy server only:
        Dim proxy As IWebProxy = WebRequest.GetSystemWebProxy()
        proxy.Credentials = CredentialCache.DefaultNetworkCredentials
        client.Proxy = proxy
        Dim data As Stream
        Try
            data = client.OpenRead(baseurl)
        Catch ex As Exception
            MsgBox("open url " & ex.Message)
            Exit Sub
        End Try
        Dim reader As StreamReader = New StreamReader(data)
        Dim s As String = reader.ReadToEnd()
        data.Close()
        reader.Close()
        s = s.Replace("<html><head><title>Current IP Check</title></head><body>", "").Replace("</body></html>", "").ToString()
        'MessageBox.Show(s)
        TextBox2.Text = s
    End Sub

    Private Sub LinkLabel1_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("https://newsystemvietnam.com")
    End Sub

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Process.Start("https://newsystemvietnam.com")
    End Sub



    Private Sub Label6_MouseMove(sender As Object, e As MouseEventArgs) Handles Label6.MouseMove
        Label8.Text = "Gõ lệnh help để biết thêm các lệnh khác"
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub rtbResult_TextChanged(sender As Object, e As EventArgs) Handles rtbResult.TextChanged

    End Sub
End Class
