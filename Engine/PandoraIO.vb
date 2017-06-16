Imports System.Collections.Generic
Imports System.Text
Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports System.Web
Imports Pandorian.Engine.Responses
Imports Pandorian.Engine.Requests
Imports Pandorian.Engine.Data
Imports Pandorian.Engine.Encryption

<Serializable()>
Public Class PandoraIO

    Property PartnerCredintials As Data.PandoraPartner
    Property Session As PandoraSession
    Property Proxy As WebProxy

    Private baseUrl As String
    Private Crypter As BlowfishCipher

    Public Sub New(AccountType As AccountType)
        PartnerCredintials = New PandoraPartner(AccountType)
        baseUrl = PartnerCredintials.API_URL + "/services/json/?"
        Crypter = New BlowfishCipher(PartnerCredintials)
    End Sub

    Public Function PartnerLogin() As PandoraSession
        Dim session As PandoraSession = DirectCast(ExecuteRequest(New PartnerLoginRequest(PartnerCredintials)), PandoraSession)
        session.Crypter = Crypter
        Return session
    End Function

    Public Function UserLogin(username As String, password As String) As PandoraUser
        Try
            Dim user As PandoraUser = DirectCast(ExecuteRequest(New UserLoginRequest(Session, username, password)), PandoraUser)
            user.PartnerCredentials = PartnerCredintials
            Session.User = user
            user.Password = password
            Return user
        Catch e As PandoraException
            If e.ErrorCode = ErrorCodeEnum.AUTH_INVALID_USERNAME_PASSWORD Then
                Return Nothing
            End If
            Throw
        End Try
    End Function

    Public Function GetStations() As List(Of PandoraStation)
        If Session Is Nothing OrElse Session.User Is Nothing Then
            Throw New PandoraException("User must be logged in to make this request.")
        End If

        Dim response As GetStationListResponse = DirectCast(ExecuteRequest(New GetStationListRequest(Session)), GetStationListResponse)

        If Not response.Stations.Count = 0 Then
            For Each s As PandoraStation In response.Stations
                s.PandoraIO = Me
            Next
        End If

        Return response.Stations
    End Function

    Public Function GetSongs(stationToken As String) As List(Of PandoraSong)
        If Session Is Nothing OrElse Session.User Is Nothing Then
            Throw New PandoraException("User must be logged in to make this request.")
        End If

        Dim response As GetPlaylistResponse = DirectCast(ExecuteRequest(New GetPlaylistRequest(Session, stationToken)), GetPlaylistResponse)

        For Each s As PandoraSong In response.Songs
            If Not String.IsNullOrEmpty(s.AdditionalAudioUrl) Then
                Dim aui As PandoraSong.AudioUrlInfo = New PandoraSong.AudioUrlInfo()
                aui.Url = s.AdditionalAudioUrl
                s.AudioUrlMap.Add("128mp3", aui)
            End If
        Next

        Return response.Songs

    End Function

    Public Function RateSong(station As PandoraStation, song As PandoraSong, rating As PandoraRating) As PandoraSongFeedback
        If Session Is Nothing OrElse Session.User Is Nothing Then
            Throw New PandoraException("User must be logged in to make this request.")
        End If

        Dim feedbackObj As PandoraSongFeedback = DirectCast(ExecuteRequest(New AddFeedbackRequest(Session, station.Token, song.Token, rating = PandoraRating.Love)), PandoraSongFeedback)
        song.Rating = rating

        Return feedbackObj
    End Function

    Public Sub AddTiredSong(song As PandoraSong)
        If Session Is Nothing OrElse Session.User Is Nothing Then
            Throw New PandoraException("User must be logged in to make this request.")
        End If

        ExecuteRequest(New SleepSongRequest(Session, song.Token))
        song.TemporarilyBanned = True
    End Sub

    Private Function ExecuteRequest(request As PandoraRequest) As PandoraData
        Try
            Dim encoder As New ASCIIEncoding()

            ' build url for request to pandora servers
            Dim prefix As String = If(request.IsSecure, "https://", "http://")
            Dim url As String = prefix & baseUrl
            url += "method=" & request.MethodName
            If request.User IsNot Nothing Then
                url += [String].Format("&user_id={0}", HttpUtility.UrlEncode(request.User.Id))
            End If
            If request.Session IsNot Nothing Then
                url += [String].Format("&auth_token={0}", HttpUtility.UrlEncode(If(request.UserAuthToken Is Nothing, request.Session.PartnerAuthToken, request.UserAuthToken)))
                url += [String].Format("&partner_id={0}", request.Session.PartnerId)
            End If

            ' build the post data for our request
            Dim settings As New JsonSerializerSettings()
            settings.NullValueHandling = NullValueHandling.Ignore
            Dim postStr As String = JsonConvert.SerializeObject(request, settings)
            Dim postData As Byte() = encoder.GetBytes(If(request.IsEncrypted, Crypter.EnCrypt(postStr), postStr))

            ' configure our connection
            ServicePointManager.Expect100Continue = False
            Dim webRequest As WebRequest = webRequest.Create(url)
            webRequest.ContentType = "text/xml"
            webRequest.ContentLength = postData.Length
            webRequest.Method = "POST"
            If Proxy IsNot Nothing Then
                webRequest.Proxy = Proxy
            End If

            ' send request to remote servers
            Dim os As Stream = webRequest.GetRequestStream()
            os.Write(postData, 0, postData.Length)
            os.Close()

            ' retrieve reply from servers
            Using response As WebResponse = webRequest.GetResponse()
                Dim sr As New StreamReader(response.GetResponseStream())
                Dim replyStr As String = sr.ReadToEnd()
                Dim reply As PandoraResponse = JsonConvert.DeserializeObject(Of PandoraResponse)(replyStr)

                ' parse and throw any errors or return our result
                If Not reply.Success Then
                    Throw New PandoraException(reply)
                End If

                If request.ReturnType Is Nothing Then
                    Return Nothing
                End If
                Return DirectCast(JsonConvert.DeserializeObject(reply.Result.ToString(), request.ReturnType), PandoraData)
            End Using
        Catch ex As Exception
            If TypeOf ex Is PandoraException Then
                Throw
            End If
            Throw New PandoraException("Unexpected error communicating with server: " + ex.Message, ex)
        End Try
    End Function
End Class
Public Enum PandoraRating
	Love = 1
	Unrated = 0
	Hate = -1
End Enum
