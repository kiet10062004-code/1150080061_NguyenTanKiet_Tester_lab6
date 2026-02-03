CREATE DATABASE Organization_unit;
GO
USE Organization_unit;
GO

CREATE TABLE OrganizationUnit (
    UnitId NVARCHAR(20) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500) NULL
);
