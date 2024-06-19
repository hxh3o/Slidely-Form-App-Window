Imports System.Windows.Forms
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports Newtonsoft.Json
Imports System.Threading.Tasks

Public Class CreateSubmissionForm
    Private WithEvents btnSubmit As Button
    Private lblHeader As Label
    Private lblName As Label
    Private lblEmail As Label
    Private lblPhone As Label
    Private lblGithub As Label
    Private lblStopwatch As Label
    Private txtName As TextBox
    Private txtEmail As TextBox
    Private txtPhone As TextBox
    Private txtGithub As TextBox
    Private txtStopwatch As TextBox
    Private WithEvents btnToggleStopwatch As Button
    Private stopwatch As Stopwatch
    Private isStopwatchRunning As Boolean = False

    Public Sub New()
        InitializeComponent()

        ' Initialize controls
        Me.btnSubmit = New Button()
        Me.lblHeader = New Label()
        Me.lblName = New Label()
        Me.lblEmail = New Label()
        Me.lblPhone = New Label()
        Me.lblGithub = New Label()
        Me.lblStopwatch = New Label()
        Me.txtName = New TextBox()
        Me.txtEmail = New TextBox()
        Me.txtPhone = New TextBox()
        Me.txtGithub = New TextBox()
        Me.txtStopwatch = New TextBox()
        Me.btnToggleStopwatch = New Button()
        Me.stopwatch = New Stopwatch()

        ' Add controls to form controls collection
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.lblHeader)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.lblEmail)
        Me.Controls.Add(Me.lblPhone)
        Me.Controls.Add(Me.lblGithub)
        Me.Controls.Add(Me.lblStopwatch)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtPhone)
        Me.Controls.Add(Me.txtGithub)
        Me.Controls.Add(Me.txtStopwatch)
        Me.Controls.Add(Me.btnToggleStopwatch)

        ' Set properties for controls
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.Location = New Point(150, 230) ' Adjusted to ensure it is visible
        '  Me.btnSubmit.Size = New Size(100, 30) ' Adjust size as needed

        Me.lblHeader.Text = "Harsh Kumar, Slidely Task 2 - Create Submission"
        Me.lblHeader.Font = New Font(lblHeader.Font, FontStyle.Bold)
        Me.lblHeader.Location = New Point(50, 20)
        Me.lblHeader.Size = New Size(300, 20)

        Me.lblName.Text = "Name:"
        Me.lblName.Location = New Point(50, 80)
        Me.txtName.Location = New Point(150, 80)

        Me.lblEmail.Text = "Email:"
        Me.lblEmail.Location = New Point(50, 110)
        Me.txtEmail.Location = New Point(150, 110)

        Me.lblPhone.Text = "Phone:"
        Me.lblPhone.Location = New Point(50, 140)
        Me.txtPhone.Location = New Point(150, 140)

        Me.lblGithub.Text = "Github Link for Task-2:"
        Me.lblGithub.Location = New Point(50, 170)
        Me.txtGithub.Location = New Point(150, 170)

        ''Me.lblStopwatch.Text = "Stopwatch Time:"
        '  Me.lblStopwatch.Location = New Point(50, 200)
         Me.txtStopwatch.Location = New Point(150, 200) ''

        ' Toggle Stopwatch button
        Me.btnToggleStopwatch.Text = "Toggle Stopwatch"
        Me.btnToggleStopwatch.Location = New Point(50, 200)

        ' Add Click event handlers
        AddHandler btnSubmit.Click, AddressOf btnSubmit_Click
        AddHandler btnToggleStopwatch.Click, AddressOf btnToggleStopwatch_Click
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs)
        ' Validate form data
        If ValidateFormData() Then
            ' Call backend service to submit data
            Dim submitted As Boolean = Await BackendService.SubmitFormData(
                txtName.Text, txtEmail.Text, txtPhone.Text, txtGithub.Text, txtStopwatch.Text)

            ' Handle response
            If submitted Then
                MessageBox.Show("Form submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearForm()
            Else
                MessageBox.Show("Error submitting form. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Please fill all fields before submitting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Function ValidateFormData() As Boolean
        ' Implement validation logic here
        Return Not String.IsNullOrWhiteSpace(txtName.Text) AndAlso
               Not String.IsNullOrWhiteSpace(txtEmail.Text) AndAlso
               Not String.IsNullOrWhiteSpace(txtPhone.Text) AndAlso
               Not String.IsNullOrWhiteSpace(txtGithub.Text)
    End Function

    Private Sub ClearForm()
        ' Clear form fields
        txtName.Clear()
        txtEmail.Clear()
        txtPhone.Clear()
        txtGithub.Clear()
        txtStopwatch.Clear()
    End Sub

    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs)
        If isStopwatchRunning Then
            ' Stop the stopwatch
            stopwatch.Stop()
            isStopwatchRunning = False
            btnToggleStopwatch.Text = "Start Stopwatch"
        Else
            ' Start the stopwatch
            stopwatch.Start()
            isStopwatchRunning = True
            btnToggleStopwatch.Text = "Pause Stopwatch"
            ' Update stopwatch time every second
            Dim timer As New Timer()
            AddHandler timer.Tick, Sub()
                                       txtStopwatch.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
                                   End Sub
            timer.Interval = 1000
            timer.Start()
        End If
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        ' Handle keyboard shortcuts
        Select Case keyData
            Case Keys.Control Or Keys.S
                ' Ctrl + S for Submit
                btnSubmit.PerformClick()
                Return True
            Case Keys.Control Or Keys.T
                ' Ctrl + T for Toggle Stopwatch
                btnToggleStopwatch.PerformClick()
                Return True
        End Select

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
End Class
