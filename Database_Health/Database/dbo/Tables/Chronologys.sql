CREATE TABLE [dbo].[Chronologys] (
    [ID]         BIGINT       IDENTITY (1, 1) NOT NULL,
    [User_id]    BIGINT       NOT NULL,
    [Date]       DATETIME     NOT NULL,
    [Product_id] BIGINT       NOT NULL,
    [Grams]      FLOAT (53)   NOT NULL,
    [Dose]       DECIMAL (18) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Chronologys_Products] FOREIGN KEY ([Product_id]) REFERENCES [dbo].[Products] ([ID]),
    CONSTRAINT [FK_Chronologys_Users] FOREIGN KEY ([User_id]) REFERENCES [dbo].[Users] ([ID])
);



