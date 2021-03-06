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
			
			
			
    		Dim Solicitud As XDocument = _
                        <?xml version="1.0" encoding="UTF-8"?>
                        <PoolServicios>
                            <Servicio ID="15" Descripcion=<%= Servicio %> Observacion=<%= txtObservaciones.Text %> Folio="" Descuento=<%= txtDescuento.Value %> Asesor=<%= Asesor %> TipoDescuento=<%= cmbTipoDescuento.SelectedItem.DisplayText %>>
                                <Cliente ID=<%= TxtCliente1.Value %> Nombre=<%= lblNombreCliente.Text %> ReferenciaBancaria=<%= lblReferenciaBancaria.Text %> ClienteVendedorID=<%= lblClienteVendedorID.Text %>/>
                                <Facturas>
                                    <%= From f In listFacturas Where f.Incluir = True And f.Saldo > 0 Select <Factura NoDocumento=<%= f.NoDocumento %> Serie=<%= f.Serie %> Importe=<%= f.Importe %> Saldo=<%= f.Saldo %> MetodoPagoID=<%= f.MetodoPagoID %> FechaDocumento=<%= f.FechaDocumento %> FechaVencimiento=<%= f.FechaVencimiento %> Descuento=<%= f.Descuento %>/> %>
                                </Facturas>
                            </Servicio>
                        </PoolServicios>

				
				
