Imports System.Collections.Generic
Imports System.Text
Imports Newtonsoft.Json

Namespace Data
	Public Class PandoraStation
		Inherits PandoraData
		<JsonProperty(PropertyName := "stationName")> _
		Public Property Name() As String
			Get
				Return m_Name
			End Get
			Friend Set
				m_Name = Value
			End Set
		End Property
		Private m_Name As String

		<JsonProperty(PropertyName := "stationId")> _
		Public Property Id() As String
			Get
				Return m_Id
			End Get
			Friend Set
				m_Id = Value
			End Set
		End Property
		Private m_Id As String

		<JsonProperty(PropertyName := "stationToken")> _
		Public Property Token() As String
			Get
				Return m_Token
			End Get
			Friend Set
				m_Token = Value
			End Set
		End Property
		Private m_Token As String

		<JsonProperty(PropertyName := "isQuickMix")> _
		Public Property IsQuickMix() As Boolean
			Get
				Return m_IsQuickMix
			End Get
			Friend Set
				m_IsQuickMix = Value
			End Set
		End Property
		Private m_IsQuickMix As Boolean

        <JsonProperty(PropertyName:="stationDetailUrl")> _
        Public Property StationURL As String
            Get
                Return _stationURL
            End Get
            Set(value As String)
                _stationURL = value
            End Set
        End Property
        Private _stationURL As String

	End Class
End Namespace
