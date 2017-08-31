DBCC FREEPROCCACHE

SET IDENTITY_INSERT FYC.Cat_Finanzas_Proveedores   ON

alter table SC.TableCAtdrop constraint FK_Cat_Finanzas_Contactos_SC.TableCAt
alter table FYC.SC.TableCAtDirectosdrop constraint FK_SC.TableCAtDirectos_SC.TableCAt

alter table SC.TableCAtadd constraint FK_Cat_Finanzas_Contactos_SC.TableCAtforeign key( ProveedorID) references FYC.SC.TableCAt(ProveedorID) on DELETE CASCADE
alter table FYC.SC.TableCAtDirectosadd constraint FK_SC.TableCAtDirectos_SC.TableCAtforeign key( ProveedorID) references FYC.SC.TableCAt(ProveedorID) on DELETE no action

--Rename column
sp_rename 'dbo.old_table_name.DD', 'IDDocumento', 'COLUMN';

--rename table 
sp_rename 'old_table_name', 'new_table_name';

--change shcema name
ALTER SCHEMA TargetSchema 
    TRANSFER SourceSchema.TableName;
