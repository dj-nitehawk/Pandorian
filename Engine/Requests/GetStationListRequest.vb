Imports System.Collections.Generic
Imports System.Text
Imports Newtonsoft.Json
Imports Pandorian.Engine.Data
Imports Pandorian.Engine.Responses

Namespace Requests
	Friend Class GetStationListRequest
		Inherits PandoraRequest
		Public Overrides ReadOnly Property MethodName() As String
			Get
				Return "user.getStationList"
			End Get
		End Property

		Public Overrides ReadOnly Property ReturnType() As Type
			Get
				Return GetType(GetStationListResponse)
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

		Public Sub New(session As PandoraSession)
			MyBase.New(session)
		End Sub
	End Class
End Namespace
