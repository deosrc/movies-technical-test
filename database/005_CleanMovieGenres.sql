USE [Movies];
GO

IF COL_LENGTH('dbo.Movies', 'Genre') IS NOT NULL
BEGIN
    PRINT 'Removing genre column from movie table...';
    ALTER TABLE dbo.Movies
    DROP COLUMN Genre;
END
GO
