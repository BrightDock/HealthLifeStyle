CREATE TABLE [dbo].[Slides] (
    [Id]              BIGINT        IDENTITY (1, 1) NOT NULL,
    [Name]            NCHAR (40)    NOT NULL,
    [Text]            NCHAR (300)   NOT NULL,
    [Image]           NCHAR (100)   NOT NULL,
    [Date_start]      DATETIME2 (7) NULL,
    [Date_end]        DATETIME2 (7) NULL,
    [Post_id]         BIGINT        DEFAULT ((1)) NOT NULL,
    [Text_background] NCHAR (6)     DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

