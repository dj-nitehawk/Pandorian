Imports System.Collections.Generic
Imports System.Text
Imports Pandorian.Engine.Responses

Public Enum ErrorCodeEnum
	UNKNOWN = -1
	APPLICATION_ERROR = -2
	INTERNAL = 0
	MAINTENANCE_MODE = 1
	URL_PARAM_MISSING_METHOD = 2
	URL_PARAM_MISSING_AUTH_TOKEN = 3
	URL_PARAM_MISSING_PARTNER_ID = 4
	URL_PARAM_MISSING_USER_ID = 5
	SECURE_PROTOCOL_REQUIRED = 6
	CERTIFICATE_REQUIRED = 7
	PARAMETER_TYPE_MISMATCH = 8
	PARAMETER_MISSING = 9
	PARAMETER_VALUE_INVALID = 10
	API_VERSION_NOT_SUPPORTED = 11
	LICENSE_RESTRICTION = 12
    INSUFFICIENT_CONNECTIVITY = 13
    PLAYLIST_EMPTY_FOR_STATION = 14 ' custom error for when no songs returned by api
	READONLY_MODE = 1000
	AUTH_INVALID_TOKEN = 1001
	AUTH_INVALID_USERNAME_PASSWORD = 1002
    LISTENER_NOT_AUTHORIZED = 1003 'generally means a pandora one subscription expired
	USER_NOT_AUTHORIZED = 1004
	PARTNER_NOT_AUTHORIZED = 1010
	DEVICE_MODEL_INVALID = 1023
	DEVICE_DISABLED = 1034
	PLAYLIST_EXCEEDED = 1039
End Enum

Public Class PandoraException
	Inherits Exception
	''' <summary>
	''' If identifiable, the raw error code from the pandora.com servers.
	''' </summary>
	Public ReadOnly Property ErrorCode() As ErrorCodeEnum
		Get
			Return _errorCode
		End Get
	End Property
	Protected _errorCode As ErrorCodeEnum

	''' <summary>
	''' Human readable details about the error this object represents.
	''' </summary>
	Public Overrides ReadOnly Property Message() As String
		Get
			Return _message
		End Get
	End Property
	Protected _message As String

	''' <summary>
	''' If an XML parsing error occured, contains the raw XML text.
	''' </summary>
	Public ReadOnly Property XmlString() As String
		Get
			Return _xmlString
		End Get
	End Property
	Protected _xmlString As String = Nothing

	''' <summary>
	''' Creates an exception from the given server response object.
	''' </summary>
	''' <param name="response"></param>
	Friend Sub New(response As PandoraResponse)
		Try
			If response Is Nothing Then
				Throw New PandoraException("Attempted to parse null response.")
			End If
			If response.Success Then
				Throw New PandoraException("Attempted to parse error from successful response.")
			End If

			_message = response.ErrorMessage
            _errorCode = CType(response.ErrorCode, ErrorCodeEnum)
		Catch e As Exception
			Throw New PandoraException("Failed parsing error response.", e)
		End Try
	End Sub

	''' <summary>
	''' Create an exception from an error code and message provided by the Pandora servers.
	''' If the error code is recognized, the ErrorCode field will be populated.
	''' </summary>
	''' <param name="errorCodeStr"></param>
	''' <param name="message"></param>
	Public Sub New(errorCode As Integer, message As String)
		Try
			_message = message
			_errorCode = CType(errorCode, ErrorCodeEnum)
		Catch generatedExceptionName As Exception
			_errorCode = ErrorCodeEnum.UNKNOWN
			_message = message
		End Try
	End Sub

	''' <summary>
	''' Creates an exception instance based on a supplied inner exception and a 
	''' message describing the situation. Should only be used for unexpected errors.
	''' </summary>
	''' <param name="message"></param>
	''' <param name="innerException"></param>
	Public Sub New(message As String, innerException As Exception)
		MyBase.New(message, innerException)

		_message = message
		_errorCode = ErrorCodeEnum.APPLICATION_ERROR
	End Sub

	''' <summary>
	''' Creates an exception instance based on a supplied inner exception and a 
	''' message describing the situation. Should only be used for unexpected errors.
	''' </summary>
	''' <param name="message"></param>
	''' <param name="innerException"></param>
	Public Sub New(message As String, innerException As Exception, xmlStr As String)
		MyBase.New(message, innerException)

		_message = message
		_errorCode = ErrorCodeEnum.APPLICATION_ERROR
		_xmlString = xmlStr
	End Sub

	''' <summary>
	''' Creates an exception instance with the given message describing the situation.
	''' Should only be used for unexpected errors.
	''' </summary>
	''' <param name="message"></param>
	Public Sub New(message As String)
		MyBase.New(message)

		_message = message
		_errorCode = ErrorCodeEnum.APPLICATION_ERROR
	End Sub

	''' <summary>
	''' Creates an exception instance based on a supplied message describing the 
	''' situation. Should only be used for unexpected errors.
	''' </summary>
	''' <param name="message"></param>
	''' <param name="innerException"></param>
	Public Sub New(innerException As Exception)
		Me.New("Unexpected library error.", innerException)

		_message = "Unexpected library error. So Sorry!"
	End Sub

End Class
