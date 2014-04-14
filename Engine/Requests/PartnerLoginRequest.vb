Imports System.Collections.Generic
Imports System.Text
Imports Newtonsoft.Json
Imports Pandorian.Engine.Data

Namespace Requests
	Friend Class PartnerLoginRequest
        Inherits PandoraRequest

        Property PandoraPartnerCredentials As Data.PandoraPartner
        Public Sub New(PartnerCredentials As Data.PandoraPartner)
            PandoraPartnerCredentials = PartnerCredentials
        End Sub

		Public Overrides ReadOnly Property MethodName() As String
			Get
				Return "auth.partnerLogin"
			End Get
		End Property

		Public Overrides ReadOnly Property ReturnType() As Type
			Get
				Return GetType(PandoraSession)
			End Get
		End Property

		Public Overrides ReadOnly Property IsSecure() As Boolean
			Get
				Return True
			End Get
		End Property

		Public Overrides ReadOnly Property IsEncrypted() As Boolean
			Get
				Return False
			End Get
		End Property

		<JsonProperty(PropertyName := "deviceModel")> _
		Public ReadOnly Property DeviceModel() As String
			Get
                Return PandoraPartnerCredentials.DeviceID
			End Get
		End Property

		<JsonProperty(PropertyName := "username")> _
		Public ReadOnly Property UserName() As String
			Get
                Return PandoraPartnerCredentials.Username
			End Get
		End Property

		<JsonProperty(PropertyName := "includeUrls")> _
		Public ReadOnly Property IncludeUrls() As Boolean
			Get
				Return True
			End Get
		End Property

		<JsonProperty(PropertyName := "password")> _
		Public ReadOnly Property Password() As String
			Get
                Return PandoraPartnerCredentials.Password
			End Get
		End Property

		<JsonProperty(PropertyName := "version")> _
		Public ReadOnly Property Version() As String
			Get
                Return PandoraPartnerCredentials.API_Version
			End Get
        End Property

    End Class
End Namespace
