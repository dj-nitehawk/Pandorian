Imports System.Collections.Generic
Imports System.Text
Imports Newtonsoft.Json

Namespace Data

    <Serializable()>
    Public Class PandoraSongFeedback
        Inherits PandoraData
        <JsonProperty(PropertyName:="isPositive")> _
        Public Property IsPositive() As Boolean
            Get
                Return m_IsPositive
            End Get
            Set(value As Boolean)
                m_IsPositive = value
            End Set
        End Property
        Private m_IsPositive As Boolean

        <JsonProperty(PropertyName:="artistName")> _
        Public Property Artist() As String
            Get
                Return m_Artist
            End Get
            Friend Set(value As String)
                m_Artist = value
            End Set
        End Property
        Private m_Artist As String

        <JsonProperty(PropertyName:="songName")> _
        Public Property Title() As String
            Get
                Return m_Title
            End Get
            Friend Set(value As String)
                m_Title = value
            End Set
        End Property
        Private m_Title As String

        <JsonProperty(PropertyName:="feedbackId")> _
        Public Property FeedbackId() As String
            Get
                Return m_FeedbackId
            End Get
            Friend Set(value As String)
                m_FeedbackId = value
            End Set
        End Property
        Private m_FeedbackId As String
    End Class
End Namespace
