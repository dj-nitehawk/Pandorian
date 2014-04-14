Imports System.Collections.Generic
Imports System.Text
Imports Newtonsoft.Json
Imports Pandorian.Engine.Data

Namespace Requests
	Friend Class AddFeedbackRequest
		Inherits PandoraRequest
		Public Overrides ReadOnly Property MethodName() As String
			Get
				Return "station.addFeedback"
			End Get
		End Property

		Public Overrides ReadOnly Property ReturnType() As Type
			Get
				Return GetType(PandoraSongFeedback)
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

		<JsonProperty(PropertyName := "isPositive")> _
		Public Property IsPositive() As Boolean
			Get
				Return m_IsPositive
			End Get
			Set
				m_IsPositive = Value
			End Set
		End Property
		Private m_IsPositive As Boolean

		Public Sub New(session As PandoraSession, stationToken As String, trackToken As String, positive As Boolean)
			MyBase.New(session)
			Me.StationToken = stationToken
			Me.TrackToken = trackToken
			Me.IsPositive = positive
		End Sub
	End Class
End Namespace
