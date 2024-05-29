USE [ClinicManagementSystemDB]
GO

/****** Object:  Table [dbo].[Staff]    Script Date: 5/29/2024 3:31:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Staff](
	[StaffId] [int] IDENTITY(1,1) NOT NULL,
	[StaffFirstName] [varchar](50) NOT NULL,
	[StaffMiddleName] [varchar](50) NULL,
	[StaffLastName] [varchar](50) NOT NULL,
	[StaffPhoneNumber] [nchar](10) NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[StaffId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

