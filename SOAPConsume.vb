Imports System.IO
    Imports System.Net
    Imports System.Xml


Namespace Core.VYM

    Public Class clsConsultaSATws

        Const SERVICE_URL As String = "https://consultaqr.facturaelectronica.sat.gob.mx/ConsultaCFDIService.svc?wsdl"

        Private Shared Function CreateWebRequest() As HttpWebRequest
            Try

                Dim res As HttpWebRequest = Nothing
                res = WebRequest.Create(SERVICE_URL)
                res.Headers.Add("SOAPAction", "http://tempuri.org/IConsultaCFDIService/Consulta")
                res.ContentType = "text/xml;charset=""utf-8"""
                res.Accept = "text/xml"
                res.Method = "POST"

                Return res

            Catch p As FaultException(Of Mensaje)
                Throw p
            Catch ex As Exception
                Throw Mensaje.GenerarMensaje(ErrorHandled.TryDecodeDbUpdateException(ex), Nothing, Mensaje.TipoError.Error, Mensaje.LayerError.BLL)
            End Try
        End Function
        Private Shared Function GetSoapXML(Qr As String) As XmlDocument
            Try
                Dim res As XmlDocument = New XmlDocument

                Dim tem As XNamespace = "http://tempuri.org/"
                Dim soapenv As XNamespace = "http://schemas.xmlsoap.org/soap/envelope/"



                Dim soapEnvDoc As XDocument = <?xml version="1.0" encoding="UTF-8"?>
                                              <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
                                                  <soapenv:Header/>
                                                  <soapenv:Body>
                                                      <tem:Consulta>
                                                          <!--Optional:-->
                                                          <tem:expresionImpresa></tem:expresionImpresa>
                                                      </tem:Consulta>
                                                  </soapenv:Body>
                                              </soapenv:Envelope>
                Dim nodeExpImpresaCDATA = soapEnvDoc.Descendants(soapenv + "Envelope").Descendants(soapenv + "Body").Descendants(tem + "Consulta").Descendants(tem + "expresionImpresa").FirstOrDefault

                nodeExpImpresaCDATA.ReplaceNodes(New XCData(Qr))

                '<tem:expresionImpresa><![CDATA[?re=LSO1306189R5&rr=GACJ940911ASA&tt=4999.99&id=e7df3047-f8de-425d-b469-37abe5b4dabb]]></tem:expresionImpresa>
                Using XmlReader As XmlReader = soapEnvDoc.CreateReader
                    res.Load(XmlReader)
                End Using

                Return res
            Catch p As FaultException(Of Mensaje)
                Throw p
            Catch ex As Exception
                Throw Mensaje.GenerarMensaje(ErrorHandled.TryDecodeDbUpdateException(ex), Nothing, Mensaje.TipoError.Error, Mensaje.LayerError.BLL)
            End Try
        End Function
        Public Shared Function ConsultarEstatusCFDI(pFolio As Integer, pSerie As String) As SAT_ConsultaResult
            Try

                Dim result As SAT_ConsultaResult = Nothing

                Dim request As HttpWebRequest
                Dim soapEnvelopeXML As XmlDocument = Nothing
                Dim asyncResult As IAsyncResult
                Dim qrString As String
                Dim soapResult As String = Nothing


                request = CreateWebRequest()


                qrString = Core.VYM.Facturas.getFacturaQR(pFolio, pSerie)

                soapEnvelopeXML = GetSoapXML(qrString)
                Using stream As Stream = request.GetRequestStream()
                    soapEnvelopeXML.Save(stream)
                End Using

                asyncResult = request.BeginGetResponse(Nothing, Nothing)
                asyncResult.AsyncWaitHandle.WaitOne()

                Using webResponse As WebResponse = request.EndGetResponse(asyncResult)
                    Using rd As StreamReader = New StreamReader(webResponse.GetResponseStream())
                        soapResult = rd.ReadToEnd()
                    End Using
                End Using

                '   File.WriteAllText("resultadoSOAPSAT.xml", soapResult)
                If Not String.IsNullOrEmpty(soapResult) Then
                    result = ProcesaResultado(soapResult)
                End If


                Return result

            Catch p As FaultException(Of Mensaje)
                Throw p
            Catch ex As Exception
                Throw Mensaje.GenerarMensaje(ErrorHandled.TryDecodeDbUpdateException(ex), Nothing, Mensaje.TipoError.Error, Mensaje.LayerError.BLL)
            End Try
        End Function



        Private Shared Function ProcesaResultado(SoapResult As String) As SAT_ConsultaResult
            Try

                Dim res As SAT_ConsultaResult = Nothing
                Dim xdoc As XDocument


                'Namespaces
                Dim s As XNamespace = "http://schemas.xmlsoap.org/soap/envelope/"
                Dim a As XNamespace = "http://schemas.datacontract.org/2004/07/Sat.Cfdi.Negocio.ConsultaCfdi.Servicio"
                Dim cr As XNamespace = "http://tempuri.org/"


                xdoc = XDocument.Parse(SoapResult)



                Dim f = xdoc.Descendants(s + "Envelope").Descendants(s + "Body").Descendants(cr + "ConsultaResponse").Descendants(cr + "ConsultaResult").Select(Function(x) x)


                If Not IsNothing(f) Then
                    res = f.Select(Function(n) New SAT_ConsultaResult With {
                              .CodigoEstatus = n.Elements(a + "CodigoEstatus").Value,
                              .EsCancelable = n.Elements(a + "EsCancelable").Value,
                              .Estado = n.Elements(a + "Estado").Value,
                              .EstatusCancelacion = n.Elements(a + "EstatusCancelacion").Value
                             }).FirstOrDefault

                End If

                Return res

            Catch p As FaultException(Of Mensaje)
                Throw p
            Catch ex As Exception
                Throw Mensaje.GenerarMensaje(ErrorHandled.TryDecodeDbUpdateException(ex), Nothing, Mensaje.TipoError.Error, Mensaje.LayerError.BLL)
            End Try
        End Function


    End Class
End Namespace
