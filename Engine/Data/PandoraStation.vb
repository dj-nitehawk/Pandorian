Imports System.Collections.Generic
Imports System.Text
Imports Newtonsoft.Json

Namespace Data

    <Serializable()>
    Public Class PandoraStation
        Inherits PandoraData
        <JsonProperty(PropertyName:="stationName")>
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Friend Set(value As String)
                m_Name = value
            End Set
        End Property
        Private m_Name As String

        <JsonProperty(PropertyName:="stationId")>
        Public Property Id() As String
            Get
                Return m_Id
            End Get
            Friend Set(value As String)
                m_Id = value
            End Set
        End Property
        Private m_Id As String

        <JsonProperty(PropertyName:="stationToken")>
        Public Property Token() As String
            Get
                Return m_Token
            End Get
            Friend Set(value As String)
                m_Token = value
            End Set
        End Property
        Private m_Token As String

        <JsonProperty(PropertyName:="isQuickMix")>
        Public Property IsQuickMix() As Boolean
            Get
                Return m_IsQuickMix
            End Get
            Friend Set(value As Boolean)
                m_IsQuickMix = value
            End Set
        End Property
        Private m_IsQuickMix As Boolean

        <JsonProperty(PropertyName:="stationDetailUrl")>
        Public Property StationURL As String
            Get
                Return _stationURL
            End Get
            Set(value As String)
                _stationURL = value
            End Set
        End Property
        Private _stationURL As String

        <JsonProperty(PropertyName:="quickMixStationIds")>
        Private _qMixStations As List(Of String)
        Public Property QuickMixStations() As List(Of String)
            Get
                Return _qMixStations
            End Get
            Set(ByVal value As List(Of String))
                _qMixStations = value
            End Set
        End Property

        Public Property PandoraIO() As PandoraIO
            Get
                Return _PandoraIO
            End Get
            Set(ByVal value As PandoraIO)
                _PandoraIO = value
            End Set
        End Property
        Private _PandoraIO As PandoraIO

        Public Property CurrentSong As PandoraSong

        Public Property PlayList As New LimitedSizePlaylist(12)

        Public Sub FetchSongs()
            Dim newSongs As New List(Of PandoraSong)()
            newSongs = PandoraIO.GetSongs(Me.m_Token)

            For Each s As PandoraSong In newSongs.ToArray
                If String.IsNullOrEmpty(s.Token) Then
                    newSongs.Remove(s)
                End If
            Next

            If newSongs.Count = 0 Then
                Throw New PandoraException(ErrorCodeEnum.PLAYLIST_EMPTY_FOR_STATION, "Pandora didn't return any songs for this station.")
            End If

            For Each s As PandoraSong In newSongs
                CheckForStationTags(s)
                If Not IsNothing(PlayList.LastAddedSong) Then
                    PlayList.LastAddedSong.NextSong = s
                End If
                s.PreviousSong = PlayList.LastAddedSong
                s.FetchedAt = Now
                PlayList.Add(s)
            Next
            CurrentSong = PlayList.ToArray(PlayList.Count - newSongs.Count)
        End Sub

        Private Sub CheckForStationTags(song As PandoraSong)
            Dim specialStationTags As String() = New String() {"(Holiday)", "(Children's)"}
            For Each currTag As String In specialStationTags
                If song.Artist.EndsWith(currTag) Then
                    song.Artist = song.Artist.Remove(song.Artist.LastIndexOf(currTag)).Trim()
                    Return
                End If
            Next
        End Sub

    End Class

    <Serializable()>
    Public Class LimitedSizePlaylist
        Inherits Queue(Of PandoraSong)

        Public Property LastAddedSong As PandoraSong = Nothing

        Dim Limit As Integer

        Public Sub New(limit As Integer)
            MyBase.New(limit)
            Me.Limit = limit
        End Sub

        Public Shadows Sub Add(song As PandoraSong)

            While Count >= Limit
                Dim s As PandoraSong = Dequeue()
                s.NextSong.PreviousSong = Nothing
                IO.File.Delete(s.AudioFileName)
                IO.File.Delete(s.CoverFileName)
            End While

            MyBase.Enqueue(song)
            LastAddedSong = song
        End Sub

        Public Sub RemoveExpiredSongs()
            Dim songs = MyBase.ToArray
            MyBase.Clear()
            LastAddedSong = Nothing
            For Each s As PandoraSong In songs
                If s.IsStillValid Then
                    s.PreviousSong = LastAddedSong
                    If Not IsNothing(s.PreviousSong) Then
                        s.PreviousSong.NextSong = s
                    End If
                    MyBase.Enqueue(s)
                    LastAddedSong = s
                End If
            Next
            songs = Nothing

        End Sub
    End Class
End Namespace
