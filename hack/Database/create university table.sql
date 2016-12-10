CREATE TABLE [dbo].[University] (
    [Id]         INT            NOT NULL,
    [Edrpou]     NVARCHAR (10)  NOT NULL,
    [Title]      NVARCHAR (300) NOT NULL,
    [YearBudget] MONEY          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

