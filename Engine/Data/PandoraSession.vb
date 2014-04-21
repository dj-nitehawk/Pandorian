Imports System.Collections.Generic
Imports System.Text
Imports Newtonsoft.Json

Namespace Data

	<JsonObject(MemberSerialization.OptIn)> _
	Public Class PandoraSession
		Inherits PandoraData
		Public Property User() As PandoraUser
			Get
				Return m_User
			End Get
			Friend Set
				m_User = Value
			End Set
		End Property
		Private m_User As PandoraUser

        Private Function StripNonNumeric(Data As String) As String
            Dim RX As New RegularExpressions.Regex("\D")
            Return RX.Replace(Data, String.Empty)
        End Function

        Public ReadOnly Property ServerSyncTime() As Long
            Get
                If _partnerSyncTime Is Nothing Then
                    Dim decryptedTime As String = StripNonNumeric(PandoraIO.crypter.DeCrypt(EncryptedSyncTime))
                    Dim serverTime As Long
                    If Long.TryParse(decryptedTime, serverTime) Then
                        _partnerSyncTime = serverTime
                    End If
                End If

                If _partnerSyncTime Is Nothing Then
                    Return 0
                Else
                    Return CLng(_partnerSyncTime)
                End If
            End Get
        End Property
        Private _partnerSyncTime As System.Nullable(Of Long) = Nothing

        <JsonProperty(PropertyName:="partnerId")> _
        Public Property PartnerId() As Integer
            Get
                Return m_PartnerId
            End Get
            Friend Set(value As Integer)
                m_PartnerId = Value
            End Set
        End Property
        Private m_PartnerId As Integer

        <JsonProperty(PropertyName:="partnerAuthToken")> _
        Public Property PartnerAuthToken() As String
            Get
                Return m_PartnerAuthToken
            End Get
            Friend Set(value As String)
                m_PartnerAuthToken = value
            End Set
        End Property
        Private m_PartnerAuthToken As String

        <JsonProperty(PropertyName:="syncTime")> _
        Public Property EncryptedSyncTime() As String
            Get
                Return m_EncryptedSyncTime
            End Get
            Friend Set(value As String)
                m_EncryptedSyncTime = Value
            End Set
        End Property
        Private m_EncryptedSyncTime As String

        Public Function GetSyncTime() As Long
            Dim timeNow As Long = TicksToUnixTimeStamp(DateTime.UtcNow.Ticks)
            Return ServerSyncTime + (timeNow - ClientStartTime) ' according to http://pan-do-ra-api.wikia.com/wiki/Json/5
        End Function

        Private Function TicksToUnixTimeStamp(Ticks As Long) As Long
            Return (Ticks - 621355968000000000) / 10000000
        End Function

        <JsonProperty(PropertyName:="stationSkipUnit")> _
        Public Property StationSkipUnit() As String
            Get
                Return _skipUnit
            End Get
            Set(value As String)
                _skipUnit = value
            End Set
        End Property
        Private _skipUnit As String

        <JsonProperty(PropertyName:="stationSkipLimit")> _
        Public Property StationSkipLimit() As Integer
            Get
                Return _stationSkipLimit
            End Get
            Set(value As Integer)
                _stationSkipLimit = value
            End Set
        End Property
        Private _stationSkipLimit As Integer

        Private ClientStartTime As Long
        Public Sub New()
            ClientStartTime = TicksToUnixTimeStamp(DateTime.UtcNow.Ticks)
        End Sub

        Public Sub DebugCorruptAuthToken()
            PartnerAuthToken = "dsfuldshfdugiulghwuihwefuiewgwruig"
        End Sub
    End Class
End Namespace
