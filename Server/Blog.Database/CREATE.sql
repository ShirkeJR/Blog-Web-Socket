/* Najpierw ręcznie utwórz bazę danych [BlogDB] w instancji mssqllocaldb! */

USE [BlogDB]
GO

CREATE TABLE [dbo].[Users] (
    [Id]       INT        IDENTITY (1, 1) NOT NULL,
    [Login]    NVARCHAR (32) NOT NULL,
    [Password] NVARCHAR (64) NOT NULL,
    [IsLocked] BIT        NOT NULL,
    [BlogName] NVARCHAR (64) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Users_Id]
    ON [dbo].[Users]([Id] ASC);


CREATE TABLE [dbo].[Posts] (
    [Id]      INT          IDENTITY (1, 1) NOT NULL,
    [UserId]  INT          NOT NULL,
    [Title]   NVARCHAR (32)   NOT NULL,
    [Content] NVARCHAR (2048) NOT NULL,
    [Date]    DATETIME     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Posts_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Posts_Id]
    ON [dbo].[Posts]([Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Posts_UserId]
    ON [dbo].[Posts]([UserId] ASC);

