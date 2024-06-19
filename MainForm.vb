Imports System.Windows.Forms

Public Class MainForm
    ' Declare controls with WithEvents
    Private WithEvents btnCreateSubmission As Button
    Private WithEvents btnViewSubmissions As Button
    Private lblHeader As Label

    ' Constructor for the form
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Initialize controls
        Me.btnCreateSubmission = New Button()
        Me.btnViewSubmissions = New Button()
        Me.lblHeader = New Label()

        ' Set properties for lblHeader
        Me.lblHeader.Text = "Harsh Kumar, Slidely Task 2 - Slidely Form App"
        Me.lblHeader.Font = New Font(lblHeader.Font, FontStyle.Bold)
        Me.lblHeader.Location = New Point(50, 20)
        Me.lblHeader.Size = New Size(300, 20) ' Adjust the size as needed

        ' Set properties for btnCreateSubmission
        Me.btnCreateSubmission.Text = "Create Submission"
        Me.btnCreateSubmission.Location = New Point(50, 50)

        ' Set properties for btnViewSubmissions
        Me.btnViewSubmissions.Text = "View Submissions"
        Me.btnViewSubmissions.Location = New Point(50, 100)

        ' Add controls to form controls collection
        Me.Controls.Add(Me.lblHeader)
        Me.Controls.Add(Me.btnCreateSubmission)
        Me.Controls.Add(Me.btnViewSubmissions)
    End Sub

    ' Event handler for btnCreateSubmission_Click
    Private Sub btnCreateSubmission_Click(sender As Object, e As EventArgs) Handles btnCreateSubmission.Click
        ' Open the Create Submission Form
        Dim createSubmissionForm As New CreateSubmissionForm()
        createSubmissionForm.Show()
    End Sub

    ' Event handler for btnViewSubmissions_Click
    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles btnViewSubmissions.Click
        ' Open the View Submissions Form
        Dim viewSubmissionsForm As New ViewSubmissionsForm()
        viewSubmissionsForm.Show()
    End Sub

    ' Override ProcessCmdKey to handle keyboard shortcuts
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.N) Then
            ' Ctrl+N pressed, open Create Submission Form
            btnCreateSubmission.PerformClick()
            Return True
        ElseIf keyData = (Keys.Control Or Keys.V) Then
            ' Ctrl+V pressed, open View Submissions Form
            btnViewSubmissions.PerformClick()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
End Class
