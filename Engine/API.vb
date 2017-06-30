Imports System.Collections.Generic
Imports Pandorian.Engine.Data
Imports System.Net

<Serializable()>
Public Class API

    Private pandoraIO As PandoraIO

    Public Sub New(IsPandoraOne As Boolean)
        pandoraIO = New PandoraIO(IsPandoraOne)
    End Sub

    Public Sub ClearSession(IsPandoraOne As Boolean)
        pandoraIO = New PandoraIO(IsPandoraOne)
        Me.Session = Nothing
    End Sub

    Public Property Session() As PandoraSession
        Get
            Return m_Session
        End Get
        Set(value As PandoraSession)
            m_Session = value
            pandoraIO.Session = value
        End Set
    End Property
    Private m_Session As PandoraSession

    Public Property Proxy() As WebProxy
        Get
            Return _proxy
        End Get
        Set(value As WebProxy)
            _proxy = value
            pandoraIO.Proxy = value
        End Set
    End Property
    Protected _proxy As WebProxy = Nothing

    Public Property CurrentStation() As PandoraStation
        Get
            Return _currentStation
        End Get
        Set(value As PandoraStation)
            _currentStation = value
        End Set
    End Property
    Private _currentStation As PandoraStation

    Public Property AvailableStations() As List(Of PandoraStation)
        Get
            Return m_AvailableStations
        End Get
        Protected Set(value As List(Of PandoraStation))
            m_AvailableStations = value
        End Set
    End Property
    Private m_AvailableStations As List(Of PandoraStation)

    Public Property SkipLimitReachedAt As Date

    Public Property SkipLimitReached() As Boolean

    Public Function OkToFetchSongs() As Boolean
        If SkipLimitReached Then
            If Now.Subtract(SkipLimitReachedAt).TotalMinutes > 10 Then
                Return True
            End If
            Return False
        End If
        Return True
    End Function

    Public Function Login(username As String, password As String, DontIndicateQuickMixMembers As Boolean) As Boolean

        Session = pandoraIO.PartnerLogin()
        Session.User = pandoraIO.UserLogin(username, password)

        If Session.User IsNot Nothing Then
            If Session.User.CanListen Then

                Dim OldStations As PandoraStation()
                If Not IsNothing(AvailableStations) Then
                    OldStations = AvailableStations.ToArray()
                Else
                    OldStations = New PandoraStation() {}
                End If

                AvailableStations = pandoraIO.GetStations()

                For Each stn As PandoraStation In OldStations
                    Dim s = AvailableStations.Find(Function(x) x.Id = stn.Id)
                    If Not IsNothing(s) Then
                        s.CurrentSong = stn.CurrentSong
                        s.PlayList = stn.PlayList
                    End If
                Next

                Dim qMixStation As New PandoraStation
                For Each s As PandoraStation In AvailableStations
                    If s.IsQuickMix Then
                        qMixStation = s
                        Exit For
                    End If
                Next

                If Not DontIndicateQuickMixMembers Then
                    For Each stn As PandoraStation In AvailableStations
                        If qMixStation.QuickMixStations.Contains(stn.Id) Then
                            stn.Name = stn.Name + " ✪"
                        End If
                    Next
                End If

                Return True
            Else
                Throw New PandoraException(ErrorCodeEnum.LISTENER_NOT_AUTHORIZED, "Your are not a Pandora Plus subscriber. Please change the settings.")
            End If
        End If

        Return False
    End Function

    Public Sub RateSong(song As PandoraSong, rating As PandoraRating)
        pandoraIO.RateSong(CurrentStation, song, rating)
    End Sub

    Public Sub TemporarilyBanSong(song As PandoraSong)
        pandoraIO.AddTiredSong(song)
    End Sub

End Class
