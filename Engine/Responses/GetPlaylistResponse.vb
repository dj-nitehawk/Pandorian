Imports System.Collections.Generic
Imports System.Text
Imports Newtonsoft.Json
Imports Pandorian.Engine.Data

Namespace Responses
	Friend Class GetPlaylistResponse
		Inherits PandoraData
		<JsonProperty(PropertyName := "items")> _
		Public Property Songs() As List(Of PandoraSong)
			Get
				Return m_Songs
			End Get
			Set
				m_Songs = Value
			End Set
		End Property
		Private m_Songs As List(Of PandoraSong)
	End Class
End Namespace
