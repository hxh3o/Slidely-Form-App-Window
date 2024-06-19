Imports System.Net.Http
Imports System.Net.Http.Headers
Imports Newtonsoft.Json
Imports System.Threading.Tasks

Public Class BackendService
    Private Shared ReadOnly BaseUrl As String = "http://localhost:5000"

    Private Shared ReadOnly HttpClient As New HttpClient()



    Public Shared Async Function PingBackend() As Task(Of Boolean)
        Try
            Dim endpoint As String = $"{BaseUrl}/ping"
            Dim response As HttpResponseMessage = Await HttpClient.GetAsync(endpoint)
            Return response.IsSuccessStatusCode
        Catch ex As Exception
            Console.WriteLine($"Error pinging backend: {ex.Message}")
            Return False
        End Try
    End Function

    Public Shared Async Function SubmitFormData(
        name As String, email As String, phone As String, githubLink As String, stopwatchTime As String) As Task(Of Boolean)
        Try
            Dim endpoint As String = $"{BaseUrl}/api/submit"

            ' Prepare data
            Dim formData As New Dictionary(Of String, String) From {
                {"name", name},
                {"email", email},
                {"phone", phone},
                {"github_link", githubLink},
                {"stopwatch_time", stopwatchTime}
            }

            ' Serialize formData to JSON
            Dim json As String = JsonConvert.SerializeObject(formData)

            ' Prepare HTTP request content
            Dim content As New StringContent(json)
            content.Headers.ContentType = New MediaTypeHeaderValue("application/json")

            ' Send POST request
            Dim response As HttpResponseMessage = Await HttpClient.PostAsync(endpoint, content)

            ' Return true if successfully submitted
            Return response.IsSuccessStatusCode
        Catch ex As Exception
            Console.WriteLine($"Error submitting form data: {ex.Message}")
            Return False
        End Try
    End Function

    Public Shared Async Function GetSubmission(index As Integer) As Task(Of Submission)
        Try
            Dim endpoint As String = $"{BaseUrl}/api/read?index={index}"

            ' Send GET request
            Dim response As HttpResponseMessage = Await HttpClient.GetAsync(endpoint)

            ' Check if request was successful
            If response.IsSuccessStatusCode Then
                ' Deserialize JSON response to Submission object
                Dim json As String = Await response.Content.ReadAsStringAsync()
                Dim submission As Submission = JsonConvert.DeserializeObject(Of Submission)(json)
                Return submission
            Else
                ' Handle unsuccessful response
                Console.WriteLine($"Error fetching submission: {response.StatusCode}")
                Return Nothing
            End If
        Catch ex As Exception
            Console.WriteLine($"Error fetching submission: {ex.Message}")
            Return Nothing
        End Try
    End Function
End Class
