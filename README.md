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
- Missing unit tests for problem details
- The search mechanism used is rather primitive as it does not handle scenarios such as special characters, mis-types and partial matches. This could be expanded in future to use a different mechanism such as full-text search.
- Genres are currently stored as a comma separated list. This would be better as a separate table.
- Database operations should ideally use `ToListAsync`, but this causes issues for unit testing. A solution should be found to allow ToListAsync to be used in the application whilst also allowing for unit testing.
- API documentation produced by swagger is basic and should be expanded with descriptions and examples.
- Database scripts do not contain error handling.
