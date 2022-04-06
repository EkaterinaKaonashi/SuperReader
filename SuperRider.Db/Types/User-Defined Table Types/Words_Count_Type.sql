CREATE TYPE [dbo].[Words_Count_Type] AS TABLE
(
	[Word] NVARCHAR(20) not null Unique,
	[Count] int not null
)
