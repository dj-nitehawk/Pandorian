Imports System.Collections.Generic
Imports System.Text
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

    Public Property SkipLimitReached() As Boolean
        Get
            Return _SkipLimitReached
        End Get
        Set(ByVal value As Boolean)
            _SkipLimitReached = value
        End Set
    End Property
    Private _SkipLimitReached As Boolean

    Public Function Login(username As String, password As String) As Boolean

        Session = pandoraIO.PartnerLogin()
        Session.User = pandoraIO.UserLogin(username, password)

        If Session.User IsNot Nothing Then
            If Session.User.CanListen Then

                AvailableStations = pandoraIO.GetStations()
                Return True
            Else
                Throw New PandoraException(ErrorCodeEnum.LISTENER_NOT_AUTHORIZED, "Your are not a Pandora One subscriber. Please change the settings.")
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
