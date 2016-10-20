Imports System.Collections.Generic
Imports System.Text
Imports Newtonsoft.Json

Namespace Data

    <Serializable()>
    Public Class PandoraStation
        Inherits PandoraData
        <JsonProperty(PropertyName:="stationName")> _
        Public Property Name() As String
            Get
                Return m_Name
            End Get
            Friend Set(value As String)
                m_Name = value
            End Set
        End Property
        Private m_Name As String

        <JsonProperty(PropertyName:="stationId")> _
        Public Property Id() As String
            Get
                Return m_Id
            End Get
            Friend Set(value As String)
                m_Id = value
            End Set
        End Property
        Private m_Id As String

        <JsonProperty(PropertyName:="stationToken")> _
        Public Property Token() As String
            Get
                Return m_Token
            End Get
            Friend Set(value As String)
                m_Token = value
            End Set
        End Property
        Private m_Token As String

        <JsonProperty(PropertyName:="isQuickMix")> _
        Public Property IsQuickMix() As Boolean
            Get
                Return m_IsQuickMix
            End Get
            Friend Set(value As Boolean)
                m_IsQuickMix = value
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

        Public Property CurrentSong As PandoraSong

        Public Property PlayList As New Queue(Of PandoraSong)

        Public Property PandoraIO() As PandoraIO
            Get
                Return _PandoraIO
            End Get
            Set(ByVal value As PandoraIO)
                _PandoraIO = value
            End Set
        End Property
        Private _PandoraIO As PandoraIO

        Public ReadOnly Property SongLoadingOccurred() As Boolean
            Get
                Dim r As Boolean = _SongLoadingOccurred
                _SongLoadingOccurred = False
                Return r
            End Get
        End Property
        Private _SongLoadingOccurred As Boolean

        Public Function GetNextSong() As PandoraSong

            ' load 4 more songs if playlist empty
            If PlayList.Count = 0 Then
                LoadSongs()
            End If

            'check if loading songs worked
            If Not PlayList.Count = 0 Then
                CurrentSong = PlayList.Dequeue()
            Else
                Throw New PandoraException(ErrorCodeEnum.PLAYLIST_EMPTY_FOR_STATION, "API didn't return any songs for this station.")
            End If

            Return CurrentSong
        End Function

        Public Sub LoadSongs()
            Dim newSongs As New List(Of PandoraSong)()
            newSongs = PandoraIO.GetSongs(Me)
            PlayList.Clear()

            For Each s As PandoraSong In newSongs
                If s.Token Is Nothing Then
                    Continue For
                End If
                CheckForStationTags(s)
                PlayList.Enqueue(s)
            Next

            _SongLoadingOccurred = True
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
End Namespace
