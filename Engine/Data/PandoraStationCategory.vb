Imports System.Collections.Generic
Imports System.Text
Imports Newtonsoft.Json

Namespace Data
	Public Class PandoraStationCategory
		Inherits PandoraData
		<JsonProperty(PropertyName := "categoryName")> _
		Public Property Name() As [String]
			Get
				Return m_Name
			End Get
			Friend Set
				m_Name = Value
			End Set
		End Property
		Private m_Name As [String]

		<JsonProperty(PropertyName := "stations")> _
		Public Property Stations() As List(Of PandoraStation)
			Get
				Return m_Stations
			End Get
			Friend Set
				m_Stations = Value
			End Set
		End Property
		Private m_Stations As List(Of PandoraStation)
	End Class
End Namespace
