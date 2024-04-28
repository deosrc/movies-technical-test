# Importing Data

This is a manual step to import the example dataset from <https://www.kaggle.com/datasets/disham993/9000-movies-dataset>. A copy of the dataset is included in the repository.

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
