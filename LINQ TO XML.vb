   Dim Facturas = (From Factura In Solicitud.Descendants("Factura")
                                    Select New dtolistDesFinCarVar With
                                    {.NoDocumento = Factura.@NoDocumento,
                                     .Serie = Factura.@Serie,
                                     .Importe = Factura.@Importe,
                                     .Saldo = Factura.@Saldo,
                                     .FechaDocumento = Factura.@FechaDocumento,
                                     .Descuento = Factura.@Descuento,
                                     .FechaVencimiento = Factura.@FechaVencimiento,
                                     .MetodoPagoID = If(Factura.@MetodoPagoID, 7)}).ToList

									 
									 
									 
                 
Dim Solicitud As XDocument = XDocument.Parse(Res.d.Solicitud)


   'Obtenemos datos del pedido
                    tmpResultado.listDetallePedido = (From Dato In Solicitud.Descendants("Pedidos")
                                  Select New dtoPedidosRetenidos.dtoPedidoRetenidoDetalle With
                                  {.PedidoID = Dato.<Pedido>.@ID,
                                   .Origen = Dato.<Pedido>.@Origen,
                                   .Total = Dato.<Pedido>.@Total,
                                   .Fecha = Dato.<Pedido>.@Fecha,
                                   .Observaciones = Dato.<Pedido>.@Observaciones,
                                   .Motivo = Dato.<Pedido>.@Motivo}).ToList
                                                                                                                                                                                             


    Dim doc As New XmlDocument
                            doc.LoadXml(Res.Solicitud)

                            Dim root As XmlElement = doc.DocumentElement.FirstChild.FirstChild
                            root.SetAttribute("Token", pToken)

                            'Actualizamos los datos de la solicitud
                            Solicitud.SetRespuestaSolicitud(pFolio, True, doc.InnerXml.ToString)
