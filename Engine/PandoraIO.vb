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

''' <summary>
''' Low level class providing direct access to the Pandora API. 
''' </summary>
Public Class PandoraIO

    Property PartnerCredintials As Data.PandoraPartner
    Private baseUrl As String
    Friend Shared crypter As BlowfishCipher

    Public Sub New(AccountType As AccountType)
        PartnerCredintials = New PandoraPartner(AccountType)
        baseUrl = PartnerCredintials.API_URL + "/services/json/?"
        crypter = New BlowfishCipher(PartnerCredintials)
    End Sub

    ''' <summary>
    ''' Initiates a Pandora session.
    ''' </summary>
    ''' <param name="proxy">If not null, this proxy will be used to connect to the Pandora servers.</param>
    ''' <returns>A PandoraSession object that should be used with all other requests.</returns>
	Public Function PartnerLogin(proxy As WebProxy) As PandoraSession
        Return DirectCast(ExecuteRequest(New PartnerLoginRequest(PartnerCredintials), proxy), PandoraSession)
	End Function

    ''' <summary>
    ''' Given the username and password, attempts to log into the Pandora music service.
    ''' </summary>
    ''' <returns>If login is successful, returns a PandoraUser object. If invalid username or password
    ''' null is returned.</returns>
	Public Function UserLogin(session As PandoraSession, username As String, password As String, proxy As WebProxy) As PandoraUser
		Try
			Dim user As PandoraUser = DirectCast(ExecuteRequest(New UserLoginRequest(session, username, password), proxy), PandoraUser)
            user.PartnerCredentials = PartnerCredintials
            session.User = user
            user.Password = password
			Return user
		Catch e As PandoraException
			If e.ErrorCode = ErrorCodeEnum.AUTH_INVALID_USERNAME_PASSWORD Then
				Return Nothing
            End If
			Throw
		End Try
	End Function

    ''' <summary>
    ''' Retrieves a list of stations for the current user.
    ''' </summary>
	Public Function GetStations(session As PandoraSession, proxy As WebProxy) As List(Of PandoraStation)
		If session Is Nothing OrElse session.User Is Nothing Then
			Throw New PandoraException("User must be logged in to make this request.")
		End If

        Dim response As GetStationListResponse = DirectCast(ExecuteRequest(New GetStationListRequest(session), proxy), GetStationListResponse)
		Return response.Stations
	End Function

    ''' <summary>
    ''' Retrieves a playlist for the given station.
    ''' </summary>
	Public Function GetSongs(session As PandoraSession, station As PandoraStation, proxy As WebProxy) As List(Of PandoraSong)
		If session Is Nothing OrElse session.User Is Nothing Then
			Throw New PandoraException("User must be logged in to make this request.")
		End If

		Dim response As GetPlaylistResponse = DirectCast(ExecuteRequest(New GetPlaylistRequest(session, station.Token), proxy), GetPlaylistResponse)
		Return response.Songs

	End Function

    Public Function RateSong(session As PandoraSession, station As PandoraStation, song As PandoraSong, rating As PandoraRating, proxy As WebProxy) As PandoraSongFeedback
        If session Is Nothing OrElse session.User Is Nothing Then
            Throw New PandoraException("User must be logged in to make this request.")
        End If

        Dim feedbackObj As PandoraSongFeedback = DirectCast(ExecuteRequest(New AddFeedbackRequest(session, station.Token, song.Token, rating = PandoraRating.Love), proxy), PandoraSongFeedback)
        song.Rating = rating

        Return feedbackObj
    End Function

    Public Sub AddTiredSong(session As PandoraSession, song As PandoraSong, proxy As WebProxy)
        If session Is Nothing OrElse session.User Is Nothing Then
            Throw New PandoraException("User must be logged in to make this request.")
        End If

        ExecuteRequest(New SleepSongRequest(session, song.Token), proxy)
        song.TemporarilyBanned = True
    End Sub


	''' <summary>
	''' Returns true if the given PandoraSong is still valid. Links will expire after an unspecified
	''' number of hours.
	''' </summary>
	Public Function IsValid(song As PandoraSong, proxy As WebProxy) As Boolean
		Try
			Dim request As WebRequest = WebRequest.Create(song.AudioURL)
			If proxy IsNot Nothing Then
				request.Proxy = proxy
			End If
			request.Method = "HEAD"

			Using response As WebResponse = request.GetResponse()
				Dim bytes As Long = response.ContentLength
			End Using

			Return True
		Catch generatedExceptionName As WebException
			Return False
		End Try
	End Function

	Private Function ExecuteRequest(request As PandoraRequest, proxy As WebProxy) As PandoraData
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
            Dim postData As Byte() = encoder.GetBytes(If(request.IsEncrypted, crypter.EnCrypt(postStr), postStr))

            ' configure our connection
            ServicePointManager.Expect100Continue = False
            Dim webRequest As WebRequest = webRequest.Create(url)
            webRequest.ContentType = "text/xml"
            webRequest.ContentLength = postData.Length
            webRequest.Method = "POST"
            If proxy IsNot Nothing Then
                webRequest.Proxy = proxy
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
                    'Console.WriteLine(url);
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
