Imports System.Collections.Generic
Imports System.Text
Imports Newtonsoft.Json
Imports Pandorian.Engine.Data

Namespace Requests
	<JsonObject(MemberSerialization.OptIn)> _
	Friend MustInherit Class PandoraRequest
		Public MustOverride ReadOnly Property MethodName() As String

		Public MustOverride ReadOnly Property ReturnType() As Type

		Public MustOverride ReadOnly Property IsSecure() As Boolean

		Public MustOverride ReadOnly Property IsEncrypted() As Boolean

		Public Property User() As PandoraUser
			Get
				Return m_User
			End Get
			Set
				m_User = Value
			End Set
		End Property
		Private m_User As PandoraUser

		Public Property Session() As PandoraSession
			Get
				Return m_Session
			End Get
			Set
				m_Session = Value
			End Set
		End Property
		Private m_Session As PandoraSession

		<JsonProperty(PropertyName := "userAuthToken")> _
		Public ReadOnly Property UserAuthToken() As String
			Get
				If User IsNot Nothing Then
					Return User.AuthorizationToken
				End If
				Return Nothing
			End Get
		End Property

		<JsonProperty(PropertyName := "syncTime")> _
		Public ReadOnly Property SyncTime() As System.Nullable(Of Long)
			Get
				If Session IsNot Nothing Then
                    Return Session.GetSyncTime()
				End If
				Return Nothing
			End Get
		End Property

		Public Sub New()
			Me.New(Nothing)
		End Sub

		Public Sub New(session As PandoraSession)
			Me.Session = session
            Me.User = If(session Is Nothing, Nothing, session.User)
		End Sub
	End Class
End Namespace
