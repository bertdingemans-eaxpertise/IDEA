''' <summary>
''' Class for a list of datatypes for the conversion from one layer in the other in
''' the data model (LDM - TDM)
''' </summary>
Public Class IDEADataTypes
	''' <summary>
	''' Class for doing transformations of the datatypes for the IDEA routines to go
	''' from one datamodeling layer to the other.
	''' </summary>
    Public Const VBNet As String = "String,Date,Long,Integer,Double,Boolean,Integer,Datetime,Decimal"
    Public Const SQLServer As String = "nvarchar,date,number,number,number,bit,int,datetime,float"
End Class
