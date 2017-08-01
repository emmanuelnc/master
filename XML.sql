/*SELECT FROM XML*/
declare @Facturas as xml = '<Factura><Numero>194938</Numero><Serie>PEOF</Serie><MontoAplicar>468.756</MontoAplicar> </Factura>' + 
							'<Factura><Numero>218646</Numero><Serie>PEOF</Serie><MontoAplicar>524.8784</MontoAplicar></Factura>' 

Select 
a.b.value('Numero[1]', 'int'),
a.b.value('Serie[1]', 'varchar(4)'),
a.b.value('MontoAplicar[1]', 'Decimal(9,2)')
from @Facturas.nodes('//Factura') a(b)


/*MORE */


declare @Facturas as xml = '<Factura><Numero>194938</Numero><Serie>PEOF</Serie><MontoAplicar>468.756</MontoAplicar> </Factura>' + 
							'<Factura><Numero>218646</Numero><Serie>PEOF</Serie><MontoAplicar>524.8784</MontoAplicar></Factura>' 

Select 
a.b.value('Numero[1]', 'int'),
a.b.value('Serie[1]', 'varchar(4)'),
a.b.value('MontoAplicar[1]', 'Decimal(9,2)')
from @Facturas.nodes('//Factura') a(b)


declare @tblBom as table (BomID  int,NoReferencia varchar(30), ReferenciaID  smallint, Padre bit)

select C.D.value('(/PoolServicios/Servicio/Pedidos/Pedido/@ID)[1]', 'varchar(10)') PedidoID
	from
	(
		select Solicitud
		from FYC.Tra_CXC_PoolServicios_Servicios
		where FechaSolicitud > '20/01/2017' and ServicioID = 24
		)C (D)





declare @comps as xml = '<Relacion>
  <Padre NoReferencia="6R3Z-10346-DA" ReferenciaID="83">
    <Componente NoReferencia="F601HD" ReferenciaID="83" />
    <Componente NoReferencia="F601HD" ReferenciaID="142" />
    <Componente NoReferencia="FR-6023" ReferenciaID="142" />
    <Componente NoReferencia="FR6023" ReferenciaID="244" />
  </Padre>
  <Padre NoReferencia="FD-504" ReferenciaID="244">
    <Componente NoReferencia="S-819" ReferenciaID="195" />
  </Padre>
</Relacion>'

SELECT  C.D.value('../@NoReferencia', 'varchar(50)') pRef
	, C.D.value('@NoReferencia', 'varchar(50)')cRef
	, C.D.value('@ReferenciaID', 'int')cRefID
FROM (SELECT @comps a) B 
CROSS APPLY B.a.nodes('Relacion/Padre/Componente') C (D)
