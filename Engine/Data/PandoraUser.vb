Imports System.Collections.Generic
Imports System.Text
Imports Pandorian.Engine.Encryption
Imports Newtonsoft.Json

Namespace Data
	Public Enum AccountType
        FREE_USER = False
        PANDORA_ONE_USER = True
	End Enum

	<JsonObject(MemberSerialization.OptIn)> _
	Public Class PandoraUser
        Inherits PandoraData

        Property PartnerCredentials As Data.PandoraPartner

		<JsonProperty(PropertyName := "username")> _
		Public Property Name() As String
			Get
				Return m_Name
			End Get
			Friend Set
				m_Name = Value
			End Set
		End Property
		Private m_Name As String

		Friend Property EncryptedPassword() As String
			Get
				Return m_EncryptedPassword
			End Get
			Set
				m_EncryptedPassword = Value
			End Set
		End Property
		Private m_EncryptedPassword As String

		Friend Property Password() As String
			Get
				Try
                    Return New BlowfishCipher(PartnerCredentials).DeCrypt(EncryptedPassword)
				Catch generatedExceptionName As Exception
				End Try
				Return ""
			End Get
			Set
				If value IsNot Nothing Then
                    EncryptedPassword = New BlowfishCipher(PartnerCredentials).EnCrypt(Value)
				End If
			End Set
		End Property

		<JsonProperty(PropertyName := "canListen")> _
		Public Property CanListen() As Boolean
			Get
				Return m_CanListen
			End Get
			Friend Set
				m_CanListen = Value
			End Set
		End Property
		Private m_CanListen As Boolean

		<JsonProperty(PropertyName := "userAuthToken")> _
		Public Property AuthorizationToken() As String
			Get
				Return m_AuthorizationToken
			End Get
			Friend Set
				m_AuthorizationToken = Value
			End Set
		End Property
		Private m_AuthorizationToken As String

		<JsonProperty(PropertyName := "userId")> _
		Public Property Id() As String
			Get
				Return m_Id
			End Get
			Friend Set
				m_Id = Value
			End Set
		End Property
		Private m_Id As String

		<JsonProperty(PropertyName := "hasAudioAds")> _
		Public Property RequiresAds() As Boolean
			Get
				Return m_RequiresAds
			End Get
			Friend Set
				m_RequiresAds = Value
			End Set
		End Property
		Private m_RequiresAds As Boolean

		<JsonProperty(PropertyName := "age")> _
		Public Property Age() As Integer
			Get
				Return m_Age
			End Get
			Set
				m_Age = Value
			End Set
		End Property
		Private m_Age As Integer

		<JsonProperty(PropertyName := "zip")> _
		Public Property ZipCode() As Integer
			Get
				Return m_ZipCode
			End Get
			Friend Set
				m_ZipCode = Value
			End Set
		End Property
		Private m_ZipCode As Integer

		<JsonProperty(PropertyName := "listeningTimeoutMinutes")> _
		Public Property TimeoutMinutes() As Integer
			Get
				Return m_TimeoutMinutes
			End Get
			Friend Set
				m_TimeoutMinutes = Value
			End Set
		End Property
		Private m_TimeoutMinutes As Integer

		Public ReadOnly Property TimeoutInterval() As TimeSpan
			Get
				Return New TimeSpan(0, TimeoutMinutes, 0)
			End Get
		End Property

    End Class
End Namespace
