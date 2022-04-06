CREATE TABLE [dbo].[Words_Count]
(
	[Id] INT NOT NULL Identity PRIMARY KEY, 
    [Word] NVARCHAR(20) NOT NULL Unique, 
    [Count] INT NOT NULL
)
