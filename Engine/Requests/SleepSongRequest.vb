Imports System.Collections.Generic
Imports System.Text
Imports Newtonsoft.Json
Imports Pandorian.Engine.Data

Namespace Requests
	Friend Class SleepSongRequest
		Inherits PandoraRequest
		Public Overrides ReadOnly Property MethodName() As String
			Get
				Return "user.sleepSong"
			End Get
		End Property

		Public Overrides ReadOnly Property ReturnType() As Type
			Get
				Return Nothing
			End Get
		End Property

		Public Overrides ReadOnly Property IsSecure() As Boolean
			Get
				Return False
			End Get
		End Property

		Public Overrides ReadOnly Property IsEncrypted() As Boolean
			Get
				Return True
			End Get
		End Property

		<JsonProperty(PropertyName := "trackToken")> _
		Public Property TrackToken() As String
			Get
				Return m_TrackToken
			End Get
			Set
				m_TrackToken = Value
			End Set
		End Property
		Private m_TrackToken As String

		Public Sub New(session As PandoraSession, trackToken As String)
			MyBase.New(session)
			Me.TrackToken = trackToken
		End Sub
	End Class
End Namespace
