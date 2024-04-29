USE [Movies];
GO

IF OBJECT_ID('dbo.MovieGenres') IS NULL
BEGIN
    PRINT 'Creating movie genres mapping table...';
    CREATE TABLE dbo.MovieGenres
    (
        MovieId UNIQUEIDENTIFIER NOT NULL,
        GenreId UNIQUEIDENTIFIER NOT NULL,

        PRIMARY KEY (
            MovieId,
            GenreId
        ),

        CONSTRAINT fk_Movie
            FOREIGN KEY (MovieId)
            REFERENCES dbo.Movies(Id),


        CONSTRAINT fk_Genre
            FOREIGN KEY (GenreId)
            REFERENCES dbo.Genres(Id)
    );
END
GO

IF NOT EXISTS(SELECT TOP 1 1 FROM dbo.MovieGenres)
BEGIN
    PRINT 'Populating movie genres mapping table...';
    INSERT INTO dbo.MovieGenres
    (
        MovieId,
        GenreId
    )
    SELECT
        m.Id AS MoveId,
        g.Id AS GenreId
    FROM
        dbo.Movies m
    CROSS APPLY
        STRING_SPLIT(m.Genre, ',') sg
    INNER JOIN
        dbo.Genres g
    ON
        TRIM(sg.value) = g.[Name]
END
