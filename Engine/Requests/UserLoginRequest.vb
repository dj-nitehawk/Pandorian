Imports System.Collections.Generic
Imports System.Text
Imports Newtonsoft.Json
Imports Pandorian.Engine.Data

Namespace Requests
	Friend Class UserLoginRequest
		Inherits PandoraRequest

		Public Overrides ReadOnly Property MethodName() As String
			Get
				Return "auth.userLogin"
			End Get
		End Property

		Public Overrides ReadOnly Property ReturnType() As Type
			Get
				Return GetType(PandoraUser)
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

		<JsonProperty(PropertyName := "includeDemographics")> _
		Public ReadOnly Property IncludeDemographics() As Boolean
			Get
				Return True
			End Get
		End Property

		<JsonProperty(PropertyName := "includePandoraOneInfo")> _
		Public ReadOnly Property IncludePandoraOneInfo() As Boolean
			Get
				Return True
			End Get
		End Property

		<JsonProperty(PropertyName := "includeAdAttributes")> _
		Public ReadOnly Property IncludeAdAttributes() As Boolean
			Get
				Return True
			End Get
		End Property

		<JsonProperty(PropertyName := "returnStationList")> _
		Public ReadOnly Property ReturnStationList() As Boolean
			Get
				Return False
			End Get
		End Property

		<JsonProperty(PropertyName := "returnGenreStations")> _
		Public ReadOnly Property ReturnGenreStations() As Boolean
			Get
				Return False
			End Get
		End Property

		<JsonProperty(PropertyName := "includeStationArtUrl")> _
		Public ReadOnly Property IncludeStationArtUrl() As Boolean
			Get
				Return True
			End Get
		End Property

		<JsonProperty(PropertyName := "complimentarySponsorSupported")> _
		Public ReadOnly Property ComplimentarySponsorSupported() As Boolean
			Get
				Return True
			End Get
		End Property

		<JsonProperty(PropertyName := "includeSubscriptionExpiration")> _
		Public ReadOnly Property IncludeSubscriptionExpiration() As Boolean
			Get
				Return True
			End Get
		End Property

		<JsonProperty(PropertyName := "partnerAuthToken")> _
		Public ReadOnly Property PartnerAuthToken() As String
			Get
				If Session IsNot Nothing Then
					Return Session.PartnerAuthToken
				End If
				Return Nothing
			End Get
		End Property

		<JsonProperty(PropertyName := "loginType")> _
		Public ReadOnly Property LoginType() As String
			Get
				Return "user"
			End Get
		End Property

		<JsonProperty(PropertyName := "username")> _
		Public Property UserName() As String
			Get
				Return m_UserName
			End Get
			Set
				m_UserName = Value
			End Set
		End Property
		Private m_UserName As String

		<JsonProperty(PropertyName := "password")> _
		Public Property Password() As String
			Get
				Return m_Password
			End Get
			Set
				m_Password = Value
			End Set
		End Property
		Private m_Password As String

		Public Sub New(session As PandoraSession, username As String, password As String)
			MyBase.New(session)

			Me.UserName = username
			Me.Password = password
		End Sub
	End Class
End Namespace
