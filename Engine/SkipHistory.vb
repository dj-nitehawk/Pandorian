Imports System.Collections.Generic
Imports System.Text
Imports Pandorian.Engine.Data
Imports Newtonsoft.Json

Public Class SkipHistory
    Private stationSkipHistory As Dictionary(Of String, Queue(Of DateTime))
    Private globalSkipHistory As Queue(Of DateTime)

    Public Function GetStationSkipHisoryJSON() As String
        Return JsonConvert.SerializeObject(stationSkipHistory, Formatting.None)
    End Function
    Public Function GetGlobalSkipHistoryJSON() As String
        Return JsonConvert.SerializeObject(globalSkipHistory, Formatting.None)
    End Function

    Public Sub SetStationSkipHistory(JSON As String)
        JsonConvert.PopulateObject(JSON, stationSkipHistory)
    End Sub
    Public Sub SetGlobalSkipHistory(JSON As String)
        globalSkipHistory = JsonConvert.DeserializeObject(Of Queue(Of DateTime))(JSON)
    End Sub
    Public Property AllowedStationsSkipsPerHour() As System.Nullable(Of Integer)
        Get
            Return _allowedStationsSkipsPerHour
        End Get
        Friend Set(value As System.Nullable(Of Integer))
            _allowedStationsSkipsPerHour = Value
        End Set
    End Property
    Protected _allowedStationsSkipsPerHour As System.Nullable(Of Integer)

	Public Property AllowedSkipsPerDay() As System.Nullable(Of Integer)
		Get
			Return _allowedSkipsPerDay
		End Get
		Friend Set
			_allowedSkipsPerDay = value
		End Set
	End Property
    Protected _allowedSkipsPerDay As System.Nullable(Of Integer)

    Friend Sub New(Session As PandoraSession)
        Me.New()
        Me.AllowedStationsSkipsPerHour = Session.StationSkipLimit
        If Session.User.PartnerCredentials.AccountType = AccountType.PANDORA_ONE_USER Then
            Me.AllowedSkipsPerDay = Nothing
        Else
            Me.AllowedSkipsPerDay = 24 ' according to: http://help.pandora.com/customer/portal/articles/24601-skip-limit
        End If
    End Sub

	Friend Sub New()
		stationSkipHistory = New Dictionary(Of String, Queue(Of DateTime))()
        globalSkipHistory = New Queue(Of DateTime)()
	End Sub

	Public Sub Skip(station As PandoraStation)
		If Not CanSkip(station) Then
			Throw New PandoraException("User is not currently allowed to skip tracks.")
		End If

		' log the current time as a skip event
		stationSkipHistory(station.Id).Enqueue(DateTime.Now)
        globalSkipHistory.Enqueue(DateTime.Now)
	End Sub

	Public Function CanSkip(station As PandoraStation) As Boolean
		' remove any skip history records older than an hour
		For Each currHistory As Queue(Of DateTime) In stationSkipHistory.Values
			While currHistory.Count > 0 AndAlso DateTime.Now - currHistory.Peek() > New TimeSpan(1, 0, 0)
				currHistory.Dequeue()
			End While
		Next

		' remove any daily skip history older than a day
		While globalSkipHistory.Count > 0 AndAlso DateTime.Now - globalSkipHistory.Peek() > New TimeSpan(24, 0, 0)
			globalSkipHistory.Dequeue()
		End While

		' if the current station has no skip history, add it
		If Not stationSkipHistory.ContainsKey(station.Id) Then
			stationSkipHistory.Add(station.Id, New Queue(Of DateTime)())
		End If

		' if we are allowed to skip, record the current time and finish
		If IsGlobalSkipAllowed() AndAlso IsStationSkipAllowed(station) Then
			Return True
		End If

		' too bad, no skip for you!
		Return False
	End Function

	Private Function IsStationSkipAllowed(station As PandoraStation) As Boolean
		If AllowedStationsSkipsPerHour Is Nothing Then
			Return True
		End If

		If stationSkipHistory(station.Id).Count < AllowedStationsSkipsPerHour Then
			Return True
		End If

		Return False
	End Function

	Private Function IsGlobalSkipAllowed() As Boolean
		If AllowedSkipsPerDay Is Nothing Then
			Return True
		End If

		If globalSkipHistory.Count < AllowedSkipsPerDay Then
			Return True
		End If

		Return False
	End Function
End Class
