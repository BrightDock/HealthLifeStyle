CREATE TABLE [dbo].[WEB_Pages_topics] (
    [ID]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Topic_pic]   NCHAR (100)    NULL,
    [Topic_text]  NVARCHAR (MAX) NOT NULL,
    [WEB_Page_id] BIGINT         NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_WEB_Pages_typics_WEB_Pages] FOREIGN KEY ([WEB_Page_id]) REFERENCES [dbo].[WEB_Pages] ([ID])
);




