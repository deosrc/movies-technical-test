IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Movies')
BEGIN
    PRINT 'Creating database...';
    CREATE DATABASE Movies;
END

USE [Movies];
GO

IF OBJECT_ID('dbo.Movies') IS NULL
BEGIN
    PRINT 'Creating movies table...';
    CREATE TABLE dbo.Movies
    (
         Id                 UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID()
        ,Title              NVARCHAR(850) NOT NULL
        ,ReleaseDate        DATE NOT NULL
        ,Overview           TEXT NOT NULL
        ,Popularity         DECIMAL(18,3) NOT NULL
        ,VoteCount          INT NOT NULL
        ,VoteAverage        DECIMAL(18,3) NOT NULL
        ,OriginalLanguage   CHAR(2) NOT NULL
        ,Genre              NVARCHAR(4000) NOT NULL
        ,PosterUrl          NVARCHAR(4000) NOT NULL

        INDEX [idx_title] (Title)
    );
END

PRINT 'Initial database setup complete.';
