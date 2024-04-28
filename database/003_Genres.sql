USE [Movies];
GO

IF OBJECT_ID('dbo.Genres') IS NULL
BEGIN
    PRINT 'Creating genres table...';
    CREATE TABLE dbo.Genres
    (
         Id     UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID()
        ,[Name] NVARCHAR(50) NOT NULL

        INDEX [idx_name] ([Name])
    );
END
GO

IF NOT EXISTS(SELECT TOP 1 1 FROM dbo.Genres)
BEGIN
    PRINT 'Populating genres table...';
    INSERT INTO dbo.Genres ([Name])
    SELECT DISTINCT
        TRIM(sg.value)
    FROM
        dbo.Movies m
    CROSS APPLY
        STRING_SPLIT(Genre, ',') sg
END
