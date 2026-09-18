-- Candidate acceptance database only. No automatic runtime migrations or Portal schema access.
SET XACT_ABORT ON;
IF DB_NAME() NOT LIKE N'CrmMigration[_]%' OR LEN(DB_NAME()) <= 13
    THROW 51000, 'A dedicated CrmMigration_ acceptance database is required.', 1;
BEGIN TRANSACTION;
IF SCHEMA_ID(N'crm') IS NULL EXEC(N'CREATE SCHEMA crm');
IF OBJECT_ID(N'crm.FoundationRecords', N'U') IS NULL
BEGIN
    CREATE TABLE crm.FoundationRecords
    (
        TenantId nvarchar(64) COLLATE Latin1_General_100_BIN2 NOT NULL,
        Store nvarchar(64) COLLATE Latin1_General_100_BIN2 NOT NULL,
        Id nvarchar(128) COLLATE Latin1_General_100_CI_AS NOT NULL,
        PayloadJson nvarchar(max) NOT NULL,
        CONSTRAINT PK_CrmFoundationRecords PRIMARY KEY (TenantId, Store, Id),
        CONSTRAINT CK_CrmFoundationRecords_Json CHECK (ISJSON(PayloadJson) = 1)
    );
END;
COMMIT;
