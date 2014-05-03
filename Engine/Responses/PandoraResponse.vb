Imports System.Collections.Generic
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Pandorian.Engine.Data

Namespace Responses
	<JsonObject(MemberSerialization.OptIn)> _
	Friend Class PandoraResponse
		Inherits PandoraData
        Public ReadOnly Property Success() As Boolean
            Get
                Return Status = "ok"
            End Get
        End Property

		<JsonProperty(PropertyName := "stat")> _
		Public Property Status() As String
			Get
				Return m_Status
			End Get
			Set
				m_Status = Value
			End Set
		End Property
		Private m_Status As String

		<JsonProperty(PropertyName := "result")> _
		Public Property Result() As JToken
			Get
				Return m_Result
			End Get
			Set
				m_Result = Value
			End Set
		End Property
		Private m_Result As JToken

		<JsonProperty(PropertyName := "message")> _
		Public Property ErrorMessage() As String
			Get
				Return m_ErrorMessage
			End Get
			Set
				m_ErrorMessage = Value
			End Set
		End Property
		Private m_ErrorMessage As String

		<JsonProperty(PropertyName := "code")> _
		Public Property ErrorCode() As Integer
			Get
				Return m_ErrorCode
			End Get
			Set
				m_ErrorCode = Value
			End Set
		End Property
		Private m_ErrorCode As Integer
	End Class
End Namespace
