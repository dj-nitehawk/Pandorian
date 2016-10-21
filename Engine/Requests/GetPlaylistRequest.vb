Imports System.Collections.Generic
Imports System.Text
Imports Newtonsoft.Json
Imports Pandorian.Engine.Data
Imports Pandorian.Engine.Responses

Namespace Requests
	Friend Class GetPlaylistRequest
		Inherits PandoraRequest
		Public Overrides ReadOnly Property MethodName() As String
			Get
				Return "station.getPlaylist"
			End Get
		End Property

		Public Overrides ReadOnly Property ReturnType() As Type
			Get
				Return GetType(GetPlaylistResponse)
			End Get
		End Property

		Public Overrides ReadOnly Property IsSecure() As Boolean
			Get
				Return True
			End Get
		End Property

		Public Overrides ReadOnly Property IsEncrypted() As Boolean
			Get
				Return True
			End Get
		End Property

		<JsonProperty(PropertyName := "stationToken")> _
		Public Property StationToken() As String
			Get
				Return m_StationToken
			End Get
			Set
				m_StationToken = Value
			End Set
		End Property
        Private m_StationToken As String
        <JsonProperty(PropertyName:="additionalAudioUrl")>
        Public ReadOnly Property AdditionalAudioTypeRequests() As String
            Get
                Return "HTTP_128_MP3"
                'Return ""
            End Get
        End Property

        Public Sub New(session As PandoraSession, stationToken As String)
			MyBase.New(session)
			Me.StationToken = stationToken
		End Sub
	End Class
End Namespace
