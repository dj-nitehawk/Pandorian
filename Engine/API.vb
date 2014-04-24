Imports System.Collections.Generic
Imports System.Text
Imports Pandorian.Engine.Data
Imports System.Net

Public Class API

    Protected pandora As PandoraIO
    Protected playlist As New Dictionary(Of String, Queue(Of PandoraSong))

    Private specialStationTags As String() = New String() {"(Holiday)", "(Children's)"}

    Public Sub New(IsPandoraOne As Boolean)
        pandora = New PandoraIO(IsPandoraOne)
    End Sub

    ''' <summary>
    ''' The current user that is logged in.
    ''' </summary>
    Public Property User() As PandoraUser
        Get
            Return m_User
        End Get
        Protected Set(value As PandoraUser)
            m_User = Value
        End Set
    End Property
    Private m_User As PandoraUser

    ''' <summary>
    ''' Information about the current Pandora session.
    ''' </summary>
    Public Property Session() As PandoraSession
        Get
            Return m_Session
        End Get
        Protected Set(value As PandoraSession)
            m_Session = Value
        End Set
    End Property
    Private m_Session As PandoraSession

    ''' <summary>
    ''' If set, this proxy object will be used for all web requests.
    ''' </summary>
    Public Property Proxy() As WebProxy
        Get
            Return _proxy
        End Get
        Set(value As WebProxy)
            _proxy = value
        End Set
    End Property
    Protected _proxy As WebProxy = Nothing

    ''' <summary>
    ''' The current station being listened to.
    ''' </summary>
    Public Property CurrentStation() As PandoraStation
        Get
            Return _currentStation
        End Get
        Set(value As PandoraStation)
            If AvailableStations.Contains(value) Then
                _currentStation = value
                CurrentSong = Nothing
            End If
        End Set
    End Property
    Protected _currentStation As PandoraStation

    ''' <summary>
    ''' The current song being played.
    ''' </summary>
    Public Property CurrentSong() As PandoraSong
        Get
            Return m_CurrentSong
        End Get
        Set(value As PandoraSong)
            m_CurrentSong = Value
        End Set
    End Property
    Private m_CurrentSong As PandoraSong

    ''' <summary>
    ''' A list of all available stations for the currently logged in user.
    ''' </summary>
    Public Property AvailableStations() As List(Of PandoraStation)
        Get
            Return m_AvailableStations
        End Get
        Protected Set(value As List(Of PandoraStation))
            m_AvailableStations = Value
        End Set
    End Property
    Private m_AvailableStations As List(Of PandoraStation)

    ''' <summary>
    ''' Keeps track of when the user has skipped tracks on various stations and if
    ''' they are allowed to skip at the moment.
    ''' </summary>
    Public Property SkipHistory() As SkipHistory
        Get
            Return m_SkipHistory
        End Get
        Set(value As SkipHistory)
            m_SkipHistory = Value
        End Set
    End Property
    Private m_SkipHistory As SkipHistory

    ''' <summary>
    ''' If set to true, special station tags like "Holiday" and "Children's" will
    ''' be removed from the artist name in song meta data.
    ''' </summary>
    Public Property RemoveStationTags() As Boolean
        Get
            Return _removeSpecialStationTag
        End Get
        Set(value As Boolean)
            _removeSpecialStationTag = value
        End Set
    End Property
    Private _removeSpecialStationTag As Boolean = True

    ''' <summary>
    ''' Logs into Pandora with the given credentials.
    ''' </summary>
    ''' <returns>true if the user was successfully logged in.</returns>
    Public Function Login(username As String, password As String) As Boolean
        Clear()
        Session = pandora.PartnerLogin(Proxy)
        User = pandora.UserLogin(Session, username, password, Proxy)
        If User IsNot Nothing Then
            If User.CanListen Then
                SkipHistory = New SkipHistory(Session)
                AvailableStations = pandora.GetStations(Session, Proxy)
                For Each currStation As PandoraStation In AvailableStations
                    If Not currStation.IsQuickMix Then
                        CurrentStation = currStation
                        Exit For
                    End If
                Next
                Return True
            Else
                Throw New PandoraException(1003, "Your are not a Pandora One subscriber. Please change the settings.")
            End If
        End If
        Clear()
        Return False
    End Function

    ''' <summary>
    ''' Clears internal settings reseting the class.
    ''' </summary>
    Public Sub Logout()
        Clear()
    End Sub

    ''' <summary>
    ''' Returns true if the user is allowed to skip the current track on the current station
    ''' </summary>
    ''' <returns></returns>
    Public Function CanSkip() As Boolean
        If CurrentStation IsNot Nothing Then
            Return SkipHistory.CanSkip(CurrentStation)
        End If
        Return True
    End Function

    ''' <summary>
    ''' Returns the next song and updates the CurrentSong property. Will throw a PandoraException if
    ''' skipping and the user is not allowed to skip at this point in time. Call CanSkip() first as needed.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetNextSong(isSkip As Boolean) As PandoraSong

        ' check if there's really a need to log a skip, cause the user may have paused the song 
        ' and song duration may have elapsed
        If isSkip = True Then
            If CurrentSong.DurationElapsed Then
                isSkip = False
            End If
        End If

        ' if necessary log a skip event. this will throw an exception if a skip is not allowed
        If isSkip Then
            SkipHistory.Skip(CurrentStation)
        End If

        ' check if there's a playlist for currentstation, if not make one
        If Not playlist.ContainsKey(CurrentStation.Id) Then
            playlist.Add(CurrentStation.Id, New Queue(Of PandoraSong))
        End If

        ' grab the next song in our queue. songs become invalid after an 
        ' unspecified number of hours.
        Do
            If playlist(CurrentStation.Id).Count < 2 Then
                LoadMoreSongs()
            End If
            CurrentSong = playlist(CurrentStation.Id).Dequeue()
        Loop While Not pandora.IsValid(CurrentSong, Proxy)

        Return CurrentSong
    End Function

    ''' <summary>
    ''' Rate the specified song. A positive or negative rating will influence future songs 
    ''' played from the current station.
    ''' </summary>
    ''' <param name="rating"></param>
    ''' <param name="song"></param>
    Public Sub RateSong(song As PandoraSong, rating As PandoraRating)
        pandora.RateSong(Session, CurrentStation, song, rating, Proxy)
    End Sub

    ''' <summary>
    ''' Ban this song from playing on any of the users stations for one month.
    ''' </summary>
    ''' <param name="song"></param>
    Public Sub TemporarilyBanSong(song As PandoraSong)
        pandora.AddTiredSong(Session, song, Proxy)
    End Sub

    Protected Sub Clear()
        If AvailableStations Is Nothing Then
            AvailableStations = New List(Of PandoraStation)()
        End If
        _currentStation = Nothing
        CurrentSong = Nothing
        AvailableStations.Clear()
        SkipHistory = Nothing
        User = Nothing
        playlist.Clear()
    End Sub

    Protected Sub LoadMoreSongs()
        Dim newSongs As New List(Of PandoraSong)()

        newSongs = pandora.GetSongs(Session, CurrentStation, Proxy)

        ' add our new songs to the appropriate station playlist
        For Each currSong As PandoraSong In newSongs
            If currSong.Token Is Nothing Then
                Continue For
            End If
            CheckForStationTags(currSong)
            playlist(CurrentStation.Id).Enqueue(currSong)
        Next
    End Sub

    Protected Sub CheckForStationTags(song As PandoraSong)
        If Not RemoveStationTags Then
            Return
        End If

        For Each currTag As String In specialStationTags
            If song.Artist.EndsWith(currTag) Then
                song.Artist = song.Artist.Remove(song.Artist.LastIndexOf(currTag)).Trim()
                Return
            End If
        Next
    End Sub

    Public Sub DebugClearPlayList()
        playlist.Clear()
    End Sub

End Class
