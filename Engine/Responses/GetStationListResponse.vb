Imports System.Collections.Generic
Imports System.Text
Imports Newtonsoft.Json
Imports Pandorian.Engine.Data

Namespace Responses
	Friend Class GetStationListResponse
		Inherits PandoraData

		<JsonProperty(PropertyName := "stations")> _
		Public Property Stations() As List(Of PandoraStation)
			Get
				Return m_Stations
			End Get
			Set
				m_Stations = Value
			End Set
		End Property
		Private m_Stations As List(Of PandoraStation)

	End Class
End Namespace
