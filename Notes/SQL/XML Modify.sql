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

-- EDIT -- EDIT -- EDIT


declare @correo as xml = 
'<Correos><Correo><Direccion>cmexi@autopartesymas.mx</Direccion><FacturaElectronica>0</FacturaElectronica></Correo><Correo><Direccion>lrromero@pasa.mx</Direccion><FacturaElectronica>0</FacturaElectronica></Correo></Correos>
'

select 
@correo.value('(Correos/Correo/Direccion/text())[1]','varchar (150)') ProvID
set @correo.modify('delete //Correos//*[contains(.,"@autopartesymas.mx")]')
--set @correo.modify('delete //Correos//*')
select @correo
