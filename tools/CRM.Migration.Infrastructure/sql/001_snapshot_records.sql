-- Apply ONLY to a newly created dedicated CrmMigration_ database.
SET XACT_ABORT ON;
IF DB_NAME() NOT LIKE N'CrmMigration[_]%' OR LEN(DB_NAME()) <= 13
    THROW 51000, 'Dedicated migration database required', 1;
BEGIN TRANSACTION;
IF SCHEMA_ID(N'crm') IS NULL EXEC(N'CREATE SCHEMA crm AUTHORIZATION dbo');
IF OBJECT_ID(N'crm.MigrationSnapshotRecords', N'U') IS NULL
BEGIN
    CREATE TABLE crm.MigrationSnapshotRecords (
        TenantId nvarchar(64) COLLATE Latin1_General_100_BIN2 NOT NULL,
        Store nvarchar(64) COLLATE Latin1_General_100_BIN2 NOT NULL,
        Id nvarchar(128) NOT NULL,
        Payload nvarchar(max) NOT NULL,
        CONSTRAINT PK_CrmMigrationSnapshotRecords PRIMARY KEY (TenantId, Store, Id),
        CONSTRAINT CK_CrmMigrationPayload CHECK (ISJSON(Payload) = 1)
    );
END;
COMMIT;
