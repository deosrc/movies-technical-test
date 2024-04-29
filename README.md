# Movies API Technical Test

## Introduction

This repository contains the output for a technical test portion of a job application. It is not intended as production code.

## Database

### Connecting to the database

Using either SQL Server Management Studio (SSMS), Azure Data Studio, or the SQL Server Object Explorer in Visual Studio;
create a new database connection using the following details:

- Server Name: `localhost,1401`
- Authentication: `SQL Server Authentication`
- User Name: `sa`
- Password: `Developer1!`
- Trust Server Certificate: `True`

### Setup

The database setup is currently manually performed by running each of the scripts with the `database` folder in order.

In future, this could be improved by implementing a migration based approach via a tool such as DbUp, either within the application or as a init container.

### Data Import

The dataset is imported from <https://www.kaggle.com/datasets/disham993/9000-movies-dataset>.

> ⚠ There is one known entry in the file for the movie "Pixie Hollow Bake Off" which does not import correctly. This should be removed from the file, and added into the table manually.

1. Download the dataset and remove the line mentioned in the warning above.
1. Connect to the database using SSMS, right click the database, tasks and then "Import Flat File"
1. Follow the wizard using the following import options:

    | Column Name       | Data Type      | Primary Key | Allow Nulls |
    | ----------------- | -------------- | ----------- | ----------- |
    | Release_Date      | date           | False       | True        |
    | Title             | nvarchar(4000) | False       | True        |
    | Overview          | nvarchar(4000) | False       | True        |
    | Popularity        | decimal(18, 3) | False       | True        |
    | Vote_Count        | int            | False       | True        |
    | Vote_Average      | decimal(18, 3) | False       | True        |
    | Original_Language | nvarchar(4000) | False       | True        |
    | Genre             | nvarchar(4000) | False       | True        |
    | Poster_Url        | nvarchar(4000) | False       | True        |

1. Run the following to re-add the problematic row:

    ```sql
    INSERT INTO [dbo].[mymoviedb]
              ([Release_Date]
              ,[Title]
              ,[Overview]
              ,[Popularity]
              ,[Vote_Count]
              ,[Vote_Average]
              ,[Original_Language]
              ,[Genre]
              ,[Poster_Url])
        VALUES
              ('2013-10-20',
          'Pixie Hollow Bake Off',
          'Tink challenges Gelata to see who can bake the best cake for the queen''s party.  Plus 10 Disney Fairies Mini-Shorts:
    - Just Desserts
    - If The Hue Fits
    - Dust Up
    - Scents And Sensibility
    - Just One Of The Girls
    - Volleybug
    - Hide And Tink
    - Rainbow''s Ends
    - Fawn And Games
    - Magic Tricks',61.328,35,7.1,'en','Animation','https://image.tmdb.org/t/p/original/6iXYe7AkQ1QIfMFuvXsSCT2zF7s.jpg')
    GO
    ```

1. Transfer the data to the application table using the following query:

    ```sql
    INSERT INTO [dbo].[Movies]
              ([Title]
              ,[ReleaseDate]
              ,[Overview]
              ,[Popularity]
              ,[VoteCount]
              ,[VoteAverage]
              ,[OriginalLanguage]
              ,[Genre]
              ,[PosterUrl])
    SELECT [Title]
          ,[Release_Date]
          ,[Overview]
          ,[Popularity]
          ,[Vote_Count]
          ,[Vote_Average]
          ,[Original_Language]
          ,[Genre]
          ,[Poster_Url]
      FROM [dbo].[mymoviedb]
    ```

In future, this should be improved to import automatically when starting the solution via a init container.

## Design Decisions Made

These are design decisions which I made which I would have clarified:

- The ID added to the dataset on loading is using a GUID type to simplify a potential future migration to a distributed database system (incrementing IDs are often difficult).
- Various assumptions on text length for the data.
  - `NVARCHAR(850)` was chosen for title as it is the largest key length for an index.
- `Original_Language` is assumed to be an [ISO 639](https://en.wikipedia.org/wiki/List_of_ISO_639_language_codes) set 1 language code. The source of the data (themoviedb.com), does not seem to have documentation on this.

## Requirements Clarification

Below is a list of question I would ask to clarify the requirements of the task:

- Should all of the fields of the dataset be included in the results?

## Known Issues

- No CI/CD to automatically run unit tests
- The search mechanism used is rather primitive as it does not handle scenarios such as special characters, mis-types and partial matches. This could be expanded in future to use a different mechanism such as full-text search.
- Genres are currently stored as a comma separated list. This would be better as a separate table.
- Database operations should ideally use `ToListAsync`, but this causes issues for unit testing. A solution should be found to allow ToListAsync to be used in the application whilst also allowing for unit testing.
- API documentation produced by swagger is basic and should be expanded with descriptions and examples.
