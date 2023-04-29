CREATE TABLE [dbo].[nationalTeam] (
    [id]            INT          IDENTITY (1, 1) NOT NULL,
    [name]          VARCHAR (50) NOT NULL,
    [confederation] VARCHAR (50) NOT NULL,
    [marketValue]   VARCHAR(50)        NOT NULL,
    [ranking]       INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

