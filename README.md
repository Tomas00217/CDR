# **CDR**
**Author**: Tomáš Štucka

**Time spent**: 7-8 hours with some pauses

**Problems encountered**
-  Mocking the database context for tests. Spent quite a lot of time trying to make the tests run.
-  Working with timespan in LINQ queries. Was omitted as it would require more time.
-  The github has had an incident during my implementation which lead to not be able to push to my repository.

## **Technologies**
- **.NET 8** - Newest technology
- **EntityFrameworkCore** - I decided to use EF core as my **ORM** framework mainly because of my previous experience with it. Even though, EF core might not have the best performace, it supports **LINQ**, unlike it's more performant counterpart **Dapper**, which is my prefered choice of writing sql statements in C#.
- **SQLite** - For this project I decided to use SQLite Database, mainly to save time with setting up another database.
- **Automapper** - An easy choice for making life easier when mapping models to DTOs.
- **NUnit** - For testing I have used the NUnit framework. The choice was again made mainly because of my previous experience with it and it's ease of use.
- **Moq** - For the ability to mock the database context and test the functionality.
- **MockQueryable.Moq** - A helper for mocking and testing asynchronous functions. Saved time with creating own wrappers.

## **Design**
The application has a simple design for an API. It is split into two projects. One with the app logic and second with test. For the scope of this assignment i deemed this as sufficient.
- **Repository pattern** - I have used this pattern to decouple the business logic from the data access logic. This improved the testability of the application.
- **DTO pattern** - Since I was creating an API, the DTO pattern seemed like a perfect candidate to transfer data to the client. Although we have only one model and thus we do not use the DTO pattern to it's fullest potential (coupling models into one dto), it allows us to change the underlying models without affecting the results. 

## **Assumptions**
- I have assumed that the database might be filled with a lot of records and thus decided to implement pagination to api calls that could use it as well as filters.
- I would also assume that the api should be a part of larger project which would handle the upload of data to database, but since it was not the case I have created a simple service to do so.

## **Future considerations, enhancements**
- I have skipped most of the validation for the data file and made it that correct rows are added to the DB and the rest is skipped. There would have to be a discussion for what the correct behaviour should be when handling the input.
- The application could be decoupled into multiple projects in case it starts growing.
- **UnitOfWork** pattern might as well as a **GenericRepository** might be useful if the app grows even more, but for this assignment it would lead to bloated code.
- Testing would require more time. I have provided only a few tests for the repository as an example.