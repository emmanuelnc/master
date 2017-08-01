/*ADD CONSTRAINT*/
alter table SCHEME.TABLENAME
add constraint FK_TABLE1_TABLE2
foreign key(TABLE1FKEY) References TABLE2 (PK)


/*Obtener varios ids insertados a la vez*/
DECLARE @MyTableVar table( NewScrapReasonID smallint,
                           Name varchar(50),
                           ModifiedDate datetime);
INSERT FYC.tmpPrueba
    OUTPUT INSERTED.ID, INSERTED.Nombre, GETDATE()
        INTO @MyTableVar
VALUES (N'Emmanuel 1', 'otro 1') , (N'Emmanuel2', 'otro2'), (N'Emmanuel3', 'otro33');

--Display the result set of the table variable.
SELECT NewScrapReasonID, Name, ModifiedDate FROM @MyTableVar;
--Display the result set of the table.
select *
from FYC.tmpPrueba
GO
