declare @HR_XML as table (ID INT IDENTITY, Salaries XML);
INSERT into @HR_XML VALUES(
 '  <Correos>
		<Correo>
			<FacturaElectronica>0</FacturaElectronica>
			<Direccion>OMEDINA@APYMSA.COM.MX</Direccion>
		</Correo>
	</Correos>
'
)
UPDATE @HR_XML
--SET Salaries.modify('insert attribute  TipoCorreo{"Principal"} into (/Correos/Correo)[1]')
SET Salaries.modify('insert <TipoCorreo /> into (/Correos/Correo)[1]')
select * from @HR_XML
