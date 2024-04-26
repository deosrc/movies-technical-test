# Movies API Technical Test

## Introduction

This repository contains the output for a technical test portion of a job application. It is not intended as production code.

## Connecting to the database

Using either SQL Server Management Studio, Azure Data Studio, or the SQL Server Object Explorer in Visual Studio;
create a new database connection using the following details:

- Server Name: `localhost,1401`
- Authentication: `SQL Server Authentication`
- User Name: `sa`
- Password: `Developer1!`
- Trust Server Certificate: `True`

## Design Decisions Made

These are design decisions which I made which I would have clarified:

- The ID added to the dataset on loading is using a GUID type to simplify a potential future migration to a distributed database system (incrementing IDs are often difficult).
- Various assumptions on text length for the data.
  - `NVARCHAR(850)` was chosen for title as it is the largest key length for an index.
- `Original_Language` is assumed to be an [ISO 639](https://en.wikipedia.org/wiki/List_of_ISO_639_language_codes) set 1 language code. The source of the data (themoviedb.com), does not seem to have documentation on this.

## Requirements Clarification

Below is a list of question I would ask to clarify the requirements of the task:

- Should all of the fields of the dataset be included in the results?
