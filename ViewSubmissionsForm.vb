Imports System.Windows.Forms

Public Class ViewSubmissionsForm
    Private WithEvents btnPrevious As Button
    Private WithEvents btnNext As Button
    Private WithEvents lstSubmissions As ListBox
    Private submissionIndex As Integer = 0
    Private lblHeader As Label

    Public Sub New()
        InitializeComponent()

        ' Initialize controls
        Me.lblHeader = New Label()
        Me.btnPrevious = New Button()
        Me.btnNext = New Button()
        Me.lstSubmissions = New ListBox()

        ' Add controls to form controls collection
        Me.Controls.Add(Me.lblHeader)
        Me.Controls.Add(Me.btnPrevious)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.lstSubmissions)

        ' Set properties for controls
        Me.lblHeader.Text = "Harsh Kumar, Slidely Task 2 - View Submission"
        Me.lblHeader.Font = New Font(lblHeader.Font, FontStyle.Bold)
        Me.lblHeader.Location = New Point(50, 20)
        Me.lblHeader.Size = New Size(300, 20) ' Adjust the size as needed

        Me.btnPrevious.Text = "Previous"
        Me.btnPrevious.Location = New Point(50, 200)

        Me.btnNext.Text = "Next"
        Me.btnNext.Location = New Point(150, 200)

        Me.lstSubmissions.Size = New Size(300, 200)
        Me.lstSubmissions.Location = New Point(50, 50)

        ' Add Click event handlers
        AddHandler btnPrevious.Click, AddressOf btnPrevious_Click
        AddHandler btnNext.Click, AddressOf btnNext_Click

        ' Load submissions
        LoadSubmissions()
    End Sub

    Private Async Sub LoadSubmissions()
        Try
            ' Call backend service to fetch submission
            Dim submission As Submission = Await BackendService.GetSubmission(submissionIndex)

            ' Display submission in the ListBox
            If submission IsNot Nothing Then
                lstSubmissions.Items.Clear()
                lstSubmissions.Items.Add($"Name: {submission.Name}")
                lstSubmissions.Items.Add($"Email: {submission.Email}")
                lstSubmissions.Items.Add($"Phone: {submission.Phone}")
                lstSubmissions.Items.Add($"Github Link: {submission.GithubLink}")
                lstSubmissions.Items.Add($"Stopwatch Time: {submission.StopwatchTime}")
            End If
        Catch ex As Exception
            ' Handle exception
            Console.WriteLine($"Error loading submission: {ex.Message}")
        End Try
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs)
        If submissionIndex > 0 Then
            submissionIndex -= 1
            LoadSubmissions()
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs)
        submissionIndex += 1
        LoadSubmissions()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        ' Handle keyboard shortcuts
        Select Case keyData
            Case Keys.Control Or Keys.P
                ' Ctrl + P for Previous
                btnPrevious.PerformClick()
                Return True
            Case Keys.Control Or Keys.N
                ' Ctrl + N for Next
                btnNext.PerformClick()
                Return True
        End Select

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
End Class
